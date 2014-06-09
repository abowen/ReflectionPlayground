using GenericLineOfBusiness.Common.Entities;
using GenericLineOfBusiness.Common.Rules;

namespace GenericLineOfBusiness.Common.Interfaces
{
    public interface IOrderRule
    {
        string RuleName { get; }
        OrderRuleResult CheckRule(Order order);
    }
}
