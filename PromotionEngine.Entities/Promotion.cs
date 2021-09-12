using System;
using System.Collections.Generic;
using System.Text;
using static PromotionEngine.Common.Enums;

namespace PromotionEngine.Entities
{
    /// <summary>
    /// The class representing a Promotion entity.
    /// </summary>
    public class Promotion
    {
        /// <summary>
        /// Gets or sets the promotion parts.
        /// The items in different promotion parts together form a Promotion
        /// </summary>
        /// <value>
        /// The promotion parts.
        /// </value>
        public IList<PromotionPart> PromotionParts { get; set; }

        /// <summary>
        /// Gets or sets the type of Promotion.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public PromotionType Type { get; set; }

        /// <summary>
        /// Gets or sets the value of Promotion.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public double Value { get; set; }
    }
}
