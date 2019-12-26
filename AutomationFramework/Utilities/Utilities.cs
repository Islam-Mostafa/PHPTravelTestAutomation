using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationFramework.Utilities
{
    public class Utilities
    {
        private static readonly Random Getrandom = new Random();
        private static readonly object SyncLock = new object();

        public static int GetRandomNumber(int min, int max)
        {
            lock (SyncLock)
            {
                // synchronize
                return Getrandom.Next(min, max);
            }
        }
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            lock (SyncLock)
            {
                return new string(Enumerable.Repeat(chars, length)
                    .Select(s => s[Getrandom.Next(s.Length)]).ToArray());
            }
        }
      
        public static bool ContainsAll(List<string> a, List<string> check)
        {
            var l = new List<string>(check);
            foreach (string t in a)
            {
                if (check.Contains(t))
                {
                    check.Remove(t);
                    if (check.Count == 0)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
