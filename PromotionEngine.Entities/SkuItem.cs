namespace PromotionEngine.Entities
{
    /// <summary>
    /// The class representing a single SKU Item.
    /// </summary>
    /// <seealso cref="PromotionEngine.Entities.ItemBase" />
    public class SkuItem : ItemBase
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
