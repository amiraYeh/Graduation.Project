using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.Focusi.Core.RepositoriesContract
{
	public interface IParentTestRepository
	{
		Task<int> AddParentTestAnswerAsync(string childEmail,int DistractionRatio);
	}
}
