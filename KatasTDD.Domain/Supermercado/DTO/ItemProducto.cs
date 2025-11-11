namespace KatasTDD.Domain.Supermercado.DTO;

public class ItemProducto(Producto producto, int cantidad, decimal valorTotal) : ListaCompra(producto, cantidad)
{
    public decimal ValorTotal { get; set; } = valorTotal;
}