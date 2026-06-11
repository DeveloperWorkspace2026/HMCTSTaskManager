using System.ComponentModel.DataAnnotations;

namespace HMCTSTaskManager.API.Models;

public class TaskItem
{
    public int Id { get; set; }

    [Required]
    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    public string? Description { get; set; }

    [Required]
    public string Status { get; set; } = "pending";

    [Required]
    public DateTime DueDate { get; set; }

    public DateTime CreateDate { get; set; } = DateTime.UtcNow;
}
