namespace Logic;

public interface IBolillero
{
    byte SacarBolilla();
    bool Jugar(List<byte> bolillas);
    int JugarMasVeces(List<byte> bolillas, int JugarNVeces);
    void ReIngresar();
}
