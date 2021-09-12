namespace PromotionEngine.Entities
{
    /// <summary>
    /// The class representing an item in a cart.
    /// </summary>
    public class CartItem
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CartItem"/> class.
        /// </summary>
        public CartItem()
        {
            PromotionApplied = new System.Tuple<int, double>(0, 0);
        }

        /// <summary>
        /// Gets or sets the product.
        /// </summary>
        /// <value>
        /// The product.
        /// </value>
        public Product Product { get; set; }

        /// <summary>
        /// Gets or sets the quantity.
        /// </summary>
        /// <value>
        /// The quantity.
        /// </value>
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets the promotions applied.
        /// </summary>
        /// <value>
        /// The promotions applied.
        /// </value>
        public System.Tuple<int, double> PromotionApplied { get; set; }
    }
}
