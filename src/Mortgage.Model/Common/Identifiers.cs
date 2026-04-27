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
    /// Gets or sets the Nationwide Multistate Licensing System identifier for the organization.
    /// </summary>
    public string? NmlsId { get; set; }
    /// <summary>
    /// Gets or sets the tax identifier for the organization.
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
    /// Gets or sets the URI or location where the document can be accessed.
    /// </summary>
    public string? Uri { get; set; }
    /// <summary>
    /// Gets or sets the MIME content type of the referenced document.
    /// </summary>
    public string? MimeType { get; set; }
    /// <summary>
    /// Gets or sets a free-form description of the referenced document.
    /// </summary>
    public string? Description { get; set; }
}

/// <summary>
/// Represents a validation finding produced during mortgage data quality checks.
/// </summary>
public sealed class ValidationIssue : DomainEntity
{
    /// <summary>
    /// Gets or sets the machine-readable validation or business-rule code.
    /// </summary>
    public string? Code { get; set; }
    /// <summary>
    /// Gets or sets the validation severity, such as informational, warning, or error.
    /// </summary>
    public string? Severity { get; set; }
    /// <summary>
    /// Gets or sets the human-readable validation message.
    /// </summary>
    public string? Message { get; set; }
    /// <summary>
    /// Gets or sets the model or payload path associated with the validation issue.
    /// </summary>
    public string? Path { get; set; }
}
