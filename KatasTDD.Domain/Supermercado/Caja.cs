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
        recibo.AgregarProductos(carritoCompras.ProductosAgregados);
        return recibo;
    }

    public class Recibo
    {
        public List<ResumenProducto> Items { get; } = [];

        public void AgregarProductos(List<ListaCompra> carrito)
            => Items.AddRange(carrito.Select(compra => new ResumenProducto(compra.Producto, compra.Unidades, 1)).ToList());
    }

    public class ResumenProducto(Producto producto, int unidades, decimal valorTotal) : ListaCompra(producto, unidades)
    {
        public decimal ValorTotal { get; set; } = valorTotal;
    }
    
}