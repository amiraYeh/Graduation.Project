using GP.Focusi.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.Focusi.Core.ServicesContract
{
	public interface IStoryAndAdviceServices
	{
		Task<List<string>> AllAdvices(string email);
		Task<List<string>> AllStories(string email);
		Task<List<string>> AllVideosByClassAsync(string email);
		Task<string> getChildClass(string email);

    }
}
