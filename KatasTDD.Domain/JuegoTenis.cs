namespace KatasTDD.Domain;

public class JuegoTenis
{
    private const int PuntosParaDeuce = 3;
    private const int PuntosMinimosParaGanar = 4;
    private const int DiferenciaParaVentaja = 1;
    private const int DiferenciaParaGanar = 2;
    private readonly int _puntosJugadorA;
    private readonly int _puntosJugadorB;

    public JuegoTenis(int puntosJugadorA, int puntosJugadorB)
    {
        ValidarPuntos(puntosJugadorA);
        ValidarPuntos(puntosJugadorB);

        _puntosJugadorA = puntosJugadorA;
        _puntosJugadorB = puntosJugadorB;
    }
    
    public string ObtenerMarcador()
    {
        if (EsDeuce())
            return "Deuce";

        if (HayVentajaOGanador(out var jugador, out var esGanador))
            return esGanador ? $"Ganador-{jugador}" : $"Ventaja-{jugador}";
        
        return ObtenerMarcadorBase();
    }
    
    private static void ValidarPuntos(int puntos)
    {
        if (puntos < 0)
            throw new ArgumentOutOfRangeException( nameof(puntos), "Los puntos no pueden ser negativos.");
    }
    
    private bool EsDeuce()
        => _puntosJugadorA >= PuntosParaDeuce
               && _puntosJugadorA == _puntosJugadorB;

    private bool HayVentajaOGanador(out string jugador, out bool esGanador)
    {
        jugador = string.Empty;
        esGanador = false;

        if (!EstanEnFaseDecisiva())
            return false;

        var diferencia = _puntosJugadorA - _puntosJugadorB;
        var diferenciaAbsoluta = Math.Abs(diferencia);

        if (diferenciaAbsoluta < DiferenciaParaVentaja)
            return false;

        jugador = ObtenerJugadorSegunDiferencia(diferencia);

        if (diferenciaAbsoluta >= DiferenciaParaGanar)
            esGanador = true;

        return true;
    }
    
    private bool EstanEnFaseDecisiva()
        => _puntosJugadorA >= PuntosMinimosParaGanar
               || _puntosJugadorB >= PuntosMinimosParaGanar;
    

    private static string ObtenerJugadorSegunDiferencia(int diferencia)
        => diferencia > 0 ? "JugadorA" : "JugadorB";

    private string ObtenerMarcadorBase()
    {
        var marcadorA = ConvertirPuntosAMarcador(_puntosJugadorA);
        var marcadorB = ConvertirPuntosAMarcador(_puntosJugadorB);

        return $"{marcadorA}-{marcadorB}";
    }

    private static string ConvertirPuntosAMarcador(int puntos) => puntos switch
    {
        0 => "Love",
        1 => "Fifteen",
        2 => "Thirty",
        3 => "Forty",
        _ => puntos.ToString()
    };
}
