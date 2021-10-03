using PromotionEngine.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PromotionEngine.Interfaces.PromotionRules
{
    /// <summary>
    /// The interface for all promotion calculators.
    /// </summary>
    public interface IPromotionCalculator
    {
        /// <summary>
        /// Applies the promotion.
        /// </summary>
        /// <param name="cartItems">The cart items.</param>
        /// <param name="p">The promotion.</param>
        void ApplyPromotion(IEnumerable<CartItem> cartItems, Promotion p);
    }
}
