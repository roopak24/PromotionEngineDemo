using Roopak.PromotionEngineDemo.Enums;
using System.Collections.Generic;

namespace Roopak.PromotionEngineDemo.Models
{
    public class Promotion
    {
        public PromotionType Type { get; set; }

        public int QualifyingQuantity { get; set; }

        public List<string> SkuIds { get; set; }

        public decimal Price { get; set; }
    }
}