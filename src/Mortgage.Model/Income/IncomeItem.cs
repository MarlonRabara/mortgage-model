using Mortgage.Model.Common;

namespace Mortgage.Model.Borrowers;

/// <summary>
/// Represents a borrower income component considered during qualification and underwriting.
/// </summary>
public sealed class IncomeItem : DomainEntity
{
    /// <summary>
    /// Gets or sets the income source type used for underwriting treatment.
    /// </summary>
    public IncomeType IncomeType { get; set; }
    /// <summary>
    /// Gets or sets the normalized monthly amount used in qualifying income calculations.
    /// </summary>
    public decimal? MonthlyAmount { get; set; }
    /// <summary>
    /// Gets or sets free-form notes describing this income source.
    /// </summary>
    public string? Description { get; set; }
    /// <summary>
    /// Gets or sets a value indicating whether this income source is employment-related.
    /// </summary>
    public bool? EmploymentRelatedIndicator { get; set; }
    /// <summary>
    /// Gets or sets a value indicating whether this income source is tax-exempt.
    /// </summary>
    public bool? TaxExemptIndicator { get; set; }
}
