using GP.Focusi.Core.Entites;
using GP.Focusi.Repository.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GP.Focusi.Repository.Data
{
    public static class FocusiDbContextSeed
    {
        //private readonly FocusiAppDbContext _context;

        //public FocusiDbContextSeed(FocusiAppDbContext context)
        //{
        //   _context = context;
        //}

        public static async Task seedAsync(FocusiAppDbContext _context)
        {
            if (_context.Advices.Count() == 0)
            {
                //D:\NewDownloads\VS&CB\C#\Graduation.Project.Solution\GP.Focusi.Repository\Data\DataSeeds\advices.json
                var adviceData = File.ReadAllText(@"..\GP.Focusi.Repository\Data\DataSeeds\advices.json");

                var advices = JsonSerializer.Deserialize<List<Advice>>(adviceData);

                if (advices is null ) 
                    return;
                if (advices.Count() > 0)
                {
                    await _context.Advices.AddRangeAsync(advices);
                    int res = await _context.SaveChangesAsync();
                    Console.WriteLine(res);
                }
            }
            if (_context.Stories.Count() == 0)
            {
                //D:\NewDownloads\VS&CB\C#\Graduation.Project.Solution\GP.Focusi.Repository\Data\DataSeeds\Stories.json
                var storyData = File.ReadAllText(@"..\GP.Focusi.Repository\Data\DataSeeds\Stories.json");

                var stories = JsonSerializer.Deserialize<List<Story>>(storyData);

                if(stories is null )
                    return ;
                if (stories.Count() > 0)
                {
                    await _context.Stories.AddRangeAsync(stories);
                    int res = await _context.SaveChangesAsync();
                    Console.WriteLine(res);
                }
            }
            if (_context.Videos.Count() == 0)
            {
                //D:\NewDownloads\VS&CB\C#\Graduation.Project.Solution\GP.Focusi.Repository\Data\DataSeeds\Videos.json
                var videoData = File.ReadAllText(@"..\GP.Focusi.Repository\Data\DataSeeds\Videos.json");

                var vides = JsonSerializer.Deserialize < List<Videos>>(videoData);

                if(vides is null )
                    return ;
                if(vides.Count() > 0)
                {
                    await _context.Videos.AddRangeAsync(vides);
                    int res = await _context.SaveChangesAsync();
                    Console.WriteLine(res);
                }
            }
        }
    }
}
