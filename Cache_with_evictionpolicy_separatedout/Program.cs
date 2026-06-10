namespace Cache_with_evictionpolicy_separatedout
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var lru = new Cache(2, new LRUPolicy());
            lru.Put(1,10);
            lru.Put(2,20);
            lru.Get(1);
            lru.Put(3, 30);
            lru.Get(2);
            lru.Get(3);
            lru.Get(1);

            var lfu = new Cache(2, new LFUPolicy());
            lfu.Put(1, 10);
            lfu.Put(2, 20);
            lfu.Get(1);
            lfu.Put(3, 30);
            lfu.Get(2);
            lfu.Get(3);
            lfu.Get(1);
        }
    }
}
