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
}

public class JuegoTenis
{
    private string _puntuacion = "Love-Love";
    private int _puntajeJugadorA = 0;
    private int _puntajeJugadorB = 0;
    public string ObtenerPuntuacion()
    {
        return _puntuacion;
    }

    public void AgregarPuntuacionJugadorA(int puntosJugadorA)
    {
        throw new NotImplementedException();
    }

    public void AgregarPuntuacionJugadorB(int puntosJugadorB)
    {
        throw new NotImplementedException();
    }
}