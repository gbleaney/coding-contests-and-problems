using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Google_Code_Jam___Round_1A
{
    class Problem_A
    {
        public static void Run()
        {
            List<string> output = new List<string>();

            // Open the file to read from. 
            using (StreamReader sr = File.OpenText(@"E:\My Documents\Google Drive\Coding\Google Code Jam\Google Code Jam - Round 1A\Google Code Jam - Round 1A\A-large.in"))
            {
                int testCases = Int32.Parse(sr.ReadLine());
                for (int caseNum = 0; caseNum < testCases; caseNum++)
                {
                    int numTimePoints = int.Parse(sr.ReadLine());
                    string testCase = sr.ReadLine();
                    int [] timePoints = testCase.Split(' ').Select(int.Parse).ToArray();

                    int firstMin = 0, secondMin = 0, minVelocity = 0;

                    for (int i = 1; i < timePoints.Length; i++)
                    {
                        int prevMushrooms = timePoints[i - 1];
                        int currMushrooms = timePoints[i];
                        int delta = prevMushrooms - currMushrooms;

                        if (delta > 0)
                        {
                            firstMin += delta;
                            minVelocity = Math.Max(minVelocity, delta);
                        }
                    }

                    int missingMushrooms = 0;

                    for (int i = 0; i < timePoints.Length - 1; i++)
                    {
                        int currMushrooms = timePoints[i];

                        if (currMushrooms < minVelocity)
                        {
                            missingMushrooms += minVelocity - currMushrooms;
                        }
                    }

                    secondMin = minVelocity*(numTimePoints - 1) - missingMushrooms;

                    output.Add(String.Format("Case #{0}: {1} {2}", caseNum + 1, firstMin, secondMin));
                }
            }


            using (StreamWriter sw = File.CreateText(@"E:\My Documents\Google Drive\Coding\Google Code Jam\Google Code Jam - Round 1A\Google Code Jam - Round 1A\A-large.out"))
            {
                foreach (var line in output)
                {
                    sw.WriteLine(line);
                }
            }


        }
    }
}
