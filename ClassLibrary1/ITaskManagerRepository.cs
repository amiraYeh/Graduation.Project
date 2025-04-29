//using GP.Focusi.Core.DTOs;
//using GP.Focusi.Core.Entites;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace GP.Focusi.Core.RepositoriesContract
//{
//	public interface ITaskManagerRepository
//	{
//		#region MyRegion
//		//Task<List<string>> GetByEmailTaskManagerAsync(string UserEmail);	
//		//Task<TaskManagerItems> AddItemAsync(TaskManagerItems item);	 
//		//Task<TaskManagerItems> UpdateItemAsync(TaskManagerItems item);
//		//public Task<int> DeleteItemAsync(string name);
//		//public Task<int> GetTasksScoreAsync(string userEmail);
//		//public Task<TaskManagerItems> GetItemAsync(string userEmail);
//		#endregion
		
//		Task<List<object>> GetAllTasksAsync(string email);
//		Task<TaskManagerItems> GetTaskAsync(string name);
//		Task<int> CreateTaskAsync(TaskManagerItems taskItem);
//		Task<TaskManagerItems> UpdateTaskAsync(int taskId, TaskManagerItems taskItem);
//		Task<int> DeleteTaskAsync(string name);
//	}
//}
