namespace KatasTDD.Domain.Supermercado.DTO;

public class Oferta(TipoOferta tipo, Producto producto, double? valorOferta = null)
{
    public TipoOferta Tipo { get; } = tipo;
    public Producto ProductoAplicado { get; } = producto;
    public double? ValorOferta { get; } =  valorOferta;
}

public enum TipoOferta
{
    Descuento
}