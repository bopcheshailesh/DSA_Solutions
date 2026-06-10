using System;
using System.Collections.Generic;
using System.Text;

namespace Cache_with_evictionpolicy_separatedout
{
    public class LFUPolicy : IEvictionPolicy
    {
        private Dictionary<int, int> keyFreq;                  // key -> current frequency (shelf number)
        private Dictionary<int, OrderedKeyList> freqLists;     // frequency -> shelf
        private int minFreq;

        public LFUPolicy()
        {
            keyFreq = new Dictionary<int, int>();
            freqLists = new Dictionary<int, OrderedKeyList>();
            minFreq = 0;
        }

        public void RecordInsert(int key)
        {
            // freq = 1; record keyFreq; ensure shelf 1 exists; add key to shelf 1; minFreq = 1
            int freq = 1;

            if(!keyFreq.ContainsKey(key)) keyFreq.Add(key, freq);

            AddToShelf(key, freq);
            
            minFreq = freq;
        }

        public void RecordAccess(int key)
        {
            // f = keyFreq[key]; remove key from shelf f;
            int existingFrequency = keyFreq[key];
            OrderedKeyList existingShelf = freqLists[existingFrequency];
            existingShelf.Remove(key);

            int newFrequency = existingFrequency + 1;
            
            // if shelf f now empty: (maybe clean it up) and if f == minFreq, minFreq = f + 1;
            if (existingShelf.IsEmpty && existingFrequency == minFreq) minFreq = newFrequency;

            // ensure shelf f+1 exists; add key to shelf f+1; keyFreq[key] = f + 1
            keyFreq[key] = newFrequency;
            AddToShelf(key, newFrequency);
        }

        public int Evict()
        {
            // shelf = freqLists[minFreq]; victim = shelf.RemoveLRU(); keyFreq.Remove(victim);
            OrderedKeyList LFUShelf = freqLists[minFreq];
            int lruNodeInLFUShelf = LFUShelf.RemoveLRU();
            keyFreq.Remove(lruNodeInLFUShelf);

            // (note: minFreq will be fixed by the next RecordInsert) ; return victim
            return lruNodeInLFUShelf;
        }

        private void AddToShelf(int key, int freq)
        {
            if (!freqLists.ContainsKey(freq))
                freqLists.Add(freq, new OrderedKeyList());
            freqLists[freq].Add(key);
        }
    }
}
