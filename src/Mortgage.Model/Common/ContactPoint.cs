namespace Mortgage.Model.Common;

/// <summary>
/// Represents borrower, employer, or organization contact information captured in a mortgage file.
/// </summary>
public sealed class ContactPoint : DomainEntity
{
    /// <summary>
    /// Gets or sets the email address.
    /// </summary>
    public string? Email { get; set; }
    /// <summary>
    /// Gets or sets the home telephone number.
    /// </summary>
    public string? HomePhone { get; set; }
    /// <summary>
    /// Gets or sets the mobile telephone number.
    /// </summary>
    public string? MobilePhone { get; set; }
    /// <summary>
    /// Gets or sets the work telephone number.
    /// </summary>
    public string? WorkPhone { get; set; }
    /// <summary>
    /// Gets or sets the fax number.
    /// </summary>
    public string? Fax { get; set; }
    /// <summary>
    /// Gets or sets the preferred contact method.
    /// </summary>
    public string? PreferredMethod { get; set; }
}
