using KatasTDD.Domain.Supermercado.DTO;

namespace KatasTDD.Domain.Supermercado;

public class Recibo
{
    public List<ItemProducto> Items { get; } = [];
    public List<Descuento> Descuentos { get; } = [];

    public void AgregarItem(ItemProducto item)
        => Items.Add(item);
        
    public void AgregarDescuento(Descuento descuento)
        => Descuentos.Add(descuento);
}