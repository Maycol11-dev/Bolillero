using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using System.Diagnostics;
using Logic;
using Logic.Utilidad;

namespace Tests
{
    public class UnitTest1
    {
        private Bolillero bolillero;
        private Simulacion s = new Simulacion();
        private List<byte> jugada =new List<byte>(){ 0, 1, 2, 3};
        public UnitTest1() => bolillero = new Bolillero(10, new Primero());

        [Fact]
        public void SeClona_a_si_Mismo()
        {
            var bolilleroClonado = (Bolillero)bolillero.Clone();

            Assert.Equal(10, bolilleroClonado.bolillas.Count);
        }
        [Fact]
        public void SacarBolilla()
        {
            Assert.Equal(10, bolillero.bolillas.Count);
            
            var bolillaCero = bolillero.SacarBolilla();

            Assert.Equal(0, bolillaCero);

            Assert.Equal(9, bolillero.bolillas.Count);

            Assert.Single(bolillero.bolillasSacadas);
        }

        [Fact]
        public void ReIngresar()
        {
            bolillero.SacarBolilla();
            bolillero.ReIngresar();

            Assert.Equal(10, bolillero.bolillas.Count);
            Assert.Empty(bolillero.bolillasSacadas);
        }

        [Fact]
        public void JugarGana()
        {
            Assert.True(bolillero.Jugar(new List<byte>{0,1,2,3}));
        }

        [Fact]
        public void JugarPierde()
        {
            Assert.False(bolillero.Jugar(new List<byte>(){4,2,1}));
        }

        [Fact]
        public void GanarNVeces()
        {
            var rondasGanadas = bolillero.JugarMasVeces(new List<byte>(){0, 1}, 1);
            Assert.Equal(1,rondasGanadas);
        }

        [Fact]
        public void TiempoConHilos_es_menor_a_SinHilos()
        {
            int cantSimulaciones = 15_000_000;
            // var tiempoSinHilos = Stopwatch.StartNew();
            // s.simularSinHilos(bolillero, jugada, cantSimulaciones);
            // tiempoSinHilos.Stop();

            // var tiempoConHilos = Stopwatch.StartNew();
            // s.simularConHilos(bolillero, jugada, cantSimulaciones, 4);
            // tiempoConHilos.Stop();
            
            // var miliSin = tiempoSinHilos.ElapsedMilliseconds;
            // var miliCon = tiempoConHilos.ElapsedMilliseconds;

            var miliSin = AutoStop.StartNewSync(() =>
                s.simularSinHilos(bolillero, jugada, cantSimulaciones));
            var miliCon = AutoStop.StartNewSync(() =>
                s.simularConHilos(bolillero, jugada, cantSimulaciones, 4));

            Assert.True(miliCon < miliSin);
        }
        
        // [Fact]
        // public async Task TiempoSincronico_es_mayor_a_Asincronico()
        // {
        //     int cantSimulaciones = 200_000;
        //     var timeAsync = await AutoStop.StartNewAsync(async () => 
        //     {
        //         Task<long>[] SimulaAsync = new Task<long>[50];
        //         for (int i = 0; i < 50; i++)
        //         {               
        //             SimulaAsync[i] = Task.Run(() => s.simularConHilosAsync(bolillero, jugada, cantSimulaciones, 4));
        //         }

        //         await Task.WhenAll(SimulaAsync);
        //     });

        //     var timeSync = AutoStop.StartNewSync(() =>
        //     {
        //         for (int i = 0; i < 20; i++)
        //         {
        //             s.simularConHilos(bolillero, jugada, cantSimulaciones, 4);
        //         }
        //     });

        //     Assert.True(timeSync > timeAsync);
        //}
    }
}