

//namespace HMCTSTaskManager.API.Models;

using Microsoft.EntityFrameworkCore;
using HMCTSTaskManager.API.Models;

namespace HMCTSTaskManager.API.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
    public DbSet<TaskItem> Tasks => Set<TaskItem>();
    //protected ApplicationDbContext()
    //{
    //}
}
