using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDevExperience
{
    public static class RandomHelper
    {
        static Random rand = new Random();

        public static int Next() => rand.Next();

        public static int Next(int max) => rand.Next(max);

        public static int Next(int min, int max) => rand.Next(min, max);
    }
}
