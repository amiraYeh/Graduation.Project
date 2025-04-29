//using GP.Focusi.Core.Entites;
//using GP.Focusi.Core.Entites.Identity;
//using GP.Focusi.Core.RepositoriesContract;
//using GP.Focusi.Repository.Data.Contexts;
//using GP.Focusi.Repository.Identity.Contexts;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.EntityFrameworkCore;
//using sib_api_v3_sdk.Model;
//using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace GP.Focusi.Repository.Repositories
//{
//	public class TaskManagerRepository : ITaskManagerRepository
//	{
//		private readonly FocusiAppDbContext _context;
//		public TaskManagerRepository(FocusiAppDbContext context)
//		{
//			_context = context;
//		}

//		public async Task<List<object>> GetAllTasksAsync(string email)
//		{
//			var taskManager = await _context.TaskManagers.FirstOrDefaultAsync(T=>T.ChildMail == email);

//			if (taskManager is null) return null;

//			var result = _context.TaskManagerItems.GroupBy(T => T.ChildEmail);
//			List<object> tasks = new List<object>();
//			foreach (var TManager in result)
//			{
//				if (TManager.Key == email)
//				{
//					foreach (var item in TManager)
//					{
//						tasks.Add(new { Name = item.Name, Date = item.date.ToShortTimeString(), Done = item.IsCompleted });
//					}
//				}
//			}
//			return tasks;
//		}

//		public async Task<TaskManagerItems> GetTaskAsync(string name)
//		{
//			return await _context.TaskManagerItems.FirstOrDefaultAsync(T => T.Name == name);
//		}

//		public async Task<int> CreateTaskAsync(TaskManagerItems taskItem)
//		{
//			List<TaskManagerItems> x = new List<TaskManagerItems>();
			

//			TaskManager taskManager = await _context.TaskManagers.FirstOrDefaultAsync(T => T.ChildMail == taskItem.ChildEmail);

//			if (taskManager is not null)
//			{
//				x.Add( taskItem);
				
//				if (taskManager.Items is not null)
//					taskManager.Items.Add(taskItem);
//				else
//					taskManager.Items = x;

//				_context.TaskManagers.Update(taskManager);
//			}
//			else
//			{
//				var newList = new TaskManager($"{taskItem.Id}_{taskItem.Name}");
//				taskItem.TaskManager = newList;
//				newList.ChildMail = taskItem.ChildEmail;
//				x.Add(taskItem);
//				newList.Items = x;
//				await _context.TaskManagers.AddAsync(newList);
//			}
//			await _context.TaskManagerItems.AddAsync(taskItem);
//			return await _context.SaveChangesAsync();
//		}

//		public async Task<TaskManagerItems> UpdateTaskAsync(int taskId, TaskManagerItems taskItem)
//		{
//			var task = await _context.TaskManagerItems.FindAsync(taskId);

//			if (task is null ) return null;

//			var taskManager = await _context.TaskManagers.FirstOrDefaultAsync(T=>T.ChildMail == taskItem.ChildEmail);

//			if (task.TaskManagerId != taskManager.Id) return null;

//			//taskItem.Id = taskId;
//			//_context.TaskManagers.Update(taskManager);
//			_context.TaskManagerItems.Update(taskItem);
//			_context.SaveChanges();
//			return await GetTaskAsync(task.Name);
//		}

//		public async Task<int> DeleteTaskAsync(string name)
//		{
//			var task = await _context.TaskManagerItems.FirstOrDefaultAsync(T=>T.Name == name);

//			if (task is null) return 0;

//			CalcTaskScore(task.ChildEmail);

//			_context.Remove(task);
//			return await _context.SaveChangesAsync();
//		}


//		private void CalcTaskScore(string userEmail)
//		{
//			if (userEmail is null)
//				return;

//			var taskList = _context.TaskManagers.FirstOrDefault(T => T.ChildMail == userEmail);
//			var task = _context.TaskManagerItems.FirstOrDefault(T=>T.ChildEmail == userEmail);

//			if (taskList is null || task is null)
//				return;

//			if (task.IsCompleted)
//				taskList.TaskManagerScore += 2;

//			_context.SaveChanges();
//		}


//		#region MyRegion


//		//public async Task<List<string>> GetByEmailTaskManagerAsync(string UserEmail)
//		//{
//		//	try
//		//	{
//		//		if (UserEmail is null) return null;

//		//		var taskManager = await _context.TaskManagers.Where(T => T.ChildMail == UserEmail).FirstOrDefaultAsync();
				
//		//		if (taskManager is null) return null;

//		//		var result = _context.TaskManagerItems.GroupBy(T => T.ChildEmail);
//		//		List<string> list = new List<string>();
//		//		foreach (var TManager in result)
//		//		{
//		//			if (TManager.Key == UserEmail)
//		//			{
//		//				foreach (var item in TManager)
//		//				{
//		//					var task = new { Name = item.Name, Date = item.date.ToShortTimeString(), Done = item.IsCompleted };
//		//					//(item.Name, item.date.ToShortTimeString()).ToTuple<string,string>();
//		//					list.Add(task.ToString());
							
//		//				}
//		//			}
//		//		}
//		//		return list ;
//		//	}
//		//	catch (Exception ex)
//		//	{
//		//		new ErrorModel() { Message = ex.Message };
//		//	}
//		//	return null;
//		//}

//		//public async Task<TaskManagerItems> AddItemAsync(TaskManagerItems item)
//		//{
//		//	try
//		//	{
//		//		if (item is null) return null;
//		//		List<TaskManagerItems> x = new List<TaskManagerItems>();
//		//		TaskManager test = await _context.TaskManagers.FirstOrDefaultAsync(T=>T.ChildMail == item.ChildEmail);
//		//		if (test is not null)
//		//		{
//		//			x.Add(item);
//		//			//var re = 
//		//			if (test.Items is not null)
//		//				test.Items.Add(item);

//		//			else
//		//				test.Items = x;
					
//		//		}
//		//		else
//		//		{
//		//			var newList = new TaskManager($"{item.Id}_{item.Name}");
//		//			item.TaskManager = newList;
//		//			newList.ChildMail = item.ChildEmail;
//		//			x.Add(item);
//		//			newList.Items = x;
//		//			await _context.TaskManagers.AddAsync(newList);
//		//		}
//		//		await _context.TaskManagerItems.AddAsync(item);

//		//		var result = await _context.SaveChangesAsync();

//		//		if (result <= 0)
//		//			return null;
//		//	}
//		//	catch (Exception ex)
//		//	{
//		//		//new ErrorModel() { Message = ex.Message };
//		//	}
//		//	return item;

//		//}
		
//		////public async Task<TaskManagerItems> UpdateItemAsync(TaskManagerItems item)
//		////{
//		////	if (item is null)
//		////		return null;

//		////	var test = await _context.TaskManagerItems.FirstOrDefaultAsync(T => T.ChildEmail == item.ChildEmail);
//		////	//if (test is not null)
//		////	//{
//		////	//	item.TaskManagerId = test.TaskManagerId;
//		////	//	//item.Id = test.Id;
//		////	//}
//		////	var task = await _context.TaskManagerItems.FirstOrDefaultAsync(T=> T.ChildEmail == item.ChildEmail);
//		////	if (task is null)
//		////		return null;

//		////	 _context.TaskManagerItems.Update(item);
//		////	_context.SaveChanges();

//		////	return item;
//		////}

//		//public async Task<int> DeleteItemAsync(string name)
//		//{
//		//	if (name is null) return 0;

//		//	var item = await _context.TaskManagerItems.FirstOrDefaultAsync(T => T.Name == name);
//		//	var taskManager = await _context.TaskManagers.FirstOrDefaultAsync(T => T.ChildMail == item.ChildEmail);

//		//	if (item is null) return 0;

//		//	if(item.TaskManagerId != taskManager.Id) return 0;

//		//	if (item.IsCompleted)
//		//		taskManager.TaskManagerScore += 2;

//		//	//var child = await _userManager.FindByEmailAsync(item.ChildEmail);

//		//	//if (child is not null)
//		//	//	child.ChildScore += item.TaskManager.TaskManagerScore; 
//		//	_context.TaskManagerItems.Remove(item);
//		//	return await _context.SaveChangesAsync();
//		// }

//		//public async Task<int> GetTasksScoreAsync(string userEmail)
//		//{
//		//	if (userEmail is null)
//		//		return -1;

//		//	var taskList = await _context.TaskManagers.FirstOrDefaultAsync(T => T.ChildMail == userEmail);

//		//	if (taskList is null)
//		//		return -1;

			
//		//	return taskList.TaskManagerScore;

//		//}

//		//public async Task<TaskManagerItems> UpdateItemAsync(TaskManagerItems item)
//		//{
//		//	if (item is null) return null;

//		//	var taskManager = await _context.TaskManagers.FirstOrDefaultAsync(T => T.ChildMail == item.ChildEmail);

//		//	var task = await GetItemAsync(item.Name);

//		//	if (task is null || taskManager is null) return null;

//		//	if (task.TaskManagerId != taskManager.Id) return null;

//		//	item.TaskManager = taskManager;
//		//	//item.Id = task.Id;
//		//	_context.TaskManagerItems.Update(item);
//		//	int x = await _context.SaveChangesAsync();

//		//	if (x < 1) return null;

//		//	return item;
//		//}

//		//public async Task<TaskManagerItems> GetItemAsync(string name)
//		//{
//		//	if (name is null) return null;

//		//	var item = await _context.TaskManagerItems.FirstOrDefaultAsync(T=>T.Name == name);

//		//	if (item is null) return null;

//		//	return item;

//		//}

		
//		#endregion
//	}
//}
