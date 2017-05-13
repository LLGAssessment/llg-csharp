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

        private byte[][] Lookup;

        private bool[] Visited;

        private List<byte> Result;

        public List<string> FindLongest(List<string> dic)
        {
            Init(dic);

            Find(dic.Count, new byte[dic.Count], 0);

            var result = Result.Select(i => dic[i]).ToList();
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

        private byte[] Find(int currentIndex, byte[] rest, int depth)
        {
            var list = Lookup[currentIndex];
            for (var i = 0; i < list.Length; ++i)
            {
                var nextIndex = list[i];

                if (IsVisited(nextIndex))
                {
                    continue;
                }

                Visit(nextIndex);

                rest[depth] = nextIndex;
                var candidate = Find(nextIndex, rest, depth + 1);

                Exit(nextIndex);

                if (depth + 1 > Result.Count)
                {
                    Result = candidate.Take(depth + 1).ToList();
                }
            }

            return rest;
        }

        private void Init(List<string> dic)
        {
            Dic = dic;
            Lookup = new byte[dic.Count + 1][];

            for (var io = 0; io < dic.Count; ++io)
            {
                var wo = dic[io];
                var lastCharacter = wo[wo.Length - 1];

                var entries = new List<byte>(dic.Count);
                for (var ii = 0; ii < dic.Count; ++ii)
                {
                    var wi = dic[ii];

                    if (lastCharacter == wi[0] && wo != wi)
                    {
                        entries.Add((byte)ii);
                    }
                }

                Lookup[io] = entries.ToArray();
            }

            Lookup[dic.Count] = Enumerable.Range(0, dic.Count - 1).Select(v => (byte)v).ToArray();

            Visited = new bool[dic.Count];

            Result = new List<byte>();
        }
    }
}
