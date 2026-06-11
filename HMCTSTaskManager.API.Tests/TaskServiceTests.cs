using FluentAssertions;
using HMCTSTaskManager.API.DTOs;
using HMCTSTaskManager.API.Models;
using HMCTSTaskManager.API.Repositories;
using HMCTSTaskManager.API.Services;
using Moq;

namespace HMCTSTaskManager.API.Tests;

public class TaskServiceTests
{
    private readonly Mock<ITaskRepository> _mockRepository;
    private readonly TaskService _taskService;

    public TaskServiceTests()
    {
        _mockRepository = new Mock<ITaskRepository>();
        _taskService = new TaskService(_mockRepository.Object);
    }
    
    
    //Get all tasks
    [Fact(DisplayName ="Get all tasks")]
    public async Task GetAllTasksAsync_ShouldReturnAllTasks()
    {
        var tasks = new List<TaskItem>
        {
            new TaskItem
            {
                Id =1,
                Title = "Review case",
                Description = "Check case evidence",
                Status =  "Pending",
                DueDate = DateTime.UtcNow.AddDays(1)
            },
            new TaskItem
            {
                Id = 2,
                Title = "Prepare hearing documents",
                Description = "Upload documnts",
                Status = "In Progress",
                DueDate = DateTime.UtcNow.AddDays(2)
            }
        };

        _mockRepository.Setup(Repositories => Repositories.GetAllAsync()).ReturnsAsync(tasks);

        var result = await _taskService.GetAllTasksAsync();

        result.Should().HaveCount(2);
        result[0].Title.Should().Be("Review case");
    }

    //Get task by id
    [Fact(DisplayName = "Get task by id")]
    public async Task GetTaskByIdAsync_WhenTaskExists_ShouldReturnTask()
    {
        var task = new TaskItem
        {
            Id = 1,
            Title = "Review benefit appeal case",
            Description = "Check submitted evidence",
            Status = "Pending",
            DueDate = DateTime.UtcNow.AddDays(1)
        };

        _mockRepository
            .Setup(repo => repo.GetByIdAsync(1))
            .ReturnsAsync(task);

        var result = await _taskService.GetTaskByIdAsync(1);

        result.Should().NotBeNull();
        result!.Id.Should().Be(1);
        result.Title.Should().Be("Review benefit appeal case");
    }

    //Return null when task not found
    [Fact(DisplayName = "Task not found returns null")]
    public async Task GetTaskByIdAsync_WhenTaskDoesNotExist_ShouldReturnNull()
    {
        _mockRepository
            .Setup(repo => repo.GetByIdAsync(99))
            .ReturnsAsync((TaskItem?)null);

        var result = await _taskService.GetTaskByIdAsync(99);

        result.Should().BeNull();
    }

    //Create task with default status
    [Fact(DisplayName = "Create task")]
    public async Task CreateTaskAsync_ShouldCreateTaskWithPendingStatus()
    {
        var dto = new CreateTaskDto
        {
            Title = "Create caseworker task",
            Description = "Create task from frontend",
            DueDate = DateTime.UtcNow.AddDays(1)
        };

        _mockRepository
            .Setup(repo => repo.CreateAsync(It.IsAny<TaskItem>()))
            .ReturnsAsync((TaskItem task) =>
            {
                task.Id = 1;
                return task;
            });

        var result = await _taskService.CreateTaskAsync(dto);

        result.Id.Should().Be(1);
        result.Title.Should().Be(dto.Title);
        result.Status.Should().Be("Pending");

        _mockRepository.Verify(
            repo => repo.CreateAsync(It.IsAny<TaskItem>()),
            Times.Once);
    }

    //Update when task not found
    [Fact(DisplayName = "Update task status")]
    public async Task UpdateTaskStatusAsync_WhenTaskExists_ShouldReturnTrue()
    {
        var task = new TaskItem
        {
            Id = 1,
            Title = "Review case",
            Status = "Pending",
            DueDate = DateTime.UtcNow.AddDays(1)
        };

        var dto = new UpdateTaskStatusDto
        {
            Status = "Completed"
        };

        _mockRepository
            .Setup(repo => repo.GetByIdAsync(1))
            .ReturnsAsync(task);

        var result = await _taskService.UpdateTaskStatusAsync(1, dto);

        result.Should().BeTrue();
        task.Status.Should().Be("Completed");

        _mockRepository.Verify(
            repo => repo.UpdateAsync(task),
            Times.Once);
    }

    //Fail when task not found
    [Fact(DisplayName = "Update fails when task not found")]
    public async Task UpdateTaskStatusAsync_WhenTaskDoesNotExist_ShouldReturnFalse()
    {
        var dto = new UpdateTaskStatusDto
        {
            Status = "Completed"
        };

        _mockRepository
            .Setup(repo => repo.GetByIdAsync(99))
            .ReturnsAsync((TaskItem?)null);

        var result = await _taskService.UpdateTaskStatusAsync(99, dto);

        result.Should().BeFalse();

        _mockRepository.Verify(
            repo => repo.UpdateAsync(It.IsAny<TaskItem>()),
            Times.Never);
    }

    //Delete existing task
    [Fact(DisplayName = "Delete task")]
    public async Task DeleteTaskAsync_WhenTaskExists_ShouldReturnTrue()
    {
        var task = new TaskItem
        {
            Id = 1,
            Title = "Delete old task",
            Status = "Pending",
            DueDate = DateTime.UtcNow.AddDays(1)
        };

        _mockRepository
            .Setup(repo => repo.GetByIdAsync(1))
            .ReturnsAsync(task);

        var result = await _taskService.DeleteTaskAsync(1);

        result.Should().BeTrue();

        _mockRepository.Verify(
            repo => repo.DeleteAsync(task),
            Times.Once);
    }

    //Fail delete when task not found
    [Fact(DisplayName = "Delete fails when task not found")]
    public async Task DeleteTaskAsync_WhenTaskDoesNotExist_ShouldReturnFalse()
    {
        _mockRepository
            .Setup(repo => repo.GetByIdAsync(99))
            .ReturnsAsync((TaskItem?)null);

        var result = await _taskService.DeleteTaskAsync(99);

        result.Should().BeFalse();

        _mockRepository.Verify(
            repo => repo.DeleteAsync(It.IsAny<TaskItem>()),
            Times.Never);
    }
}
