using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace IntroductionToLINQ
{
    public static class DynamicOrder
    {
        public static IEnumerable<T> OrderBy<T>(this IEnumerable<T> source, string propertyName, SortOrder order)
        {
            ParameterExpression paramExpr = Expression.Parameter(typeof(T), "x");
            Expression propExpr = Expression.Property(paramExpr, propertyName);
            var resultExpr = Expression.Lambda(propExpr, paramExpr);

            var lambda = resultExpr.Compile();

            Type t = typeof(Enumerable);
            var methods = t.GetMethods(BindingFlags.Public | BindingFlags.Static);
            var selectedMethods = methods
                .Where(m =>
                    order == SortOrder.Ascending ? m.Name == "OrderBy" : m.Name == "OrderByDescending"
                    && m.GetParameters().Count() == 2);

            var method = selectedMethods.First();

            method = method.MakeGenericMethod(typeof(T), propExpr.Type);

            var result = (IEnumerable<T>)method.Invoke(null, new object[] { source, lambda });

            return result;
        }
    }
}
