using System.Text;

namespace KatasTDD.Domain;

public static class NumeracionRomana
{
    private const int NumeroMinimo = 1;
    private const int NumeroMaximo = 3999;
    
    private static readonly Dictionary<int, string> ValoresRomanos = new()
    {
        { 1000, "M" },
        { 900, "CM" },
        { 500, "D" },
        { 400, "CD" },
        { 100, "C" },
        { 90, "XC" },
        { 50, "L" },
        { 40, "XL" },
        { 10, "X" },
        { 9, "IX" },
        { 5, "V" },
        { 4, "IV" },
        { 1, "I" },
    };
    public static string ConvertirNumeroARomano(int numero)
    {
        ValidarRangoPermitido(numero);
        return ConstruirNumeroRomano(numero);
    }
    
    private static void ValidarRangoPermitido(int numero)
    {
        if(numero is < NumeroMinimo or > NumeroMaximo)
            throw new ArgumentOutOfRangeException(nameof(numero), $"El número debe estar entre {NumeroMinimo} y {NumeroMaximo}.");
    }

    private static string ConstruirNumeroRomano(int numero)
    {
        var numeroRomanoCalculado = new StringBuilder();
        var numeroResultante = numero;

        foreach (var valorRomano in ValoresRomanos)
        {
            while (numeroResultante >= valorRomano.Key)
            {
                numeroRomanoCalculado.Append(valorRomano.Value);
                numeroResultante -= valorRomano.Key;
            }
        }

        return numeroRomanoCalculado.ToString();
    }
}