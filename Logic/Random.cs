using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Logic
{
    public class Random : IRandom
    {
        private Random r = new Random();
        public int Next(int max) => r.Next(max);
    }
}