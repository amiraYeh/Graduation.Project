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
        private int focusTestScore = 0;
        private int parentTestScore = -1;
        public ChildTestRepository(UserManager<AppUserChild> userManager,FocusiAppDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<string> GameChildTestAsync(string email, int gameScore)
        {
            var child = await _userManager.FindByEmailAsync(email);
            if (child is null)
                return null;

            focusTestScore = gameScore;
            parentTestScore = await getParentTestResAsync(child);

            var res = await generateChildClassAsync(child);
            return res;
        }
        public async Task<string> VideoChildTestAsync(string email, int videoScore)
        {
            var child = await _userManager.FindByEmailAsync(email);
            if (child is null)
                return null;

           // var gameScore = child.GameTestScore;
            //if (gameScore == 0)
            //    return "You Should do Game Test First";

            focusTestScore = videoScore ;
            parentTestScore = await getParentTestResAsync(child);

            var res = await generateChildClassAsync(child);
            return res;
        }   
        private async Task<string>  generateChildClassAsync(AppUserChild child)
        {
            string res ="", chClass = getChildClass();
            if(parentTestScore > 0 && parentTestScore <=21) 
            {
                chClass = "no";
                return "You are Normal, You don't Need to Have a Class"; //Normal        
            }
            else 
            {
                if(chClass == "A")
                    res = "Your Distraction Level is High, and Your Class is Class A ";

                else if (chClass == "B") 
                    res = "Your Distraction Level is Medium, and Your Class is Class B ";
                
                else if (chClass == "C")
                    res = "Your Distraction Level is Low, and Your Class is Class C ";   
            }
            if (child == null)
                return null;

            child.ChildClass = chClass;
            var r = await _userManager.UpdateAsync(child);

            if (!r.Succeeded)
                return null;

            return res;
        }
        private async Task<int> getParentTestResAsync(AppUserChild child)
        {

            if (child is null)
                return -1;

            var parentTestScore = await _context.ParentTests.Where(P => P.ChildEmail == child.Email).Select(P => P.DistractionRatio).FirstOrDefaultAsync();

            if (parentTestScore < 0)
                return -1;

            return parentTestScore;
        }
        private string getChildClass()
        {
            if (parentTestScore > 21 && parentTestScore <= 40) //P3
            {
                if (focusTestScore > 0 && focusTestScore <= 30)//C1, class B
                    return "B";

                else if (focusTestScore > 30 && focusTestScore <= 50) //C2, class C
                    return "C";

                else if (focusTestScore > 50 && focusTestScore <= 60) //C3, class C
                    return "C";
            }
            else if (parentTestScore > 40 && parentTestScore <= 60) //P2, class B
            {
                if (focusTestScore > 0 && focusTestScore <= 30) //C1, class A
                    return "A";
          
                else if (focusTestScore > 30 && focusTestScore <= 50) //C2, class B
                    return "B";
                
                else if (focusTestScore > 50 && focusTestScore <= 60) //C3, class C
                    return "C";
            }
            else if (parentTestScore >= 61) //P1, 
            {
                if (focusTestScore > 0 && focusTestScore <= 30) //C1, class A
                    return "A";
                
                else if (focusTestScore > 30 && focusTestScore <= 50) //C2, class A
                    return "A";

                else if (focusTestScore > 50 && focusTestScore <= 60) //C3, class B
                    return "B";
            }
            return null;
        }
    }
}
