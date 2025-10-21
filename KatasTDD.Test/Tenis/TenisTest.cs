using AwesomeAssertions;

namespace KatasTDD.Test.Tenis;

public class TenisTest
{
    [Fact]
    public void Si_IniciaJuegoTenis_Debe_ElResultadoSerLove_Love()
    {
        var juegoTennis = new JuegoTenis();

        juegoTennis.ObtenerPuntuacion().Should().BeEquivalentTo("Love-Love");
    }

    [Theory]
    [InlineData(1,0,"Fifteen-Love")]
    [InlineData(0,1,"Love-Fifteen")]
    public void Si_UnJugadorObtieneUnPunto_Y_SuOponenteObtieneCeroPuntos_Debe_ElPuntajeDelJugadorSerQuinceYSuOponenteLove(int puntosJugadorA, int puntosJugadorB, string puntuacionEsperada)
    {
        var juegoTennis = new JuegoTenis();
        
        juegoTennis.AgregarPuntuacionJugadorA(puntosJugadorA);
        juegoTennis.AgregarPuntuacionJugadorB(puntosJugadorB);
        
        juegoTennis.ObtenerPuntuacion().Should().BeEquivalentTo(puntuacionEsperada);
    }

    [Fact]
    public void Si_AmbosJugadoresRealizanDosPuntos_Debe_ElResultadoSerTreintaTreinta()
    {
        var juegoTennis = new JuegoTenis();
        
        juegoTennis.AgregarPuntuacionJugadorA(2);
        juegoTennis.AgregarPuntuacionJugadorB(2);
        
        juegoTennis.ObtenerPuntuacion().Should().BeEquivalentTo("Thirty-Thirty");
    }
    
    [Theory]
    [InlineData(1,3,"Fifteen-Forty")]
    [InlineData(3,1,"Forty-Fifteen")]
    public void Si_UnJugadorObtieneTresPuntos_Y_SuOponenteObtieneUnPunto_Debe_ElPuntajeDelJugadorSerCuarentaYSuOponenteQuince(int puntosJugadorA, int puntosJugadorB, string puntuacionEsperada)
    {
        var juegoTennis = new JuegoTenis();
        
        juegoTennis.AgregarPuntuacionJugadorA(puntosJugadorA);
        juegoTennis.AgregarPuntuacionJugadorB(puntosJugadorB);
        
        juegoTennis.ObtenerPuntuacion().Should().BeEquivalentTo(puntuacionEsperada);
    }

    [Theory]
    [InlineData(4,2,"Ganador-JugadorA")]
    [InlineData(2,4,"Ganador-JugadorB")]
    public void Si_UnJugadorObteneMasDeTresPuntos_Y_SuOponenteObteneDosPuntosMenos_Debe_ElResultadoSerGanadorX(int puntosJugadorA, int puntosJugadorB, string puntuacionEsperada)
    {
        var juegoTennis = new JuegoTenis();
        
        juegoTennis.AgregarPuntuacionJugadorA(puntosJugadorA);
        juegoTennis.AgregarPuntuacionJugadorB(puntosJugadorB);
        
        juegoTennis.ObtenerPuntuacion().Should().BeEquivalentTo(puntuacionEsperada);
    }
    
    [Fact]
    public void Si_AmbosJugadoresRealizanTresPuntos_Debe_ElResultadoSerDeuce()
    {
        var juegoTennis = new JuegoTenis();
        
        juegoTennis.AgregarPuntuacionJugadorA(3);
        juegoTennis.AgregarPuntuacionJugadorB(3);
        
        juegoTennis.ObtenerPuntuacion().Should().BeEquivalentTo("Deuce");
    }
        
    
    
}

public class JuegoTenis
{
    private string _puntuacion = "Love-Love";
    private int _puntajeJugadorA;
    private int _puntajeJugadorB;
    private static string PuntuacionObtenida(int puntuacion) => puntuacion switch
    {
        0 => "Love",
        1 => "Fifteen",
        2 => "Thirty",
        3 => "Forty",
        _ => puntuacion.ToString()
    };
    
    public void AgregarPuntuacionJugadorA(int puntosJugadorA) =>  _puntajeJugadorA = puntosJugadorA;
    public void AgregarPuntuacionJugadorB(int puntosJugadorB) =>  _puntajeJugadorB = puntosJugadorB;

    private bool JugadorGanador()
    {
        if (_puntajeJugadorA >= 4 || _puntajeJugadorB >= 4)
        {
            int diferencia = _puntajeJugadorA - _puntajeJugadorB;

            if (Math.Abs(diferencia) >= 2)
            {
                _puntuacion = diferencia > 0 ? "Ganador-JugadorA" : "Ganador-JugadorB";
                return true;
            }
        }

        return false;
    }

    private bool Deuce()
    {
        if (_puntajeJugadorA >= 3 && _puntajeJugadorB == _puntajeJugadorA)
        {
            _puntuacion = "Deuce";
            return true;
        }
        return false;
    }
    
    
    private void CalcularPuntuacion()
    {
        if (!JugadorGanador() && !Deuce())
            _puntuacion = $"{PuntuacionObtenida(_puntajeJugadorA)}-{PuntuacionObtenida(_puntajeJugadorB)}";
    }
        
    
    public string ObtenerPuntuacion()
    {
        CalcularPuntuacion();
        return _puntuacion;
    }
}