using Roopak.PromotionEngineDemo.Models;
using System.Collections;
using System.Collections.Generic;

namespace Roopak.PromotionEngineDemo.Tests.TestData
{
    public class OrderWithoutOrderItems_TotalZero_TestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { 0, null, new List<Promotion>() };
            yield return new object[] { 0, new Order { Items = new List<OrderItem>() }, new List<Promotion>() };
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}