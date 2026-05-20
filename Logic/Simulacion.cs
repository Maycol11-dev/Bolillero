using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Logic
{
    public class Simulacion
    {
        public long simularSinHilos(Bolillero bolillero, List<byte> jugada, int simulaciones) => 
            bolillero.JugarMasVeces(jugada, simulaciones);
        

        public long simularConHilos(Bolillero bolillero, List<byte> jugada, int simulaciones, int Hilos)
        {
            long vecesGanadas = 0;
            int simulacionesPorHilo = simulaciones / Hilos;
            int resto = simulaciones % Hilos;

            Task<int>[] tareas = new Task<int>[Hilos];

            for (int indice = 0; indice < Hilos; indice++)
            {   
                Bolillero bolilleroClonado = (Bolillero)bolillero.Clone();

                int CantSimulaciones = indice == Hilos-1 ?
                simulacionesPorHilo+resto : simulacionesPorHilo ;

                tareas[indice] = Task.Run( () =>
                    bolilleroClonado.JugarMasVeces(jugada, CantSimulaciones)
                );
            }
            
            Task.WaitAll(tareas);

            vecesGanadas = tareas.Sum(t => t.Result);

            return vecesGanadas;
        }

        public async Task<long> simularConHilosAsync(Bolillero bolillero, List<byte> jugada, int simulaciones, int Hilos)
        {
            long vecesGanadas = 0;
            int simulacionesPorHilo = simulaciones / Hilos;
            int resto = simulaciones % Hilos;

            Task<int>[] tareas = new Task<int>[Hilos];

            for (int indice = 0; indice < Hilos; indice++)
            {   
                Bolillero bolilleroClonado = (Bolillero)bolillero.Clone();

                int CantSimulaciones = indice == Hilos-1 ?
                simulacionesPorHilo+resto : simulacionesPorHilo ;

                tareas[indice] = Task.Run( () =>
                    bolilleroClonado.JugarMasVeces(jugada, CantSimulaciones)
                );
            }
            
            await Task.WhenAll(tareas);

            vecesGanadas = tareas.Sum(t => t.Result);

            return vecesGanadas;
        }
    }
}