using System;
using System.Collections.Generic;
using System.Linq;
namespace CodeWars
{
    class Program
    {
        static void Main(string[] args)
        {
            Kata.GetVowelCount("abebibobub");
            Console.ReadLine();
        }
    }

    public static class Kata
    {
        public static int GetVowelCount(string str)
        {
            int vowelCount = 0;

            vowelCount = str.Where(c => c == 'a' || c == 'e' || c == 'i' || c == 'o' || c == 'u').Count();
            Console.WriteLine(vowelCount);
            return vowelCount;
        }
    }
}
