using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BitbucketSystem.Data.Models;

public class User
{
    public int Id { get; set; }

    [Required]
    [MaxLength(30)]
    [Column(TypeName = "varchar(30)")]
    public string Username { get; set; } = string.Empty;

    [Required]
    [MaxLength(30)]
    [Column(TypeName = "varchar(30)")]
    public string Password { get; set; } = string.Empty;

    [Required]
    [MaxLength(50)]
    [Column(TypeName = "varchar(50)")]
    public string Email { get; set; } = string.Empty;

    // Зв'язки
    public List<Repository> Repositories { get; set; } = new();
    public List<Issue> Issues { get; set; } = new();
    public List<Commit> Commits { get; set; } = new();
}