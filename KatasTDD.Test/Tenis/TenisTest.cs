using AwesomeAssertions;

namespace KatasTDD.Test.Tenis;

public class TenisTest
{
    [Fact]
    public void Si_IniciaJuegoTenis_Debe_ElResultadoSerCero()
    {
        var juegoTennis = new JuegoTenis();

        juegoTennis.ObtenerPuntuacion().Should().BeEquivalentTo("0-0");
    }
}

public class JuegoTenis
{
    public object ObtenerPuntuacion()
    {
        throw new NotImplementedException();
    }
}