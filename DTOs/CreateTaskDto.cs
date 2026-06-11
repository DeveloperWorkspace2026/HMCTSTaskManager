using System.ComponentModel.DataAnnotations;

namespace HMCTSTaskManager.API.DTOs;

public class CreateTaskDto
{
    [Required(ErrorMessage = "Title is required.")]
    [MaxLength(200,ErrorMessage = "Title cannot exceed 200 characters.")]
    public string Title { get; set; }

    [MaxLength(1000, ErrorMessage = "Description cannot exceed 1000 characters.")]
    public string? Description { get; set; }

    [Required(ErrorMessage = "Due date is required.")]
    public DateTime DueDate { get; set; }
}
