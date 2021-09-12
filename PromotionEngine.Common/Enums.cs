namespace PromotionEngine.Common
{
    /// <summary>
    /// The class for representing all the common enums.
    /// </summary>
    public class Enums
    {
        /// <summary>
        /// The enum for Promotion Type
        /// </summary>
        public enum PromotionType
        {
            /// <summary>
            /// The defult enum
            /// </summary>
            None = 0,

            /// <summary>
            /// The N Items fixed price
            /// </summary>
            FixedPriceForNItems = 1,

            /// <summary>
            /// The fixed price for combined items
            /// </summary>
            FixedPriceForCombinedItems = 2,

            /// <summary>
            /// The percentage
            /// </summary>
            Percentage = 3
        }
    }
}
