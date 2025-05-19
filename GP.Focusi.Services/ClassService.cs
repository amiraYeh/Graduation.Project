using GP.Focusi.Core.DTOs;
using GP.Focusi.Core.Entites.Identity;
using GP.Focusi.Core.ServicesContract;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.Focusi.Repository.Repositories
{
    public class ClassService : IClassServices
	{
		private readonly UserManager<AppUserChild> _userManager;

		public ClassService(UserManager<AppUserChild> userManager)
		{
			_userManager = userManager;
		}
		public async Task<int?> addVideoData(VideoDto videoDto, string childEmail)
		{
			if (videoDto is null)
				return null;

			int score = calcVideoScore(videoDto);
			var res = await SumToChildScore(score, childEmail);

			if (res == 0)
				return null;
			return res;
		}

		public Task<int?> gameDuragion(double duration, string childEmail)
		{
			throw new NotImplementedException();
		}
		private int calcVideoScore(VideoDto videoDto)
		{
			int score = 0;

			if(videoDto.QuestionsNum-2 <= videoDto.CorrectAnswersNum)
			{
				var x = videoDto.CorrectAnswersNum ;
				x *= 2;
				score += x;
			}

			return score;
		}


		private async Task<int> SumToChildScore(int score, string childEmail)
		{
			if(childEmail is  null)
				return 0;

			var child = await _userManager.FindByEmailAsync(childEmail);

			if (child is null)
				return 0;

			child.ChildScore += score;
			var res = await _userManager.UpdateAsync(child);

			if(!res.Succeeded) 
				return 0;

			return 1;
		}
	}
}
