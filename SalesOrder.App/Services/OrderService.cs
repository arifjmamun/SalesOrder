using System.Net.Http.Json;
using SalesOrder.Common.DTO.Order;
using SalesOrder.Common.Models;
using SalesOrder.Common.Helpers;


namespace SalesOrder.App.Services;

public interface IOrderService
{
    Task<OrderDto> GetOrderAsync(int orderId);
    Task<PaginatedList<OrderDto>> GetOrders(PaginationQuery paginationQuery);
}

public class OrderService : IOrderService
{
    private const string OrdersUrl = "Orders";
    private readonly HttpClient _httpClient;

    public OrderService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    // public OrderService(IHttpClientFactory clientFactory, IConfiguration configuration)
    // {
    //     _httpClient = clientFactory.CreateClient("Orders");
    //     _httpClient.BaseAddress = new Uri($"{configuration.GetValue<string>("ApiBaseAddress")}");
    // }

    public async Task<OrderDto> GetOrderAsync(int orderId)
    {
        var response = await _httpClient.GetJsonAsync($"{OrdersUrl}/{orderId}");
        response.EnsureSuccessStatusCode();
        
        var result = await response.Content.ReadFromJsonAsync<ApiResponse<OrderDto>>();
        return result.Result;
    }

    public async Task<PaginatedList<OrderDto>> GetOrders(PaginationQuery paginationQuery)
    {
        var response = await _httpClient.GetJsonAsync(OrdersUrl, paginationQuery);
        response.EnsureSuccessStatusCode();
        
        var result = await response.Content.ReadFromJsonAsync<ApiPaginatedResponse<OrderDto>>();
        return result.Result;
    }
}