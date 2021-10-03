using PromotionEngine.Entities;
using PromotionEngine.Interfaces.PromotionRules;
using PromotionEngine.PromotionRules;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PromotionEngine.PromotionCalculators
{
    /// <summary>
    /// The promotion calculator for promotion type that combines different type of cart items.
    /// </summary>
    /// <seealso cref="PromotionEngine.PromotionRules.BasePromotionCalculator" />
    /// <seealso cref="PromotionEngine.Interfaces.PromotionRules.IPromotionCalculator" />
    public class FixedPriceCombinedItemsPromotionCalculator : BasePromotionCalculator, IPromotionCalculator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FixedPriceCombinedItemsPromotionCalculator"/> class.
        /// </summary>
        public FixedPriceCombinedItemsPromotionCalculator() : base(Common.Enums.PromotionType.FixedPriceForCombinedItems)
        {
        }

        /// <summary>
        /// Applies the promotion.
        /// </summary>
        /// <param name="cartItems">The cart items.</param>
        /// <param name="p">The promotion.</param>
        public void ApplyPromotion(IEnumerable<CartItem> cartItems, Promotion p)
        {
            // Ordering the cart items.
            List<CartItem> cartItemsOrdered = cartItems.Select(ci => ci).OrderBy(e => e.Product.Item.Id).ToList();

            // Ordering the promotion parts.
            List<PromotionPart> promotionPartsOrdered = p.PromotionParts.Select(pp => pp).OrderBy(e => e.Item.Id).ToList();

            // Apply the promotion in case if the cart items matches the items which forms the promotion.
            if (Enumerable.SequenceEqual(cartItemsOrdered.Select(ci => ci.Product.Item.Id), promotionPartsOrdered.Select(pp => pp.Item.Id))
                && promotionPartsOrdered.All(pp => pp.Quantity <= cartItemsOrdered.Single(ci => ci.Product.Item.Id == pp.Item.Id).Quantity))
            {
                // Finding out how many items the same promotion would be possible with the number of items in the cart.
                int promotionsCountPossible = Enumerable.Min(cartItemsOrdered.Select((item, i) => item.Quantity / promotionPartsOrdered[i].Quantity));
                
                // Calculating the possible promotion.
                cartItemsOrdered[0].PromotionApplied = Tuple.Create(p.PromotionParts.Single(pp => pp.Item.Id == cartItemsOrdered[0].Product.Item.Id).Quantity * promotionsCountPossible, p.Value * promotionsCountPossible);
                
                // Making sure all the items for which the promotion can be applied are marked so that these are excluded from the individual calculation.
                cartItemsOrdered.Skip(1).ToList().ForEach(ci => ci.PromotionApplied = Tuple.Create(p.PromotionParts.Single(pp => pp.Item.Id == ci.Product.Item.Id).Quantity * promotionsCountPossible, 0d));
                
                // Updating back the cart items with the promotions.
                cartItems = cartItemsOrdered;
            }                                
        }
    }
}
