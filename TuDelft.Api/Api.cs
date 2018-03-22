using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace TuDelft.Api
{
    public static class TuDelftWorkspace
    {
        public static List<Workplace> Get()
        {
            return Enumerable.Range(0, 100)
                 .AsParallel()
                 .Select(JsonHelper.Load)
                 .Where(a => a != null)
                 .SelectMany(a => a)
                 .ToList();
        }
    }
}


