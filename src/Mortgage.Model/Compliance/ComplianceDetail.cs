using Mortgage.Model.Common;

namespace Mortgage.Model.Compliance;

/// <summary>
/// Represents regulatory disclosure and HMDA classification values associated with a loan file.
/// </summary>
public sealed class ComplianceDetail : DomainEntity
{
    /// <summary>
    /// Gets or sets the HMDA loan purpose classification reported for this application.
    /// </summary>
    public string? HmdaLoanPurposeType { get; set; }
    /// <summary>
    /// Gets or sets the HMDA occupancy classification for the subject property.
    /// </summary>
    public string? HmdaOccupancyType { get; set; }
    /// <summary>
    /// Gets or sets the HMDA lien status classification for the originated loan.
    /// </summary>
    public string? HmdaLienStatusType { get; set; }
    /// <summary>
    /// Gets or sets a value indicating whether the borrower completed homeownership education.
    /// </summary>
    public string? HomeOwnershipEducationIndicator { get; set; }
    /// <summary>
    /// Gets or sets a value indicating whether the borrower received housing counseling.
    /// </summary>
    public string? HousingCounselingIndicator { get; set; }
}
