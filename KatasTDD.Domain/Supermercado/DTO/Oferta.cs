namespace KatasTDD.Domain.Supermercado.DTO;

public class Oferta(TipoOferta tipo, Producto producto, decimal? valorOferta = null)
{
    public TipoOferta Tipo { get; } = tipo;
    public Producto ProductoAplicado { get; } = producto;
    public decimal? ValorOferta { get; } =  valorOferta;
}

public enum TipoOferta
{
    Descuento
}