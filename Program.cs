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

    class Stack
    {
        private byte[] Data;
        public byte Length { get; private set; }

        public Stack(byte capacity)
        {
            Data = new byte[capacity];
            Length = 0;
        }

        public void Push(byte value)
        {
            Data[Length++] = value;
        }

        public void Pop()
        {
            --Length;
        }

        public List<byte> ToList()
        {
            return Data.Take(Length).ToList();
        }
    }

    class PathFinder
    {
        private List<string> Dic;

        private byte[][] Lookup;

        private List<bool> Visited;

        private List<byte> Result;

        public List<string> FindLongest(List<string> dic)
        {
            Init(dic);

            Find(dic.Count, new Stack((byte)dic.Count));

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

        private Stack Find(int currentIndex, Stack rest)
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

                rest.Push(nextIndex);
                var candidate = Find(nextIndex, rest);

                Exit(nextIndex);

                if (candidate.Length > Result.Count)
                {
                    Result = candidate.ToList();
                }

                rest.Pop();
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

            Visited = Enumerable.Repeat<bool>(false, dic.Count).ToList();

            Result = new List<byte>();
        }
    }
}
