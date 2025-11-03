using AwesomeAssertions;
using KatasTDD.Domain.RutinaMananera;

namespace KatasTDD.Test.RutinaMananera;

public class RutinaMananeraTests
{
    [Fact]
    public void Si_HoraEstaEntre0600y0659_Debe_MostrarHacerEjercicio()
    {
        var rutinaMananera = new RutinaMananeraBuilder().ActividadesPorDefecto().Build();

        var actividadParaRealizar = rutinaMananera.ConsultarActividad("06:00");

        actividadParaRealizar.Should().BeEquivalentTo("Hacer ejercicio");
    }

    [Fact]
    public void Si_HoraEstaEntre0700y0759_Debe_MostrarLeerYEstudiar()
    {
        var rutinaMananera = new RutinaMananeraBuilder().ActividadesPorDefecto().Build();

        var actividadParaRealizar = rutinaMananera.ConsultarActividad("07:00");

        actividadParaRealizar.Should().BeEquivalentTo("Leer y estudiar");
    }
    
    [Fact]
    public void Si_HoraEstaEntre0800y0859_Debe_MostrarDesayunar()
    {
        var rutinaMananera = new RutinaMananeraBuilder().ActividadesPorDefecto().Build();

        var actividadParaRealizar = rutinaMananera.ConsultarActividad("08:00");

        actividadParaRealizar.Should().BeEquivalentTo("Desayunar");
    }
    
    [Theory]
    [InlineData("05:59")]
    [InlineData("09:00")]
    public void Si_HoraEstaFueraDelIntervalo_Debe_MostrarSinActividad(string hora)
    {
        var rutinaMananera = new RutinaMananeraBuilder().ActividadesPorDefecto().Build();

        var actividadParaRealizar = rutinaMananera.ConsultarActividad(hora);

        actividadParaRealizar.Should().BeEquivalentTo("Sin actividad");
    }
    
    [Theory]
    [InlineData("06:32", "Hacer ejercicio")]
    [InlineData("06:52", "Ducharse")]
    [InlineData("07:38", "Estudiar")]
    [InlineData("08:07", "Desayunar")]
    public void Si_HoraEstaEnRangoPermitido_Debe_MostrarActividadCorrespondiente(string hora, string actividad)
    {
        var actividadesPersonalizadas = new List<Actividad>
        {
            new("06:00", "06:44", "Hacer ejercicio"),
            new("06:45", "06:59", "Ducharse"),
            new("07:00", "07:29", "Leer"),
            new("07:30", "07:59", "Estudiar"),
            new("08:00", "08:59", "Desayunar"),
        };
        var rutinaMananera = new RutinaMananeraBuilder().ActividadesPersonalizadas(actividadesPersonalizadas).Build();

        var actividadParaRealizar = rutinaMananera.ConsultarActividad(hora);

        actividadParaRealizar.Should().BeEquivalentTo(actividad);
    }
}