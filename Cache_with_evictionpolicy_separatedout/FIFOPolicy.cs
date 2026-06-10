using System;
using System.Collections.Generic;
using System.Text;

namespace Cache_with_evictionpolicy_separatedout
{
    public class FIFOPolicy : IEvictionPolicy
    {
        private Queue<int> queue;

        public FIFOPolicy() 
        {
            this.queue = new Queue<int>();
        }

        public int Evict()
        {
            int evictionKey = queue.Dequeue();
            return evictionKey;
        }

        public void RecordAccess(int key)
        {
            //not required in this case
            //keep it unimplemented
        }

        public void RecordInsert(int key)
        {
            queue.Enqueue(key);
        }
    }
}
