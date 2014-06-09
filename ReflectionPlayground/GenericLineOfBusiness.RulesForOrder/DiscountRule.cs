using GenericLineOfBusiness.Common.Entities;
using GenericLineOfBusiness.Common.Interfaces;
using GenericLineOfBusiness.Common.Rules;

namespace GenericLineOfBusiness.RulesForOrder
{
    public class DiscountRule : IOrderRule
    {
        public DiscountRule()
        {
            RuleName = "Discount allowable";
        }

        public string RuleName { get; private set; }        

        public OrderRuleResult CheckRule(Order order)
        {
            var minValue = 0;
            var maxValue = 20;
            if (order == null)
            {
                return new OrderRuleResult(false, "Missing required information");
            }
            if (order.OrderDiscount < minValue || order.OrderDiscount > maxValue)
            {
                return new OrderRuleResult(false, string.Format("Discount is not within range : {0} - {1}", minValue, maxValue));
            }
            return new OrderRuleResult(true, string.Empty);
        }
    }
}
