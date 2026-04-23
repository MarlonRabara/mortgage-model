using Mortgage.Model.Common;

namespace Mortgage.Model.Loans;

/// <summary>
/// Represents current and proposed housing obligations used in payment shock and DTI analysis.
/// </summary>
public sealed class HousingExpense : DomainEntity
{
    /// <summary>
    /// Gets or sets the housing expense category (for example, rent, tax, insurance, or HOA).
    /// </summary>
    public string? HousingExpenseType { get; set; }
    /// <summary>
    /// Gets or sets the borrower current monthly housing expense amount.
    /// </summary>
    public decimal? PresentAmount { get; set; }
    /// <summary>
    /// Gets or sets the projected monthly housing expense amount after closing.
    /// </summary>
    public decimal? ProposedAmount { get; set; }
}
