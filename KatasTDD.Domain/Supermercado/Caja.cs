using KatasTDD.Domain.Supermercado.DTO;

namespace KatasTDD.Domain.Supermercado;

public class Caja(Catalogo catalogo)
{
    public List<Oferta> Ofertas { get; } = [];

    public void AregarOferta(TipoOferta tipo, Producto producto, decimal? valorOferta = null)
    {
        catalogo.LanzarExcepcionSiProductoNoExisteEnElCatalogo(producto.Nombre);
        LanzarExcepcionSiProductoYaCuentaConOfertaExistente(producto);
        
        Ofertas.Add(new Oferta(tipo, producto, valorOferta));
    }

    private void LanzarExcepcionSiProductoYaCuentaConOfertaExistente(Producto producto)
    {
        if (Ofertas.Any(oferta => oferta.ProductoAplicado == producto))
            throw new InvalidOperationException($"El producto con nombre {producto.Nombre} ya cuenta con una oferta existente.");
    }

    public Recibo GenerarRecibo(CarritoCompras carritoCompras)
    {
        var recibo = new Recibo();

        foreach (var compra in carritoCompras.ProductosAgregados)
        {
            var valorTotal = compra.Producto.PrecioUnitario * compra.Cantidad;
            recibo.AgregarItem(new ResumenProducto(compra.Producto, compra.Cantidad, valorTotal));
            
            var oferta = Ofertas.FirstOrDefault(f => f.ProductoAplicado ==  compra.Producto);

            if (oferta is null) continue;
            
            decimal valorDescuentoAplicado = 0;
            var descripcion = "";

            if (oferta.Tipo == TipoOferta.Descuento)
            {
                
                valorDescuentoAplicado = valorTotal * (oferta.ValorOferta.GetValueOrDefault() / 100);
                descripcion = $"Dto del {oferta.ValorOferta}%";
            }
                
            recibo.AgregarDescuento(new Descuento(compra.Producto, descripcion, valorDescuentoAplicado));
        }
        
        return recibo;
    }

    public class Recibo
    {
        public List<ResumenProducto> Items { get; } = [];
        public List<Descuento> Descuentos { get; } = [];

        public void AgregarItem(ResumenProducto item)
            => Items.Add(item);
        
        public void AgregarDescuento(Descuento descuento)
            => Descuentos.Add(descuento);
    }

    public class ResumenProducto(Producto producto, int cantidad, decimal valorTotal) : ListaCompra(producto, cantidad)
    {
        public decimal ValorTotal { get; set; } = valorTotal;
    }
    
}

public class Descuento(Producto producto, string descripcion, decimal valorDescuento)
{
    public Producto Producto { get; } = producto;
    public string Descripcion { get; } = descripcion;
    public decimal ValorDescuento { get; } = valorDescuento;
}