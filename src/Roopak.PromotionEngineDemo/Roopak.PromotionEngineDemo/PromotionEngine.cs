using Roopak.PromotionEngineDemo.Models;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;

namespace Roopak.PromotionEngineDemo
{
    public class PromotionEngine
    {
        public decimal ComputeTotalInvoiceAmount(Order order, List<Promotion> promotions)
        {
            var total = 0m;

            if (order !=  null && order.Items != null && order.Items.Count > 0)
            {
                order.Items.ForEach(x => total += x.Quantity * x.UnitPrice);
            }            

            return total;
        }
    }
}