using Mortgage.Model.Borrowers;
using Mortgage.Model.Common;

namespace Mortgage.Model.Government;

/// <summary>
/// Represents HMDA government monitoring information captured for borrower demographic reporting.
/// </summary>
public sealed class GovernmentMonitoringInformation : DomainEntity
{
    /// <summary>
    /// Gets or sets the method by which the application was taken for HMDA reporting.
    /// </summary>
    public ApplicationTakenMethodType ApplicationTakenMethodType { get; set; }
    /// <summary>
    /// Gets or sets the primary borrower demographic responses used for government monitoring.
    /// </summary>
    public Demographics? PrimaryBorrowerDemographics { get; set; }
    /// <summary>
    /// Gets or sets the co-borrower demographic responses used for government monitoring.
    /// </summary>
    public Demographics? CoBorrowerDemographics { get; set; }
}
