using System.Collections;
using System.Reflection;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Http;

namespace SalesOrder.Common.Helpers;

public static class HttpClientExtension
{
    public static async Task<HttpResponseMessage> PostJsonAsync(this HttpClient client, string requestUri, object value)
    {
        var stringContent = new StringContent(JsonSerializer.Serialize(value), Encoding.UTF8, "application/json");
        return await client.PostAsync(requestUri, stringContent);
    }


    public static async Task<HttpResponseMessage> PatchJsonAsync(this HttpClient client, string requestUri, object value)
    {
        var stringContent = new StringContent(JsonSerializer.Serialize(value), Encoding.UTF8, "application/json");
        return await client.PatchAsync(requestUri, stringContent);
    }


    public static async Task<HttpResponseMessage> PutJsonAsync(this HttpClient client, string requestUri, object value)
    {
        var stringContent = new StringContent(JsonSerializer.Serialize(value), Encoding.UTF8, "application/json");
        return await client.PutAsync(requestUri, stringContent);
    }

    public static string ToQueryString(this object obj)
    {
        if (!obj.GetType().IsComplex())
        {
            return obj.ToString();
        }

        var values = obj
            .GetType()
            .GetProperties()
            .Where(o => o.GetValue(obj, null) != null);

        var result = new QueryString();

        foreach (var value in values)
        {
            if (!typeof(string).IsAssignableFrom(value.PropertyType)
                && typeof(IEnumerable).IsAssignableFrom(value.PropertyType))
            {
                var items = value.GetValue(obj) as IList;
                if (items!.Count > 0)
                {
                    result = items.Cast<object>().Aggregate(result, (current, t) => current.Add(value.Name, ToQueryString(t)));
                }
            }
            else if (value.PropertyType.IsComplex())
            {
                result = result.Add(value.Name, ToQueryString(value));
            }
            else
            {
                result = result.Add(value.Name, value.GetValue(obj)?.ToString());
            }
        }

        return result.Value;
    }

    private static bool IsComplex(this Type type)
    {
        var typeInfo = type.GetTypeInfo();
        if (typeInfo.IsGenericType && typeInfo.GetGenericTypeDefinition() == typeof(Nullable<>))
        {
            // nullable type, check if the nested type is simple.
            return IsComplex(typeInfo.GetGenericArguments()[0]);
        }
        return !(typeInfo.IsPrimitive
                 || typeInfo.IsEnum
                 || type.Equals(typeof(Guid))
                 || type.Equals(typeof(string))
                 || type.Equals(typeof(decimal)));
    }
}