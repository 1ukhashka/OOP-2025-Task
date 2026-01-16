using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BitbucketSystem.Data.Models;

public class File
{
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    [Column(TypeName = "varchar(100)")]
    public string Name { get; set; } = string.Empty;

    [Column(TypeName = "decimal(15,2)")]
    public decimal Size { get; set; }

    public int? ParentId { get; set; }
    public File? Parent { get; set; }
    
    public int CommitId { get; set; }
    public Commit Commit { get; set; } = null!;
}