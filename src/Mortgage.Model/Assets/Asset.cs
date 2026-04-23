using Mortgage.Model.Common;

namespace Mortgage.Model.Assets;

/// <summary>
/// Represents asset in the asset details.
/// </summary>
public sealed class Asset : DomainEntity
{
    /// <summary>
    /// Gets or sets the asset type classification used by the model.
    /// </summary>
    public AssetType AssetType { get; set; }
    /// <summary>
    /// Gets or sets the account identifier used to reference this record.
    /// </summary>
    public string? AccountIdentifier { get; set; }
    /// <summary>
    /// Gets or sets the institution name associated with this mortgage record.
    /// </summary>
    public string? InstitutionName { get; set; }
    /// <summary>
    /// Gets or sets the cash or market value amount for the mortgage scenario.
    /// </summary>
    public decimal? CashOrMarketValueAmount { get; set; }
    /// <summary>
    /// Gets or sets the life insurance face value amount for the mortgage scenario.
    /// </summary>
    public decimal? LifeInsuranceFaceValueAmount { get; set; }
    /// <summary>
    /// Gets or sets a value indicating whether depository account.
    /// </summary>
    public bool? DepositoryAccountIndicator { get; set; }
    /// <summary>
    /// Gets or sets a value indicating whether borrower owned.
    /// </summary>
    public bool? BorrowerOwnedIndicator { get; set; }
    /// <summary>
    /// Gets or sets the description in the asset details.
    /// </summary>
    public string? Description { get; set; }
}
