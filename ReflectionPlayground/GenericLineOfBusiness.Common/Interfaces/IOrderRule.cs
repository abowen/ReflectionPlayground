using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GenericLineOfBusiness.Common.Entities;

namespace GenericLineOfBusiness.Common.Interfaces
{
    public interface IOrderRule
    {
        string RuleName { get; }
        OrderRuleResult CheckRule(Order order);
    }
}
