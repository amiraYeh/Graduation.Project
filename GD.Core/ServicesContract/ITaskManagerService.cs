using GP.Focusi.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.Focusi.Core.ServicesContract
{
    public interface ITaskManagerService
    {
        Task<List<object>> GetTaskManager(string Email);
        Task<TaskManagerItemsDto> GetTask(string name);
        Task<TaskManagerItemsDto> CreateTask(string email, TaskManagerItemsDto taskItemsDto);
        Task<TaskManagerItemsDto> UpdateTask(string email, TaskManagerItemsDto task);
		Task<int> DeletTask(string name);
        
    }
}
