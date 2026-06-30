using System.Text;

namespace leetcode_solutions
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            //var x = new int[] { 100,1,1000 };
            //var ans = MaximumElementAfterDecrementingAndRearranging(x);

            //var s = new string[] { "a", "a", "a" };
            //var w = "ab";
            //Console.WriteLine(NumOfStrings(s,w));

            Console.WriteLine(NumberOfSubstrings("abcabc"));
        }

        public static int MaximumElementAfterDecrementingAndRearranging(int[] arr)
        {
            Array.Sort(arr);
            if (arr[0] != 1) arr[0] = 1;
            
            int ans = -1;
            for (int i = 1; i < arr.Length; i++)
            {                
                int absDiff = Math.Abs(arr[i] - arr[i - 1]);
                if (absDiff > 1)
                {
                    arr[i] = arr[i - 1] + 1;
                }
                ans = Math.Max(ans, arr[i]);
            }
            return ans;
        }

        public static int NumOfStrings(string[] patterns, string word)
        {
            int ans = 0;

            HashSet<string> set = new HashSet<string>();
            //make all subsstrings of word and store it in a hashset
            for (int i = 0; i < word.Length; i++)
            {
                StringBuilder str = new StringBuilder();
                for (int j = i; j < word.Length; j++)
                {
                    str.Append(word[j]);
                    string s = str.ToString();
                    if (!set.Contains(s)) set.Add(s);
                }
            }
            //loop over patterns array and check if its present in word
            for (int i = 0; i < patterns.Length; i++)
            {
                if (set.Contains(patterns[i])) ans++;
            }

            return ans;
        }

        public static int NumberOfSubstrings(string s)
        {
            int count = 0;

            //O[N*N*N]
            //for (int k = 0; k < s.Length; k++)
            //{
            //    for (int i = k; i < s.Length; i++)
            //    {
            //        bool isA = false;
            //        bool isB = false;
            //        bool isC = false;

            //        for (int j = k; j <= i; j++)
            //        {
            //            if (s[j] == 'a') isA = true;
            //            if (s[j] == 'b') isB = true;
            //            if (s[j] == 'c') isC = true;
            //        }
            //        if (isA && isB && isC) count++;
            //    }
            //}

            //O[N]
            int l = 0;
            int r = 0;
            var dict = new Dictionary<char, int>() { {'a', 0 }, { 'b', 0 } , { 'c', 0 } };
            int len = s.Length;
            bool insert = true;
            while (l <= r && r < len)
            {
                if (insert && (s[r]=='a' || s[r] == 'b' || s[r] == 'c'))
                {
                    dict[s[r]] += 1;
                }

                if (dict['a'] > 0 && dict['b'] > 0 && dict['c'] > 0) 
                {
                    count = count + 1 + (len - 1 - r);
                    dict[s[l]] -= 1;
                    l += 1;
                    insert = false;
                } 
                else 
                {
                    r += 1;
                    insert = true;
                }
            }

            while (l < len)
            {
                if (dict['a'] > 0 && dict['b'] > 0 && dict['c'] > 0)
                {
                    count = count + 1;                                      
                }
                dict[s[l]] -= 1;
                l += 1;
            }


            return count;
        }
    }

}
