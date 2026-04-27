using Mortgage.Model;
using Mortgage.Model.Assets;
using Mortgage.Model.Borrowers;
using Mortgage.Model.Common;
using Mortgage.Model.Liabilities;
using Mortgage.Model.Loans;
using Mortgage.Model.Property;

namespace Mortgage.Model.Translators.MISMO.Mapping;

/// <summary>
/// Provides representative mortgage sample data used by MISMO translator scenarios.
/// </summary>
public static class ExampleFactory
{
    /// <summary>
    /// Creates a representative mortgage file populated with borrower, loan, collateral, asset, and liability data.
    /// </summary>
    /// <returns>A populated mortgage file suitable for translator examples and tests.</returns>
    public static MortgageFile CreatePopulatedMortgageFile()
    {
        return new MortgageFile
        {
            LoanApplication = new LoanApplication
            {
                Metadata = new ApplicationMetadata
                {
                    LoanIdentifier = "LN-10001",
                    UniversalLoanIdentifier = "ULI-2026-0001",
                    ApplicationDate = new DateOnly(2026, 4, 23),
                    Channel = "Retail",
                    OriginatingSystem = "Mortgage Model Demo"
                },
                Terms = new LoanTerms
                {
                    LoanPurposeType = LoanPurposeType.Purchase,
                    LoanAmortizationType = LoanAmortizationType.Fixed,
                    RequestedLoanAmount = 450000m,
                    NoteAmount = 450000m,
                    NoteRatePercent = 6.125m,
                    LoanTermMonths = 360
                },
                TransactionDetail = new TransactionDetail
                {
                    PurchasePriceAmount = 500000m,
                    EstimatedClosingCostsAmount = 8500m,
                    CashFromBorrowerAmount = 58500m
                },
                SubjectProperty = new PropertySubject
                {
                    PropertyType = PropertyType.SingleFamily,
                    PropertyUsageType = PropertyUsageType.PrimaryResidence,
                    EstimatedValueAmount = 500000m,
                    FinancedUnitCount = 1,
                    Address = new Address
                    {
                        Line1 = "123 Main Street",
                        City = "Sacramento",
                        County = "Sacramento",
                        StateCode = "CA",
                        PostalCode = "95835",
                        CountryCode = "US"
                    }
                },
                PrimaryBorrower = new Borrower
                {
                    Role = BorrowerRoleType.PrimaryBorrower,
                    FirstName = "Marlon",
                    LastName = "Rabara",
                    BirthDate = new DateOnly(1974, 2, 9),
                    Contact = new ContactPoint
                    {
                        Email = "marlon@example.com",
                        MobilePhone = "916-216-7139"
                    },
                    CurrentAddress = new Address
                    {
                        Line1 = "5731 Da Vinci Way",
                        City = "Sacramento",
                        StateCode = "CA",
                        PostalCode = "95835",
                        CountryCode = "US"
                    },
                    Demographics = new Demographics
                    {
                        Ethnicity = EthnicityType.NotHispanicOrLatino,
                        Sex = SexType.Male,
                        Races = { RaceType.White }
                    },
                    Declarations = new DeclarationSet
                    {
                        BankruptcyIndicator = false
                    }
                },
                AdditionalBorrowers =
                {
                    new Borrower
                    {
                        Role = BorrowerRoleType.CoBorrower,
                        FirstName = "Yvonne",
                        LastName = "Rabara",
                        Contact = new ContactPoint
                        {
                            Email = "yvonne@example.com",
                            MobilePhone = "916-346-8612"
                        },
                        CurrentAddress = new Address
                        {
                            Line1 = "5731 Da Vinci Way",
                            City = "Sacramento",
                            StateCode = "CA",
                            PostalCode = "95835",
                            CountryCode = "US"
                        },
                        Demographics = new Demographics
                        {
                            Ethnicity = EthnicityType.NotHispanicOrLatino,
                            Sex = SexType.Female,
                            Races = { RaceType.Asian }
                        }
                    }
                },
                Assets =
                {
                    new Asset
                    {
                        AssetType = AssetType.CheckingAccount,
                        InstitutionName = "Golden 1 Credit Union",
                        AccountIdentifier = "CHK-1234",
                        CashOrMarketValueAmount = 32000m,
                        Description = "Primary checking"
                    }
                },
                Liabilities =
                {
                    new Liability
                    {
                        LiabilityType = LiabilityType.Revolving,
                        HolderName = "Visa",
                        AccountIdentifier = "CC-9876",
                        UnpaidBalanceAmount = 1500m,
                        MonthlyPaymentAmount = 50m
                    }
                }
            }
        };
    }
}
