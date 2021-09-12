namespace PromotionEngine.Entities
{
    /// <summary>
    /// The class representing each item which is part of a promotion.
    /// </summary>
    public class PromotionPart
    {
        /// <summary>
        /// Gets or sets the item.
        /// </summary>
        /// <value>
        /// The item.
        /// </value>
        public ItemBase Item { get; set; }

        /// <summary>
        /// Gets or sets the quantity.
        /// </summary>
        /// <value>
        /// The quantity.
        /// </value>
        public int Quantity { get; set; }
    }
}
