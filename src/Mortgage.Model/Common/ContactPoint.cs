namespace Mortgage.Model.Common;

/// <summary>
/// Represents contact point in the shared mortgage metadata.
/// </summary>
public sealed class ContactPoint : DomainEntity
{
    /// <summary>
    /// Gets or sets the email in the shared mortgage metadata.
    /// </summary>
    public string? Email { get; set; }
    /// <summary>
    /// Gets or sets the home phone in the shared mortgage metadata.
    /// </summary>
    public string? HomePhone { get; set; }
    /// <summary>
    /// Gets or sets the mobile phone in the shared mortgage metadata.
    /// </summary>
    public string? MobilePhone { get; set; }
    /// <summary>
    /// Gets or sets the work phone in the shared mortgage metadata.
    /// </summary>
    public string? WorkPhone { get; set; }
    /// <summary>
    /// Gets or sets the fax in the shared mortgage metadata.
    /// </summary>
    public string? Fax { get; set; }
    /// <summary>
    /// Gets or sets the preferred method in the shared mortgage metadata.
    /// </summary>
    public string? PreferredMethod { get; set; }
}
