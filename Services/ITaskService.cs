using HMCTSTaskManager.API.DTOs;
using HMCTSTaskManager.API.Models;

namespace HMCTSTaskManager.API.Services
{
    public interface ITaskService
    {
        Task<List<TaskItem>> GetAllTasksAsync();
        Task<TaskItem?> GetTaskByIdAsync(int id);
        Task<TaskItem> CreateTaskAsync(CreateTaskDto createTaskDTo);
        Task<bool> UpdateTaskStatusAsync(int id, UpdateTaskStatusDto updateTaskStatusDto);
        Task<bool> DeleteTaskAsync(int id);
    }
}
