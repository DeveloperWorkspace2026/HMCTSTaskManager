using System.ComponentModel.DataAnnotations;

namespace HMCTSTaskManager.API.DTOs
{
    public class UpdateTaskStatusDto
    {
        [Required(ErrorMessage = "Status is required.")]
        [RegularExpression("Pending|In Progress|Completed|Cancelled", ErrorMessage = "Status must be Pending,In Progress,Completed or Cancelled.")]
        public string Status { get; set; } = string.Empty;
    }
}
