using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace SalesOrder.Common.Helpers;

public static class Extension
{
    public static bool IsEmpty(this int? value) => value == null;
    public static bool IsEmpty<T>(this T value) => value == null;

    public static bool IsNotEmpty(this string value) => !string.IsNullOrEmpty(value);

    public static bool IsNotEmpty(this int? value) => value != null;

    public static bool IsNotEmpty(this Enum value) => value != null;

    public static bool IsNotEmpty(this DateTime? value) => value != null;

    public static bool IsNotEmpty<T>(this T value) => !IsEmpty(value);

    public static IOrderedQueryable<T> OrderByField<T>(
        this IQueryable<T> source,
        string sortField,
        string order = "asc")
    {
        var lambda = GetOrderByLambda<T>(sortField);
        return order == "asc"
            ? source.OrderBy(lambda)
            : source.OrderByDescending(lambda);
    }

    private static Expression<Func<T, object>> GetOrderByLambda<T>(string sortBy)
    {
        var parameter = Expression.Parameter(typeof(T), "x");
        var member = Expression.PropertyOrField(parameter, sortBy);
        var body = Expression.Convert(member, typeof(object));
        return Expression.Lambda<Func<T, object>>(body, parameter);
    }
}
