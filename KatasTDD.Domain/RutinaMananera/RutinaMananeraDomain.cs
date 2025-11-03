namespace KatasTDD.Domain.RutinaMananera;

public class RutinaMananeraDomain(List<Actividad> actividades)
{
    private readonly TimeSpan _intervaloInicial = new(6,0,0);
    private readonly TimeSpan _intervaloFinal = new(9,0,0);
    private readonly string _actividadNoDefinida = "Sin actividad";
    private List<Actividad> Actividades { get; } = actividades;

    public string? ConsultarActividad(string horaSolicitada)
    {
        var hora = TimeSpan.Parse(horaSolicitada);
        return HoraFueraDelIntervalo(hora) ? _actividadNoDefinida : ObtenerActividadPorHora(hora);
    }
    
    private bool HoraFueraDelIntervalo(TimeSpan hora)
        => hora < _intervaloInicial || hora >= _intervaloFinal;

    private string? ObtenerActividadPorHora(TimeSpan hora)
        => Actividades.FirstOrDefault(f => hora >= f.HoraInicial && hora < f.HoraFinal)?.DescripcionActividad.ToString();
    
}