using PromotionEngine.Entities;
using PromotionEngine.Interface;
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
        private IList<Promotion> ActivePromotions { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SkuPromotionEngine"/> class.
        /// </summary>
        public SkuPromotionEngine()
        {
            // Initialising the list of active promotions.
            ActivePromotions = new List<Promotion>();
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

            if (promotion.PromotionParts.Any(p => !char.IsLetterOrDigit(((SkuItem)p.Item).Id)))
                throw new ArgumentException("Promotion passed in should be against valid Sku!", "promotion");

            if (promotion.PromotionParts.Any(p => p.Quantity <= 0))
                throw new ArgumentException("Promotion passed in should have a valid quantity against each Sku!", "promotion");

            if (promotion.Type == Common.Enums.PromotionType.None)
                throw new ArgumentException("Promotion passed in should have a valid type!", "promotion");

            if (promotion.Value <= 0)
                throw new ArgumentException("Promotion passed in should have a valid value!", "promotion");

            return true;
        }

        public bool AddActivePromotions(IList<Promotion> promotions)
        {
            throw new NotImplementedException();
        }
    }
}
