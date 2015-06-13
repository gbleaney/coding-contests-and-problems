using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft_Sorting_Question
{
    class Program
    {
        static void Main(string[] args)
        {
        }

        static List<int> QuickSort(List<int> numbers)
        {
            if (numbers.Count <= 1)
            {
                return numbers;
            }

            int pivot = numbers[numbers.Count/2];

            List<int> left = new List<int>();
            List<int> right = new List<int>();
            List<int> middle = new List<int>();

            foreach (var number in numbers)
            {
                if (number < pivot)
                {
                    left.Add(number);
                }
                else if (number > pivot)
                {
                    right.Add(number);
                }
                else
                {
                    middle.Add(number);
                }
            }

            List<int> result = QuickSort(left);
            result.AddRange(middle);
            result.AddRange(QuickSort(right));
            return result;

        }
    }
}
