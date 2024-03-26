using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace website_shopping.Utils
{
    public static class RandomUtils
    {
        public static string NextString(this Random random, int length)
        {
            const string chars = "AaBbCcDdEeFfGgHhIiJjKkLlMmNnOoPpQqRrSsTtUuVvWwXxYyZz0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}