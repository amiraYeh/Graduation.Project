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

		public async Task<int?> gameDuragion(int duration, string childEmail)
		{
			if (duration == 0)
				return null;

			double durationInMinute = (double) duration/ 60;
			int gameScore = calcGameScore(durationInMinute);

			var res = await SumToChildScore(gameScore, childEmail);
           
			if (res == 0)
                return null;
            return res;
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

		private int calcGameScore(double duration)
		{
			int score = 0;
			if(duration >0 && duration <= 1.5)
			{
				score += 6;
			}
			else if(duration >1.5 && duration <= 3.3)
			{
				score += 4;
			}
			else if(duration >3.3 && duration < 5)
			{
				score += 3;
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

			if(child.ChildScore >= 100)
			{
				string? chClass = child.ChildClass;
				
				if(chClass == "A")
					child.ChildClass = "B";

				else if(chClass == "B")
					child.ChildClass = "C";

				await _userManager.UpdateAsync(child);

			}

			if(!res.Succeeded) 
				return 0;

			return 1;
		}
	}
}
