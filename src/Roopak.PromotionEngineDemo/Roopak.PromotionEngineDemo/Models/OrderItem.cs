namespace Roopak.PromotionEngineDemo.Models
{
    public class OrderItem
    {
        public string SkuId { get; set; }

        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        public bool IsPromotionApplied { get; set; }
    }
}