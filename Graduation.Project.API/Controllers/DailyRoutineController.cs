using AutoMapper;
using GP.Focusi.API.Attributes;
using GP.Focusi.APIs.Errors;
using GP.Focusi.Core.DTOs;
using GP.Focusi.Core.DTOs.Auth;
using GP.Focusi.Core.Entites;
using GP.Focusi.Core.RepositoriesContract;
using GP.Focusi.Core.ServicesContract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
		public async Task<ActionResult<object>> GetTaskManager()
		{
			var UserEmail = User.FindFirstValue(ClaimTypes.Email);

			if (UserEmail is null)
				return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));

			var Result = await _taskManagerService.GetTaskManager(UserEmail);

			if(Result is null)
				return Ok("You Don't Have any Tasks Now");

			return Ok(Result);
		}

		[HttpPost("addtask")]
		public async Task<ActionResult<TaskManagerItemsDto>>AddTask(AddTaskManagerItemsDto itemDto)
		{
			var UserEmail = User.FindFirstValue(ClaimTypes.Email);

			var item = await _taskManagerService.CreateTask(UserEmail, itemDto);

			if (item is null) 
				return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest, "Invalid Operation!!"));

			return Ok(item);
		}

		[HttpPut("updatetask")]
		public async Task<ActionResult<TaskManagerItemsDto>> EditeTask([FromQuery]int id, [FromBody]AddTaskManagerItemsDto aItemDto)
		{
			var UserEmail = User.FindFirstValue(ClaimTypes.Email);

			if (aItemDto is null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));
            if (id == 0) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));

			var itemDto = new TaskManagerItemsDto
			{
				Id = id,
				date = aItemDto.date,
				Name = aItemDto.Name,
				IsCompleted = aItemDto.IsCompleted,
				IsDateAndTimeEnded = aItemDto.IsDateAndTimeEnded
			};

            if (itemDto.IsCompleted)
			{
				await _taskManagerService.UpdateTask(UserEmail, itemDto);
				await _taskManagerService.DeletTask(itemDto.Id);
				return Ok("You Have Finshed this Task Keep going :)");
			}
			var item = await _taskManagerService.UpdateTask(UserEmail,itemDto);

			if (item is null) 
				return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest, "Invalid Operation!!"));

			return Ok(item);
		}

        [HttpDelete("delete")]
		 public async Task <IActionResult> DeleteTask(int id)
		{
			 var res = await _taskManagerService.DeletTask(id);

			if (res <= 0) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));

			return Ok("The Task Deleted Successfully");
		}
	}
}
