using System.Linq;
using GenericLineOfBusiness.Common.Entities;
using GenericLineOfBusiness.Common.Interfaces;
using GenericLineOfBusiness.Common.Rules;

namespace GenericLineOfBusiness.RulesForOrder
{
    public class MininumItemsRule : IOrderRule
    {
        public MininumItemsRule()
        {
            RuleName = "Minimum items";
        }

        public string RuleName { get; private set; }        

        public OrderRuleResult CheckRule(Order order)
        {            
            if (order == null || order.OrderItems == null)
            {
                return new OrderRuleResult(false, "Missing required information");
            }
            if (!order.OrderItems.Any())
            {
                return new OrderRuleResult(false, "Must have at least one item");
            }
            return new OrderRuleResult(true, string.Empty);
        }
    }
}
