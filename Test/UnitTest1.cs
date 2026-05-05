using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Logic;

namespace Tests
{
    public class UnitTest1
    {
        private Bolillero bolillero;
        public UnitTest1()  =>
            bolillero = new Bolillero(10, new Primero());
        
        
        [Fact]
        public void SacarBolilla()
        {
            Assert.Equal(10, bolillero.bolillas.Count);
            
            var bolillaCero = bolillero.SacarBolilla();

            Assert.Equal(0, bolillaCero);

            Assert.Equal(9, bolillero.bolillas.Count);

            Assert.Single(bolillero.BolillasSacadas);
        }

        [Fact]
        public void ReIngresar()
        {
            bolillero.SacarBolilla();
            bolillero.ReIngresar();

            Assert.Equal(10, bolillero.bolillas.Count);
            Assert.Empty(bolillero.BolillasSacadas);
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
    }
}