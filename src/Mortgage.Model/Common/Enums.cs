namespace Mortgage.Model.Common;

/// <summary>
/// Defines MISMO-aligned loan purpose classifications used for origination and HMDA reporting.
/// </summary>
public enum LoanPurposeType { Unknown, Purchase, Refinance, CashOutRefinance, ConstructionOnly, ConstructionToPermanent, Other }
/// <summary>
/// Defines amortization structures used to describe repayment behavior in the loan terms.
/// </summary>
public enum LoanAmortizationType { Unknown, Fixed, AdjustableRate, Other }
/// <summary>
/// Defines borrower participation roles for applicants and related parties in a mortgage file.
/// </summary>
public enum BorrowerRoleType { Unknown, PrimaryBorrower, CoBorrower, Guarantor, NonOccupantCoBorrower }
/// <summary>
/// Defines marital status values commonly captured for underwriting and compliance disclosures.
/// </summary>
public enum MaritalStatusType { Unknown, Married, Unmarried, Separated }
/// <summary>
/// Defines residence timeline roles used to distinguish current, prior, and mailing addresses.
/// </summary>
public enum ResidencyType { Unknown, Current, Prior, Mailing }
/// <summary>
/// Defines borrower asset categories used in AUS evaluation and reserve calculations.
/// </summary>
public enum AssetType { Unknown, CheckingAccount, SavingsAccount, CertificateOfDeposit, RetirementFund, StocksOrBonds, MutualFund, TrustAccount, GiftFunds, Other }
/// <summary>
/// Defines liability categories used for debt-to-income and credit risk analysis.
/// </summary>
public enum LiabilityType { Unknown, Revolving, Installment, MortgageLoan, LeasePayment, Alimony, ChildSupport, Other }
/// <summary>
/// Defines employment statuses used to evaluate income stability and borrower capacity.
/// </summary>
public enum EmploymentStatusType { Unknown, Current, Previous, Retired, SelfEmployed, NotEmployed }
/// <summary>
/// Defines income source categories used during underwriting and eligibility determination.
/// </summary>
public enum IncomeType { Unknown, Base, Overtime, Bonus, Commission, DividendInterest, NetRentalIncome, Retirement, SocialSecurity, Other }
/// <summary>
/// Defines occupancy intent classifications used by underwriting and HMDA occupancy reporting.
/// </summary>
public enum PropertyUsageType { Unknown, PrimaryResidence, SecondHome, Investment, Other }
/// <summary>
/// Defines collateral property categories used in appraisal and collateral underwriting.
/// </summary>
public enum PropertyType { Unknown, SingleFamily, Condo, Townhouse, TwoToFourUnitProperty, Cooperative, ManufacturedHome, MixedUse, Other }
/// <summary>
/// Defines vesting and ownership structures used for title and closing documentation.
/// </summary>
public enum TitleHeldType { Unknown, SoleOwnership, JointTenants, TenantsInCommon, CommunityProperty, LivingTrust, Other }
/// <summary>
/// Defines borrower citizenship and residency classifications used for eligibility and compliance checks.
/// </summary>
public enum CitizenshipResidencyType { Unknown, Uscitizen, PermanentResidentAlien, NonPermanentResidentAlien, Other }
/// <summary>
/// Defines channels used to capture how the application was taken for HMDA and disclosure workflows.
/// </summary>
public enum ApplicationTakenMethodType { Unknown, FaceToFace, Mail, Telephone, Internet }
/// <summary>
/// Defines HMDA demographic sex values collected for government monitoring information.
/// </summary>
public enum SexType { Unknown, Male, Female, Both, InformationNotProvided }
/// <summary>
/// Defines HMDA ethnicity values used in borrower demographic reporting.
/// </summary>
public enum EthnicityType { Unknown, HispanicOrLatino, NotHispanicOrLatino, InformationNotProvided }
/// <summary>
/// Defines HMDA race values used in demographic collection and regulatory reporting.
/// </summary>
public enum RaceType { Unknown, AmericanIndianOrAlaskaNative, Asian, BlackOrAfricanAmerican, NativeHawaiianOrOtherPacificIslander, White, InformationNotProvided }
