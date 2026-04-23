using Mortgage.Model.Common;

namespace Mortgage.Model.Borrowers;

/// <summary>
/// Represents borrower demographic data collected for HMDA and fair lending reporting.
/// </summary>
public sealed class Demographics : DomainEntity
{
    /// <summary>
    /// Gets or sets how the borrower application was taken for HMDA collection purposes.
    /// </summary>
    public ApplicationTakenMethodType ApplicationTakenMethod { get; set; }
    /// <summary>
    /// Gets or sets the borrower ethnicity value used in HMDA reporting.
    /// </summary>
    public EthnicityType Ethnicity { get; set; }
    /// <summary>
    /// Gets or sets the borrower race selections used in demographic reporting.
    /// </summary>
    public List<RaceType> Races { get; set; } = new();
    /// <summary>
    /// Gets or sets the borrower sex value used in government monitoring information.
    /// </summary>
    public SexType Sex { get; set; }
    /// <summary>
    /// Gets or sets a value indicating whether demographics were collected by visual observation or surname.
    /// </summary>
    public bool CollectedByVisualObservationOrSurname { get; set; }
    /// <summary>
    /// Gets or sets the borrower-provided free-form ethnicity description when "Other" is selected.
    /// </summary>
    public string? EthnicityOtherDescription { get; set; }
    /// <summary>
    /// Gets or sets the borrower-provided race detail for "Other Asian" selections.
    /// </summary>
    public string? RaceOtherAsianDescription { get; set; }
    /// <summary>
    /// Gets or sets the borrower-provided American Indian or Alaska Native tribe name.
    /// </summary>
    public string? RaceNativeTribeName { get; set; }
}
