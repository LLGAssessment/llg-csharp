using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace llg_csharp
{
    class Program
    {
        static void Main(string[] args)
        {
            var dic = new List<string>();
            string s;
            while ((s = Console.ReadLine()) != null)
            {
                dic.Add(s);
            }

            var pathFinder = new PathFinder();
            var result = pathFinder.FindLongest(dic);

            Console.WriteLine(String.Join(" ", result));
        }
    }

    class PathFinder
    {
        private List<string> Dic;

        private List<List<int>> Lookup;

        private HashSet<int> Visited;

        private List<string> Result;

        public List<string> FindLongest(List<string> dic)
        {
            Init(dic);

            Find(dic.Count, new List<string>());
            return Result;
        }

        private List<string> Find(int currentIndex, List<string> rest)
        {
            foreach(var nextIndex in Lookup[currentIndex])
            {
                if (Visited.Contains(nextIndex))
                {
                    continue;
                }

                Visited.Add(nextIndex);

                rest.Add(Dic[nextIndex]);
                var candidate = Find(nextIndex, rest);

                Visited.Remove(nextIndex);

                if (candidate.Count > Result.Count)
                {
                    Result = new List<string>(candidate);
                }

                rest.RemoveAt(rest.Count - 1);
            }

            return rest;
        }

        private void Init(List<string> dic)
        {
            Dic = new List<string>(dic);
            Lookup = new List<List<int>>(dic.Count);

            for (var io = 0; io < dic.Count; ++io)
            {
                var wo = dic[io];
                var lastCharacter = wo[wo.Length - 1];
                Lookup.Add(new List<int>(dic.Count));

                for (var ii = 0; ii < dic.Count; ++ii)
                {
                    var wi = dic[ii];

                    if (lastCharacter == wi[0] && wo != wi)
                    {
                        Lookup[io].Add(ii);
                    }
                }
            }

            Lookup.Add(Enumerable.Range(0, dic.Count - 1).ToList());

            Visited = new HashSet<int>();

            Result = new List<string>();
        }
    }
}
