using static PromotionEngine.Common.Enums;

namespace PromotionEngine.PromotionRules
{
    /// <summary>
    /// The base abstract class for Promotion Type.
    /// </summary>
    public abstract class BasePromotionCalculator
    {
        protected PromotionType promotionType;

        protected BasePromotionCalculator(PromotionType promotionType)
        {
            this.promotionType = promotionType;
        }
    }
}
