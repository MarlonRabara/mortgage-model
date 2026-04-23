using Mortgage.Model.Common;

namespace Mortgage.Model.Loans;

/// <summary>
/// Represents note, product, and repayment terms used to underwrite and disclose the loan.
/// </summary>
public sealed class LoanTerms : DomainEntity
{
    /// <summary>
    /// Gets or sets the primary business purpose of the loan (purchase, refinance, and related types).
    /// </summary>
    public LoanPurposeType LoanPurposeType { get; set; }
    /// <summary>
    /// Gets or sets the repayment structure (for example, fixed-rate or adjustable-rate).
    /// </summary>
    public LoanAmortizationType LoanAmortizationType { get; set; }
    /// <summary>
    /// Gets or sets the final promissory note amount for the transaction.
    /// </summary>
    public decimal? NoteAmount { get; set; }
    /// <summary>
    /// Gets or sets the borrower-requested loan amount at application time.
    /// </summary>
    public decimal? RequestedLoanAmount { get; set; }
    /// <summary>
    /// Gets or sets the note rate percent used for underwriting and pricing.
    /// </summary>
    public decimal? NoteRatePercent { get; set; }
    /// <summary>
    /// Gets or sets the total amortization term of the loan, in months.
    /// </summary>
    public int? LoanTermMonths { get; set; }
    /// <summary>
    /// Gets or sets the loan-to-value (LTV) ratio used in underwriting and pricing.
    /// </summary>
    public decimal? LtvPercent { get; set; }
    /// <summary>
    /// Gets or sets the combined loan-to-value (CLTV) ratio used in underwriting and pricing.
    /// </summary>
    public decimal? CombinedLtvPercent { get; set; }
    /// <summary>
    /// Gets or sets the mortgage product type (for example, conventional, FHA, or VA).
    /// </summary>
    public string? MortgageType { get; set; }
    /// <summary>
    /// Gets or sets the lien position of the loan relative to other encumbrances.
    /// </summary>
    public string? LienPriorityType { get; set; }
    /// <summary>
    /// Gets or sets a value indicating whether the loan has an interest-only payment period.
    /// </summary>
    public bool? InterestOnlyIndicator { get; set; }
    /// <summary>
    /// Gets or sets a value indicating whether the loan includes a balloon payment feature.
    /// </summary>
    public bool? BalloonIndicator { get; set; }
}
