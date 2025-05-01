using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.Focusi.Core.ServicesContract
{
	public interface IParentTestService
	{
		Task<int> GetDistractionRatioAsync(string childEmail, List<int> answers);
	}
}
