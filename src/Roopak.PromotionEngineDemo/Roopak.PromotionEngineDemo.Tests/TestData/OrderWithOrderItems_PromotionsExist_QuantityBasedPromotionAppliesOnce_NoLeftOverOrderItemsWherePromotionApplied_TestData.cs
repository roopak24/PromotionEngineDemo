using Roopak.PromotionEngineDemo.Enums;
using Roopak.PromotionEngineDemo.Models;
using System.Collections;
using System.Collections.Generic;

namespace Roopak.PromotionEngineDemo.Tests.TestData
{
    public class OrderWithOrderItems_PromotionsExist_QuantityBasedPromotionAppliesOnce_NoLeftOverOrderItemsWherePromotionApplied_TestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                180,
                new Order
                {
                    Items = new List<OrderItem>
                    {
                        new OrderItem
                        {
                            SkuId = "A",
                            Quantity = 3,
                            UnitPrice = 50
                        },
                        new OrderItem
                        {
                            SkuId = "B",
                            Quantity = 1,
                            UnitPrice = 30
                        },
                        new OrderItem
                        {
                            SkuId = "C",
                            Quantity = 1,
                            UnitPrice = 20
                        }
                    }
                },
                new List<Promotion>
                {
                    new Promotion
                    {
                        Type = PromotionType.QuantityBased,
                        QualifyingQuantity = 3,
                        SkuIds = new List<string> { "A" },
                        Price = 130
                    },
                    new Promotion
                    {
                        Type = PromotionType.QuantityBased,
                        QualifyingQuantity = 2,
                        SkuIds = new List<string> { "B" },
                        Price = 45
                    },
                    new Promotion
                    {
                        Type = PromotionType.CombinationBased,
                        QualifyingQuantity = 1,
                        SkuIds = new List<string> { "C", "D" },
                        Price = 30
                    }
                }
            };
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}