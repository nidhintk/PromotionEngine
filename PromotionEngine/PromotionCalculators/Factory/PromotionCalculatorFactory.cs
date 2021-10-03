using PromotionEngine.Interfaces.PromotionRules;
using PromotionEngine.PromotionRules;
using System.Collections.Generic;
using static PromotionEngine.Common.Enums;

namespace PromotionEngine.PromotionCalculators.Factory
{
    public static class PromotionCalculatorFactory
    {
        private static IDictionary<PromotionType, IPromotionCalculator> calculatorMap = new Dictionary<PromotionType, IPromotionCalculator>();

        private static void RegisterCalculators()
        {
            if (calculatorMap.Count == 0)
            {
                calculatorMap.Add(PromotionType.FixedPriceForNItems, new FixedPriceNItemsPromotionCalculator());
                calculatorMap.Add(PromotionType.FixedPriceForCombinedItems, new FixedPriceCombinedItemsPromotionCalculator());
            }
        }

        static PromotionCalculatorFactory()
        {
            RegisterCalculators();
        }

        public static IPromotionCalculator GetCalculatorInstance(PromotionType promotionType)
        {
            if (calculatorMap.ContainsKey(promotionType))
                return calculatorMap[promotionType];

            return null;
        }
    }
}
