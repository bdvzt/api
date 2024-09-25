using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using WebApplication2.Data.DTO;
using WebApplication2.Data.Entities;
using WebApplication2.Services.IServices;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpPost("CREATE")]
        public async Task<IActionResult> AddTask([FromBody] AddDTO descriptionDTO)
        {
            await _taskService.AddTask(descriptionDTO);
            return Ok();
        }

        [HttpDelete("DELETE")]
        public async Task<IActionResult> DeleteTask([FromBody] DeleteDTO deleteDTO)
        {
            await _taskService.DeleteTask(deleteDTO);
            return Ok();
        }

        [HttpPatch("UPDATE")]
        public async Task<IActionResult> EditTask([FromBody] EditDTO editDTO)
        {
            await _taskService.EditTask(editDTO);
            return Ok();
        }

        [HttpPatch("COMPLETED")]
        public async Task<IActionResult> UpdateTask([FromBody] UpdateDTO updateDTO)
        {
            await _taskService.UpdateTask(updateDTO);
            return Ok();
        }
        
        [HttpGet("GET")]
        public async Task<ActionResult<List<TaskDTO>>> GetAllTasks(){
            var list = await _taskService.GetAllTasks();
            return Ok(list);
        }
        
        [HttpPost("UPLOAD")]
        public async Task<ActionResult> Adding([FromBody] List<LoadDTO> loadDTO)
        {
            await _taskService.DeleteAllTasks();
            if (loadDTO == null || !loadDTO.Any())
            {
                return BadRequest("No tasks provided.");
            }

            foreach (var taskDTO in loadDTO)
            {
                await _taskService.Adding(taskDTO); 
            }

            return Ok();
        }
    }
}