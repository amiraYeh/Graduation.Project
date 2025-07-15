using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.Focusi.Core.ServicesContract
{
	public interface ICacheService
	{
		Task SetChachedResponseAsync(string key, object response, TimeSpan expireTime);
		Task<string> GetCachedResponseAsync(string key);
	}
}
