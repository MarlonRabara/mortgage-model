namespace Mortgage.Model.Abstractions;

/// <summary>
/// Defines a contract for domain objects that can carry unmapped or partner-specific extension values.
/// </summary>
public interface IExtensible
{
    /// <summary>
    /// Gets extension key/value data associated with the domain object.
    /// </summary>
    IDictionary<string, object?> Extensions { get; }
}
