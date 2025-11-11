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

    public Recibo GenerarRecibo(CarritoCompras carritoCompras)
    {
        var recibo = new Recibo();

        foreach (var compra in carritoCompras.ProductosAgregados)
        {
            var valorTotal = compra.Producto.PrecioUnitario * compra.Cantidad;
            recibo.AgregarItem(new ItemProducto(compra.Producto, compra.Cantidad, valorTotal));
            
            AplicarOfertas(compra, valorTotal, recibo);
        }
        
        return recibo;
    }

    private void AplicarOfertas(ListaCompra compra, decimal valorTotal, Recibo recibo)
    {
        var ofertaParaAplicar = Ofertas.FirstOrDefault(oferta => oferta.ProductoAplicado ==  compra.Producto);
        if (ofertaParaAplicar is null) return;
        
        decimal valorDescuentoAplicado = 0;
        var descripcion = "";

        switch (ofertaParaAplicar.Tipo)
        {
            case TipoOferta.Descuento:
                AplicarDescuento(valorTotal, ofertaParaAplicar, ref descripcion, ref valorDescuentoAplicado);
                break;
            case TipoOferta.PagueDosLleveTres:
                AplicarPagueDosYLleveTres(compra, ofertaParaAplicar, ref descripcion, ref valorDescuentoAplicado);
                break;
            case TipoOferta.PagueDosPorPrecioFijo or TipoOferta.PagueCincoPorPrecioFijo:
            {
                AplicarPagueCantidadEstablecidaPorPrecioFijo(compra, ofertaParaAplicar, (int)ofertaParaAplicar.Tipo, ref valorDescuentoAplicado, ref descripcion);
                break;
            }
            default:
                return;
        }

        recibo.AgregarDescuento(new Descuento(compra.Producto, descripcion, valorDescuentoAplicado));
    }

    private void LanzarExcepcionSiProductoYaCuentaConOfertaExistente(Producto producto)
    {
        if (Ofertas.Any(oferta => oferta.ProductoAplicado == producto))
            throw new InvalidOperationException($"El producto con nombre {producto.Nombre} ya cuenta con una oferta existente.");
    }
    
    private void AplicarDescuento(decimal valorTotal, Oferta oferta, ref string descripcion, ref decimal valorDescuento)
    {
        valorDescuento = valorTotal * (oferta.ValorOferta.GetValueOrDefault() / 100);
        descripcion = $"Dto del {oferta.ValorOferta}%";
    }
    
    private void AplicarPagueDosYLleveTres(ListaCompra compra, Oferta oferta, ref string descripcion,
        ref decimal valorDescuentoAplicado)
    {
        var gruposDeTres = compra.Cantidad / (int)oferta.ValorOferta!;
        if (gruposDeTres <= 0) return;
        descripcion = "Pague dos y lleve tres";
        valorDescuentoAplicado = gruposDeTres * compra.Producto.PrecioUnitario;
    }
    
    private void AplicarPagueCantidadEstablecidaPorPrecioFijo(ListaCompra compra, Oferta ofertaParaAplicar, int cantidadEstablecida, ref decimal valorDescuentoAplicado,
        ref string descripcion)
    {
        var grupos = compra.Cantidad / cantidadEstablecida;
        if (grupos <= 0) return;

        decimal valorAplicadoPorGrupo = compra.Producto.PrecioUnitario * cantidadEstablecida;
        decimal descuentoPorGrupo = valorAplicadoPorGrupo - ofertaParaAplicar.ValorOferta.GetValueOrDefault();
        valorDescuentoAplicado = grupos * descuentoPorGrupo;
        descripcion = $"Pague {cantidadEstablecida} por ${ofertaParaAplicar.ValorOferta}";
    }
}