using Mortgage.Model.Abstractions;

namespace Mortgage.Model.Common;

/// <summary>
/// Represents a base mortgage domain entity with identity and extensibility support.
/// </summary>
public abstract class DomainEntity : IExtensible
{
    /// <summary>
    /// Gets or sets the stable identifier for this domain entity instance.
    /// </summary>
    public string Id { get; set; } = Guid.NewGuid().ToString("N");

    /// <summary>
    /// Gets extension key/value data for partner-specific, MISMO-specific, or unmapped attributes.
    /// </summary>
    public IDictionary<string, object?> Extensions { get; } = new Dictionary<string, object?>(StringComparer.OrdinalIgnoreCase);
}
