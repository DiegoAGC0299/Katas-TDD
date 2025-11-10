namespace KatasTDD.Domain.Supermercado.DTO;

public class Producto(string nombre, double precioUnitario)
{
    public string Nombre { get; } = nombre;
    public double PrecioUnitario { get; set; } = precioUnitario;
}