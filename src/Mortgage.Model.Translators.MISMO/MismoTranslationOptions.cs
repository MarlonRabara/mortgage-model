namespace Mortgage.Model.Translators.MISMO;

/// <summary>
/// Provides configuration settings that control MISMO XML serialization and parsing behavior.
/// </summary>
public sealed class MismoTranslationOptions
{
    /// <summary>
    /// Gets or sets the MISMO reference model version emitted in translation metadata.
    /// </summary>
    public string MismoVersion { get; set; } = "3.6.2";
    /// <summary>
    /// Gets or sets a value indicating whether empty MISMO containers should be omitted.
    /// </summary>
    public bool OmitEmptyContainers { get; set; } = true;
    /// <summary>
    /// Gets or sets a value indicating whether output XML should be pretty-printed with indentation.
    /// </summary>
    public bool IndentXml { get; set; } = true;
    /// <summary>
    /// Gets or sets a value indicating whether unrecognized MISMO elements should be preserved when supported.
    /// </summary>
    public bool PreserveUnknownElements { get; set; } = true;
}
