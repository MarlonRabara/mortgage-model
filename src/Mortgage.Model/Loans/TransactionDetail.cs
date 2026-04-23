using Mortgage.Model.Common;

namespace Mortgage.Model.Loans;

/// <summary>
/// Represents the borrower transaction details that make up the sources and uses of funds.
/// </summary>
public sealed class TransactionDetail : DomainEntity
{
    /// <summary>
    /// Gets or sets the contract purchase price for the subject property.
    /// </summary>
    public decimal? PurchasePriceAmount { get; set; }
    /// <summary>
    /// Gets or sets funds allocated to alterations, improvements, or renovations.
    /// </summary>
    public decimal? AlterationsImprovementsRenovationsAmount { get; set; }
    /// <summary>
    /// Gets or sets the land acquisition amount when land value is tracked separately.
    /// </summary>
    public decimal? LandAmount { get; set; }
    /// <summary>
    /// Gets or sets the refinance payoff amount for debts being satisfied at closing.
    /// </summary>
    public decimal? RefinanceIncludingDebtsToBePaidOffAmount { get; set; }
    /// <summary>
    /// Gets or sets estimated prepaid items due from the borrower at closing.
    /// </summary>
    public decimal? EstimatedPrepaidItemsAmount { get; set; }
    /// <summary>
    /// Gets or sets estimated settlement and closing costs for the transaction.
    /// </summary>
    public decimal? EstimatedClosingCostsAmount { get; set; }
    /// <summary>
    /// Gets or sets the borrower cash contribution required to close.
    /// </summary>
    public decimal? CashFromBorrowerAmount { get; set; }
    /// <summary>
    /// Gets or sets the cash proceeds disbursed to the borrower, if any.
    /// </summary>
    public decimal? CashToBorrowerAmount { get; set; }
    /// <summary>
    /// Gets or sets the amount financed by subordinate liens in the transaction.
    /// </summary>
    public decimal? SubordinateLienAmount { get; set; }
}
