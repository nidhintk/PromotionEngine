using Microsoft.VisualStudio.TestTools.UnitTesting;
using PromotionEngine.Entities;
using PromotionEngine.Interface;
using System;
using System.Collections.Generic;

namespace PromotionEngine.UnitTests
{
    [TestClass]
    public class PromotionEngineTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddActivePromotion_NullPassedIn_ThrowsException()
        {
            IPromote promotionEngine = new SkuPromotionEngine();
            promotionEngine.AddActivePromotion(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AddActivePromotion_PromotionWithNullPromotionPartsPassedIn_ThrowsException()
        {
            IPromote promotionEngine = new SkuPromotionEngine();
            promotionEngine.AddActivePromotion(new Promotion());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AddActivePromotion_PromotionWithInvalidPromotionItemNamePassedIn_ThrowsException()
        {
            IPromote promotionEngine = new SkuPromotionEngine();
            promotionEngine.AddActivePromotion(new Promotion()
            {
                PromotionParts = new List<PromotionPart>()
                {
                    new PromotionPart()
                    {
                        Item = new SkuItem()
                    }
                }
            });
        }
    }
}
