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
    
    
}

public class JuegoTenis
{
    private string _puntuacion = "Love-Love";
    private int _puntajeJugadorA = 0;
    private int _puntajeJugadorB = 0;
    
    public void AgregarPuntuacionJugadorA(int puntosJugadorA) =>  _puntajeJugadorA = puntosJugadorA;
    public void AgregarPuntuacionJugadorB(int puntosJugadorB) =>  _puntajeJugadorB = puntosJugadorB;
    
    private void CalcularPuntuacion()
    {
        _puntuacion = $"{PuntuacionObtenida(_puntajeJugadorA)}-{PuntuacionObtenida(_puntajeJugadorB)}";
    }
    public string ObtenerPuntuacion()
    {
        CalcularPuntuacion();

        return _puntuacion;
    }

    public string PuntuacionObtenida(int puntuacion) => puntuacion switch
    {
        0 => "Love",
        1 => "Fifteen",
        2 => "Thirty",
        _ => puntuacion.ToString()
    };
}