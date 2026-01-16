using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BitbucketSystem.Data.Models;

public class Commit
{
    public int Id { get; set; }

    [Required]
    [MaxLength(255)]
    [Column(TypeName = "varchar(255)")]
    public string Message { get; set; } = string.Empty;
    // зранку подивитись червоне шо не бачить
    public DateTime IssueDate { get; set; }

    public int? IssueId { get; set; }
    public Issue? Issue { get; set; }

    public int RepositoryId { get; set; }
    public Repository Repository { get; set; } = null!;

    public int ContributorId { get; set; }
    public User Contributor { get; set; } = null!;

    public List<File> Files { get; set; } = new();
}