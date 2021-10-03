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
        public void AddActivePromotion_PromotionWithInvalidPromotionPartsPassedIn_ThrowsException()
        {
            IPromote promotionEngine = new SkuPromotionEngine();
            promotionEngine.AddActivePromotion(new Promotion() {
                PromotionParts = new List<PromotionPart>()
                {
                    new PromotionPart()
                    {
                        Item = null
                    }
                }
            });
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

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void CalculateTotalOrderValue_EmptyCartPassedIn_ThrowsException()
        {
            IPromote promotionEngine = new SkuPromotionEngine();
            promotionEngine.CalculateTotalOrderValue(new Cart { CartItems = null });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CalculateTotalOrderValue_InvalidCartItem_ThrowsException()
        {
            IPromote promotionEngine = new SkuPromotionEngine();
            promotionEngine.CalculateTotalOrderValue(new Cart { 
                CartItems = new List<CartItem> { 
                    new CartItem { 
                        Product = new Product { 
                            Item = new SkuItem { 
                                Id = '\0' // invalid cart item
                            } 
                        } 
                    } 
                } 
            });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CalculateTotalOrderValue_InvalidCart1_ThrowsException()
        {
            IPromote promotionEngine = new SkuPromotionEngine();
            promotionEngine.CalculateTotalOrderValue(new Cart { 
                CartItems = new List<CartItem> { 
                    new CartItem { 
                        Product = null // invalid cart
                    } 
                } 
            });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CalculateTotalOrderValue_InvalidCart2_ThrowsException()
        {
            IPromote promotionEngine = new SkuPromotionEngine();
            promotionEngine.CalculateTotalOrderValue(new Cart { 
                CartItems = new List<CartItem> { 
                    new CartItem { 
                        Product = new Product { 
                            Item = null // invalid items
                        } 
                    } 
                } 
            });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CalculateTotalOrderValue_ProblemWithInvalidUnitPricePassedIn_ThrowsException()
        {
            IPromote promotionEngine = new SkuPromotionEngine();
            promotionEngine.CalculateTotalOrderValue(new Cart { 
                CartItems = new List<CartItem> { 
                    new CartItem { 
                        Product = new Product { 
                            Item = new SkuItem { 
                                Id = 'A' 
                            }, 
                            UnitPrice = -1 // invalid unit price
                        } 
                    } 
                } 
            });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CalculateTotalOrderValue_ProblemWithInvalidQuantityPassedIn_ThrowsException()
        {
            IPromote promotionEngine = new SkuPromotionEngine();
            promotionEngine.CalculateTotalOrderValue(new Cart { 
                CartItems = new List<CartItem> { 
                    new CartItem { 
                        Product = new Product { 
                            Item = new SkuItem { 
                                Id = 'A' 
                            }, 
                            UnitPrice = 10 
                        }, 
                        Quantity = 0 // invalid quantity
                    } 
                } 
            });
        }

        [TestMethod]
        public void CalculateTotalOrderValue_ValidCart1NoActivePromotions_Success()
        {
            IPromote promotionEngine = new SkuPromotionEngine();
            double total = promotionEngine.CalculateTotalOrderValue(new Cart
            {
                CartItems = new List<CartItem> {
                    new CartItem {
                        Product = new Product {
                            Item = new SkuItem {
                                Id = 'A'
                            },
                            UnitPrice = 20
                        },
                        Quantity = 2
                    }
                }
            });

            Assert.AreEqual(40, total);
        }

        [TestMethod]
        public void CalculateTotalOrderValue_FixedPriceNItemsValidCart1ActivePromotion1_Success()
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
                Value = 30,
                Type = Common.Enums.PromotionType.FixedPriceForNItems
            });
            Assert.AreEqual(true, addStatus);

            double total = promotionEngine.CalculateTotalOrderValue(new Cart
            {
                CartItems = new List<CartItem> {
                    new CartItem {
                        Product = new Product {
                            Item = new SkuItem {
                                Id = 'A'
                            },
                            UnitPrice = 20
                        },
                        Quantity = 3
                    }
                }
            });

            Assert.AreEqual(50, total);
        }

        [TestMethod]
        public void CalculateTotalOrderValue_FixedPriceNItemsValidCart1DifferentActivePromotion_Success()
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
                Value = 30,
                Type = Common.Enums.PromotionType.FixedPriceForNItems
            });
            Assert.AreEqual(true, addStatus);

            double total = promotionEngine.CalculateTotalOrderValue(new Cart
            {
                CartItems = new List<CartItem> {
                    new CartItem {
                        Product = new Product {
                            Item = new SkuItem {
                                Id = 'B'
                            },
                            UnitPrice = 20
                        },
                        Quantity = 3
                    }
                }
            });

            Assert.AreEqual(60, total);
        }

        [TestMethod]
        public void CalculateTotalOrderValue_FixedPriceNItemsValidCart1ActivePromotions2_Success()
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
                Value = 30,
                Type = Common.Enums.PromotionType.FixedPriceForNItems
            });
            Assert.AreEqual(true, addStatus);

            addStatus = promotionEngine.AddActivePromotion(new Promotion()
            {
                PromotionParts = new List<PromotionPart>()
                {
                    new PromotionPart()
                    {
                        Item = new SkuItem() { Id = 'B' },
                        Quantity = 2
                    }
                },
                Value = 30,
                Type = Common.Enums.PromotionType.FixedPriceForNItems
            });
            Assert.AreEqual(true, addStatus);

            double total = promotionEngine.CalculateTotalOrderValue(new Cart
            {
                CartItems = new List<CartItem> {
                    new CartItem {
                        Product = new Product {
                            Item = new SkuItem {
                                Id = 'A'
                            },
                            UnitPrice = 20
                        },
                        Quantity = 3
                    },
                    new CartItem {
                        Product = new Product {
                            Item = new SkuItem {
                                Id = 'B'
                            },
                            UnitPrice = 20
                        },
                        Quantity = 3
                    }
                }
            });

            Assert.AreEqual(100, total);
        }

        [TestMethod]
        public void CalculateTotalOrderValue_CombinedItemsValidCart1ActivePromotions1_Success()
        {
            IPromote promotionEngine = new SkuPromotionEngine();
            bool addStatus = promotionEngine.AddActivePromotion(new Promotion()
            {
                PromotionParts = new List<PromotionPart>()
                {
                    new PromotionPart()
                    {
                        Item = new SkuItem() { Id = 'A' },
                        Quantity = 1
                    },

                    new PromotionPart()
                    {
                        Item = new SkuItem() { Id = 'B' },
                        Quantity = 1
                    }
                },
                Value = 30,
                Type = Common.Enums.PromotionType.FixedPriceForCombinedItems
            });
            Assert.AreEqual(true, addStatus);

            double total = promotionEngine.CalculateTotalOrderValue(new Cart
            {
                CartItems = new List<CartItem> {
                    new CartItem {
                        Product = new Product {
                            Item = new SkuItem {
                                Id = 'A'
                            },
                            UnitPrice = 20
                        },
                        Quantity = 2
                    },
                    new CartItem {
                        Product = new Product {
                            Item = new SkuItem {
                                Id = 'B'
                            },
                            UnitPrice = 20
                        },
                        Quantity = 1
                    }
                }
            });

            Assert.AreEqual(50, total);
        }

        #endregion
    }
}
