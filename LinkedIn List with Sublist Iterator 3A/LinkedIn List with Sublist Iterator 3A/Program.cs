using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedIn_List_with_Sublist_Iterator_3A
{
    class Data<T>
    {
        private IEnumerable<Data<T>> collection;
        private T value;
        private bool isCollection;

        public IEnumerable<Data<T>> Collection
        {
            get { return collection; }
            set { collection = value; }
        }

        public bool IsCollection
        {
            get { return isCollection; }
            set { isCollection = value; }
        }

        public T Value
        {
            get { return value; }
            set { this.value = value; }
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
            currentData = currentEnumerator.Current;


            if (currentData.IsCollection)
            {
                curreDataIterator = new DataIterator<T>(currentData.Collection);
            }

            currentEnumeratorHasNext = currentEnumerator.MoveNext();
        }

        private void MoveNext()
        {
            currentData = currentEnumerator.Current;


            if (currentData.IsCollection)
            {
                curreDataIterator = new DataIterator<T>(currentData.Collection);
            }

            currentEnumeratorHasNext = currentEnumerator.MoveNext();
        }

        bool HasNext()
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

        T Next()
        {
            if (currentData.IsCollection)
            {
                if (curreDataIterator.HasNext())
                {
                    return curreDataIterator.Next();
                }
                else if (currentEnumeratorHasNext)
                {
                    
                }
                else
                {
                    throw new Exception("End of iterator");
                }
                
            }
            else
            {
                throw new Exception("End of iterator");
            }
        }

    }

    class Program
    {
        static void Main(string[] args)
        {
        }
    }
}
