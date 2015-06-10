using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace List_with_Sublist_Iterator_3A
{
    class Data<T>
    {
        public Data(IEnumerable<Data<T>> collection)
        {
            this.collection = collection;
            this.isCollection = true;
        }

        public Data(T value)
        {
            this.value = value;
            this.isCollection = false;
        } 

        private readonly IEnumerable<Data<T>> collection;
        private readonly T value;
        private readonly bool isCollection;

        public IEnumerable<Data<T>> Collection
        {
            get { return collection; }
        }

        public bool IsCollection
        {
            get { return isCollection; }
        }

        public T Value
        {
            get { return value; }
        }
    }

    class DataIterator<T>
    {
        private Data<T> currentData;

        private DataIterator<T> curreDataIterator; 

        private IEnumerator<Data<T>> currentEnumerator;

        private bool currentEnumeratorHasNext;
 
        public DataIterator(IEnumerable<Data<T>> collection)
        {
            currentEnumerator = collection.GetEnumerator();
            currentEnumerator.MoveNext(); // Get to the first valid element

            currentEnumeratorHasNext = true;

            MoveNext();
        }

        private void MoveNext()
        {
            if (currentEnumeratorHasNext)
            {
                currentData = currentEnumerator.Current;


                if (currentData.IsCollection)
                {
                    curreDataIterator = new DataIterator<T>(currentData.Collection);
                }
                currentEnumeratorHasNext = currentEnumerator.MoveNext();
            }
            else
            {
                currentData = null;
                curreDataIterator = null;
                currentEnumeratorHasNext = false;
            }

            
        }

        public bool HasNext()
        {
            if (currentData == null)
            {
                return false;
            }
            else if (!currentData.IsCollection)
            {
                return true;
            }
            else
            {
                if (curreDataIterator.HasNext())
                {
                    return true;
                }
                else if (currentEnumeratorHasNext)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public T Next()
        {
            T retVal;

            if (!currentData.IsCollection)
            {
                retVal =  currentData.Value;
                MoveNext();
            } else
            {
                retVal = curreDataIterator.Next();
                if (!curreDataIterator.HasNext())
                {
                    MoveNext();
                }
            }
            return retVal;
        }

    }

    class Program
    {
        static void Main(string[] args)
        {
            Data<int>[] data1 = {new Data<int>(1), new Data<int>(2), new Data<int>(3)};
            Data<int> data2 = new Data<int>(4);
            Data<int>[] data3 = { new Data<int>(5), new Data<int>(6), new Data<int>(7) };
            Data<int>[] data4 = { new Data<int>(8), new Data<int>(9), new Data<int>(10) };
            Data<int>[] data5 = {new Data<int>(data3), new Data<int>(data4),};

            List<Data<int>> data = new List<Data<int>>();
            data.Add(new Data<int>(data1));
            data.Add(data2);
            data.Add(new Data<int>(data5));

            DataIterator<int> iterator = new DataIterator<int>(data);

            while (iterator.HasNext())
            {
                Console.WriteLine(iterator.Next());
            }

            Console.ReadLine();
        }
    }
}
