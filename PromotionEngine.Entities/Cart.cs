using System.Collections.Generic;

namespace PromotionEngine.Entities
{
    /// <summary>
    /// The class representing the Cart.
    /// </summary>
    public class Cart
    {
        /// <summary>
        /// Gets or sets the cart items.
        /// </summary>
        /// <value>
        /// The cart items.
        /// </value>
        public List<CartItem> CartItems { get; set; }
    }
}
