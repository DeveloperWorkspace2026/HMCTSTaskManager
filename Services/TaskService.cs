
using HMCTSTaskManager.API.DTOs;
using HMCTSTaskManager.API.Models;
using HMCTSTaskManager.API.Repositories;

namespace HMCTSTaskManager.API.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;

        public TaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public  async Task<TaskItem> CreateTaskAsync(CreateTaskDto createTaskDTo)
        {
               var task = new TaskItem
                {
                    Title = createTaskDTo.Title,
                    Description = createTaskDTo.Description,
                    DueDate = createTaskDTo.DueDate,
                    Status = "Pending",
                    CreateDate = DateTime.UtcNow
                };

            return await _taskRepository.CreateAsync(task); 
        }
              

        public async Task<List<TaskItem>> GetAllTaskItems()
        {            
            return await _taskRepository.GetAllAsync();          
            
        }

        public async Task<List<TaskItem>> GetAllTasksAsync()
        {
            return await _taskRepository.GetAllAsync();
            
        }

        public async Task<TaskItem?> GetTaskByIdAsync(int id)
        {
            return await _taskRepository.GetByIdAsync(id);            
        }

        public async Task<bool> UpdateTaskStatusAsync(int id, UpdateTaskStatusDto updateTaskStatusDto)
        {
            var task = await _taskRepository.GetByIdAsync(id);

            if (task == null)
            {
                return false;
            }

            task.Status = updateTaskStatusDto.Status;

            await _taskRepository.UpdateAsync(task);

            return true;           

        }     

        public async Task<bool> DeleteTaskAsync(int id)
        {
            var task = await _taskRepository.GetByIdAsync(id);

            if (task == null)
            {
                return false;
            }

            await _taskRepository.DeleteAsync(task);

            return true;                   
            
        }
    }
}
