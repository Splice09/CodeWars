using System;

namespace CodeWars
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] phone = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 };
            Kata.CreatePhoneNumber(phone);
            Console.ReadLine();
        }
    }
    public class Kata
    {
        public static string CreatePhoneNumber(int[] numbers)
        {
            string area = string.Join("", numbers[0..3]);
            string exchange = string.Join("", numbers[3..6]);
            string lineNum = string.Join("", numbers[6..10]);
            return $"({area}) {exchange}-{lineNum}";
        }
    }
}
