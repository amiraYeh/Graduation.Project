using AutoMapper;
using GP.Focusi.Core.DTOs;
using GP.Focusi.Core.Entites;

namespace GP.Focusi.API.Mapping
{
	public class TaskManagerProfile :Profile
	{
        public TaskManagerProfile()
        {
			CreateMap<TaskManagerItems, AddTaskManagerItemsDto>().ReverseMap();

		}

	}

}

