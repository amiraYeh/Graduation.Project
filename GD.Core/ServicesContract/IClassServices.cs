using GP.Focusi.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.Focusi.Core.ServicesContract
{
    public interface IClassServices
    {
        Task<int?> addVideoData(VideoDto videoDto, string childEmail);
        Task<int?> gameDuragion(double duration, string childEmail);
    }
}
