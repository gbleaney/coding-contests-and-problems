using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palantir_LRU_Cache
{
    // Caches the 'size' most recent elements that have been inserted, discarding older ones.
    class LRUCache<K, V>
    {

        Dictionary<K, Node<K, V>> cache = new Dictionary<K, Node<K, V>>();
        int size;
        Node<K, V> listBack = null;
        Node<K, V> listFront = null;



        public LRUCache(int size)
        {
            this.size = size;
        }

        public void Insert(K key, V value)
        {
            if (cache.Count < size)
            {
                cache.Add(key, new Node<K, V>(key, value));
                AddBack(cache[key]);
            }
            else
            {
                cache.Add(key, new Node<K, V>(key, value));
                AddBack(cache[key]);
                cache.Remove(listFront.Key);
                RemoveFront();
            }

        }

        public V Retrieve(K key)
        {
            Node<K, V> node = cache[key];

            if (node.Next != null)
            {
                node.Next.Prev = node.Prev;
            }

            if (node.Prev != null)
            {
                node.Prev.Next = node.Next;
            }

            if (node == listFront)
            {
                listFront = node.Prev;
            }

            if (node == listBack)
            {
                listBack = listBack.Next;
            }

            AddBack(node);

            return node.Value;
        }

        public Dictionary<K, V> GetElements()
        {
            return cache.ToDictionary(kv => kv.Key, kv => kv.Value.Value);
        }

        private void AddBack(Node<K, V> node)
        {
            if (listBack != null)
            {
                listBack.Prev = node;
                listBack.Prev.Next = listBack;
                listBack = listBack.Prev;
            }
            else
            {
                
                if (listFront != null)
                {
                    listBack = node;
                    listBack.Next = listFront;
                    listFront.Prev = listBack;
                }
                else
                {
                    listFront = node;
                }
            }
        }

        private void RemoveFront()
        {
            if (listFront == null)
            {
                // Empty List
            }
            else
            {
                listFront = listFront.Prev;
                listFront.Next = null;
            }
        }


        class Node<K, V>
        {
            public Node(K key, V value)
            {
                Key = key;
                Value = value;
            }

            public Node<K, V> Next { get; set; }

            public Node<K, V> Prev { get; set; }

            public K Key { get; set; }

            public V Value { get; set; }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            LRUCache<int, int> cache = new LRUCache<int, int>(3);

            cache.Insert(1, 11);
            cache.Insert(2, 12);
            cache.Insert(3, 13);

            Debug.Assert(cache.Retrieve(1) == 11);
            Debug.Assert(cache.Retrieve(2) == 12);
            Debug.Assert(cache.Retrieve(3) == 13);

            cache.Insert(4, 14);

            Debug.Assert(cache.Retrieve(2) == 12);
            Debug.Assert(cache.Retrieve(3) == 13);
            Debug.Assert(cache.Retrieve(4) == 14);

            var elements = cache.GetElements();

            Debug.Assert(elements.Count == 3);

            cache.Retrieve(2);
            cache.Insert(5, 15);

            elements = cache.GetElements();

            Debug.Assert(elements.Count == 3);

            Debug.Assert(elements[2] == 12);
            Debug.Assert(elements[4] == 14);
            Debug.Assert(elements[5] == 15);

        }
    }
}
