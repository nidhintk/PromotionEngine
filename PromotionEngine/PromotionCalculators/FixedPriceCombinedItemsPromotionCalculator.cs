using PromotionEngine.Entities;
using PromotionEngine.Interfaces.PromotionRules;
using PromotionEngine.PromotionRules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PromotionEngine.PromotionCalculators
{
    public class FixedPriceCombinedItemsPromotionCalculator : BasePromotionCalculator, IPromotionCalculator
    {
        public FixedPriceCombinedItemsPromotionCalculator() : base(Common.Enums.PromotionType.FixedPriceForCombinedItems)
        {
        }

        public void ApplyPromotion(IEnumerable<CartItem> cartItems, Promotion p)
        {
            List<CartItem> cartItemsOrdered = cartItems.Select(ci => ci).OrderBy(e => e.Product.Item.Id).ToList();
            List<PromotionPart> promotionPartsOrdered = p.PromotionParts.Select(pp => pp).OrderBy(e => e.Item.Id).ToList();
            if (Enumerable.SequenceEqual(cartItemsOrdered.Select(ci => ci.Product.Item.Id), promotionPartsOrdered.Select(pp => pp.Item.Id))
                && promotionPartsOrdered.All(pp => pp.Quantity <= cartItemsOrdered.Single(ci => ci.Product.Item.Id == pp.Item.Id).Quantity))
            {
                cartItemsOrdered[0].PromotionApplied = Tuple.Create(p.PromotionParts.Single(pp => pp.Item.Id == cartItemsOrdered[0].Product.Item.Id).Quantity, p.Value);
                cartItemsOrdered.Skip(1).ToList().ForEach(ci => ci.PromotionApplied = Tuple.Create(p.PromotionParts.Single(pp => pp.Item.Id == ci.Product.Item.Id).Quantity, 0d));
                cartItems = cartItemsOrdered;
            }                                
        }
    }
}
