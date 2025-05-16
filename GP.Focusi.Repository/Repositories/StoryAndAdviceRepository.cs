using GP.Focusi.Core.Entites;
using GP.Focusi.Core.RepositoriesContract;
using GP.Focusi.Repository.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.Focusi.Repository.Repositories
{
	public class StoryAndAdviceRepository<TEntity> : IStoryAndAdviceRepository<TEntity> where TEntity : BaseEntity<int>
	{
		private readonly FocusiAppDbContext _context;

		public StoryAndAdviceRepository(FocusiAppDbContext context)
		{
			_context = context;
		}
		public async Task<IEnumerable< object>> GetAllAsync(string classType)
		{
			if (classType is null)
				return null;

			//var res =  _context.TEntity.Where(A=>A.ClassType == classType).GroupBy(A=>A.ClassType).ToList();
			//object res;
			if (typeof(TEntity) == typeof(Advice))
			{
				var res = await _context.Set<Advice>().Where(A=>A.ClassType == classType).ToListAsync();
				if (res is null)
					return null;

				return res;
			}
			else if (typeof(TEntity) == typeof(Story))
			{
				var res = await _context.Set<Story>().Where(S => S.ClassType == classType).ToListAsync();

				if (res is null)
					return null;

				return res;
			}
			return null;
		}
	}
}
