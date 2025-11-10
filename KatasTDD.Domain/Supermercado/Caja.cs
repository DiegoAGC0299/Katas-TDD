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
        recibo.Descuentos.Add(new Descuento(carritoCompras.ProductosAgregados[0].Producto, 1.192));
        return recibo;
    }

    public class Recibo
    {
        public List<ResumenProducto> Items { get; } = [];
        public List<Descuento> Descuentos { get; } = [];

        public void AgregarProductos(List<ListaCompra> carrito)
            => Items.AddRange(carrito.Select(compra => new ResumenProducto(compra.Producto, compra.Cantidad, compra.Producto.PrecioUnitario * compra.Cantidad)).ToList());
    }

    public class ResumenProducto(Producto producto, int cantidad, double valorTotal) : ListaCompra(producto, cantidad)
    {
        public double ValorTotal { get; set; } = valorTotal;
    }
    
}

public class Descuento(Producto producto, double valorDescuento)
{
    public Producto Producto { get; } = producto;
    public double ValorDescuento { get; } = valorDescuento;
}