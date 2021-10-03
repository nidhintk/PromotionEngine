using PromotionEngine.Entities;
using PromotionEngine.Interface;
using PromotionEngine.PromotionCalculators.Factory;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PromotionEngine
{
    /// <summary>
    /// The class representing the SKU Promotion Engine.
    /// </summary>
    /// <seealso cref="PromotionEngine.Interface.IPromote" />
    public class SkuPromotionEngine : IPromote
    {
        /// <summary>
        /// Gets or sets the active promotions.
        /// </summary>
        /// <value>
        /// The active promotions.
        /// </value>
        private List<Promotion> activePromotions;

        /// <summary>
        /// Initializes a new instance of the <see cref="SkuPromotionEngine"/> class.
        /// </summary>
        public SkuPromotionEngine()
        {
            // Initialising the list of active promotions.
            activePromotions = new List<Promotion>();
        }

        /// <summary>
        /// Adds the active promotion.
        /// </summary>
        /// <param name="promotion">The promotion.</param>
        /// <returns>
        /// true if the promotion has been added successfully else false.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">promotion - Valid promotion need to be passed in!</exception>
        /// <exception cref="System.ArgumentException">
        /// Valid promotion need to be passed in! - promotion
        /// or
        /// Promotion passed in should be against valid Sku! - promotion
        /// or
        /// Promotion passed in should have a valid quantity against each Sku! - promotion
        /// or
        /// Promotion passed in should have a valid type! - promotion
        /// or
        /// Promotion passed in should have a valid value! - promotion
        /// </exception>
        public bool AddActivePromotion(Promotion promotion)
        {
            if (promotion == null)
                throw new ArgumentNullException("promotion", "Valid promotion need to be passed in!");

            if (promotion.PromotionParts == null || promotion.PromotionParts.Count == 0)
                throw new ArgumentException("Valid promotion need to be passed in!", "promotion");

            if (promotion.PromotionParts.Any(pp => pp.Item == null))
                throw new ArgumentException("Valid promotion need to be passed in!", "promotion");

            if (promotion.PromotionParts.Any(p => !char.IsLetterOrDigit(p.Item.Id)))
                throw new ArgumentException("Promotion passed in should be against valid Sku!", "promotion");

            if (promotion.PromotionParts.Any(p => p.Quantity <= 0))
                throw new ArgumentException("Promotion passed in should have a valid quantity against each Sku!", "promotion");

            if (promotion.Type == Common.Enums.PromotionType.None)
                throw new ArgumentException("Promotion passed in should have a valid type!", "promotion");

            if (promotion.Value <= 0)
                throw new ArgumentException("Promotion passed in should have a valid value!", "promotion");

            activePromotions.Add(promotion);

            return true;
        }

        public double CalculateTotalOrderValue(Cart cart)
        {
            if (cart == null)
                throw new ArgumentNullException("cart", "A valid cart data needs to be passed in!");

            if (cart.CartItems == null || cart.CartItems.Count == 0)
                throw new InvalidOperationException("The Cart should have a valid number of items!!");

            if (cart.CartItems.Any(c => c.Product == null || c.Product.Item == null))
                throw new ArgumentException("The Cart has invalid items!", "cart");

            if (cart.CartItems.Any(c => !char.IsLetterOrDigit(c.Product.Item.Id)))
                throw new ArgumentException("The Cart has invalid items!", "cart");

            if (cart.CartItems.Any(c => c.Product.UnitPrice <= 0))
                throw new ArgumentException("The Cart items should have a valid unit price!", "cart");

            if (cart.CartItems.Any(c => c.Quantity <= 0))
                throw new ArgumentException("The Cart items should have a valid quantity!", "cart");

            activePromotions.ForEach(ap => PromotionCalculatorFactory.GetCalculatorInstance(ap.Type)
                                        .ApplyPromotion(cart.CartItems.Where(ci =>
                                            ap.PromotionParts.Any(pp => pp.Item.Id.CompareTo(ci.Product.Item.Id) == 0) &&
                                            ci.PromotionApplied.Item1 == 0
                                        ), ap));

            return cart.CartItems.Sum(ci => ci.PromotionApplied.Item2 + (ci.Quantity - ci.PromotionApplied.Item1) * ci.Product.UnitPrice);
        }
    }
}
