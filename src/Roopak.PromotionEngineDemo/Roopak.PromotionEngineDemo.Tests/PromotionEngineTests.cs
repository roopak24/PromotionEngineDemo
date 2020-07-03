using Roopak.PromotionEngineDemo.Models;
using Roopak.PromotionEngineDemo.Tests.TestData;
using System.Collections;
using System.Collections.Generic;
using Xunit;

namespace Roopak.PromotionEngineDemo.Tests
{
    public class PromotionEngineTests
    {
        private readonly PromotionEngine _sut;

        public PromotionEngineTests()
        {
            _sut = new PromotionEngine();
        }

        [Theory]
        [ClassData(typeof(OrderWithoutOrderItems_TotalZero_TestData))]
        public void OrderWithoutOrderItems_TotalZero(decimal expectedTotal, Order order)
        {
            var total = _sut.ComputeTotalInvoiceAmount(order);
            Assert.Equal(expectedTotal, total);
        }
    }
}