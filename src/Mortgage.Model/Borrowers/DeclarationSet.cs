using Mortgage.Model.Common;

namespace Mortgage.Model.Borrowers;

/// <summary>
/// Represents declaration set in the borrower profile.
/// </summary>
public sealed class DeclarationSet : DomainEntity
{
    /// <summary>
    /// Gets or sets a value indicating whether bankruptcy.
    /// </summary>
    public bool? BankruptcyIndicator { get; set; }
    /// <summary>
    /// Gets or sets a value indicating whether foreclosure.
    /// </summary>
    public bool? ForeclosureIndicator { get; set; }
    /// <summary>
    /// Gets or sets a value indicating whether lawsuit.
    /// </summary>
    public bool? LawsuitIndicator { get; set; }
    /// <summary>
    /// Gets or sets a value indicating whether delinquent federal debt.
    /// </summary>
    public bool? DelinquentFederalDebtIndicator { get; set; }
    /// <summary>
    /// Gets or sets a value indicating whether borrowed down payment.
    /// </summary>
    public bool? BorrowedDownPaymentIndicator { get; set; }
    /// <summary>
    /// Gets or sets a value indicating whether co maker.
    /// </summary>
    public bool? CoMakerIndicator { get; set; }
    /// <summary>
    /// Gets or sets a value indicating whether occupy property as primary residence.
    /// </summary>
    public bool? OccupyPropertyAsPrimaryResidenceIndicator { get; set; }
    /// <summary>
    /// Gets or sets a value indicating whether ownership interest in property in last three years.
    /// </summary>
    public bool? OwnershipInterestInPropertyInLastThreeYearsIndicator { get; set; }
    /// <summary>
    /// Gets or sets the ownership interest property type classification used by the model.
    /// </summary>
    public string? OwnershipInterestPropertyType { get; set; }
    /// <summary>
    /// Gets or sets the ownership interest title held type classification used by the model.
    /// </summary>
    public string? OwnershipInterestTitleHeldType { get; set; }
}
