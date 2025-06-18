using GP.Focusi.Core.Entites;
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
    public class ReportRepository : IReportRepository
    {
        private readonly UserManager<AppUserChild> _userManager;
        private readonly FocusiAppDbContext _context;

        public ReportRepository(UserManager<AppUserChild> userManager, FocusiAppDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }
        public async Task<object> getReportAsync(string email)
        {
           var res = await _context.Set<Report>().Where(R=>R.ChildEmail == email).ToListAsync();
            return res;
            
        }
    }
}
