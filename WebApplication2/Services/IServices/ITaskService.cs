using WebApplication2.Data.DTO;

namespace WebApplication2.Services.IServices;

public interface ITaskService

{
    public Task AddTask(AddDTO descriptionDTO);
    public Task DeleteTask(DeleteDTO descriptionDTO);
    public Task EditTask(EditDTO descriptionDTO);
    public Task UpdateTask(UpdateDTO descriptionDTO);
    public Task<List<TaskDTO>> GetAllTasks();
    public Task Adding(LoadDTO descriptionDTO);
    public Task DeleteAllTasks();
}