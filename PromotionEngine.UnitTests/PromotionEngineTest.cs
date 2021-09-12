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
        #region Adding Promotion tests

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

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AddActivePromotion_ProblemWithInvalidPromotionItemQuantityPassedIn_ThrowsException()
        {
            IPromote promotionEngine = new SkuPromotionEngine();
            promotionEngine.AddActivePromotion(new Promotion()
            {
                PromotionParts = new List<PromotionPart>()
                {
                    new PromotionPart()
                    {
                        Item = new SkuItem()
                        {
                            Id = 'A'
                        }
                    }
                }
            });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AddActivePromotion_ProblemWithInvalidPromotionTypePassedIn_ThrowsException()
        {
            IPromote promotionEngine = new SkuPromotionEngine();
            promotionEngine.AddActivePromotion(new Promotion()
            {
                PromotionParts = new List<PromotionPart>()
                {
                    new PromotionPart()
                    {
                        Item = new SkuItem() { Id = 'A' },
                        Quantity = 2
                    }
                },
                Value = 10
            });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AddActivePromotion_ProblemWithInvalidValuePassedIn_ThrowsException()
        {
            IPromote promotionEngine = new SkuPromotionEngine();
            promotionEngine.AddActivePromotion(new Promotion()
            {
                PromotionParts = new List<PromotionPart>()
                {
                    new PromotionPart()
                    {
                        Item = new SkuItem() { Id = 'A' },
                        Quantity = 2
                    }
                },
                Value = 0,
                Type = Common.Enums.PromotionType.FixedPriceForNItems
            });
        }

        [TestMethod]
        public void AddActivePromotion_ValidPromotionPassedIn_Success()
        {
            IPromote promotionEngine = new SkuPromotionEngine();
            bool addStatus = promotionEngine.AddActivePromotion(new Promotion()
            {
                PromotionParts = new List<PromotionPart>()
                {
                    new PromotionPart()
                    {
                        Item = new SkuItem() { Id = 'A' },
                        Quantity = 2
                    }
                },
                Value = 10,
                Type = Common.Enums.PromotionType.FixedPriceForNItems
            });
            Assert.AreEqual(true, addStatus);
        }

        #endregion

        #region Calculating total order value tests

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CalculateTotalOrderValue_NULLPassedIn_ThrowsException()
        {
            IPromote promotionEngine = new SkuPromotionEngine();
            promotionEngine.CalculateTotalOrderValue(null);
        }

        #endregion
    }
}
