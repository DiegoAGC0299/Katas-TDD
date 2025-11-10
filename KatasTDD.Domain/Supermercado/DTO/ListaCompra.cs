namespace KatasTDD.Domain.Supermercado.DTO;

public class ListaCompra(Producto producto, int cantidad)
{
    public Producto Producto { get; } = producto;
    public int Cantidad { get; set; } = cantidad;
}