namespace PromotionEngine.Entities
{
    /// <summary>
    /// The base class for an Item.
    /// Helps to keep things generic.
    /// </summary>
    public abstract class ItemBase
    {
        /// <summary>
        /// Gets or sets the single character SKU Id.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public char Id { get; set; }
    }
}
