using Mortgage.Model.Common;
using Mortgage.Model.Loans;

namespace Mortgage.Model;

/// <summary>
/// Represents mortgage file in the mortgage domain model.
/// </summary>
public sealed class MortgageFile : Common.DomainEntity
{
    public LoanApplication LoanApplication { get; set; } = new();
    public List<ValidationIssue> Issues { get; set; } = new();
}
