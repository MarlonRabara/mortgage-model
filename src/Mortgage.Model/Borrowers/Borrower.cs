using Mortgage.Model.Common;

namespace Mortgage.Model.Borrowers;

/// <summary>
/// Represents an applicant, co-applicant, or other borrower party in a mortgage application.
/// </summary>
public sealed class Borrower : DomainEntity
{
    /// <summary>
    /// Gets or sets the borrower's role in the application.
    /// </summary>
    public BorrowerRoleType Role { get; set; }
    /// <summary>
    /// Gets or sets the borrower's legal first name.
    /// </summary>
    public string? FirstName { get; set; }
    /// <summary>
    /// Gets or sets the borrower's legal middle name or initial.
    /// </summary>
    public string? MiddleName { get; set; }
    /// <summary>
    /// Gets or sets the borrower's legal last name.
    /// </summary>
    public string? LastName { get; set; }
    /// <summary>
    /// Gets or sets the borrower's name suffix, such as Jr. or Sr.
    /// </summary>
    public string? NameSuffix { get; set; }
    /// <summary>
    /// Gets or sets the borrower's date of birth.
    /// </summary>
    public DateOnly? BirthDate { get; set; }
    /// <summary>
    /// Gets or sets the borrower's marital status.
    /// </summary>
    public MaritalStatusType MaritalStatus { get; set; }
    /// <summary>
    /// Gets or sets the borrower's taxpayer identifier value.
    /// </summary>
    public string? TaxpayerIdentifierValue { get; set; }
    /// <summary>
    /// Gets or sets the borrower's citizenship or residency classification.
    /// </summary>
    public CitizenshipResidencyType CitizenshipResidencyType { get; set; }
    /// <summary>
    /// Gets or sets a value indicating whether the borrower is a United States military veteran.
    /// </summary>
    public bool IsUsMilitaryVeteran { get; set; }
    /// <summary>
    /// Gets or sets a value indicating whether the borrower is a first-time home buyer.
    /// </summary>
    public bool IsFirstTimeHomeBuyer { get; set; }
    /// <summary>
    /// Gets or sets a value indicating whether the borrower is self-employed.
    /// </summary>
    public bool IsSelfEmployed { get; set; }
    /// <summary>
    /// Gets or sets the borrower's contact information.
    /// </summary>
    public ContactPoint Contact { get; set; } = new();
    /// <summary>
    /// Gets or sets the borrower's current residential address.
    /// </summary>
    public Address CurrentAddress { get; set; } = new();
    /// <summary>
    /// Gets or sets prior residential addresses used to establish residence history.
    /// </summary>
    public List<Address> PriorAddresses { get; set; } = new();
    /// <summary>
    /// Gets or sets borrower demographic responses collected for government monitoring.
    /// </summary>
    public Demographics Demographics { get; set; } = new();
    /// <summary>
    /// Gets or sets borrower declarations used for eligibility and risk assessment.
    /// </summary>
    public DeclarationSet Declarations { get; set; } = new();
    /// <summary>
    /// Gets or sets borrower employment records used to evaluate income history.
    /// </summary>
    public List<Employment> Employments { get; set; } = new();
    /// <summary>
    /// Gets or sets borrower income records used for qualifying income calculations.
    /// </summary>
    public List<IncomeItem> Incomes { get; set; } = new();
}
