using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DocumentBroker.Domain;

public class Document
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Auto-increment
    public int Id{get;set;}
    public required string BrokerId{get;set;}
    public string Title{get;set;}
    public string Description{get;set;}
    public string FilePath{get;set;}
    public DateTime CreatedAt{get;set;}
    public DateTime UpdatedAt{get;set;}
}