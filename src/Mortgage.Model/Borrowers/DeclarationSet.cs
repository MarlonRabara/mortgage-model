using Mortgage.Model.Common;

namespace Mortgage.Model.Borrowers;

/// <summary>
/// Represents borrower declarations used to identify underwriting, eligibility, and compliance considerations.
/// </summary>
public sealed class DeclarationSet : DomainEntity
{
    /// <summary>
    /// Gets or sets a value indicating whether the borrower has declared bankruptcy.
    /// </summary>
    public bool? BankruptcyIndicator { get; set; }
    /// <summary>
    /// Gets or sets a value indicating whether the borrower has experienced foreclosure.
    /// </summary>
    public bool? ForeclosureIndicator { get; set; }
    /// <summary>
    /// Gets or sets a value indicating whether the borrower is party to an active lawsuit.
    /// </summary>
    public bool? LawsuitIndicator { get; set; }
    /// <summary>
    /// Gets or sets a value indicating whether the borrower has delinquent federal debt.
    /// </summary>
    public bool? DelinquentFederalDebtIndicator { get; set; }
    /// <summary>
    /// Gets or sets a value indicating whether any part of the down payment is borrowed.
    /// </summary>
    public bool? BorrowedDownPaymentIndicator { get; set; }
    /// <summary>
    /// Gets or sets a value indicating whether the borrower is obligated as a co-maker or endorser on another note.
    /// </summary>
    public bool? CoMakerIndicator { get; set; }
    /// <summary>
    /// Gets or sets a value indicating whether the borrower intends to occupy the subject property as a primary residence.
    /// </summary>
    public bool? OccupyPropertyAsPrimaryResidenceIndicator { get; set; }
    /// <summary>
    /// Gets or sets a value indicating whether the borrower had an ownership interest in a property during the last three years.
    /// </summary>
    public bool? OwnershipInterestInPropertyInLastThreeYearsIndicator { get; set; }
    /// <summary>
    /// Gets or sets the property type associated with the borrower's prior ownership interest.
    /// </summary>
    public string? OwnershipInterestPropertyType { get; set; }
    /// <summary>
    /// Gets or sets the title-holding manner associated with the borrower's prior ownership interest.
    /// </summary>
    public string? OwnershipInterestTitleHeldType { get; set; }
}
