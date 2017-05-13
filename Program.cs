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

        private List<bool> Visited;

        private List<int> Result;

        public List<string> FindLongest(List<string> dic)
        {
            Init(dic);

            Find(dic.Count, new Stack<int>(dic.Count));

            var result = Enumerable.Reverse(Result).Select(i => dic[i]).ToList();
            return result;
        }

        private bool IsVisited(int index)
        {
            return Visited[index];
        }

        private void Visit(int index)
        {
            Visited[index] = true;
        }

        private void Exit(int index)
        {
            Visited[index] = false;
        }

        private Stack<int> Find(int currentIndex, Stack<int> rest)
        {
            var list = Lookup[currentIndex];
            for (var i = 0; i < list.Count; ++i)
            {
                var nextIndex = list[i];

                if (IsVisited(nextIndex))
                {
                    continue;
                }

                Visit(nextIndex);

                rest.Push(nextIndex);
                var candidate = Find(nextIndex, rest);

                Exit(nextIndex);

                if (candidate.Count > Result.Count)
                {
                    Result = new List<int>(candidate);
                }

                rest.Pop();
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

            Visited = Enumerable.Repeat<bool>(false, dic.Count).ToList();

            Result = new List<int>();
        }
    }
}
