using KatasTDD.Domain.Supermercado.DTO;

namespace KatasTDD.Domain.Supermercado;

public class Caja(Catalogo catalogo)
{
    public Catalogo Catalogo { get; } = catalogo;
    public List<Oferta> Ofertas { get; } = [];

    public void AregarOferta(TipoOferta tipo, Producto producto, double? valorAplicado = null)
    {
        Catalogo.LanzarExcepcionSiProductoNoExisteEnElCatalogo(producto.Nombre);
        LanzarExcepcionSiProductoYaCuentaConOfertaExistente(producto);
        
        Ofertas.Add(new Oferta(tipo, producto, valorAplicado));
    }

    private void LanzarExcepcionSiProductoYaCuentaConOfertaExistente(Producto producto)
    {
        if (Ofertas.Any(ofeta => ofeta.ProductoAplicado == producto))
            throw new InvalidOperationException($"El producto con nombre {producto.Nombre} ya cuenta con una oferta existente.");
    }
}