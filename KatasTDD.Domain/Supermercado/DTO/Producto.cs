namespace KatasTDD.Domain.Supermercado.DTO;

public class Producto(string nombre, decimal precioUnitario)
{
    public string Nombre { get; } = nombre;
    public decimal PrecioUnitario { get; set; } = precioUnitario;
}