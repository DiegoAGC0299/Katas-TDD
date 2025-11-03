using AwesomeAssertions;

namespace KatasTDD.Test.RutinaMananera;

public class RutinaMananeraTests
{
    [Fact]
    public void Si_HoraEstaEntre0600y0659_Debe_MostrarHacerEjercicio()
    {
        var rutinaMananera = new RutinaMananera();

        var actividadParaRealizar = rutinaMananera.SolicitarActividadParaRealizar("06:00");

        actividadParaRealizar.Should().BeEquivalentTo("Hacer ejercicio");
    }

    [Fact]
    public void Si_HoraEstaEntre0700y0759_Debe_MostrarLeerYEstudiar()
    {
        var rutinaMananera = new RutinaMananera();

        var actividadParaRealizar = rutinaMananera.SolicitarActividadParaRealizar("07:00");

        actividadParaRealizar.Should().BeEquivalentTo("Leer y estudiar");
    }
}

public class RutinaMananera
{
    public string SolicitarActividadParaRealizar(string hora)
    {
        var horaCalculada = TimeSpan.Parse(hora);

        var rangoEntre0600Y0659 = horaCalculada >= new TimeSpan(6, 0, 0) && horaCalculada < new TimeSpan(7, 0, 0);
        
        return rangoEntre0600Y0659 ? "Hacer ejercicio" : "Leer y estudiar";
    }
}