using Mortgage.Model.Common;

namespace Mortgage.Model.Property;

/// <summary>
/// Represents the collateral property securing the mortgage transaction.
/// </summary>
public sealed class PropertySubject : DomainEntity
{
    /// <summary>
    /// Gets or sets the property address used for collateral and valuation records.
    /// </summary>
    public Address Address { get; set; } = new();
    /// <summary>
    /// Gets or sets the collateral property type used for eligibility and appraisal rules.
    /// </summary>
    public PropertyType PropertyType { get; set; }
    /// <summary>
    /// Gets or sets the occupancy intent for the subject property.
    /// </summary>
    public PropertyUsageType PropertyUsageType { get; set; }
    /// <summary>
    /// Gets or sets the legal description used in title and closing documentation.
    /// </summary>
    public string? LegalDescription { get; set; }
    /// <summary>
    /// Gets or sets the estimated market value used for underwriting and LTV analysis.
    /// </summary>
    public decimal? EstimatedValueAmount { get; set; }
    /// <summary>
    /// Gets or sets the purchase price associated with the subject property.
    /// </summary>
    public decimal? PurchasePriceAmount { get; set; }
    /// <summary>
    /// Gets or sets the number of financed residential units.
    /// </summary>
    public int? FinancedUnitCount { get; set; }
    /// <summary>
    /// Gets or sets the number of bedrooms in the subject property.
    /// </summary>
    public int? BedroomCount { get; set; }
    /// <summary>
    /// Gets or sets the number of bathrooms in the subject property.
    /// </summary>
    public int? BathroomCount { get; set; }
    /// <summary>
    /// Gets or sets the gross living area measured in square feet.
    /// </summary>
    public decimal? LivingAreaSquareFeetCount { get; set; }
    /// <summary>
    /// Gets or sets the lot size measured in square feet.
    /// </summary>
    public decimal? LotSizeSquareFeetCount { get; set; }
    /// <summary>
    /// Gets or sets the vesting manner in which title is or will be held.
    /// </summary>
    public TitleHeldType TitleHeldType { get; set; }
    /// <summary>
    /// Gets or sets a value indicating whether the collateral is a manufactured home.
    /// </summary>
    public bool? ManufacturedHomeIndicator { get; set; }
    /// <summary>
    /// Gets or sets a value indicating whether the property includes mixed residential and commercial use.
    /// </summary>
    public bool? MixedUsePropertyIndicator { get; set; }
}
