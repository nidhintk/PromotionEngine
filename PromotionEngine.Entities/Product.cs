namespace PromotionEngine.Entities
{
    /// <summary>
    /// The class representing a product in the Cart.
    /// </summary>
    public class Product
    {
        /// <summary>
        /// Gets or sets the item.
        /// </summary>
        /// <value>
        /// The item.
        /// </value>
        public ItemBase Item { get; set; }

        /// <summary>
        /// Gets or sets the unit price.
        /// </summary>
        /// <value>
        /// The unit price.
        /// </value>
        public double UnitPrice { get; set; }
    }
}
