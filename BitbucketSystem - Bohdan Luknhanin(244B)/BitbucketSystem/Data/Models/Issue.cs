using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BitbucketSystem.Data.Models;

public class Issue
{
    public int Id { get; set; }

    [Required]
    [MaxLength(255)]
    [Column(TypeName = "varchar(255)")]
    public string Title { get; set; } = string.Empty;

    [Required]
    [MaxLength(6)]
    [Column(TypeName = "varchar(6)")]
    public string IssueStatus { get; set; } = "open";

    public int RepositoryId { get; set; }
    public Repository Repository { get; set; } = null!;

    public int AssigneeId { get; set; }
    public User Assignee { get; set; } = null!;
}