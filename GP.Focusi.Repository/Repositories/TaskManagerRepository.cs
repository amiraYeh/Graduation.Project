using GP.Focusi.Core.DTOs.Auth;
using GP.Focusi.Core.Entites;
using GP.Focusi.Core.Entites.Identity;
using GP.Focusi.Core.RepositoriesContract;
using GP.Focusi.Repository.Data.Contexts;
using GP.Focusi.Repository.Identity.Contexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using sib_api_v3_sdk.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.Focusi.Repository.Repositories
{
	public class TaskManagerRepository : ITaskManagerRepository
	{
		private readonly FocusiAppDbContext _context;
		private readonly UserManager<AppUserChild> _userManager;

		public TaskManagerRepository(FocusiAppDbContext context, UserManager<AppUserChild> userManager)
		{
			_context = context;
			_userManager = userManager;
		}

		public async Task<List<object>> GetAllTasksAsync(string email)
		{
			var taskManager = await _context.TaskManagers.FirstOrDefaultAsync(T=>T.ChildMail == email);

			if (taskManager is null) return null;

			var result = _context.TaskManagerItems.OrderBy(T => T.ID).GroupBy(T => T.ChildEmail);
			List<object> tasks = new List<object>();
			foreach (var TManager in result)
			{
				if (TManager.Key == email)
				{
					foreach (var item in TManager)
					{
						tasks.Add(new { Name = item.Name, Time = item.date.ToShortTimeString(), Done = item.IsCompleted });
					}
				}
			}
			return tasks;
		}

		public async Task<TaskManagerItems> GetTaskAsync(int? id)
		{
			return await _context.TaskManagerItems.FindAsync(id);
		}

		public async Task<TaskManagerItemsDto> CreateTaskAsync(TaskManagerItems taskItem)
		{
			List<TaskManagerItems> x = new List<TaskManagerItems>();
			

			var taskManager = await _context.TaskManagers.FirstOrDefaultAsync(T => T.ChildMail == taskItem.ChildEmail);

			if (taskManager is not null)
			{
				x.Add( taskItem);
				
				if (taskManager.Items is not null)
					taskManager.Items.Add(taskItem);
				else
					taskManager.Items = x;

				_context.TaskManagers.Update(taskManager);
				var count = taskManager.ItemsCount;

				taskManager.ItemsCount = count + 1;
			}
			else
			{
				var newList = new TaskManager($"{taskItem.ID}_{taskItem.Name}");
				taskItem.TaskManager = newList;
				newList.ChildMail = taskItem.ChildEmail;
				x.Add(taskItem);
				newList.Items = x;
				await _context.TaskManagers.AddAsync(newList);
				newList.ItemsCount = 1;
			}

			await _context.TaskManagerItems.AddAsync(taskItem);
			 var res = await _context.SaveChangesAsync();
			if (res < 1)
				return null;

			return new TaskManagerItemsDto()
			{
				Id = taskItem.ID,
				date = taskItem.date,
				IsCompleted = taskItem.IsCompleted,
				IsDateAndTimeEnded = taskItem.IsDateAndTimeEnded,
				Name = taskItem.Name
			};
		}

		public async Task<int> DeleteTaskAsync(int? id)
		{
			var task = await _context.TaskManagerItems.FindAsync(id);

			if (task is null) return 0;

			var taskManager = await _context.TaskManagers.FirstOrDefaultAsync(T=>T.ChildMail == task.ChildEmail);

			CalcTaskScore(task.ChildEmail);

			taskManager.ItemsCount--;

			if (taskManager.ItemsCount == 0)
			{
				var child = await _userManager.FindByEmailAsync(task.ChildEmail);

				if(child is null) return 0;

				child.ChildScore += taskManager.TaskManagerScore;
				taskManager.TaskManagerScore = 0;

				await _userManager.UpdateAsync(child);
			}
			_context.Remove(task);
			return await _context.SaveChangesAsync();
		}


		private async void CalcTaskScore(string userEmail)
		{
			if (userEmail is null)
				return;

			var taskList = _context.TaskManagers.FirstOrDefault(T => T.ChildMail == userEmail);
			var task = _context.TaskManagerItems.FirstOrDefault(T=>T.ChildEmail == userEmail);

			if (taskList is null || task is null )
				return;

			if (task.IsCompleted && task.IsDateAndTimeEnded == false)
			{
				taskList.TaskManagerScore += 2;
			}
			_context.SaveChanges();
		}

        public async Task<int> UpdateTaskAsync(TaskManagerItems taskItem)
        {
			if (taskItem is null)
				return -1;

			var task = await _context.TaskManagerItems.FindAsync(taskItem.ID);

			if (task is null)
				return -1;

			task.date = taskItem.date;
			task.IsDateAndTimeEnded = taskItem.IsDateAndTimeEnded;
			task.IsCompleted = taskItem.IsCompleted;
			task.Name = taskItem.Name;

			_context.TaskManagerItems.Update(task);
            var res = await _context.SaveChangesAsync();
			
			return res;
        }
    }
}
