namespace Mortgage.Model.Common;

/// <summary>
/// Represents top-level loan application identifiers and origination context values.
/// </summary>
public sealed class ApplicationMetadata : DomainEntity
{
    /// <summary>
    /// Gets or sets the loan identifier used to reference this record.
    /// </summary>
    public string? LoanIdentifier { get; set; }
    /// <summary>
    /// Gets or sets the universal loan identifier used to reference this record.
    /// </summary>
    public string? UniversalLoanIdentifier { get; set; }
    /// <summary>
    /// Gets or sets the lender case identifier used to reference this record.
    /// </summary>
    public string? LenderCaseIdentifier { get; set; }
    /// <summary>
    /// Gets or sets the date on which the borrower application was completed.
    /// </summary>
    public DateOnly? ApplicationDate { get; set; }
    /// <summary>
    /// Gets or sets the date on which the lender received the application.
    /// </summary>
    public DateOnly? ApplicationReceivedDate { get; set; }
    /// <summary>
    /// Gets or sets the origination channel, such as retail, broker, or correspondent.
    /// </summary>
    public string? Channel { get; set; }
    /// <summary>
    /// Gets or sets the system that originated or supplied the application data.
    /// </summary>
    public string? OriginatingSystem { get; set; }
    /// <summary>
    /// Gets or sets the marketed loan product name selected for the application.
    /// </summary>
    public string? LoanProductName { get; set; }
    /// <summary>
    /// Gets or sets the originating organization and licensing identifiers.
    /// </summary>
    public OrganizationReference Originator { get; set; } = new();
}

/// <summary>
/// Represents interoperability metadata used when exchanging data with standards such as MISMO.
/// </summary>
public sealed class IntegrationMetadata : DomainEntity
{
    /// <summary>
    /// Gets or sets the integration standard represented by this metadata.
    /// </summary>
    public string? Standard { get; set; } = "MISMO";
    /// <summary>
    /// Gets or sets the integration standard version.
    /// </summary>
    public string? Version { get; set; } = "3.6.2";
    /// <summary>
    /// Gets or sets the named dataset, profile, or implementation context for the exchange.
    /// </summary>
    public string? Dataset { get; set; }
    /// <summary>
    /// Gets or sets a value indicating whether unmapped source data should be preserved when possible.
    /// </summary>
    public bool PreserveUnknownData { get; set; } = true;
}
