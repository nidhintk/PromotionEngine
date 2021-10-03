using static PromotionEngine.Common.Enums;

namespace PromotionEngine.PromotionRules
{
    /// <summary>
    /// The base abstract class for Promotion Type.
    /// </summary>
    public abstract class BasePromotionCalculator
    {
        /// <summary>
        /// The promotion type
        /// </summary>
        protected PromotionType promotionType;

        /// <summary>
        /// Initializes a new instance of the <see cref="BasePromotionCalculator"/> class.
        /// </summary>
        /// <param name="promotionType">Type of the promotion.</param>
        protected BasePromotionCalculator(PromotionType promotionType)
        {
            this.promotionType = promotionType;
        }
    }
}
