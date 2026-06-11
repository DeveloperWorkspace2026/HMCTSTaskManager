using HMCTSTaskManager.API.Data;
using HMCTSTaskManager.API.Models;
using Microsoft.EntityFrameworkCore;

namespace HMCTSTaskManager.API.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly ApplicationDbContext _context;

        public TaskRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<TaskItem> CreateAsync(TaskItem task)
        {
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
            return task;
        }

        public async Task<List<TaskItem>> GetAllAsync()
        {
            return await _context.Tasks.OrderBy(t => t.DueDate).ToListAsync();
        }                    

        public async Task<TaskItem?> GetByIdAsync(int id)
        {
            return await _context.Tasks.FindAsync(id);
        }

        public async Task UpdateAsync(TaskItem task)
        {
            _context.Tasks.Update(task);
            await _context.SaveChangesAsync();

        }

        public async Task DeleteAsync(TaskItem task)
        {
            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();

        }
        
    }
}
