using System;
using System.Collections.Generic;
using System.Text;

namespace Cache_with_evictionpolicy_separatedout
{
    public class OrderedKeyList
    {
        private class Node 
        {
            public int key; 
            public Node prev, next; 
            public Node() { } 
            public Node(int k) 
            { 
                key = k; 
            } 
        }
        private Dictionary<int, Node> map;
        private Node head, tail;

        public OrderedKeyList() 
        {
            /* init map, create + wire sentinels */
            this.map = new Dictionary<int, Node>();
            this.head = new Node();
            this.tail = new Node();
            head.next = tail;
            tail.prev = head;
        }

        public void Add(int key) 
        {
            /* new node, map it, splice at MRU end (before tail) */
            Node newNode = new Node(key);
            map.Add(key, newNode);
            AddToMRU(newNode);
        }
        public void Remove(int key) 
        {
            /* find node via map, unlink, remove from map */
            Node nodeToRemove = map[key];
            Unlink(nodeToRemove);
            map.Remove(key);
        }
        public int RemoveLRU() 
        {
            /* node = head.next; unlink; remove from map; return its key */
            Node lruNode = head.next;
            Unlink(lruNode);
            map.Remove(lruNode.key);
            return lruNode.key;
        }
        public bool IsEmpty 
        { 
            get 
            { /* empty when head.next == tail */
                return (head.next == tail);
            } 
        }

        private void AddToMRU(Node node) 
        {
            node.prev = tail.prev;
            node.next = tail;
            tail.prev.next = node;
            tail.prev = node;
        }

        private void Unlink(Node node)
        {
            node.prev.next = node.next;
            node.next.prev = node.prev;
        }
    }
}
