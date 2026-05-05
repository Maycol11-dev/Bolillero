using System.IO.Compression;
using System.Security.Cryptography.X509Certificates;

namespace Logic;

public class Bolillero : IBolillero
{
    public List<byte> bolillas {get; set;} = new();
    public List<byte> BolillasSacadas {get; set;} = new();
    public IRandom r;
    public Bolillero(int cantidad, IRandom r)
    {
        for( byte i=0; i < cantidad ; i++ ) bolillas.Add(i);
        this.r = r;
    }

    public byte SacarBolilla()
    {
        var indice = r.Next(bolillas.Count);
        byte bolilla = bolillas[indice];
 
        bolillas.RemoveAt(indice);

        BolillasSacadas.Add(bolilla);

        return bolilla;
    }
    public bool Jugar(List<byte> jugada)
    {
        ReIngresar();

        foreach (byte bolillaEsperada in jugada)
        {
            byte bolillaSacada = SacarBolilla();
            if(bolillaSacada != bolillaEsperada ) 
                return false;
            
        }

        return true;
    }

    public int JugarMasVeces(List<byte> jugada,int JugarNVeces)
    {   
        int rondasGanadas = 0;

        for (int i = 0; i < JugarNVeces; i++)
        {
            if (Jugar(jugada))
                rondasGanadas++;
        }
    
        return rondasGanadas;
    }
    public void ReIngresar()
    {
        bolillas.AddRange(BolillasSacadas);
        BolillasSacadas.Clear();
    }
}
