using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlatAuth.Business.Service
{
    public class RandomNumberGenerator
    {
        private static Random random = new Random();
        public static string DigitGen(int length = 6)
        {
            String r = random.Next(0, 1000000).ToString($"D{length}");
            return r;
        }


        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }


    }
}
