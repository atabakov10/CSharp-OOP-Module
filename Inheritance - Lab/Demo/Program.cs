using System;
using System.Linq;

namespace Demo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string str = "";
            Console.WriteLine("Enter the string");
            string s = Console.ReadLine();
            int n = s.Length;
            for (int i = n - 1; i >= 0; i--)
            {
                str = str + s[i];
            }
            if (str == s)
            {
                Console.WriteLine($"{s} is a palindrome");
            }
            else
            {
                Console.WriteLine($"{s} is not a palindrome");
                
            }
        }
           
    }
}
