namespace SalesOrder.Common.Models;

public class PaginationQuery : Query
{
    public int PageSize { get; set; } = 10;
    public int Page { get; set; } = 1;
}