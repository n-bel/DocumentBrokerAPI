using DocumentBroker.Domain;
using DocumentBroker.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace DocumentBroker.Application;

public class DocumentService(DocumentDbContext context) : IDocumentService
{
    public async Task<Document?> GetDocumentByIdAsync(int id)
    {
        return await context.Documents.FirstOrDefaultAsync(d=>d.Id==id);
    }
    
    public async Task<IEnumerable<Document>> GetDocumentAsync(int? id, string? brokerId, string? title,
        int page, int pageSize)
    {
        var query = context.Documents.AsQueryable();

        if (id.HasValue)
            query = query.Where(d => d.Id == id);

        if (!string.IsNullOrWhiteSpace(brokerId))
            query = query.Where(d => d.BrokerId == brokerId);

        if (!string.IsNullOrWhiteSpace(title))
            query = query.Where(d => d.Title == title);
        
        // Pagination
        return await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task AddDocumentAsync(string brokerId, string title, string description, string filePath)
    {
        var date = DateTime.UtcNow.Date;
        var document = new Document
        {
            BrokerId = brokerId,
            Title = title,
            Description = description,
            FilePath = filePath,
            CreatedAt = date,
            UpdatedAt = date
        };
        context.Documents.Add(document);
        await context.SaveChangesAsync();
    }
}