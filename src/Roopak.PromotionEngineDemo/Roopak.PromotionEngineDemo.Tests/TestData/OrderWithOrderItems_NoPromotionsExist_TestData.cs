using Roopak.PromotionEngineDemo.Models;
using System.Collections;
using System.Collections.Generic;

namespace Roopak.PromotionEngineDemo.Tests.TestData
{
    public class OrderWithOrderItems_NoPromotionsExist_TestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                90,
                new Order
                {
                    Items = new List<OrderItem>
                    {
                        new OrderItem
                        {
                            SkuId = "A",
                            Quantity = 3,
                            UnitPrice = 30
                        }
                    }
                },
                null
            };

            yield return new object[]
            {
                90,
                new Order
                {
                    Items = new List<OrderItem>
                    {
                        new OrderItem
                        {
                            SkuId = "A",
                            Quantity = 3,
                            UnitPrice = 30
                        }
                    }
                },
                new List<Promotion>()
            };
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}