using Mortgage.Model.Common;

namespace Mortgage.Model.Borrowers;

/// <summary>
/// Represents borrower employment history used for income stability and capacity assessment.
/// </summary>
public sealed class Employment : DomainEntity
{
    /// <summary>
    /// Gets or sets the employment status used in underwriting income analysis.
    /// </summary>
    public EmploymentStatusType EmploymentStatusType { get; set; }
    /// <summary>
    /// Gets or sets the employer name reported by the borrower.
    /// </summary>
    public string? EmployerName { get; set; }
    /// <summary>
    /// Gets or sets the borrower job title or position description.
    /// </summary>
    public string? PositionDescription { get; set; }
    /// <summary>
    /// Gets or sets the employer contact telephone number used for verification.
    /// </summary>
    public string? EmployerPhone { get; set; }
    /// <summary>
    /// Gets or sets the employer mailing address used for verification.
    /// </summary>
    public Address EmployerAddress { get; set; } = new();
    /// <summary>
    /// Gets or sets the employment start date used to derive tenure.
    /// </summary>
    public DateOnly? StartDate { get; set; }
    /// <summary>
    /// Gets or sets the employment end date when the employment is no longer active.
    /// </summary>
    public DateOnly? EndDate { get; set; }
    /// <summary>
    /// Gets or sets the monthly income amount attributed to this employment record.
    /// </summary>
    public decimal? MonthlyIncomeAmount { get; set; }
    /// <summary>
    /// Gets or sets the borrower tenure in months at this employer.
    /// </summary>
    public int? MonthsOnJob { get; set; }
    /// <summary>
    /// Gets or sets total borrower experience in the current profession.
    /// </summary>
    public int? YearsInProfession { get; set; }
}
