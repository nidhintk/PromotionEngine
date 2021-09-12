using PromotionEngine.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PromotionEngine.Interfaces.PromotionRules
{
    public interface IPromotionCalculator
    {
        void ApplyPromotion(IEnumerable<CartItem> cartItems, Promotion p);
    }
}
