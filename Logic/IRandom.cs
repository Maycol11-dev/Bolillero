using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Logic
{
    public interface IRandom
    {
        int Next(int max);
    }
}