namespace Mortgage.Model.Abstractions;

/// <summary>
/// Defines the contract for i extensible in the domain extension behavior.
/// </summary>
public interface IExtensible
{
    IDictionary<string, object?> Extensions { get; }
}
