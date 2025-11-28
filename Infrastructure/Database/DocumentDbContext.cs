using DocumentBroker.Domain;
using Microsoft.EntityFrameworkCore;

namespace DocumentBroker.Infrastructure.Database;

public class DocumentDbContext(DbContextOptions<DocumentDbContext> options) : DbContext(options)
{
    public DbSet<Document> Documents { get; set; }
}