using Mortgage.Model.Assets;
using Mortgage.Model.Borrowers;
using Mortgage.Model.Common;
using Mortgage.Model.Liabilities;
using Mortgage.Model.Property;

namespace Mortgage.Model.Loans;

/// <summary>
/// Represents the aggregate mortgage application, including borrower, collateral, credit, and compliance data.
/// </summary>
public sealed class LoanApplication : DomainEntity
{
    /// <summary>
    /// Gets or sets core loan identifiers and origination metadata.
    /// </summary>
    public ApplicationMetadata Metadata { get; set; } = new();
    /// <summary>
    /// Gets or sets requested product and repayment terms for the mortgage.
    /// </summary>
    public LoanTerms Terms { get; set; } = new();
    /// <summary>
    /// Gets or sets transaction sources-and-uses details for the loan.
    /// </summary>
    public TransactionDetail TransactionDetail { get; set; } = new();
    /// <summary>
    /// Gets or sets subject property collateral characteristics.
    /// </summary>
    public PropertySubject SubjectProperty { get; set; } = new();
    /// <summary>
    /// Gets or sets the primary borrower for the application.
    /// </summary>
    public Borrower PrimaryBorrower { get; set; } = new() { Role = BorrowerRoleType.PrimaryBorrower };
    /// <summary>
    /// Gets or sets additional borrowers or co-applicants on the loan.
    /// </summary>
    public List<Borrower> AdditionalBorrowers { get; set; } = new();
    /// <summary>
    /// Gets or sets borrower assets used for reserves and qualification.
    /// </summary>
    public List<Asset> Assets { get; set; } = new();
    /// <summary>
    /// Gets or sets borrower liabilities used in debt ratio calculations.
    /// </summary>
    public List<Liability> Liabilities { get; set; } = new();
    /// <summary>
    /// Gets or sets current and proposed housing expense line items.
    /// </summary>
    public List<HousingExpense> HousingExpenses { get; set; } = new();
    /// <summary>
    /// Gets or sets HMDA government monitoring demographic information.
    /// </summary>
    public Government.GovernmentMonitoringInformation GovernmentMonitoringInformation { get; set; } = new();
    /// <summary>
    /// Gets or sets underwriting outputs such as AUS and credit score data.
    /// </summary>
    public Compliance.UnderwritingInfo Underwriting { get; set; } = new();
    /// <summary>
    /// Gets or sets compliance and disclosure attributes for the application.
    /// </summary>
    public Compliance.ComplianceDetail Compliance { get; set; } = new();
    /// <summary>
    /// Gets or sets references to documents associated with the application.
    /// </summary>
    public List<DocumentReference> Documents { get; set; } = new();
    /// <summary>
    /// Gets or sets integration metadata used for standards-based exchange.
    /// </summary>
    public IntegrationMetadata Integration { get; set; } = new();
}
