using System;

namespace CodeWars
{
    class Program
    {
        static void Main(string[] args)
        {
            Kata.find_it(new[] { 20, 1, -1, 2, -2, 3, 3, 5, 5, 1, 2, 4, 20, 4, -1, -2, 5 });
        }
    }

    class Kata
    {
        public static int find_it(int[] seq)
        {
            int oddOccurrence = 0;
            for (int i = 0; i < seq.Length; i++)
            {
                oddOccurrence = oddOccurrence ^ seq[i];
            }
            return oddOccurrence;
        }

    }
}
