using System;
using System.Collections.Generic;
using System.Text;

namespace Cache_with_evictionpolicy_separatedout
{
    public class LRUPolicy : IEvictionPolicy
    {
        // node holds key only — no value
        private class Node
        {
            public int key;
            public Node prev, next;
            public Node(int k) 
            { 
                key = k; 
            }

            internal Node() { }
        }

        private Dictionary<int, Node> map;   // key -> node  (policy's own)
        private Node head, tail;             // sentinels

        public LRUPolicy()
        {
            // wire up sentinels and init map
            this.map = new Dictionary<int, Node>();
            this.head = new Node();
            this.tail = new Node();
            head.next = tail;
            tail.prev = head;
        }

        public void RecordInsert(int key)
        {
            // new key: make a node, add to map, splice at MRU end
            Node newNode = new Node(key);
            map.Add(key, newNode);
            AddToMRU(newNode);
        }

        public void RecordAccess(int key)
        {
            // existing key: find node, move it to MRU end
            Node node = map[key];
            Remove(node);
            AddToMRU(node);
        }

        public int Evict()
        {
            // LRU victim is head.next; unlink it, remove from map, return its key
            Node nodeToEvict = head.next;
            Remove(nodeToEvict);
            map.Remove(nodeToEvict.key);
            return nodeToEvict.key;
        }

        private void Remove(Node node)
        {
            /* unlink */
            node.prev.next = node.next;
            node.next.prev = node.prev;
        }
        private void AddToMRU(Node node) 
        {
            /* splice before tail */
            node.prev = tail.prev;
            node.next = tail;

            tail.prev.next = node;
            tail.prev = node;
        }
    }
}
