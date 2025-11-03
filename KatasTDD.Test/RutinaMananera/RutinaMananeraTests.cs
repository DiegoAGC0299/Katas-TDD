using AwesomeAssertions;

namespace KatasTDD.Test.RutinaMananera;

public class RutinaMananeraTests
{
    [Fact]
    public void Si_HoraEstaEntre0600y0700_Debe_MostrarHacerEjercicio()
    {
        var rutinaMananera = new RutinaMananera();

        var actividadParaRealizar = rutinaMananera.SolicitarActividadParaRealizar("06:00");

        actividadParaRealizar.Should().BeEquivalentTo("Hacer ejercicio");
    }
}

public class RutinaMananera
{
    public object SolicitarActividadParaRealizar(string hora)
    {
        return "Hacer ejercicio";
    }
}