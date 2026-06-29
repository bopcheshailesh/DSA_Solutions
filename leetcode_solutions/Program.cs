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

            var s = new string[] { "a", "a", "a" };
            var w = "ab";
            Console.WriteLine(NumOfStrings(s,w));
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
    }

}
