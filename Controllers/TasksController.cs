using Microsoft.AspNetCore.Mvc;
using HMCTSTaskManager.API.DTOs;
using HMCTSTaskManager.API.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace HMCTSTaskManager.API.Controllers
{
    [ApiController]
    [Route("api/tasks")]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        //Create a new tasks
        [HttpPost]
        [SwaggerOperation(Summary = "Create Task")]
        [SwaggerResponse(201,"Task created successfully.")]       
        public async Task<IActionResult> CreateTask(CreateTaskDto createTaskDto)
        {
            var task = await _taskService.CreateTaskAsync(createTaskDto);

            return CreatedAtAction(nameof(GetTaskById), new { id = task.Id }, task);
           
        }

        //Get all tasks
        [HttpGet]
        [SwaggerOperation(Summary = "View All Tasks")]
        public async Task<IActionResult> GetAllTasks()
        {
            return Ok(await _taskService.GetAllTasksAsync());
        }

        //Get task by id
        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "View Task By Id")]
        public async Task<IActionResult> GetTaskById(int id)
        {
            var task = await _taskService.GetTaskByIdAsync(id);

            if (task == null)
            {
                return NotFound();

            }
            return Ok(task);
        }

        //Update task status
        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Update Task")]
        [SwaggerResponse(201, "Task updated successfully.")]
        //[SwaggerResponse(400, "Invalid request.")]
        public async Task<IActionResult> UpdateTaskStatus(int id, [FromBody]UpdateTaskStatusDto dto)
        {
            var updated = await _taskService.UpdateTaskStatusAsync(id, dto);

            if (!updated)
            {
                return NotFound();
            }

            return NoContent();
        }

        //Delete task
        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Delete Task")]
        [SwaggerResponse(201, "Task deleted successfully.")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var deleted = await _taskService.DeleteTaskAsync(id);

            if (!deleted)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
