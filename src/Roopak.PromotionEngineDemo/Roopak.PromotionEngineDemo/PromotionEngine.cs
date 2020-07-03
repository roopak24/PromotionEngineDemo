using Roopak.PromotionEngineDemo.Enums;
using Roopak.PromotionEngineDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;

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
                            if (p.SkuIds.Contains(oi.SkuId) && !oi.IsPromotionApplied)
                            {
                                var remainingQuantity = oi.Quantity;
                                while (remainingQuantity >= p.QualifyingQuantity)
                                {
                                    total += p.Price;
                                    remainingQuantity -= p.QualifyingQuantity;
                                }
                                total += remainingQuantity * oi.UnitPrice;
                                oi.IsPromotionApplied = true;
                            }
                        });
                    }
                    else if (p.Type == PromotionType.CombinationBased)
                    {
                        if (IsExistsAllPromotionSkusInOrder(p.SkuIds, order.Items.Where(x => !x.IsPromotionApplied).ToList()))
                        {
                            var applicableOrderItems = order.Items.Where(x => p.SkuIds.Contains(x.SkuId)).ToList();
                            var promotionApplicableTimes = applicableOrderItems.Min(aoi => aoi.Quantity);
                            applicableOrderItems.ForEach(aoi => aoi.Quantity -= promotionApplicableTimes);
                            total += promotionApplicableTimes * p.Price;
                        }                     
                    }

                });

                var remainingOrderItems = order.Items.Where(x => !x.IsPromotionApplied).ToList();
                remainingOrderItems.ForEach(x => total += x.Quantity * x.UnitPrice);
            }            

            return total;
        }

        private bool IsExistsAllPromotionSkusInOrder(List<string> promotionSkuIds, List<OrderItem> orderItems)
        {
            return promotionSkuIds.All(pSkuId => orderItems.Select(oi => oi.SkuId).Contains(pSkuId));
        }
    }
}