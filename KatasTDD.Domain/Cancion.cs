using System.Text;

namespace KatasTDD.Domain;

public static class Cancion
{
    private const string NuevaLinea = "\n";
    private const int PrimerDia = 1;
    private const int UltimoDia = 12;

    public static string GenerarLetraParaDia(int dia)
    {
        ValidarDia(dia);
        return ConstruirCancionCompleta(dia);
    }

    private static void ValidarDia(int dia)
    {
        if (dia is < PrimerDia or > UltimoDia)
            throw new ArgumentOutOfRangeException(nameof(dia), $"El día debe estar entre {PrimerDia} y {UltimoDia}.");
    }

    private static string ConstruirCancionCompleta(int dia)
    {
        var encabezado = ConstruirEncabezado(dia);
        var regalos = ConstruirListaRegalos(dia);
        return $"{encabezado}{regalos}";
    }

    private static string ConstruirEncabezado(int dia)
    {
        var numeroOrdinal = ObtenerNumeroOrdinal(dia);
        return dia == PrimerDia
            ? $"El {numeroOrdinal} día de navidad{NuevaLinea} Mi verdadero amor me regaló"
            : $"En el {numeroOrdinal} día de navidad,{NuevaLinea} Mi verdadero amor me regaló:";
    }

    private static string ConstruirListaRegalos(int dia)
    {
        var stringBuilderRegalos = new StringBuilder();

        for (var diaActual = dia; diaActual >= PrimerDia; diaActual--)
        {
            stringBuilderRegalos.Append(NuevaLinea);
            stringBuilderRegalos.Append(FormatearRegalo(diaActual, dia));
        }

        return stringBuilderRegalos.ToString();
    }

    private static string FormatearRegalo(int diaRegalo, int diaCancion)
    {
        var regalo = RegalosPorDia[diaRegalo];
        var prefijo = ObtenerPrefijoRegalo(diaRegalo, diaCancion);
        var sufijo = ObtenerSufijoRegalo(diaRegalo);
        return $"{prefijo}{regalo}{sufijo}";
    }

    private static string ObtenerPrefijoRegalo(int diaRegalo, int diaCancion)
        => diaRegalo == PrimerDia && diaCancion > PrimerDia
            ? " Y "
            : " ";

    private static string ObtenerSufijoRegalo(int diaRegalo)
        => diaRegalo == PrimerDia ? "." : ",";

    private static readonly Dictionary<int, string> RegalosPorDia = new()
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

    private static string ObtenerNumeroOrdinal(int numero) => numero switch
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