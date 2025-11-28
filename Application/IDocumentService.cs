using DocumentBroker.Domain;

namespace DocumentBroker.Application;

public interface IDocumentService
{
    Task<Document?> GetDocumentByIdAsync(int id);
    Task<IEnumerable<Document>> GetDocumentAsync(int? id, string? brokerId, string? title,
        int page=1, int pageSize=10);
    Task AddDocumentAsync(string brokerId, string title, 
        string description, string filePath);
}