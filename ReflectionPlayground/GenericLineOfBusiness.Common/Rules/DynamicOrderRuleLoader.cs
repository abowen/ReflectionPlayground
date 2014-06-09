using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using GenericLineOfBusiness.Common.Interfaces;

namespace GenericLineOfBusiness.Common.Rules
{
    public static class DynamicOrderRuleLoader
    {
        public static List<DynamicOrderRule> LoadRules(string assemblyPath)
        {
            var rules = new List<DynamicOrderRule>();

            if (!Directory.Exists(assemblyPath))
                return rules;

            var assemblyFiles = Directory.EnumerateFiles(assemblyPath, "*.dll", SearchOption.TopDirectoryOnly);

            foreach (var assemblyFile in assemblyFiles)
            {
                var assembly = Assembly.LoadFrom(assemblyFile);
                foreach (var type in assembly.ExportedTypes)
                {
                    if (type.IsClass && typeof(IOrderRule).IsAssignableFrom(type))
                    {
                        var rule = Activator.CreateInstance(type) as IOrderRule;
                        var dynamicRule = new DynamicOrderRule(rule, type.FullName, type.Assembly.GetName().Name);
                        rules.Add(dynamicRule);
                    }
                }
            }

            return rules;
        }
    }
}
