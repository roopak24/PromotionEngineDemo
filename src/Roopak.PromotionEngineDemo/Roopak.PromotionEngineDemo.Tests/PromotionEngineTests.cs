using Roopak.PromotionEngineDemo.Models;
using Roopak.PromotionEngineDemo.Tests.TestData;
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
        public void OrderWithoutOrderItems_TotalZero(decimal expectedTotal, Order order, List<Promotion> promotions)
        {
            var total = _sut.ComputeTotalInvoiceAmount(order, promotions);
            Assert.Equal(expectedTotal, total);
        }

        [Theory]
        [ClassData(typeof(OrderWithOrderItems_NoPromotionsExist_TestData))]
        public void OrderWithOrderItems_NoPromotionsExist(decimal expectedTotal, Order order, List<Promotion> promotions)
        {
            var total = _sut.ComputeTotalInvoiceAmount(order, promotions);
            Assert.Equal(expectedTotal, total);
        }

        [Theory]
        [ClassData(typeof(OrderWithOrderItems_PromotionsExist_ButNoPromotionsApply_TestData))]
        public void OrderWithOrderItems_PromotionsExist_ButNoPromotionsApply(decimal expectedTotal, Order order, List<Promotion> promotions)
        {
            var total = _sut.ComputeTotalInvoiceAmount(order, promotions);
            Assert.Equal(expectedTotal, total);
        }
    }
}