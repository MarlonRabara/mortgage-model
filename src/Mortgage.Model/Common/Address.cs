namespace Mortgage.Model.Common;

/// <summary>
/// Represents a postal or property address used by borrowers, employers, properties, and organizations.
/// </summary>
public sealed class Address : DomainEntity
{
    /// <summary>
    /// Gets or sets the attention or care-of line for the address.
    /// </summary>
    public string? Attention { get; set; }
    /// <summary>
    /// Gets or sets the primary street address line.
    /// </summary>
    public string? Line1 { get; set; }
    /// <summary>
    /// Gets or sets the secondary street address line, such as unit or suite.
    /// </summary>
    public string? Line2 { get; set; }
    /// <summary>
    /// Gets or sets an additional address line for extended address details.
    /// </summary>
    public string? Line3 { get; set; }
    /// <summary>
    /// Gets or sets the city or locality name.
    /// </summary>
    public string? City { get; set; }
    /// <summary>
    /// Gets or sets the county or parish name.
    /// </summary>
    public string? County { get; set; }
    /// <summary>
    /// Gets or sets the state, province, or region code.
    /// </summary>
    public string? StateCode { get; set; }
    /// <summary>
    /// Gets or sets the postal or ZIP code.
    /// </summary>
    public string? PostalCode { get; set; }
    /// <summary>
    /// Gets or sets the ISO-style country code.
    /// </summary>
    public string? CountryCode { get; set; } = "US";
    /// <summary>
    /// Gets or sets the residency role for this address, such as current, prior, or mailing.
    /// </summary>
    public ResidencyType ResidencyType { get; set; }
    /// <summary>
    /// Gets or sets the date on which occupancy or use of this address began.
    /// </summary>
    public DateOnly? FromDate { get; set; }
    /// <summary>
    /// Gets or sets the date on which occupancy or use of this address ended.
    /// </summary>
    public DateOnly? ToDate { get; set; }
}
