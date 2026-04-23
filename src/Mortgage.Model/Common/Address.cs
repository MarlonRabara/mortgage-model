namespace Mortgage.Model.Common;

/// <summary>
/// Represents address in the shared mortgage metadata.
/// </summary>
public sealed class Address : DomainEntity
{
    /// <summary>
    /// Gets or sets the attention in the shared mortgage metadata.
    /// </summary>
    public string? Attention { get; set; }
    /// <summary>
    /// Gets or sets the line1 in the shared mortgage metadata.
    /// </summary>
    public string? Line1 { get; set; }
    /// <summary>
    /// Gets or sets the line2 in the shared mortgage metadata.
    /// </summary>
    public string? Line2 { get; set; }
    /// <summary>
    /// Gets or sets the line3 in the shared mortgage metadata.
    /// </summary>
    public string? Line3 { get; set; }
    /// <summary>
    /// Gets or sets the city in the shared mortgage metadata.
    /// </summary>
    public string? City { get; set; }
    /// <summary>
    /// Gets or sets the county in the shared mortgage metadata.
    /// </summary>
    public string? County { get; set; }
    /// <summary>
    /// Gets or sets the state code in the shared mortgage metadata.
    /// </summary>
    public string? StateCode { get; set; }
    /// <summary>
    /// Gets or sets the postal code in the shared mortgage metadata.
    /// </summary>
    public string? PostalCode { get; set; }
    /// <summary>
    /// Gets or sets the country code in the shared mortgage metadata.
    /// </summary>
    public string? CountryCode { get; set; } = "US";
    /// <summary>
    /// Gets or sets the residency type classification used by the model.
    /// </summary>
    public ResidencyType ResidencyType { get; set; }
    /// <summary>
    /// Gets or sets the from date associated with this mortgage record.
    /// </summary>
    public DateOnly? FromDate { get; set; }
    /// <summary>
    /// Gets or sets the to date associated with this mortgage record.
    /// </summary>
    public DateOnly? ToDate { get; set; }
}
