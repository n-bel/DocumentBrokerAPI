using System.Text.Json;
using DocumentBroker.Application;
using DocumentBroker.Domain;
using DocumentBroker.Presentation.DTO;
using Microsoft.AspNetCore.Mvc;

namespace DocumentBroker.Presentation.Controller;

[Route("api/document")]
[ApiController]
public class DocumentController(IDocumentService service) : ControllerBase
{
    [HttpGet("id")]
    public async Task<Document?> GetDocumentById(int id)
    {
        return await service.GetDocumentByIdAsync(id);
    }

    [HttpGet("search")]
    public async Task<IEnumerable<Document>> GetDocument([FromQuery]int? id, [FromQuery]string? brokerId, [FromQuery]string? title)
    {
        return await service.GetDocumentAsync(id, brokerId, title);
    }
    
    [HttpPost("add")]
    public async Task SetDocument([FromForm] string documentJson, [FromForm] IFormFile file)
    {
        var request = JsonSerializer.Deserialize<DocumentDTO>(documentJson);
        
        var filePath = Path.Combine("/app/Uploads", file.FileName);
        await using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }
        
        await service.AddDocumentAsync(request.BrokerId, request.Title,request.Description, filePath);
    }
}