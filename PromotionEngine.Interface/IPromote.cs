using PromotionEngine.Entities;
using System.Collections.Generic;

namespace PromotionEngine.Interface
{
    /// <summary>
    /// The interface for Promotion Engine
    /// </summary>
    public interface IPromote
    {
        /// <summary>
        /// Adds the active promotion.
        /// </summary>
        /// <param name="promotion">The promotion.</param>
        /// <returns>true if the promotion has been added successfully else false.</returns>
        bool AddActivePromotion(Promotion promotion);
    }
}
