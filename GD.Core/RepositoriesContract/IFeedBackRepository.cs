using GP.Focusi.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.Focusi.Core.RepositoriesContract
{
	public interface IFeedBackRepository
	{
		Task<int> AddFeedBackAsync(string ChildEmail, FeedBackDto FeedBackDto);
	}
}
