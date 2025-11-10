namespace KatasTDD.Domain.Supermercado.DTO;

public class ListaCompra(Producto producto, int unidades)
{
    public Producto Producto { get; } = producto;
    public int Unidades { get; set; } = unidades;
}