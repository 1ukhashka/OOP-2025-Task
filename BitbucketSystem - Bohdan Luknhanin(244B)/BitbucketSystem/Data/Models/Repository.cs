using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BitbucketSystem.Data.Models;

public class Repository
{
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    [Column(TypeName = "varchar(50)")]
    public string Name { get; set; } = string.Empty;

    // Зв'язки
    public List<User> Contributors { get; set; } = new();
    public List<Issue> Issues { get; set; } = new();
    public List<Commit> Commits { get; set; } = new();
}