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
            decimal total = 0m;

            if (order?.Items?.Count > 0)
            {
                promotions?.ForEach(p =>
                {
                    if (p.Type == PromotionType.QuantityBased)
                    {
                        total += ComputeTotalInvoiceAmountForQuantityBasedPromotion(order, p);
                    }
                    else if (p.Type == PromotionType.CombinationBased)
                    {
                        total += ComputeTotalInvoiceAmountForCombinationBasedPromotion(order, p);
                    }

                });

                List<OrderItem> remainingOrderItems = order.Items.Where(x => !x.IsPromotionApplied).ToList();
                remainingOrderItems.ForEach(x => total += x.Quantity * x.UnitPrice);
            }

            return total;
        }

        private static decimal ComputeTotalInvoiceAmountForQuantityBasedPromotion(Order order, Promotion promotion)
        {
            decimal total = 0m;
            order.Items.ForEach(oi =>
            {
                if (promotion.SkuIds.Contains(oi.SkuId) && !oi.IsPromotionApplied)
                {
                    int remainingQuantity = oi.Quantity;
                    while (remainingQuantity >= promotion.QualifyingQuantity)
                    {
                        total += promotion.Price;
                        remainingQuantity -= promotion.QualifyingQuantity;
                    }
                    total += remainingQuantity * oi.UnitPrice;
                    oi.IsPromotionApplied = true;
                }
            });
            return total;
        }

        private decimal ComputeTotalInvoiceAmountForCombinationBasedPromotion(Order order, Promotion promotion)
        {
            decimal total = 0m;
            if (IsExistsAllPromotionSkusInOrder(promotion.SkuIds, order.Items.Where(x => !x.IsPromotionApplied).ToList()))
            {
                List<OrderItem> applicableOrderItems = order.Items.Where(x => promotion.SkuIds.Contains(x.SkuId)).ToList();
                int promotionApplicableTimes = applicableOrderItems.Min(aoi => aoi.Quantity);
                applicableOrderItems.ForEach(aoi => aoi.Quantity -= promotionApplicableTimes);
                total += promotionApplicableTimes * promotion.Price;
            }
            return total;
        }

        private bool IsExistsAllPromotionSkusInOrder(List<string> promotionSkuIds, List<OrderItem> orderItems)
        {
            return promotionSkuIds.All(pSkuId => orderItems.Select(oi => oi.SkuId).Contains(pSkuId));
        }
    }
}