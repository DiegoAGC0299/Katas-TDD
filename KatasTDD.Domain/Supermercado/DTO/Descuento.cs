namespace KatasTDD.Domain.Supermercado.DTO;

public class Descuento(Producto producto, string descripcion, decimal valor)
{
    public Producto Producto { get; } = producto;
    public string Descripcion { get; } = descripcion;
    public decimal Valor { get; } = valor;
}