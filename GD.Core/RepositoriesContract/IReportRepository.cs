using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.Focusi.Core.RepositoriesContract
{
    public interface IReportRepository
    {
        Task<object> getReportAsync(string email);
    }
}
