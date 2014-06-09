using GenericLineOfBusiness.Common.Entities;
using GenericLineOfBusiness.Common.Interfaces;
using GenericLineOfBusiness.Common.Rules;

namespace GenericLineOfBusiness.RulesForPeople
{
    public class PersonAgeRule : IOrderRule
    {
        public PersonAgeRule ()
        {
            RuleName = "Person of legal age";
        }

        public string RuleName { get; private set; }

        public OrderRuleResult CheckRule(Order order)
        {
            if (order == null || order.Customer == null)
            {
                return new OrderRuleResult(false, "Missing required information");
            }
            if (order.Customer.Age < 18 || order.Customer.Age > 65)
            {
                return new OrderRuleResult(false, "Customer is not within valid age range");
            }
            return new OrderRuleResult(true, string.Empty);
        }
    }
}
