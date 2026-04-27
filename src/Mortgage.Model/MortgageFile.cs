using Mortgage.Model.Common;
using Mortgage.Model.Loans;

namespace Mortgage.Model;

/// <summary>
/// Represents the top-level mortgage file aggregate exchanged within the domain model.
/// </summary>
public sealed class MortgageFile : Common.DomainEntity
{
    /// <summary>
    /// Gets or sets the loan application contained in the mortgage file.
    /// </summary>
    public LoanApplication LoanApplication { get; set; } = new();
    /// <summary>
    /// Gets or sets validation findings associated with the mortgage file.
    /// </summary>
    public List<ValidationIssue> Issues { get; set; } = new();
}
