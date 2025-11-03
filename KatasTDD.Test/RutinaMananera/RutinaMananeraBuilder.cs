using KatasTDD.Domain.RutinaMananera;

namespace KatasTDD.Test.RutinaMananera;

public class RutinaMananeraBuilder
{
    private List<Actividad> _actividades = [];

    public RutinaMananeraBuilder ActividadesPorDefecto()
    {
        _actividades =
        [
            new Actividad("06:00", "06:59", "Hacer ejercicio"),
            new Actividad("07:00", "07:59", "Leer y estudiar"),
            new Actividad("08:00", "08:59", "Desayunar")
        ];
        return this;
    }

    public RutinaMananeraBuilder ActividadesPersonalizadas(List<Actividad> actividades)
    {
        _actividades = actividades;
        return this;
    }
    
    public RutinaMananeraDomain Build() => new(_actividades);
}