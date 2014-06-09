using System.Collections.Generic;
using GenericLineOfBusiness.Common.Entities;

namespace GenericLineOfBusiness.Common.Rules
{
    public class OrderRuleChecker
    {
        public List<DynamicOrderRule> Rules { get; private set; }
        public List<DynamicOrderRule> BrokenRules { get; private set; }

        public OrderRuleChecker(string rulePath)
        {
            Rules = DynamicOrderRuleLoader.LoadRules(rulePath);
        }

        public bool CheckRules(Order order)
        {
            BrokenRules = new List<DynamicOrderRule>();
            foreach (var rule in Rules)
            {
                var result = rule.OrderRule.CheckRule(order);
                if (!result.Result)
                {
                    rule.Message = result.Message;
                    BrokenRules.Add(rule);
                }
            }
            return BrokenRules.Count == 0;
        }
    }
}
