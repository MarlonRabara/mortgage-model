using Mortgage.Model.Common;

namespace Mortgage.Model.Assets;

/// <summary>
/// Represents a borrower-owned financial asset used for funds-to-close, reserves, and underwriting analysis.
/// </summary>
public sealed class Asset : DomainEntity
{
    /// <summary>
    /// Gets or sets the asset type classification used by the model.
    /// </summary>
    public AssetType AssetType { get; set; }
    /// <summary>
    /// Gets or sets the account identifier or masked account number for the asset.
    /// </summary>
    public string? AccountIdentifier { get; set; }
    /// <summary>
    /// Gets or sets the name of the financial institution or asset holder.
    /// </summary>
    public string? InstitutionName { get; set; }
    /// <summary>
    /// Gets or sets the current cash balance or market value of the asset.
    /// </summary>
    public decimal? CashOrMarketValueAmount { get; set; }
    /// <summary>
    /// Gets or sets the face value for life insurance assets, when applicable.
    /// </summary>
    public decimal? LifeInsuranceFaceValueAmount { get; set; }
    /// <summary>
    /// Gets or sets a value indicating whether the asset is held in a depository account.
    /// </summary>
    public bool? DepositoryAccountIndicator { get; set; }
    /// <summary>
    /// Gets or sets a value indicating whether the borrower owns the asset.
    /// </summary>
    public bool? BorrowerOwnedIndicator { get; set; }
    /// <summary>
    /// Gets or sets a free-form description of the asset.
    /// </summary>
    public string? Description { get; set; }
}
