using PromotionEngine.Entities;
using PromotionEngine.Interfaces.PromotionRules;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PromotionEngine.PromotionRules
{
    /// <summary>
    /// The promotion calculator for promotion type that focus on one type of cart item.
    /// </summary>
    /// <seealso cref="PromotionEngine.PromotionRules.BasePromotionCalculator" />
    /// <seealso cref="PromotionEngine.Interfaces.PromotionRules.IPromotionCalculator" />
    public class FixedPriceNItemsPromotionCalculator : BasePromotionCalculator, IPromotionCalculator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FixedPriceNItemsPromotionCalculator"/> class.
        /// </summary>
        public FixedPriceNItemsPromotionCalculator() : base(Common.Enums.PromotionType.FixedPriceForNItems)
        {
        }

        /// <summary>
        /// Applies the promotion.
        /// </summary>
        /// <param name="cartItems">The cart items.</param>
        /// <param name="p">The promotion.</param>
        public void ApplyPromotion(IEnumerable<CartItem> cartItems, Promotion p)
        {
            if (cartItems.Count() > 0 && p.PromotionParts[0].Quantity <= cartItems.First().Quantity)
            {
                // Finding out how many items the same promotion would be possible with the number of items in the cart.
                int promotionsCountPossible = cartItems.First().Quantity / p.PromotionParts[0].Quantity;

                // Calculating the possible promotion.
                cartItems.First().PromotionApplied = Tuple.Create(p.PromotionParts[0].Quantity * promotionsCountPossible, p.Value * promotionsCountPossible);
            }
        }
    }
}
