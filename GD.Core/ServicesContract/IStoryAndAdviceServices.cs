using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.Focusi.Core.ServicesContract
{
	public interface IStoryAndAdviceServices
	{
		Task<IEnumerable<object>> AllAdvices(string email);
		Task<IEnumerable<object>> AllStories(string email);

	}
}
