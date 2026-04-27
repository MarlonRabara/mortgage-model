using System.Xml.Linq;
using Mortgage.Model;
using Mortgage.Model.Assets;
using Mortgage.Model.Borrowers;
using Mortgage.Model.Common;
using Mortgage.Model.Liabilities;
using Mortgage.Model.Loans;
using Mortgage.Model.Property;
using Mortgage.Model.Translators.MISMO.Serialization;

namespace Mortgage.Model.Translators.MISMO;

/// <summary>
/// Converts mortgage domain data to and from MISMO XML payloads.
/// </summary>
public sealed class MortgageFileMismoTranslator
{
    /// <summary>
    /// Serializes a mortgage file domain object into a MISMO XML payload.
    /// </summary>
    /// <param name="file">The mortgage file to serialize.</param>
    /// <param name="options">Optional serialization settings.</param>
    /// <returns>A MISMO XML string containing the mapped mortgage file data.</returns>
    public string Serialize(MortgageFile file, MismoTranslationOptions? options = null)
    {
        options ??= new MismoTranslationOptions();
        var ns = (XNamespace)MismoXmlConstants.Namespace;
        var loan = file.LoanApplication;

        var doc = new XDocument(
            new XDeclaration("1.0", "utf-8", "yes"),
            new XElement(ns + "MESSAGE",
                new XAttribute(XNamespace.Xmlns + "xsi", XNamespace.Get("http://www.w3.org/2001/XMLSchema-instance")),
                new XElement(ns + "ABOUT_VERSIONS",
                    new XElement(ns + "ABOUT_VERSION",
                        new XElement(ns + "CreatedDatetime", DateTime.UtcNow.ToString("O")),
                        new XElement(ns + "VersionIdentifier", options.MismoVersion))),
                new XElement(ns + "DEAL_SETS",
                    new XElement(ns + "DEAL_SET",
                        new XElement(ns + "DEALS",
                            new XElement(ns + "DEAL",
                                BuildLoans(ns, loan),
                                BuildParties(ns, loan),
                                BuildAssets(ns, loan),
                                BuildLiabilities(ns, loan),
                                BuildCollaterals(ns, loan)))))));

        return doc.ToString(options.IndentXml ? SaveOptions.None : SaveOptions.DisableFormatting);
    }

    /// <summary>
    /// Reads a MISMO XML file from disk and deserializes it into a loan application.
    /// </summary>
    /// <param name="mismoXmlFilePath">The path to the MISMO XML file.</param>
    /// <returns>The deserialized loan application, or <see langword="null"/> when the file cannot be read or parsed.</returns>
    public LoanApplication Deserialize(string mismoXmlFilePath)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(mismoXmlFilePath) || !File.Exists(mismoXmlFilePath))
            {
                return null!;
            }

            var xml = File.ReadAllText(mismoXmlFilePath);
            return Deserialize(xml, options: null).LoanApplication;
        }
        catch
        {
            return null!;
        }
    }

    /// <summary>
    /// Deserializes a MISMO XML payload into a mortgage file domain object.
    /// </summary>
    /// <param name="xml">The MISMO XML payload to parse.</param>
    /// <param name="options">Optional parsing settings.</param>
    /// <returns>A mortgage file containing the mapped MISMO data.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the XML does not contain a MISMO DEAL payload.</exception>
    public MortgageFile Deserialize(string xml, MismoTranslationOptions? options = null)
    {
        options ??= new MismoTranslationOptions();
        var ns = (XNamespace)MismoXmlConstants.Namespace;
        var doc = XDocument.Parse(xml);
        var file = new MortgageFile();
        var loan = file.LoanApplication;

        var deal = doc.Root?
            .Element(ns + "DEAL_SETS")?
            .Element(ns + "DEAL_SET")?
            .Element(ns + "DEALS")?
            .Element(ns + "DEAL");

        if (deal is null)
        {
            throw new InvalidOperationException("The supplied XML does not contain a MISMO DEAL payload.");
        }

        var loanElement = deal.Element(ns + "LOANS")?.Element(ns + "LOAN");
        if (loanElement is not null)
        {
            loan.Metadata.LoanIdentifier = ReadValue(loanElement, ns, "LoanIdentifier");
            loan.Metadata.UniversalLoanIdentifier = ReadValue(loanElement, ns, "UniversalLoanIdentifier");

            var detail = loanElement.Element(ns + "LOAN_DETAIL");
            loan.Terms.LoanPurposeType = ParseLoanPurpose(ReadValue(detail, ns, "LoanPurposeType"));
            loan.Terms.NoteAmount = ReadDecimal(detail, ns, "NoteAmount");
            loan.Terms.RequestedLoanAmount = ReadDecimal(detail, ns, "RequestedLoanAmount");
            loan.Terms.NoteRatePercent = ReadDecimal(detail, ns, "NoteRatePercent");
            loan.Terms.LoanTermMonths = ReadInt(detail, ns, "LoanTermMonthsCount");
            loan.Terms.LoanAmortizationType = ParseAmortization(ReadValue(detail, ns, "LoanAmortizationType"));

            var application = loanElement.Element(ns + "LOAN_APPLICATION");
            loan.Metadata.ApplicationDate = ReadDate(application, ns, "ApplicationDate");
            loan.TransactionDetail.PurchasePriceAmount = ReadDecimal(application, ns, "PurchasePriceAmount");
            loan.TransactionDetail.EstimatedClosingCostsAmount = ReadDecimal(application, ns, "EstimatedClosingCostsAmount");
            loan.TransactionDetail.CashFromBorrowerAmount = ReadDecimal(application, ns, "CashFromBorrowerAmount");
        }

        var parties = deal.Element(ns + "PARTIES")?.Elements(ns + "PARTY") ?? Enumerable.Empty<XElement>();
        foreach (var party in parties)
        {
            var role = ReadValue(party.Element(ns + "ROLES")?.Element(ns + "ROLE")?.Element(ns + "BORROWER"), ns, "BorrowerRoleType");
            var borrower = DeserializeBorrower(party, ns, role);
            if (borrower.Role == BorrowerRoleType.PrimaryBorrower)
            {
                loan.PrimaryBorrower = borrower;
            }
            else
            {
                loan.AdditionalBorrowers.Add(borrower);
            }
        }

        var assets = deal.Element(ns + "ASSETS")?.Elements(ns + "ASSET") ?? Enumerable.Empty<XElement>();
        foreach (var assetElement in assets)
        {
            loan.Assets.Add(new Asset
            {
                AssetType = ParseAssetType(ReadValue(assetElement, ns, "AssetType")),
                InstitutionName = ReadValue(assetElement, ns, "AssetHolderName"),
                AccountIdentifier = ReadValue(assetElement, ns, "AssetAccountIdentifier"),
                CashOrMarketValueAmount = ReadDecimal(assetElement, ns, "CashOrMarketValueAmount"),
                Description = ReadValue(assetElement, ns, "AssetDescription")
            });
        }

        var liabilities = deal.Element(ns + "LIABILITIES")?.Elements(ns + "LIABILITY") ?? Enumerable.Empty<XElement>();
        foreach (var liabilityElement in liabilities)
        {
            loan.Liabilities.Add(new Liability
            {
                LiabilityType = ParseLiabilityType(ReadValue(liabilityElement, ns, "LiabilityType")),
                HolderName = ReadValue(liabilityElement, ns, "LiabilityHolderName"),
                AccountIdentifier = ReadValue(liabilityElement, ns, "LiabilityAccountIdentifier"),
                UnpaidBalanceAmount = ReadDecimal(liabilityElement, ns, "LiabilityUnpaidBalanceAmount"),
                MonthlyPaymentAmount = ReadDecimal(liabilityElement, ns, "LiabilityMonthlyPaymentAmount")
            });
        }

        var collateral = deal.Element(ns + "COLLATERALS")?.Element(ns + "COLLATERAL")?.Element(ns + "SUBJECT_PROPERTY");
        if (collateral is not null)
        {
            loan.SubjectProperty.PropertyType = ParsePropertyType(ReadValue(collateral, ns, "PropertyType"));
            loan.SubjectProperty.PropertyUsageType = ParsePropertyUsageType(ReadValue(collateral, ns, "PropertyUsageType"));
            loan.SubjectProperty.EstimatedValueAmount = ReadDecimal(collateral, ns, "PropertyEstimatedValueAmount");
            loan.SubjectProperty.FinancedUnitCount = ReadInt(collateral, ns, "FinancedUnitCount");
            loan.SubjectProperty.Address = DeserializeAddress(collateral.Element(ns + "ADDRESS"), ns);
        }

        return file;
    }

    /// <summary>
    /// Builds the MISMO LOANS container for the supplied loan application.
    /// </summary>
    /// <param name="ns">The MISMO XML namespace.</param>
    /// <param name="loan">The loan application to map.</param>
    /// <returns>The populated LOANS XML element.</returns>
    private static XElement BuildLoans(XNamespace ns, LoanApplication loan) =>
        new(ns + "LOANS",
            new XElement(ns + "LOAN",
                new XElement(ns + "LoanIdentifier", loan.Metadata.LoanIdentifier ?? string.Empty),
                Maybe(ns + "UniversalLoanIdentifier", loan.Metadata.UniversalLoanIdentifier),
                new XElement(ns + "LOAN_DETAIL",
                    new XElement(ns + "LoanPurposeType", MapLoanPurpose(loan.Terms.LoanPurposeType)),
                    Maybe(ns + "RequestedLoanAmount", loan.Terms.RequestedLoanAmount),
                    Maybe(ns + "NoteAmount", loan.Terms.NoteAmount),
                    Maybe(ns + "NoteRatePercent", loan.Terms.NoteRatePercent),
                    Maybe(ns + "LoanTermMonthsCount", loan.Terms.LoanTermMonths),
                    new XElement(ns + "LoanAmortizationType", MapAmortization(loan.Terms.LoanAmortizationType))),
                new XElement(ns + "LOAN_APPLICATION",
                    Maybe(ns + "ApplicationDate", loan.Metadata.ApplicationDate),
                    Maybe(ns + "PurchasePriceAmount", loan.TransactionDetail.PurchasePriceAmount),
                    Maybe(ns + "EstimatedClosingCostsAmount", loan.TransactionDetail.EstimatedClosingCostsAmount),
                    Maybe(ns + "CashFromBorrowerAmount", loan.TransactionDetail.CashFromBorrowerAmount))));

    /// <summary>
    /// Builds the MISMO PARTIES container for borrowers associated with the loan application.
    /// </summary>
    /// <param name="ns">The MISMO XML namespace.</param>
    /// <param name="loan">The loan application containing borrower data.</param>
    /// <returns>The populated PARTIES XML element.</returns>
    private static XElement BuildParties(XNamespace ns, LoanApplication loan)
    {
        var borrowers = new List<Borrower> { loan.PrimaryBorrower };
        borrowers.AddRange(loan.AdditionalBorrowers);

        return new XElement(ns + "PARTIES", borrowers.Select(BuildParty));

        XElement BuildParty(Borrower b)
        {
            return new XElement(ns + "PARTY",
                MaybeAttribute("SequenceNumber", b.Id),
                new XElement(ns + "INDIVIDUAL",
                    Maybe(ns + "FirstName", b.FirstName),
                    Maybe(ns + "MiddleName", b.MiddleName),
                    Maybe(ns + "LastName", b.LastName),
                    Maybe(ns + "NameSuffix", b.NameSuffix),
                    Maybe(ns + "BirthDate", b.BirthDate),
                    new XElement(ns + "CONTACT_POINTS",
                        new XElement(ns + "CONTACT_POINT",
                            Maybe(ns + "ContactPointEmailValue", b.Contact.Email),
                            Maybe(ns + "ContactPointTelephoneValue", b.Contact.MobilePhone ?? b.Contact.HomePhone))),
                    new XElement(ns + "RESIDENCES",
                        new XElement(ns + "RESIDENCE",
                            new XElement(ns + "ADDRESS",
                                Maybe(ns + "AddressLineText", b.CurrentAddress.Line1),
                                Maybe(ns + "AddressLineText", b.CurrentAddress.Line2),
                                Maybe(ns + "CityName", b.CurrentAddress.City),
                                Maybe(ns + "StateCode", b.CurrentAddress.StateCode),
                                Maybe(ns + "PostalCode", b.CurrentAddress.PostalCode))))),
                new XElement(ns + "ROLES",
                    new XElement(ns + "ROLE",
                        new XElement(ns + "BORROWER",
                            new XElement(ns + "BorrowerRoleType", b.Role == BorrowerRoleType.PrimaryBorrower ? "Borrower" : "CoBorrower"),
                            new XElement(ns + "DECLARATION", Maybe(ns + "BankruptcyIndicator", b.Declarations.BankruptcyIndicator)),
                            new XElement(ns + "GOVERNMENT_MONITORING",
                                new XElement(ns + "HMDA_ETHNICITIES",
                                    new XElement(ns + "HMDA_ETHNICITY",
                                        new XElement(ns + "HMDAEthnicityType", MapEthnicity(b.Demographics.Ethnicity)))),
                                new XElement(ns + "HMDA_RACES",
                                    b.Demographics.Races.Select(r =>
                                        new XElement(ns + "HMDA_RACE",
                                            new XElement(ns + "HMDARaceType", MapRace(r))))),
                                new XElement(ns + "HMDASexType", MapSex(b.Demographics.Sex)))))));
        }
    }

    /// <summary>
    /// Builds the MISMO ASSETS container for borrower asset records.
    /// </summary>
    /// <param name="ns">The MISMO XML namespace.</param>
    /// <param name="loan">The loan application containing asset data.</param>
    /// <returns>The populated ASSETS XML element.</returns>
    private static XElement BuildAssets(XNamespace ns, LoanApplication loan) =>
        new(ns + "ASSETS", loan.Assets.Select(a =>
            new XElement(ns + "ASSET",
                new XElement(ns + "AssetType", MapAssetType(a.AssetType)),
                Maybe(ns + "AssetHolderName", a.InstitutionName),
                Maybe(ns + "AssetAccountIdentifier", a.AccountIdentifier),
                Maybe(ns + "CashOrMarketValueAmount", a.CashOrMarketValueAmount),
                Maybe(ns + "AssetDescription", a.Description))));

    /// <summary>
    /// Builds the MISMO LIABILITIES container for borrower liability records.
    /// </summary>
    /// <param name="ns">The MISMO XML namespace.</param>
    /// <param name="loan">The loan application containing liability data.</param>
    /// <returns>The populated LIABILITIES XML element.</returns>
    private static XElement BuildLiabilities(XNamespace ns, LoanApplication loan) =>
        new(ns + "LIABILITIES", loan.Liabilities.Select(l =>
            new XElement(ns + "LIABILITY",
                new XElement(ns + "LiabilityType", MapLiabilityType(l.LiabilityType)),
                Maybe(ns + "LiabilityHolderName", l.HolderName),
                Maybe(ns + "LiabilityAccountIdentifier", l.AccountIdentifier),
                Maybe(ns + "LiabilityUnpaidBalanceAmount", l.UnpaidBalanceAmount),
                Maybe(ns + "LiabilityMonthlyPaymentAmount", l.MonthlyPaymentAmount))));

    /// <summary>
    /// Builds the MISMO COLLATERALS container for the subject property.
    /// </summary>
    /// <param name="ns">The MISMO XML namespace.</param>
    /// <param name="loan">The loan application containing subject property data.</param>
    /// <returns>The populated COLLATERALS XML element.</returns>
    private static XElement BuildCollaterals(XNamespace ns, LoanApplication loan) =>
        new(ns + "COLLATERALS",
            new XElement(ns + "COLLATERAL",
                new XElement(ns + "SUBJECT_PROPERTY",
                    new XElement(ns + "PropertyType", MapPropertyType(loan.SubjectProperty.PropertyType)),
                    new XElement(ns + "PropertyUsageType", MapPropertyUsageType(loan.SubjectProperty.PropertyUsageType)),
                    Maybe(ns + "PropertyEstimatedValueAmount", loan.SubjectProperty.EstimatedValueAmount),
                    Maybe(ns + "FinancedUnitCount", loan.SubjectProperty.FinancedUnitCount),
                    BuildAddress(ns, loan.SubjectProperty.Address))));

    /// <summary>
    /// Builds a MISMO ADDRESS element from a domain address.
    /// </summary>
    /// <param name="ns">The MISMO XML namespace.</param>
    /// <param name="address">The address to map.</param>
    /// <returns>The populated ADDRESS XML element.</returns>
    private static XElement BuildAddress(XNamespace ns, Address address) =>
        new(ns + "ADDRESS",
            Maybe(ns + "AddressLineText", address.Line1),
            Maybe(ns + "AddressLineText", address.Line2),
            Maybe(ns + "CityName", address.City),
            Maybe(ns + "CountyName", address.County),
            Maybe(ns + "StateCode", address.StateCode),
            Maybe(ns + "PostalCode", address.PostalCode),
            Maybe(ns + "CountryCode", address.CountryCode));

    /// <summary>
    /// Deserializes a MISMO PARTY element into a borrower domain object.
    /// </summary>
    /// <param name="party">The PARTY element to parse.</param>
    /// <param name="ns">The MISMO XML namespace.</param>
    /// <param name="role">The MISMO borrower role value.</param>
    /// <returns>The mapped borrower domain object.</returns>
    private static Borrower DeserializeBorrower(XElement party, XNamespace ns, string? role)
    {
        var individual = party.Element(ns + "INDIVIDUAL");
        var gov = party.Element(ns + "ROLES")?.Element(ns + "ROLE")?.Element(ns + "BORROWER")?.Element(ns + "GOVERNMENT_MONITORING");
        var borrower = new Borrower
        {
            Role = string.Equals(role, "CoBorrower", StringComparison.OrdinalIgnoreCase)
                ? BorrowerRoleType.CoBorrower
                : BorrowerRoleType.PrimaryBorrower,
            FirstName = ReadValue(individual, ns, "FirstName"),
            MiddleName = ReadValue(individual, ns, "MiddleName"),
            LastName = ReadValue(individual, ns, "LastName"),
            NameSuffix = ReadValue(individual, ns, "NameSuffix"),
            BirthDate = ReadDate(individual, ns, "BirthDate"),
            Contact = new ContactPoint
            {
                Email = ReadValue(individual?.Element(ns + "CONTACT_POINTS")?.Element(ns + "CONTACT_POINT"), ns, "ContactPointEmailValue"),
                MobilePhone = ReadValue(individual?.Element(ns + "CONTACT_POINTS")?.Element(ns + "CONTACT_POINT"), ns, "ContactPointTelephoneValue")
            },
            CurrentAddress = DeserializeAddress(individual?.Element(ns + "RESIDENCES")?.Element(ns + "RESIDENCE")?.Element(ns + "ADDRESS"), ns),
            Demographics = new Demographics
            {
                Ethnicity = ParseEthnicity(gov?.Element(ns + "HMDA_ETHNICITIES")?.Element(ns + "HMDA_ETHNICITY")?.Element(ns + "HMDAEthnicityType")?.Value),
                Sex = ParseSex(gov?.Element(ns + "HMDASexType")?.Value)
            }
        };

        var races = gov?.Element(ns + "HMDA_RACES")?.Elements(ns + "HMDA_RACE") ?? Enumerable.Empty<XElement>();
        borrower.Demographics.Races = races
            .Select(r => ParseRace(r.Element(ns + "HMDARaceType")?.Value))
            .Where(r => r != RaceType.Unknown)
            .ToList();

        return borrower;
    }

    /// <summary>
    /// Deserializes a MISMO ADDRESS element into a domain address object.
    /// </summary>
    /// <param name="address">The ADDRESS element to parse.</param>
    /// <param name="ns">The MISMO XML namespace.</param>
    /// <returns>The mapped address, or an empty address when no element is supplied.</returns>
    private static Address DeserializeAddress(XElement? address, XNamespace ns)
    {
        if (address is null) return new Address();
        var lines = address.Elements(ns + "AddressLineText").Select(e => e.Value).ToList();
        return new Address
        {
            Line1 = lines.ElementAtOrDefault(0),
            Line2 = lines.ElementAtOrDefault(1),
            City = ReadValue(address, ns, "CityName"),
            County = ReadValue(address, ns, "CountyName"),
            StateCode = ReadValue(address, ns, "StateCode"),
            PostalCode = ReadValue(address, ns, "PostalCode"),
            CountryCode = ReadValue(address, ns, "CountryCode")
        };
    }

    private static XElement? Maybe(XName name, string? value) => string.IsNullOrWhiteSpace(value) ? null : new XElement(name, value);
    private static XElement? Maybe(XName name, int? value) => value is null ? null : new XElement(name, value.Value);
    private static XElement? Maybe(XName name, decimal? value) => value is null ? null : new XElement(name, value.Value);
    private static XElement? Maybe(XName name, bool? value) => value is null ? null : new XElement(name, value.Value ? "true" : "false");
    private static XElement? Maybe(XName name, DateOnly? value) => value is null ? null : new XElement(name, value.Value.ToString("yyyy-MM-dd"));
    private static XAttribute? MaybeAttribute(string name, string? value) => string.IsNullOrWhiteSpace(value) ? null : new XAttribute(name, value);

    private static string? ReadValue(XElement? element, XNamespace ns, string localName) => element?.Element(ns + localName)?.Value;
    private static decimal? ReadDecimal(XElement? element, XNamespace ns, string localName) => decimal.TryParse(ReadValue(element, ns, localName), out var v) ? v : null;
    private static int? ReadInt(XElement? element, XNamespace ns, string localName) => int.TryParse(ReadValue(element, ns, localName), out var v) ? v : null;
    private static DateOnly? ReadDate(XElement? element, XNamespace ns, string localName) => DateOnly.TryParse(ReadValue(element, ns, localName), out var v) ? v : null;

    private static string MapLoanPurpose(LoanPurposeType value) => value switch
    {
        LoanPurposeType.Purchase => "Purchase",
        LoanPurposeType.Refinance => "Refinance",
        LoanPurposeType.CashOutRefinance => "CashOutRefinance",
        LoanPurposeType.ConstructionOnly => "ConstructionOnly",
        LoanPurposeType.ConstructionToPermanent => "ConstructionToPermanent",
        LoanPurposeType.Other => "Other",
        _ => "Other"
    };

    private static LoanPurposeType ParseLoanPurpose(string? value) => value?.ToLowerInvariant() switch
    {
        "purchase" => LoanPurposeType.Purchase,
        "refinance" => LoanPurposeType.Refinance,
        "cashoutrefinance" => LoanPurposeType.CashOutRefinance,
        "constructiononly" => LoanPurposeType.ConstructionOnly,
        "constructiontopermanent" => LoanPurposeType.ConstructionToPermanent,
        _ => LoanPurposeType.Unknown
    };

    private static string MapAmortization(LoanAmortizationType value) => value switch
    {
        LoanAmortizationType.Fixed => "Fixed",
        LoanAmortizationType.AdjustableRate => "AdjustableRate",
        LoanAmortizationType.Other => "Other",
        _ => "Other"
    };

    private static LoanAmortizationType ParseAmortization(string? value) => value?.ToLowerInvariant() switch
    {
        "fixed" => LoanAmortizationType.Fixed,
        "adjustablerate" => LoanAmortizationType.AdjustableRate,
        "other" => LoanAmortizationType.Other,
        _ => LoanAmortizationType.Unknown
    };

    private static string MapAssetType(AssetType value) => value switch
    {
        AssetType.CheckingAccount => "CheckingAccount",
        AssetType.SavingsAccount => "SavingsAccount",
        AssetType.CertificateOfDeposit => "CertificateOfDeposit",
        AssetType.RetirementFund => "RetirementFund",
        AssetType.StocksOrBonds => "StocksOrBonds",
        AssetType.MutualFund => "MutualFund",
        AssetType.TrustAccount => "TrustAccount",
        AssetType.GiftFunds => "GiftFunds",
        AssetType.Other => "Other",
        _ => "Other"
    };

    private static AssetType ParseAssetType(string? value) => value?.ToLowerInvariant() switch
    {
        "checkingaccount" => AssetType.CheckingAccount,
        "savingsaccount" => AssetType.SavingsAccount,
        "certificateofdeposit" => AssetType.CertificateOfDeposit,
        "retirementfund" => AssetType.RetirementFund,
        "stocksorbonds" => AssetType.StocksOrBonds,
        "mutualfund" => AssetType.MutualFund,
        "trustaccount" => AssetType.TrustAccount,
        "giftfunds" => AssetType.GiftFunds,
        _ => AssetType.Unknown
    };

    private static string MapLiabilityType(LiabilityType value) => value switch
    {
        LiabilityType.Revolving => "Revolving",
        LiabilityType.Installment => "Installment",
        LiabilityType.MortgageLoan => "MortgageLoan",
        LiabilityType.LeasePayment => "LeasePayment",
        LiabilityType.Alimony => "Alimony",
        LiabilityType.ChildSupport => "ChildSupport",
        LiabilityType.Other => "Other",
        _ => "Other"
    };

    private static LiabilityType ParseLiabilityType(string? value) => value?.ToLowerInvariant() switch
    {
        "revolving" => LiabilityType.Revolving,
        "installment" => LiabilityType.Installment,
        "mortgageloan" => LiabilityType.MortgageLoan,
        "leasepayment" => LiabilityType.LeasePayment,
        "alimony" => LiabilityType.Alimony,
        "childsupport" => LiabilityType.ChildSupport,
        _ => LiabilityType.Unknown
    };

    private static string MapPropertyType(PropertyType value) => value switch
    {
        PropertyType.SingleFamily => "SingleFamily",
        PropertyType.Condo => "Condominium",
        PropertyType.Townhouse => "Townhouse",
        PropertyType.TwoToFourUnitProperty => "TwoToFourUnitProperty",
        PropertyType.Cooperative => "Cooperative",
        PropertyType.ManufacturedHome => "ManufacturedHome",
        PropertyType.MixedUse => "MixedUse",
        PropertyType.Other => "Other",
        _ => "Other"
    };

    private static PropertyType ParsePropertyType(string? value) => value?.ToLowerInvariant() switch
    {
        "singlefamily" => PropertyType.SingleFamily,
        "condominium" => PropertyType.Condo,
        "townhouse" => PropertyType.Townhouse,
        "twotofourunitproperty" => PropertyType.TwoToFourUnitProperty,
        "cooperative" => PropertyType.Cooperative,
        "manufacturedhome" => PropertyType.ManufacturedHome,
        "mixeduse" => PropertyType.MixedUse,
        _ => PropertyType.Unknown
    };

    private static string MapPropertyUsageType(PropertyUsageType value) => value switch
    {
        PropertyUsageType.PrimaryResidence => "PrimaryResidence",
        PropertyUsageType.SecondHome => "SecondHome",
        PropertyUsageType.Investment => "Investment",
        PropertyUsageType.Other => "Other",
        _ => "Other"
    };

    private static PropertyUsageType ParsePropertyUsageType(string? value) => value?.ToLowerInvariant() switch
    {
        "primaryresidence" => PropertyUsageType.PrimaryResidence,
        "secondhome" => PropertyUsageType.SecondHome,
        "investment" => PropertyUsageType.Investment,
        _ => PropertyUsageType.Unknown
    };

    private static string MapEthnicity(EthnicityType value) => value switch
    {
        EthnicityType.HispanicOrLatino => "HispanicOrLatino",
        EthnicityType.NotHispanicOrLatino => "NotHispanicOrLatino",
        EthnicityType.InformationNotProvided => "InformationNotProvidedByApplicantInMailInternetOrTelephoneApplication",
        _ => "InformationNotProvidedByApplicantInMailInternetOrTelephoneApplication"
    };

    private static EthnicityType ParseEthnicity(string? value) => value?.ToLowerInvariant() switch
    {
        "hispanicorlatino" => EthnicityType.HispanicOrLatino,
        "nothispanicorlatino" => EthnicityType.NotHispanicOrLatino,
        _ => EthnicityType.InformationNotProvided
    };

    private static string MapRace(RaceType value) => value switch
    {
        RaceType.AmericanIndianOrAlaskaNative => "AmericanIndianOrAlaskaNative",
        RaceType.Asian => "Asian",
        RaceType.BlackOrAfricanAmerican => "BlackOrAfricanAmerican",
        RaceType.NativeHawaiianOrOtherPacificIslander => "NativeHawaiianOrOtherPacificIslander",
        RaceType.White => "White",
        _ => "InformationNotProvidedByApplicantInMailInternetOrTelephoneApplication"
    };

    private static RaceType ParseRace(string? value) => value?.ToLowerInvariant() switch
    {
        "americanindianoralaskanative" => RaceType.AmericanIndianOrAlaskaNative,
        "asian" => RaceType.Asian,
        "blackorafricanamerican" => RaceType.BlackOrAfricanAmerican,
        "nativehawaiianorotherpacificislander" => RaceType.NativeHawaiianOrOtherPacificIslander,
        "white" => RaceType.White,
        _ => RaceType.Unknown
    };

    private static string MapSex(SexType value) => value switch
    {
        SexType.Male => "Male",
        SexType.Female => "Female",
        SexType.Both => "BothMaleAndFemale",
        _ => "InformationNotProvidedByApplicantInMailInternetOrTelephoneApplication"
    };

    private static SexType ParseSex(string? value) => value?.ToLowerInvariant() switch
    {
        "male" => SexType.Male,
        "female" => SexType.Female,
        "bothmaleandfemale" => SexType.Both,
        _ => SexType.InformationNotProvided
    };
}
