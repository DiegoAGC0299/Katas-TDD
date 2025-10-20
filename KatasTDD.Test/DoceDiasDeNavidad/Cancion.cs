using System.Text;

namespace KatasTDD.Test.DoceDiasDeNavidad;

public class Cancion
{
    private const string NuevaLinea = "\n";
    private string _letra = string.Empty;
    public void ConstruirCancion(int dia)
    {
        if (dia < 1 || dia > 12)
            throw new ArgumentOutOfRangeException(nameof(dia), "El día debe estar entre 1 y 12.");

        ConstruirLineas(dia);
    }

    private void ConstruirLineas(int dia)
    {
        var sb = new StringBuilder();
        var ordinario = OrdinalNumero(dia);
        
        var inicial = dia == 1 
            ? $"El {ordinario} día de navidad{NuevaLinea} Mi verdadero amor me regaló"
            : $"En el {ordinario} día de navidad,{NuevaLinea} Mi verdadero amor me regaló:";
        
        sb.Append(inicial);
        
        for(int i = dia; i >= 1; i--)
        {
            sb.Append(NuevaLinea);

            if (i == 1 && dia > 1)
                sb.Append(" Y");
            
            if (_regalosDia.TryGetValue(i, out var regalo) && !string.IsNullOrEmpty(regalo))
            {
                sb.Append($" {regalo}");
                sb.Append(i == 1 ? "." : ",");
            }
        }
        
        _letra = sb.ToString();
    }

    public string ImprimirLetra()
    {
        return _letra;
    }

    private static readonly Dictionary<int, string> _regalosDia = new()
    {
        { 1, "Una perdiz en un árbol de peras" },
        { 2, "Dos tórtolas" },
        { 3, "Tres gallinas francesas" },
        { 4, "Cuatro pájaros que llaman" },
        { 5, "Cinco anillos de oro" },
        { 6, "Seis gansos poniendo" },
        { 7, "Siete cisnes nadando" },
        { 8, "Ocho doncellas ordeñando" },
        { 9, "Nueve damas bailando" },
        { 10, "Diez señores saltando" },
        { 11, "Once gaiteros" },
        { 12, "Doce tamborileros tocando el tambor" }
    };

    private string OrdinalNumero(int numero) => numero switch
    {
        1 => "primer",
        2 => "segundo",
        3 => "tercer",
        4 => "cuarto",
        5 => "quinto",
        6 => "sexto",
        7 => "séptimo",
        8 => "octavo",
        9 => "noveno",
        10 => "décimo",
        11 => "undécimo",
        12 => "duodécimo",
        _ => numero.ToString()
    };


}