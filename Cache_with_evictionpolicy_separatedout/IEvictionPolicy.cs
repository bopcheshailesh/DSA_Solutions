using System;
using System.Collections.Generic;
using System.Text;

namespace Cache_with_evictionpolicy_separatedout
{
    public interface IEvictionPolicy
    {
        public void RecordInsert(int key); // a new key entered the cache
        public void RecordAccess(int key); // an existing key was read or updated
        public int Evict(); // decide & return the key to remove
    }
}
