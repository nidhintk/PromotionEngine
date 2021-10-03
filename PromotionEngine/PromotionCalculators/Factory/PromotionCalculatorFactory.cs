using PromotionEngine.Interfaces.PromotionRules;
using PromotionEngine.PromotionRules;
using System.Collections.Generic;
using static PromotionEngine.Common.Enums;

namespace PromotionEngine.PromotionCalculators.Factory
{
    /// <summary>
    /// A factory class to create and return the required Promotion Calculator based on the Promotion type.
    /// </summary>
    public static class PromotionCalculatorFactory
    {
        /// <summary>
        /// The calculator map
        /// </summary>
        private static IDictionary<PromotionType, IPromotionCalculator> calculatorMap = new Dictionary<PromotionType, IPromotionCalculator>();

        /// <summary>
        /// Registers the calculators.
        /// </summary>
        private static void RegisterCalculators()
        {
            if (calculatorMap.Count == 0)
            {
                // Place to add all the possible promotion type calculators.
                calculatorMap.Add(PromotionType.FixedPriceForNItems, new FixedPriceNItemsPromotionCalculator());
                calculatorMap.Add(PromotionType.FixedPriceForCombinedItems, new FixedPriceCombinedItemsPromotionCalculator());
            }
        }

        /// <summary>
        /// Initializes the <see cref="PromotionCalculatorFactory"/> class.
        /// </summary>
        static PromotionCalculatorFactory()
        {
            RegisterCalculators();
        }

        /// <summary>
        /// Gets the calculator instance.
        /// </summary>
        /// <param name="promotionType">Type of the promotion.</param>
        /// <returns>The promotion calculator.</returns>
        public static IPromotionCalculator GetCalculatorInstance(PromotionType promotionType)
        {
            if (calculatorMap.ContainsKey(promotionType))
                return calculatorMap[promotionType];

            return null;
        }
    }
}
