using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Google_Code_Jam___Round_1A
{
    class Problem_B_New
    {
        private class Barber : IComparable<Barber>
        {
            private int position;
            private int timePerCustomer;
            private int timeAlotted = 0;

            public Barber(int position, int timePerCustomer)
            {
                this.position = position;
                this.timePerCustomer = timePerCustomer;
            }

            public void AddCustomer()
            {
                timeAlotted += timePerCustomer;
            }

            public int Position
            {
                get { return position; }
                set { position = value; }
            }

            public int TimePerCustomer
            {
                get { return timePerCustomer; }
                set { timePerCustomer = value; }
            }

            public int TimeAlotted
            {
                get { return timeAlotted; }
                set { timeAlotted = value; }
            }

            public int CompareTo(Barber other)
            {
                if (this.TimeAlotted < other.TimeAlotted)
                {
                    return -1;
                }
                else if (this.TimeAlotted > other.TimeAlotted)
                {
                    return 1;
                }
                else
                {
                    if (this.Position < other.Position)
                    {
                        return -1;
                    }
                    else if (this.Position > other.Position)
                    {
                        return 1;
                    }
                    else
                    {
                        return 0;
                    }
                }
            }

            public override string ToString()
            {
                return "Position: " + this.Position + " Time Alotted: " + this.TimeAlotted + " Time Per Customer: " +
                       this.TimePerCustomer;
            }
        }
        public static void Run()
        {
            List<string> output = new List<string>();

            //Barber b1 = new Barber(1, 10);
            //Barber b2 = new Barber(2, 10);
            //Barber b3 = new Barber(3, 10);
            //Barber b4 = new Barber(4, 10);
            //Barber b5 = new Barber(5, 10);

            //b1.AddCustomer();
            //b1.AddCustomer();
            //b2.AddCustomer();
            //b2.AddCustomer();
            //b3.AddCustomer();
            //b4.AddCustomer();


            //SortedSet<Barber> testSet = new SortedSet<Barber>(new Barber[] { b1, b2, b3, b4, b5 });
            //foreach (var barber in testSet)
            //{
            //    Console.WriteLine(barber);
            //}

            //Console.WriteLine("Min: " + testSet.Min);
            //Console.WriteLine("Max: " + testSet.Max);

            //Console.ReadLine();


            // Open the file to read from. 
            using (StreamReader sr = File.OpenText(@"E:\My Documents\Google Drive\Coding\Google Code Jam\Google Code Jam - Round 1A\Google Code Jam - Round 1A\B-small-attempt0.in"))
            {
                int testCases = int.Parse(sr.ReadLine());
                for (int caseNum = 1; caseNum <= testCases; caseNum++)
                {
                    int[] firstLine = sr.ReadLine().Split(' ').Select(int.Parse).ToArray();
                    int numBarbers = firstLine[0];
                    int placeInLine = firstLine[1];

                    int[] barbersTimes = sr.ReadLine().Split(' ').Select(int.Parse).ToArray();
                    Barber [] barberArray = new Barber[numBarbers];

                    for (int i = 0; i < numBarbers; i++)
                    {
                        barberArray[i] = new Barber(i + 1, barbersTimes[i]);
                    }

                    SortedSet<Barber> barbers = new SortedSet<Barber>(barberArray);


                    for (int i = 0; i < placeInLine - 1; i++)
                    {
                        Barber min = barbers.Min;
                        barbers.Remove(min);
                        min.AddCustomer();
                        barbers.Add(min);
                    }

                    //Console.WriteLine("Case " + caseNum);

                    //foreach (var barber in barbers)
                    //{
                    //    Console.WriteLine(barber);
                    //}

                    //Console.WriteLine("Min: " + barbers.Min);
                    //Console.WriteLine("Max: " + barbers.Max);

                    output.Add(String.Format("Case #{0}: {1}", caseNum, barbers.Min.Position));
                }
            }

            using (StreamWriter sw = File.CreateText(@"E:\My Documents\Google Drive\Coding\Google Code Jam\Google Code Jam - Round 1A\Google Code Jam - Round 1A\B-small-attempt0.out"))
            {
                foreach (var line in output)
                {
                    sw.WriteLine(line);
                }
            }
        }
    }
}
