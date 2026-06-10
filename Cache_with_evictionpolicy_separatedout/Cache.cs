using System;
using System.Collections.Generic;
using System.Text;

namespace Cache_with_evictionpolicy_separatedout
{
    public class Cache
    {
        private int capacity;
        private Dictionary<int, int> dict;
        private IEvictionPolicy policy;

        public Cache(int capacity, IEvictionPolicy policy)
        {
            this.capacity = capacity;
            this.policy = policy;
            this.dict = new Dictionary<int, int>();
        }

        public int Get(int key)
        {
            if (!dict.ContainsKey(key)) return -1;
            policy.RecordAccess(key);
            return dict[key];
        }

        public void Put(int key, int value)
        {
            if (dict.ContainsKey(key)) { dict[key] = value; policy.RecordAccess(key); return; }
            if (dict.Count == capacity) { int victim = policy.Evict(); dict.Remove(victim); }
            dict[key] = value;
            policy.RecordInsert(key);
        }
    }   
}
