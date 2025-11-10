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

    public Recibo GenerarRecibo(CarritoCompras carritoCompras)
    {
        var recibo = new Recibo();
        var productoInicial = carritoCompras.ProductosAgregados[0];
        recibo.Items.Add(new ResumenProducto(productoInicial.Producto, productoInicial.Unidades, 1));
        return recibo;
    }

    public class Recibo
    {
        public List<ResumenProducto> Items { get; } = [];
    }

    public class ResumenProducto(Producto producto, int unidades, decimal valorTotal) : ListaCompra(producto, unidades)
    {
        public decimal ValorTotal { get; set; } = valorTotal;
    }
    
}