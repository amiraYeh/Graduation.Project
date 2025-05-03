using GP.Focusi.Core.RepositoriesContract;
using GP.Focusi.Core.ServicesContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.Focusi.Services
{
	public class ParentTestService : IParentTestService
	{
		private readonly IParentTestRepository _parentTestRepository;

		public ParentTestService(IParentTestRepository parentTestRepository)
		{
			_parentTestRepository = parentTestRepository;
		}
		public async Task<int> GetDistractionRatioAsync(string childEmail, List<int> answers)
		{
			if (answers.Count < 0 || childEmail is null) return 0;

			var ratio = calcDistractionRatio(answers);

			return await _parentTestRepository.AddParentTestAnswerAsync(childEmail, ratio);

		}
		private int calcDistractionRatio(List<int> answers)
		{
			int sum = 0;
			foreach (int answersItem in answers)
			{
				sum += answersItem;
			}
			return sum;
		}
	}
}
