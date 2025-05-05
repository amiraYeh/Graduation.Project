using AutoMapper;
using GP.Focusi.API.Attributes;
using GP.Focusi.APIs.Errors;
using GP.Focusi.Core.DTOs;
using GP.Focusi.Core.Entites;
using GP.Focusi.Core.RepositoriesContract;
using GP.Focusi.Core.ServicesContract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GP.Focusi.API.Controllers
{
	
	[Authorize]
	public class DailyRoutineController : BaseAppController
	{
		private readonly ITaskManagerService _taskManagerService;

		public DailyRoutineController(ITaskManagerService taskManagerService)
        {
			_taskManagerService = taskManagerService;
		}
		
		[HttpGet("taskManager")]
		public async Task<ActionResult<object>> GetTaskManager(string UserEmail)
		{
			if (UserEmail is null)
				return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));

			var Result = await _taskManagerService.GetTaskManager(UserEmail);

			if(Result is null)
				return Ok("You Don't Have any Tasks Now");

			return Ok(Result);
		}

		[HttpPost("addtask{UserEmail}")]
		public async Task<ActionResult<TaskManagerItemsDto>>AddTask(string UserEmail,TaskManagerItemsDto itemDto)
		{
			
			var item = await _taskManagerService.CreateTask(UserEmail, itemDto);

			if (item is null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest, "Invalid Operation!!"));

			return Ok(item);
		}

		[HttpPut("updatetask{UserEmail}")]
		public async Task<ActionResult<TaskManagerItemsDto>> EditeTask(string UserEmail, TaskManagerItemsDto itemDto)
		{
			if (itemDto is null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));

			if (itemDto.IsCompleted)
			{
				await _taskManagerService.DeletTask(itemDto.Name);
				return Ok("You Have Finshed this Task Keep going :)");
			}
			var item = await _taskManagerService.UpdateTask(UserEmail,itemDto);

			if (item is null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest, "Invalid Operation!!"));

			return Ok(item);
		}

        [HttpDelete("delete")]
		 public async Task <IActionResult>  DeleteTask(string name)
		{
			 var res = await _taskManagerService.DeletTask(name);

			if (res <= 0) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));

			return Ok();
		}
	}
}
