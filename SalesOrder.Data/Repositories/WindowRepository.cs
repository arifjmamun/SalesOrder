using Microsoft.EntityFrameworkCore;
using SalesOrder.Common.Exceptions;
using SalesOrder.Common.Helpers;
using SalesOrder.Common.Models;
using SalesOrder.Data.Models;

namespace SalesOrder.Data.Repositories;

public interface IWindowRepository
{
    Task<PaginatedList<Window>> GetWindows(PaginationQuery queryParams);
    Task<Window> GetWindow(int id);
    Task AddWindow(Window window);
    Task UpdateWindow(Window window);
    Task DeleteWindow(int id);
}

public class WindowRepository : IWindowRepository
{
    private readonly ApplicationDbContext _context;

    public WindowRepository(ApplicationDbContext context)
    {
        _context = context;
    }


    public async Task<PaginatedList<Window>> GetWindows(PaginationQuery queryParams)
    {
        var query = _context.Windows.AsQueryable();

        if (queryParams.SearchTerm.IsNotEmpty())
        {
            var searchTerm = queryParams.SearchTerm.ToLower();
            query = query.Where(o => o.Name.ToLower().Contains(searchTerm));
        }

        var count = await query.CountAsync();
            
        var list = await query
            .OrderByField(queryParams.SortField, queryParams.SortOrder)
            .Skip(queryParams.PageSize * (queryParams.Page - 1))
            .Take(queryParams.PageSize)
            .ToListAsync();
        
        return new PaginatedList<Window> { Count = count, Items = list };
    }

    public async Task<Window> GetWindow(int id) => await _context.Windows.FindAsync(id);

    public async Task AddWindow(Window window)
    {
        _context.Windows.Add(window);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateWindow(Window window)
    {
        window.UpdatedAt = DateTime.Now;
        _context.Entry(window).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteWindow(int id)
    {
        var window = await _context.Windows.FindAsync(id);
        if (window == null) throw new WindowNotFoundException();
        _context.Windows.Remove(window);
        await _context.SaveChangesAsync();
    }
}