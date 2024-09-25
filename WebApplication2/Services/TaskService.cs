using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;
using WebApplication2.Data.DTO;
using WebApplication2.Data.Entities;
using WebApplication2.Services.IServices;

namespace WebApplication2.Services;

public class TaskService : ITaskService
{
    private readonly AppDBContext _dbcontext;

    public TaskService(AppDBContext dbcontext)
    {
        _dbcontext = dbcontext;
    }

    public async Task AddTask(AddDTO descriptionDTO)
    {
        if (descriptionDTO == null || descriptionDTO.Description == "")
        {
            throw new BadHttpRequestException("Empty description");
        }

        Doing task = new Doing()
        {
            Description = descriptionDTO.Description, 
            Status = false
        };
        
        _dbcontext.Tasks.Add(task);
        await _dbcontext.SaveChangesAsync();
    }

    public async Task DeleteTask(DeleteDTO descriptionDTO)
    {
        var task = await _dbcontext.Tasks.FirstOrDefaultAsync(i => i.Id == descriptionDTO.Id);
        if (task == null)
        {
            throw new BadHttpRequestException("Task not found");
        }

        _dbcontext.Tasks.Remove(task);
        await _dbcontext.SaveChangesAsync();
    }

    public async Task EditTask(EditDTO descriptionDTO)
    {
        if (descriptionDTO.Description == "")
        {
            throw new BadHttpRequestException("Empty description");
        }
        var task = await _dbcontext.Tasks.FirstOrDefaultAsync(i => i.Id == descriptionDTO.Id);
        if (task == null)
        {
            throw new BadHttpRequestException("Task not found");
        }
        task.Description = descriptionDTO.Description;
        
        await _dbcontext.SaveChangesAsync();
    }

    public async Task UpdateTask(UpdateDTO descriptionDTO)
    {
        if (descriptionDTO == null)
        {
            throw new BadHttpRequestException("Empty description");
        }
        var task = await _dbcontext.Tasks.FirstOrDefaultAsync(i => i.Id == descriptionDTO.Id);
        task.Status = descriptionDTO.Status;
        
        await _dbcontext.SaveChangesAsync();
    }

    public async Task <List<TaskDTO>> GetAllTasks()
    {
        var tasks = await _dbcontext.Tasks.ToListAsync();
        var allTasks = tasks.Select(i => new TaskDTO
        {
            Id = i.Id,
            Description = i.Description,
            Status = i.Status
        }).ToList();
        return allTasks;
    }
    
    public async Task DeleteAllTasks()
    {
        var allTasks = await _dbcontext.Tasks.ToListAsync(); 
        _dbcontext.Tasks.RemoveRange(allTasks); 
        await _dbcontext.SaveChangesAsync(); 
    }

    public async Task Adding(LoadDTO loadDTO)
    {
        if (loadDTO == null || string.IsNullOrWhiteSpace(loadDTO.Description))
        {
            throw new BadHttpRequestException("Empty description");
        }
    
        Doing task = new Doing()
        {
            Description = loadDTO.Description, 
            Status = loadDTO.Completed
        };
    
        _dbcontext.Tasks.Add(task);
        await _dbcontext.SaveChangesAsync();
    }
}