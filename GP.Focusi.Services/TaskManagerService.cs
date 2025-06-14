using AutoMapper;
using GP.Focusi.Core.DTOs;
using GP.Focusi.Core.DTOs.Auth;
using GP.Focusi.Core.Entites;
using GP.Focusi.Core.RepositoriesContract;
using GP.Focusi.Core.ServicesContract;
using GP.Focusi.Repository.Repositories;
using sib_api_v3_sdk.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.Focusi.Services
{
	public class TaskManagerService : ITaskManagerService
	{
		private readonly ITaskManagerRepository _taskRepository;
		private readonly IMapper _mapper;

		public TaskManagerService(ITaskManagerRepository taskManagerRepository, IMapper mapper)
		{
			_taskRepository = taskManagerRepository;
			_mapper = mapper;
		}
		public async Task<List<object>> GetTaskManager(string Email)
		{
			try
			{
				if (Email is null) return null;

				List<object> res = await _taskRepository.GetAllTasksAsync(Email);

				if (res is null) return null;

				return res;
			}
			catch (Exception ex)
			{
			  ErrorModel error = new ErrorModel(message:ex.Message);
			}
			return null;
		}
		public async Task<TaskManagerItemsDto> GetTask(int? id)
		{
			if(id is null) return null ;

			var task = await _taskRepository.GetTaskAsync(id);
			
			if (task is null) return null;

			return _mapper.Map<TaskManagerItemsDto>(task);
		}
		public async Task<TaskManagerItemsDto> CreateTask(string email, AddTaskManagerItemsDto taskItemsDto)
		{
			try
			{
				if (taskItemsDto is null || email is null) return null;

				var task = _mapper.Map<TaskManagerItems>(taskItemsDto);
				task.ChildEmail = email;
				
				return await _taskRepository.CreateTaskAsync(task);

			}
			catch (Exception ex)
			{
				ErrorModel error = new ErrorModel(message: ex.Message);
			}
			return null;
		}
		public async Task<TaskManagerItemsDto> UpdateTask(string email, TaskManagerItemsDto taskDto)
		{
			try
			{
				if ( taskDto is null) return null;

				if (email is null)
					return null;

				var Task = new TaskManagerItems()
				{
					ID = taskDto.Id,
					Name = taskDto.Name,
					date = taskDto.date,
					IsCompleted = taskDto.IsCompleted,
					IsDateAndTimeEnded = taskDto.IsDateAndTimeEnded,
					
				};
				var res = await _taskRepository.UpdateTaskAsync(Task);

				if (res < 1)
					return null;

				return taskDto;
				//if(taskDto.IsCompleted == true)
				//{
				//	await _taskRepository.DeleteTaskAsync(taskDto.Id);
				//}
				//
				//var result = await _taskRepository.CreateTaskAsync(T);

				//if (result < 1) return null;

				//return taskDto;
			}
			catch (Exception ex) 
			{
				ErrorModel error = new ErrorModel(message: ex.Message);
			}
			return null;
		}
		public async Task<int> DeletTask(int? id)
		{
			if (id is null) return 0;

			 return	await _taskRepository.DeleteTaskAsync(id);
		}

	}
}
