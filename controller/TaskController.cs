using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PITON_Project.Controllers{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TaskController : ControllerBase{
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService){
            _taskService = taskService;
        }

        [HttpPost("addtask")]
        public async Task<IActionResult> AddTask([FromBody] TaskRequest taskRequest){
            var response = await _taskService.AddTask(taskRequest);
            if(response == null){
                return BadRequest("Something bad happened, please try again later");
            }
            return Ok(response);
        }

        [HttpGet("gettasks")]
        public async Task<IActionResult> GetTasks([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery][Range(0,2)] TaskDateEnum? period = null, [FromQuery] bool? completed = null){
            var response = await _taskService.GetTasks(pageNumber, pageSize, period, completed);

            return Ok(response);
        }

        [HttpGet("gettask/{id}")]
        public async Task<IActionResult> GetSingleTask([FromRoute] long id){
            try{
                var task = await _taskService.GetTaskByIdAsync(id);
                return Ok(task);
            } catch (Exception e){
                return BadRequest(e.Message);
            }
        }

        [HttpPut("taskupdate/{id}")]
        public async Task<IActionResult> UpdateTask([FromRoute] long id, [FromBody] TaskUpdateRequest taskUpdateRequest){
            try{
                var task = await _taskService.UpdateTaskAsync(id, taskUpdateRequest);
                return Ok(task);
            } catch(Exception e){
                return BadRequest(e.Message);
            }

        }

        [HttpDelete("taskdelete/{id}")]
        public async Task<IActionResult> DeleteTask([FromRoute] long id){
            try{
                await _taskService.DeleteTaskAsync(id);
                return NoContent();
            } catch(Exception e){
                return BadRequest(e.Message);
            }
        }
    }
}