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
    
    [Fact]
    public void Si_HoraEstaEntre0800y0859_Debe_MostrarDesayunar()
    {
        var rutinaMananera = new RutinaMananera();

        var actividadParaRealizar = rutinaMananera.SolicitarActividadParaRealizar("08:00");

        actividadParaRealizar.Should().BeEquivalentTo("Desayunar");
    }
}

public class RutinaMananera
{
    public string SolicitarActividadParaRealizar(string hora)
    {
        var actividad = string.Empty;
        var horaCalculada = TimeSpan.Parse(hora);

        var rangoEntre0600Y0659 = horaCalculada >= new TimeSpan(6, 0, 0) && horaCalculada < new TimeSpan(7, 0, 0);
        var rangoEntre0700Y0759 = horaCalculada >= new TimeSpan(7, 0, 0) && horaCalculada < new TimeSpan(8, 0, 0);   
        var rangoEntre0800Y0859 = horaCalculada >= new TimeSpan(8, 0, 0) && horaCalculada < new TimeSpan(9, 0, 0);   
        
        if (rangoEntre0600Y0659)
            actividad = "Hacer ejercicio";
        
        if (rangoEntre0700Y0759)
            actividad = "Leer y estudiar";
        
        if (rangoEntre0800Y0859)
            actividad = "Desayunar";
        
        return actividad;
    }
}