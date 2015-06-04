using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Google_Code_Jam___Round_1A
{
    class Problem_B
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
                if (this.TimePerCustomer < other.TimePerCustomer)
                {
                    return -1;
                }
                else if (this.TimePerCustomer > other.TimePerCustomer)
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

            // Open the file to read from. 
            using (StreamReader sr = File.OpenText(@"E:\My Documents\Google Drive\Coding\Google Code Jam\Google Code Jam - Round 1A\Google Code Jam - Round 1A\Problem B.in"))
            {
                int testCases = int.Parse(sr.ReadLine());
                for (int caseNum = 1; caseNum <= testCases; caseNum++)
                {
                    int[] firstLine = sr.ReadLine().Split(' ').Select(int.Parse).ToArray();
                    int numBarbers = firstLine[0];
                    int placeInLine = firstLine[1];

                    int chosenBarber;
                    int[] barbersTimes = sr.ReadLine().Split(' ').Select(int.Parse).ToArray();

                    if (numBarbers < placeInLine) {
                        Barber [] barberArray = new Barber[numBarbers];

                        for (int i = 0; i < numBarbers; i++)
                        {
                            barberArray[i] = new Barber(i + 1, barbersTimes[i]);
                            barberArray[i].AddCustomer();
                            placeInLine--;
                        }

                        Array.Sort(barberArray);

                        int currentBarber = 0;

                        for (int i = 0; i < placeInLine - 1; i++)
                        {
                            barberArray[currentBarber++].AddCustomer();

                            currentBarber = currentBarber % numBarbers;

                            if (barberArray[currentBarber].TimeAlotted >= barberArray[0].TimeAlotted)
                            {
                                currentBarber = 0;
                            }

                        }
                        chosenBarber = barberArray[currentBarber].Position;
                    }
                    else
                    {
                        chosenBarber = placeInLine;
                    }

                    output.Add(String.Format("Case #{0}: {1}", caseNum, chosenBarber));
                }
            }

            using (StreamWriter sw = File.CreateText(@"E:\My Documents\Google Drive\Coding\Google Code Jam\Google Code Jam - Round 1A\Google Code Jam - Round 1A\Problem B - 2.out"))
            {
                foreach (var line in output)
                {
                    sw.WriteLine(line);
                }
            }
        }
    }
}
