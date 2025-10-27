using AwesomeAssertions;
using KatasTDD.Domain;

namespace KatasTDD.Test.Tenis;

public class TenisTests
{
    [Fact]
    public void Si_IniciaJuegoTenis_Debe_ElResultadoSerLove_Love()
    {
        var juegoTenis = new JuegoTenis(0,0);

        juegoTenis.ObtenerMarcador().Should().BeEquivalentTo("Love-Love");
    }
    
    [Fact]
    public void Si_PuntuacionDeUnJugadorEsEsNegativa_Debe_RetornarExcepcionTipoFueraRango()
    {
        var caller = () => new JuegoTenis(-1,0);
        
        caller.Should().ThrowExactly<ArgumentOutOfRangeException>().WithMessage("Los puntos no pueden ser negativos. (Parameter 'puntos')");
    }

    [Theory]
    [InlineData(1, 0, "Fifteen-Love")]
    [InlineData(0, 1, "Love-Fifteen")]
    public void Si_UnJugadorObtieneUnPunto_Y_SuOponenteObtieneCeroPuntos_Debe_ElPuntajeDelJugadorSerQuinceYSuOponenteLove(int puntosJugadorA, int puntosJugadorB, string puntuacionEsperada)
    {
        var juegoTenis = new JuegoTenis(puntosJugadorA, puntosJugadorB);

        juegoTenis.ObtenerMarcador().Should().BeEquivalentTo(puntuacionEsperada);
    }

    [Fact]
    public void Si_AmbosJugadoresRealizanDosPuntos_Debe_ElResultadoSerTreintaTreinta()
    {
        var juegoTenis = new JuegoTenis(2, 2);

        juegoTenis.ObtenerMarcador().Should().BeEquivalentTo("Thirty-Thirty");
    }

    [Theory]
    [InlineData(1, 3, "Fifteen-Forty")]
    [InlineData(3, 1, "Forty-Fifteen")]
    public void Si_UnJugadorObtieneTresPuntos_Y_SuOponenteObtieneUnPunto_Debe_ElPuntajeDelJugadorSerCuarentaYSuOponenteQuince(int puntosJugadorA, int puntosJugadorB, string puntuacionEsperada)
    {
        var juegoTennis = new JuegoTenis(puntosJugadorA, puntosJugadorB);

        juegoTennis.ObtenerMarcador().Should().BeEquivalentTo(puntuacionEsperada);
    }

    [Theory]
    [InlineData(4, 2, "Ganador-JugadorA")]
    [InlineData(2, 4, "Ganador-JugadorB")]
    public void Si_UnJugadorObteneMasDeTresPuntos_Y_SuOponenteObteneDosPuntosMenos_Debe_ElResultadoSerGanadorX(int puntosJugadorA, int puntosJugadorB, string puntuacionEsperada)
    {
        var juegoTennis = new JuegoTenis(puntosJugadorA,  puntosJugadorB);

        juegoTennis.ObtenerMarcador().Should().BeEquivalentTo(puntuacionEsperada);
    }

    [Fact]
    public void Si_AmbosJugadoresRealizanTresPuntos_Debe_ElResultadoSerDeuce()
    {
        var juegoTennis = new JuegoTenis(3,3);

        juegoTennis.ObtenerMarcador().Should().BeEquivalentTo("Deuce");
    }

    [Theory]
    [InlineData(4, 3, "Ventaja-JugadorA")]
    [InlineData(5, 6, "Ventaja-JugadorB")]
    public void Si_JuegoEstaEnDeuceYUnJugadorObtienePuntoAdicionalQueSuOponente_Debe_ElResultadoSerVentajaJugadorX(
        int puntosJugadorA, int puntosJugadorB, string puntuacionEsperada)
    {
        var juegoTennis = new JuegoTenis(puntosJugadorA, puntosJugadorB);

        juegoTennis.ObtenerMarcador().Should().BeEquivalentTo(puntuacionEsperada);
    }
}