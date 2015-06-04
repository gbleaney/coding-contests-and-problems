using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Google_Code_Jam___Round_1C
{
    class Problem_C
    {
        private static long maxCoins;
        private static long numExistingDenominations;
        private static long maxValue;
        private static long[] denominations;
        private static List<List<long>> possibleSingleCoinPayments;
        private static bool[] canMake;
        public static void Run()
        {
            List<string> output = new List<string>();

            // Open the file to read from. 
            using (StreamReader sr = File.OpenText(@"E:\Documents\Google Drive\Coding\Google Code Jam - Round 1C\Google Code Jam - Round 1C\C-Test.in"))
            {
                int testCases = Int32.Parse(sr.ReadLine());
                for (int caseNum = 0; caseNum < testCases; caseNum++)
                {
                    string[] testCase = sr.ReadLine().Split(' ');
                    maxCoins = long.Parse(testCase[0]);
                    numExistingDenominations = long.Parse(testCase[1]);
                    maxValue = long.Parse(testCase[2]);

                    string[] sDenominations = sr.ReadLine().Split(' ');
                    denominations = sDenominations.Select(long.Parse).ToArray();

                    canMake = new bool[maxValue + 1];
                    canMake[0] = true;

                    // Construct possible combinations of a single coin
                    possibleSingleCoinPayments = new List<List<long>>();
                    for (int i = 0; i < numExistingDenominations; i++)
                    {
                        possibleSingleCoinPayments[i] = new List<long>();
                        for (int j = 0; j < maxCoins; j++)
                        {
                            possibleSingleCoinPayments[i][j] = denominations[i]*(j + 1);

                            if (possibleSingleCoinPayments[i][j] <= maxValue)
                            {
                                canMake[possibleSingleCoinPayments[i][j]] = true;
                            }
                        }
                    }


                    for (int i = 0; i <= maxValue; i++)
                    {
                        bool addDenom = !CanMake(i, possibleSingleCoinPayments.Count - 1);
                    }

                    output.Add(String.Format("Case #{0}: {1}", caseNum + 1, ""));
                }
            }


            using (StreamWriter sw = File.CreateText(@"E:\Documents\Google Drive\Coding\Google Code Jam - Round 1C\Google Code Jam - Round 1C\C-Test.out"))
            {
                foreach (var line in output)
                {
                    sw.WriteLine(line);
                }
            }


        }

        private static bool CanMake(long value, int maxDenom)
        {
            if (value == 0)
            {
                return true;
            }
            if (maxDenom < 0)
            {
                return false;
            }
            if (value < 0)
            {
                return false;
            }


            for (int i = possibleSingleCoinPayments[maxDenom].Count - 1; i >= 0; i--)
            {
                if (CanMake(value - possibleSingleCoinPayments[maxDenom][i], maxDenom - 1))
                {
                    return true;
                }
            }
            
            return false;
        }
    }
}
