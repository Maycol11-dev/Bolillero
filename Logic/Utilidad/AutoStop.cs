using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Logic.Utilidad
{
    public class AutoStop
    {
        public static long StartNewSync(Action a)
        {
            var inicio = Stopwatch.StartNew();
            a();
            inicio.Stop();
            return inicio.ElapsedMilliseconds;
        }
        
        public async static Task<long> StartNewAsync(Func<Task> a)
        {
            var inicio = Stopwatch.StartNew();
            await a();
            inicio.Stop();

            return inicio.ElapsedMilliseconds;
        }
    }
}