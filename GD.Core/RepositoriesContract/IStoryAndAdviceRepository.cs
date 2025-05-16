using GP.Focusi.Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.Focusi.Core.RepositoriesContract
{
	public interface IStoryAndAdviceRepository<TEntity> where TEntity : BaseEntity<int>
	{
		Task<IEnumerable<object>> GetAllAsync(string classType);
	}
}
