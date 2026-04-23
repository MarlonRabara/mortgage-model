using Mortgage.Model.Common;

namespace Mortgage.Model.Liabilities;

/// <summary>
/// Represents a borrower debt obligation included in mortgage underwriting and qualification.
/// </summary>
public sealed class Liability : DomainEntity
{
    /// <summary>
    /// Gets or sets the liability category used for debt-to-income calculations.
    /// </summary>
    public LiabilityType LiabilityType { get; set; }
    /// <summary>
    /// Gets or sets the creditor or account holder name associated with the liability.
    /// </summary>
    public string? HolderName { get; set; }
    /// <summary>
    /// Gets or sets the account identifier used to reference this record.
    /// </summary>
    public string? AccountIdentifier { get; set; }
    /// <summary>
    /// Gets or sets the unpaid principal balance currently outstanding on the liability.
    /// </summary>
    public decimal? UnpaidBalanceAmount { get; set; }
    /// <summary>
    /// Gets or sets the recurring monthly payment used in underwriting ratio analysis.
    /// </summary>
    public decimal? MonthlyPaymentAmount { get; set; }
    /// <summary>
    /// Gets or sets the remaining repayment term of the liability, in months.
    /// </summary>
    public int? RemainingTermMonths { get; set; }
    /// <summary>
    /// Gets or sets a value indicating whether this liability is excluded from underwriting.
    /// </summary>
    public bool? ExcludedFromUnderwritingIndicator { get; set; }
    /// <summary>
    /// Gets or sets a value indicating whether this liability requires subject loan resubordination.
    /// </summary>
    public bool? SubjectLoanResubordinationIndicator { get; set; }
    /// <summary>
    /// Gets or sets free-form notes describing the liability.
    /// </summary>
    public string? Description { get; set; }
}
