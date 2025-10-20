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
}

public class JuegoTenis
{
    private string _puntuacion = "Love-Love";
    public string ObtenerPuntuacion()
    {
        return _puntuacion;
    }
}