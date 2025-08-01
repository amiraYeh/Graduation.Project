﻿using GP.Focusi.Core.DTOs;
using GP.Focusi.Core.Entites;
using GP.Focusi.Core.Entites.Identity;
using GP.Focusi.Core.RepositoriesContract;
using GP.Focusi.Repository.Data.Contexts;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.Focusi.Repository.Repositories
{
	public class FeedBackRepository : IFeedBackRepository
	{
		private readonly FocusiAppDbContext _context;
		private readonly UserManager<AppUserChild> _userManager;

		public FeedBackRepository(FocusiAppDbContext context, UserManager<AppUserChild> userManager)
		{
			_context = context;
			_userManager = userManager;
		}
		public async Task<int> AddFeedBackAsync(string ChildEmail, FeedBackDto FeedBackDto)
		{
			if (ChildEmail is null || FeedBackDto is null) return 0;

			var child = await _userManager.FindByEmailAsync(ChildEmail);
			if(child is null) return 0;

			var feedback = MapFeedback(FeedBackDto);
			feedback.ChildEmail = ChildEmail;
			if (feedback.Suggestions == feedback.Q7MostHelpfulPart) feedback.Suggestions = null;
			await _context.AddAsync(feedback);
			return await _context.SaveChangesAsync();
		}

	

		private FeedBack MapFeedback(FeedBackDto feedBack)
		{
			var res = new FeedBack()
			{
				Q1ProgramHelpParent = feedBack.Q1Answer,
				Q2SuitableActivity = feedBack.Q2Answer,
				Q3ContentUnderstand = feedBack.Q3Answer,
				Q4behaviurImprovement = feedBack.Q4Answer,
				Q5ContinueInProgram = feedBack.Q5Answer,
				Q6RecomendProgram = feedBack.Q6Answer,
				Q7MostHelpfulPart = feedBack.Q7Answer,
				Suggestions = feedBack.Suggestions
			};
			return res;
		}
	}
}
