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

        /// <summary>
        /// Tests the AddActivePromotion method to throw an exception in case of a NULL Promotion.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddActivePromotion_NullPassedIn_ThrowsException()
        {
            IPromote promotionEngine = new SkuPromotionEngine();
            promotionEngine.AddActivePromotion(null);
        }

        /// <summary>
        /// Tests the AddActivePromotion method to throw an exception in case an invalid Promotion.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AddActivePromotion_PromotionWithNullPromotionPartsPassedIn_ThrowsException()
        {
            IPromote promotionEngine = new SkuPromotionEngine();
            promotionEngine.AddActivePromotion(new Promotion());
        }

        /// <summary>
        /// Tests the AddActivePromotion method to throw an exception in case an invalid Promotion.
        /// </summary>
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

        /// <summary>
        /// Tests the AddActivePromotion method to throw an exception in case an invalid item is specified in a Promotion.
        /// </summary>
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

        /// <summary>
        /// Tests the AddActivePromotion method to throw an exception in case an invalid quantity is specified for a Promotion.
        /// </summary>
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

        /// <summary>
        /// Tests the AddActivePromotion method to throw an exception in case an invalid promotion type is specified for a Promotion.
        /// </summary>
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

        /// <summary>
        /// Tests the AddActivePromotion method to throw an exception in case an invalid value is specified for a Promotion.
        /// </summary>
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

        /// <summary>
        /// Tests the AddActivePromotion method to add a Fixed Price for N Items Promotion type.
        /// </summary>
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

        /// <summary>
        /// Tests the CalculateTotalOrderValue method with a null cart.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CalculateTotalOrderValue_NULLPassedIn_ThrowsException()
        {
            IPromote promotionEngine = new SkuPromotionEngine();
            promotionEngine.CalculateTotalOrderValue(null);
        }

        /// <summary>
        /// Tests the CalculateTotalOrderValue method where the cart is invalid.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void CalculateTotalOrderValue_EmptyCartPassedIn_ThrowsException()
        {
            IPromote promotionEngine = new SkuPromotionEngine();
            promotionEngine.CalculateTotalOrderValue(new Cart { CartItems = null });
        }

        /// <summary>
        /// Tests the CalculateTotalOrderValue method where the cart is invalid.
        /// </summary>
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

        /// <summary>
        /// Tests the CalculateTotalOrderValue method where the cart is invalid.
        /// </summary>
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

        /// <summary>
        /// Tests the CalculateTotalOrderValue method where the cart is invalid.
        /// </summary>
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

        /// <summary>
        /// Tests the CalculateTotalOrderValue method where invalid unit price passed in for a SKU.
        /// </summary>
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

        /// <summary>
        /// Tests the CalculateTotalOrderValue method where there are no valid number of cart items.
        /// </summary>
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

        /// <summary>
        /// Tests the CalculateTotalOrderValue method where there are no active promotions.
        /// </summary>
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

        /// <summary>
        /// Tests the CalculateTotalOrderValue method for N number of items promotion with ! active promotion that matches
        /// the cart.
        /// </summary>
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

        /// <summary>
        /// Tests the CalculateTotalOrderValue method for N number of items promotion but with no active promotion that matches
        /// the cart.
        /// </summary>
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

        /// <summary>
        /// Tests the CalculateTotalOrderValue method for N number of items promotion with multiple cart items.
        /// </summary>
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

        /// <summary>
        /// Tests the CalculateTotalOrderValue method for Combined Items promotion with multiple cart items.
        /// </summary>
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

        /// <summary>
        /// Tests the CalculateTotalOrderValue method for multiple active promotions with multiple cart items.
        /// </summary>
        [TestMethod]
        public void CalculateTotalOrderValue_ValidCart1ActivePromotions3_Success()
        {
            IPromote promotionEngine = new SkuPromotionEngine();
            bool addStatus = promotionEngine.AddActivePromotion(new Promotion()
            {
                PromotionParts = new List<PromotionPart>()
                {
                    new PromotionPart()
                    {
                        Item = new SkuItem() { Id = 'A' },
                        Quantity = 3
                    }
                },
                Value = 130,
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
                Value = 45,
                Type = Common.Enums.PromotionType.FixedPriceForNItems
            });
            Assert.AreEqual(true, addStatus);

            addStatus = promotionEngine.AddActivePromotion(new Promotion()
            {
                PromotionParts = new List<PromotionPart>()
                {
                    new PromotionPart()
                    {
                        Item = new SkuItem() { Id = 'C' },
                        Quantity = 1
                    },

                    new PromotionPart()
                    {
                        Item = new SkuItem() { Id = 'D' },
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
                            UnitPrice = 50
                        },
                        Quantity = 1
                    },
                    new CartItem {
                        Product = new Product {
                            Item = new SkuItem {
                                Id = 'B'
                            },
                            UnitPrice = 30
                        },
                        Quantity = 1
                    },
                    new CartItem {
                        Product = new Product {
                            Item = new SkuItem {
                                Id = 'C'
                            },
                            UnitPrice = 20
                        },
                        Quantity = 1
                    }
                }
            });

            Assert.AreEqual(100, total);
        }

        /// <summary>
        /// Tests the CalculateTotalOrderValue method for multiple active promotions with multiple cart items.
        /// </summary>
        [TestMethod]
        public void CalculateTotalOrderValue_ValidCart2ActivePromotions3_Success()
        {
            IPromote promotionEngine = new SkuPromotionEngine();
            bool addStatus = promotionEngine.AddActivePromotion(new Promotion()
            {
                PromotionParts = new List<PromotionPart>()
                {
                    new PromotionPart()
                    {
                        Item = new SkuItem() { Id = 'A' },
                        Quantity = 3
                    }
                },
                Value = 130,
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
                Value = 45,
                Type = Common.Enums.PromotionType.FixedPriceForNItems
            });
            Assert.AreEqual(true, addStatus);

            addStatus = promotionEngine.AddActivePromotion(new Promotion()
            {
                PromotionParts = new List<PromotionPart>()
                {
                    new PromotionPart()
                    {
                        Item = new SkuItem() { Id = 'C' },
                        Quantity = 1
                    },

                    new PromotionPart()
                    {
                        Item = new SkuItem() { Id = 'D' },
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
                            UnitPrice = 50
                        },
                        Quantity = 5
                    },
                    new CartItem {
                        Product = new Product {
                            Item = new SkuItem {
                                Id = 'B'
                            },
                            UnitPrice = 30
                        },
                        Quantity = 5
                    },
                    new CartItem {
                        Product = new Product {
                            Item = new SkuItem {
                                Id = 'C'
                            },
                            UnitPrice = 20
                        },
                        Quantity = 1
                    }
                }
            });

            Assert.AreEqual(370, total);
        }

        /// <summary>
        /// Tests the CalculateTotalOrderValue method for multiple active promotions with multiple cart items.
        /// </summary>
        [TestMethod]
        public void CalculateTotalOrderValue_ValidCart3ActivePromotions3_Success()
        {
            IPromote promotionEngine = new SkuPromotionEngine();
            bool addStatus = promotionEngine.AddActivePromotion(new Promotion()
            {
                PromotionParts = new List<PromotionPart>()
                {
                    new PromotionPart()
                    {
                        Item = new SkuItem() { Id = 'A' },
                        Quantity = 3
                    }
                },
                Value = 130,
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
                Value = 45,
                Type = Common.Enums.PromotionType.FixedPriceForNItems
            });
            Assert.AreEqual(true, addStatus);

            addStatus = promotionEngine.AddActivePromotion(new Promotion()
            {
                PromotionParts = new List<PromotionPart>()
                {
                    new PromotionPart()
                    {
                        Item = new SkuItem() { Id = 'C' },
                        Quantity = 1
                    },

                    new PromotionPart()
                    {
                        Item = new SkuItem() { Id = 'D' },
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
                            UnitPrice = 50
                        },
                        Quantity = 3
                    },
                    new CartItem {
                        Product = new Product {
                            Item = new SkuItem {
                                Id = 'B'
                            },
                            UnitPrice = 30
                        },
                        Quantity = 5
                    },
                    new CartItem {
                        Product = new Product {
                            Item = new SkuItem {
                                Id = 'C'
                            },
                            UnitPrice = 20
                        },
                        Quantity = 1
                    },
                    new CartItem {
                        Product = new Product {
                            Item = new SkuItem {
                                Id = 'D'
                            },
                            UnitPrice = 15
                        },
                        Quantity = 1
                    }
                }
            });

            Assert.AreEqual(280, total);
        }

        /// <summary>
        /// Tests the CalculateTotalOrderValue method for multiple active promotions with multiple cart items.
        /// </summary>
        [TestMethod]
        public void CalculateTotalOrderValue_ValidCart4ActivePromotions3_Success()
        {
            IPromote promotionEngine = new SkuPromotionEngine();
            bool addStatus = promotionEngine.AddActivePromotion(new Promotion()
            {
                PromotionParts = new List<PromotionPart>()
                {
                    new PromotionPart()
                    {
                        Item = new SkuItem() { Id = 'A' },
                        Quantity = 3
                    }
                },
                Value = 130,
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
                Value = 45,
                Type = Common.Enums.PromotionType.FixedPriceForNItems
            });
            Assert.AreEqual(true, addStatus);

            addStatus = promotionEngine.AddActivePromotion(new Promotion()
            {
                PromotionParts = new List<PromotionPart>()
                {
                    new PromotionPart()
                    {
                        Item = new SkuItem() { Id = 'C' },
                        Quantity = 1
                    },

                    new PromotionPart()
                    {
                        Item = new SkuItem() { Id = 'D' },
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
                            UnitPrice = 50
                        },
                        Quantity = 3
                    },
                    new CartItem {
                        Product = new Product {
                            Item = new SkuItem {
                                Id = 'B'
                            },
                            UnitPrice = 30
                        },
                        Quantity = 5
                    },
                    new CartItem {
                        Product = new Product {
                            Item = new SkuItem {
                                Id = 'C'
                            },
                            UnitPrice = 20
                        },
                        Quantity = 3
                    },
                    new CartItem {
                        Product = new Product {
                            Item = new SkuItem {
                                Id = 'D'
                            },
                            UnitPrice = 15
                        },
                        Quantity = 2
                    }
                }
            });

            Assert.AreEqual(330, total);
        }

        #endregion
    }
}
