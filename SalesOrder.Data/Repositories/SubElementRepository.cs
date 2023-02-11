using Microsoft.EntityFrameworkCore;
using SalesOrder.Common.Exceptions;
using SalesOrder.Common.Helpers;
using SalesOrder.Common.Models;
using SalesOrder.Data.Models;

namespace SalesOrder.Data.Repositories;

public interface ISubElementRepository
{
    Task<PaginatedList<SubElement>> GetSubElements(PaginationQuery queryParams);
    Task<SubElement> GetSubElement(int id);
    Task AddSubElement(SubElement subElement);
    Task UpdateSubElement(SubElement subElement);
    Task DeleteSubElement(int id);
}

public class SubElementRepository : ISubElementRepository
{
    private readonly ApplicationDbContext _context;

    public SubElementRepository(ApplicationDbContext context)
    {
        _context = context;
    }


    public async Task<PaginatedList<SubElement>> GetSubElements(PaginationQuery queryParams)
    {
        var query = _context.SubElements.AsQueryable();

        if (queryParams.SearchTerm.IsNotEmpty())
        {
            var searchTerm = queryParams.SearchTerm.ToLower();
            query = query.Where(o => 
                o.Element.ToString().Contains(searchTerm) ||
                o.Type.ToLower().Contains(searchTerm)
            );
        }

        var count = await query.CountAsync();
            
        var list = await query
            .OrderByField(queryParams.SortField, queryParams.SortOrder)
            .Skip(queryParams.PageSize * (queryParams.Page - 1))
            .Take(queryParams.PageSize)
            .ToListAsync();
        
        return new PaginatedList<SubElement> { Count = count, Items = list };
    }

    public async Task<SubElement> GetSubElement(int id) => await _context.SubElements.FindAsync(id);

    public async Task AddSubElement(SubElement subElement)
    {
        _context.SubElements.Add(subElement);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateSubElement(SubElement subElement)
    {
        subElement.UpdatedAt = DateTime.Now;
        _context.Entry(subElement).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteSubElement(int id)
    {
        var subElement = await _context.SubElements.FindAsync(id);
        if (subElement == null) throw new SubElementNotFoundException();
        _context.SubElements.Remove(subElement);
        await _context.SaveChangesAsync();
    }
}