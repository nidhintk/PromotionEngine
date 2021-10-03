using PromotionEngine.Entities;

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

        /// <summary>
        /// Calculates the total order value.
        /// </summary>
        /// <param name="cart">The cart.</param>
        /// <returns>The total order value.</returns>
        double CalculateTotalOrderValue(Cart cart); 
    }
}
