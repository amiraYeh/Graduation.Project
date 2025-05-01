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

			var result = _context.TaskManagerItems.GroupBy(T => T.ChildEmail);
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

		public async Task<TaskManagerItems> GetTaskAsync(string name)
		{
			return await _context.TaskManagerItems.FirstOrDefaultAsync(T => T.Name == name);
		}

		public async Task<int> CreateTaskAsync(TaskManagerItems taskItem)
		{
			List<TaskManagerItems> x = new List<TaskManagerItems>();
			

			TaskManager taskManager = await _context.TaskManagers.FirstOrDefaultAsync(T => T.ChildMail == taskItem.ChildEmail);

			if (taskManager is not null)
			{
				x.Add( taskItem);
				
				if (taskManager.Items is not null)
					taskManager.Items.Add(taskItem);
				else
					taskManager.Items = x;

				_context.TaskManagers.Update(taskManager);
			}
			else
			{
				var newList = new TaskManager($"{taskItem.ID}_{taskItem.Name}");
				taskItem.TaskManager = newList;
				newList.ChildMail = taskItem.ChildEmail;
				x.Add(taskItem);
				newList.Items = x;
				await _context.TaskManagers.AddAsync(newList);
			}
			await _context.TaskManagerItems.AddAsync(taskItem);
			return await _context.SaveChangesAsync();
		}

		public async Task<int> DeleteTaskAsync(string name)
		{
			var task = await _context.TaskManagerItems.FirstOrDefaultAsync(T=>T.Name == name);

			if (task is null) return 0;

			CalcTaskScore(task.ChildEmail);

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

			if (task.IsCompleted)
			{
				taskList.TaskManagerScore += 2;
			}
			_context.SaveChanges();
		}



	}
}
