using Mortgage.Model.Common;

namespace Mortgage.Model.Borrowers;

/// <summary>
/// Represents borrower in the borrower profile.
/// </summary>
public sealed class Borrower : DomainEntity
{
    /// <summary>
    /// Gets or sets the role in the borrower profile.
    /// </summary>
    public BorrowerRoleType Role { get; set; }
    /// <summary>
    /// Gets or sets the first name associated with this mortgage record.
    /// </summary>
    public string? FirstName { get; set; }
    /// <summary>
    /// Gets or sets the middle name associated with this mortgage record.
    /// </summary>
    public string? MiddleName { get; set; }
    /// <summary>
    /// Gets or sets the last name associated with this mortgage record.
    /// </summary>
    public string? LastName { get; set; }
    /// <summary>
    /// Gets or sets the name suffix in the borrower profile.
    /// </summary>
    public string? NameSuffix { get; set; }
    /// <summary>
    /// Gets or sets the birth date associated with this mortgage record.
    /// </summary>
    public DateOnly? BirthDate { get; set; }
    /// <summary>
    /// Gets or sets the marital status in the borrower profile.
    /// </summary>
    public MaritalStatusType MaritalStatus { get; set; }
    /// <summary>
    /// Gets or sets the taxpayer identifier value in the borrower profile.
    /// </summary>
    public string? TaxpayerIdentifierValue { get; set; }
    /// <summary>
    /// Gets or sets the citizenship residency type classification used by the model.
    /// </summary>
    public CitizenshipResidencyType CitizenshipResidencyType { get; set; }
    /// <summary>
    /// Gets or sets the is us military veteran in the borrower profile.
    /// </summary>
    public bool IsUsMilitaryVeteran { get; set; }
    /// <summary>
    /// Gets or sets the is first time home buyer in the borrower profile.
    /// </summary>
    public bool IsFirstTimeHomeBuyer { get; set; }
    /// <summary>
    /// Gets or sets the is self employed in the borrower profile.
    /// </summary>
    public bool IsSelfEmployed { get; set; }
    public ContactPoint Contact { get; set; } = new();
    public Address CurrentAddress { get; set; } = new();
    public List<Address> PriorAddresses { get; set; } = new();
    public Demographics Demographics { get; set; } = new();
    public DeclarationSet Declarations { get; set; } = new();
    public List<Employment> Employments { get; set; } = new();
    public List<IncomeItem> Incomes { get; set; } = new();
}
