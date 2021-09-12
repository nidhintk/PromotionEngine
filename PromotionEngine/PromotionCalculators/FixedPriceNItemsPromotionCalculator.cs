using PromotionEngine.Entities;
using PromotionEngine.Interfaces.PromotionRules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PromotionEngine.PromotionRules
{
    public class FixedPriceNItemsPromotionCalculator : BasePromotionCalculator, IPromotionCalculator
    {
        public FixedPriceNItemsPromotionCalculator() : base(Common.Enums.PromotionType.FixedPriceForNItems)
        {
        }

        public void ApplyPromotion(IEnumerable<CartItem> cartItems, Promotion p)
        {
            if (cartItems.Count() > 0 && p.PromotionParts[0].Quantity <= cartItems.First().Quantity)
                cartItems.First().PromotionApplied = Tuple.Create(p.PromotionParts[0].Quantity, p.Value);
        }
    }
}
