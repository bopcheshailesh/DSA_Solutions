using System.Text;

namespace LRUCache
{
    public static class Program
    {
        public static void Main() 
        {
            int capacity = 2;
            LRUCache cache = new LRUCache(capacity);
            cache.put(1, 1);
            cache.put(2, 2);
            cache.get(1);
            cache.put(3,3);
            cache.get(2);
        }
    }
    public class Node 
    {
        public int value { get; set; }
        public int key { get; set; }
        public Node prev { get; set; }
        public Node next { get; set; }

        public Node(int key, int val)
        {
            this.key = key;
            this.value = val;
            this.prev = null;
            this.next = null;
        }
    }
    public class LRUCache 
    {
        private int capacity { get; set; }
        private Dictionary<int, Node> dict { get; set; }
        private Node head = new Node(-50,-50);
        private Node tail = new Node(-50,-50);

        public LRUCache(int cap) 
        {
            this.capacity = cap;
            head.next = tail;
            tail.prev = head;
            dict = new Dictionary<int, Node>();
        }

        public int get(int key)
        {
            Console.WriteLine($"\nGet {key}");
            if (dict.ContainsKey(key)) 
            {
                //remove the element from its current position in doubly linked list
                Remove(dict[key]);

                //mark the key value as most recently used value and return the value
                AddToMRU(dict[key]);

                //print list state
                loopOverDoublyLinkedList(head.next);
                //print dict contents
                loopOverDictionary(dict);

                return dict[key].value;
            }
            //print list state
            loopOverDoublyLinkedList(head.next);
            //print dict contents
            loopOverDictionary(dict);
            return -1;
        }

        public void put(int key, int value)
        {
            Console.WriteLine($"\nPut {key}:{value}");
            if (dict.ContainsKey(key))
            {
                //update the value
                dict[key].value = value;

                //Remove the node from its current position in doubly linked list
                Remove(dict[key]);

                //mark the key value as most recently used value and return the value
                AddToMRU(dict[key]);
            }
            else 
            {
                //check capacity, if reached then remove LRU node
                if (dict.Count == this.capacity) 
                {
                    dict.Remove(head.next.key);
                    Remove(head.next);                    
                }

                //Add new value as most recently used node
                dict.Add(key, new Node(key, value));
                AddToMRU(dict[key]);

            }

            //print list state
            loopOverDoublyLinkedList(head.next);
            //print dict contents
            loopOverDictionary(dict);
            return;
        }

        private void AddToMRU(Node node)
        {
            node.prev = tail.prev;
            node.next = tail;

            tail.prev.next = node;
            tail.prev = node;
        }

        private void Remove(Node node)
        {
            node.prev.next = node.next;
            node.next.prev = node.prev;
        }

        private void loopOverDoublyLinkedList(Node node)
        {
            StringBuilder values = new StringBuilder();

            while (node != tail) 
            {
                var x = node.value.ToString();
                values.Append(x+ " <--> ");
                node = node.next;
            }

            string result = $"head <--> {values.ToString()} tail";

            Console.WriteLine(result);
        }

        private void loopOverDictionary(Dictionary<int, Node> dict) 
        {
            foreach (var x in dict)
            {
                Console.WriteLine($"key {x.Key} value {x.Value.value}");
            }
        }

    }
}
