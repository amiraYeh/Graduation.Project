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
		public async Task<List<string>> GetAllAsync(string classType)
		{
			if (classType is null)
				return null;

		
			if (typeof(TEntity) == typeof(Advice))
			{
				var res = await _context.Set<Advice>().OrderBy(A=>A.ID).Select(A=>A.Content).ToListAsync();
				
				if (res is null)
					return null;

				return res;
			}
			else if (typeof(TEntity) == typeof(Story))
			{
				var res = await _context.Set<Story>().Select(S => S.StoryName).ToListAsync();

				if (res is null)
					return null;

				return res;
			}
			else if (typeof(TEntity) == typeof(Videos))
			{
				var res = await _context.Set<Videos>().Where(V => V.ClassType == classType).Select(V => V.VideoName).ToListAsync();

				if (res is null)
					return null;
				return res;
			}
			return null;
		}
	}
}
