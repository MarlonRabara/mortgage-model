using Mortgage.Model.Common;

namespace Mortgage.Model.Compliance;

/// <summary>
/// Represents key automated underwriting and credit evaluation outputs for the loan.
/// </summary>
public sealed class UnderwritingInfo : DomainEntity
{
    /// <summary>
    /// Gets or sets the automated underwriting system (AUS) name used to evaluate this loan.
    /// </summary>
    public string? AutomatedUnderwritingSystemName { get; set; }
    /// <summary>
    /// Gets or sets the AUS recommendation outcome returned for this application.
    /// </summary>
    public string? AutomatedUnderwritingRecommendationDescription { get; set; }
    /// <summary>
    /// Gets or sets the credit scoring model used for the borrower assessment.
    /// </summary>
    public string? CreditScoreModelName { get; set; }
    /// <summary>
    /// Gets or sets the representative credit score used in underwriting decisions.
    /// </summary>
    public int? CreditScoreValue { get; set; }
}
