namespace KatasTDD.Domain.RutinaMananera;

public class Actividad(string horaInicial, string horaFinal, string descripcionActividad)
{
    public TimeSpan HoraInicial  { get; } = TimeSpan.Parse(horaInicial);
    public TimeSpan HoraFinal { get; } = TimeSpan.Parse(horaFinal);
    public string DescripcionActividad { get; } = descripcionActividad;
}