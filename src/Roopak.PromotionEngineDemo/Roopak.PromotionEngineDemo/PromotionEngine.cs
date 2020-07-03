using Roopak.PromotionEngineDemo.Enums;
using Roopak.PromotionEngineDemo.Models;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;

namespace Roopak.PromotionEngineDemo
{
    public class PromotionEngine
    {
        public decimal ComputeTotalInvoiceAmount(Order order, List<Promotion> promotions)
        {
            var total = 0m;

            if (order?.Items?.Count > 0)
            {
                promotions?.ForEach(p =>
                {
                    if (p.Type == PromotionType.QuantityBased)
                    {
                        order.Items.ForEach(oi =>
                        {
                            if (p.SkuIds.Contains(oi.SkuId) && oi.Quantity >= p.QualifyingQuantity)
                            {
                                if (!oi.IsPromotionApplied)
                                {
                                    total += p.Price;
                                    oi.IsPromotionApplied = true;
                                }
                            }
                        });
                    }
                });

                var remainingOrderItems = order.Items.Where(x => !x.IsPromotionApplied).ToList();
                remainingOrderItems.ForEach(x => total += x.Quantity * x.UnitPrice);
            }            

            return total;
        }
    }
}