namespace Mortgage.Model.Common;

/// <summary>
/// Represents lender, broker, or partner organization identifiers associated with a mortgage file.
/// </summary>
public sealed class OrganizationReference : DomainEntity
{
    /// <summary>
    /// Gets or sets the legal or business name of the referenced organization.
    /// </summary>
    public string? Name { get; set; }
    /// <summary>
    /// Gets or sets the nmls id in the shared mortgage metadata.
    /// </summary>
    public string? NmlsId { get; set; }
    /// <summary>
    /// Gets or sets the tax id in the shared mortgage metadata.
    /// </summary>
    public string? TaxId { get; set; }
    /// <summary>
    /// Gets or sets the system identifier used to reference this record.
    /// </summary>
    public string? SystemIdentifier { get; set; }
}

/// <summary>
/// Represents metadata for a document linked to the mortgage application lifecycle.
/// </summary>
public sealed class DocumentReference : DomainEntity
{
    /// <summary>
    /// Gets or sets the display name of the referenced document.
    /// </summary>
    public string? Name { get; set; }
    /// <summary>
    /// Gets or sets the uri in the shared mortgage metadata.
    /// </summary>
    public string? Uri { get; set; }
    /// <summary>
    /// Gets or sets the MIME content type of the referenced document.
    /// </summary>
    public string? MimeType { get; set; }
    /// <summary>
    /// Gets or sets the description in the shared mortgage metadata.
    /// </summary>
    public string? Description { get; set; }
}

/// <summary>
/// Represents a validation finding produced during mortgage data quality checks.
/// </summary>
public sealed class ValidationIssue : DomainEntity
{
    /// <summary>
    /// Gets or sets the code in the shared mortgage metadata.
    /// </summary>
    public string? Code { get; set; }
    /// <summary>
    /// Gets or sets the severity in the shared mortgage metadata.
    /// </summary>
    public string? Severity { get; set; }
    /// <summary>
    /// Gets or sets the message in the shared mortgage metadata.
    /// </summary>
    public string? Message { get; set; }
    /// <summary>
    /// Gets or sets the path in the shared mortgage metadata.
    /// </summary>
    public string? Path { get; set; }
}
