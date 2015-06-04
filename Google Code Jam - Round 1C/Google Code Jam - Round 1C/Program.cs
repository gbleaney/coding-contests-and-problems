using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Google_Code_Jam___Round_1C
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Choose Problem: ");
            string problem = Console.ReadLine();

            switch (problem)
            {
                case "A":
                    Problem_A.Run();
                    break;
                case "B":
                    Problem_B.Run();
                    break;
                case "C":
                    Problem_C.Run();
                    break;
                //case "D":
                //    Problem_D.Run();
                //    break;
            }
        }
    }
}
