using GP.Focusi.Core.DTOs;
using GP.Focusi.Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.Focusi.Core.RepositoriesContract
{
	public interface ITaskManagerRepository
	{
		
		Task<List<object>> GetAllTasksAsync(string email);
		Task<TaskManagerItems> GetTaskAsync(string name);
		Task<int> CreateTaskAsync(TaskManagerItems taskItem);
		Task<int> DeleteTaskAsync(string name);
	}
}
