using GP.Focusi.Core.Entites.Identity;
using GP.Focusi.Core.RepositoriesContract;
using GP.Focusi.Repository.Data.Contexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.Focusi.Repository.Repositories
{
    public class ChildTestRepository : IChildTestRepository
    {
        private readonly UserManager<AppUserChild> _userManager;
        private readonly FocusiAppDbContext _context;
        private int testScore = 0;
        public ChildTestRepository(UserManager<AppUserChild> userManager,FocusiAppDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public Task<int> GameChildTestAsync(string email, int gameScore)
        {

            testScore += gameScore;
            throw new NotImplementedException();
        }

        public Task<int> VideoChildTestAsync(string email, int videoScore)
        {
            testScore += videoScore;
            throw new NotImplementedException();
        }

        private async Task<int>  generateChildClassAsync(string email)
        {
            var child = await _userManager.FindByEmailAsync(email);

            if (child is null)
                return -1;

            var parentTestScore = await _context.ParentTests.Where(P => P.ChildEmail == email).Select(P=>P.DistractionRatio).SingleOrDefaultAsync();

            if(parentTestScore < 0)
                return -1;

            string chClass = "";
            //int testScore = gScore + vScore;

            if(parentTestScore > 0 && parentTestScore <=21) 
            {
                return 5; //Normal
            }
            else if(parentTestScore > 21 && parentTestScore <= 40) //class C
            {
                //if(testScore )
            }
            else if(parentTestScore > 40 &&  parentTestScore <= 60)// class B
            {

            }
            else if (parentTestScore >= 61) // class A
            {
                
            }
            child.ChildClass = chClass;
            var x= await _userManager.UpdateAsync(child);
            if (x.Succeeded)
                return 1;

            return -1;
        }
    }
}
