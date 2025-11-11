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
                valorDescuentoAplicado = AplicarDescuento(valorTotal, ofertaParaAplicar, out descripcion);
                break;
            case TipoOferta.PagueDosLleveTres:
                descripcion = AplicarPagueDosYLleveTres(compra, ofertaParaAplicar, descripcion, ref valorDescuentoAplicado);
                break;
            case TipoOferta.PagueDosPorPrecioFijo:
            {
                var grupos = compra.Cantidad / 2;
                if (grupos <= 0) return;
                
                decimal descuentoPorGrupo = valorTotal - ofertaParaAplicar.ValorOferta.GetValueOrDefault();
                valorDescuentoAplicado = grupos * descuentoPorGrupo;
                descripcion = $"Pague dos por ${ofertaParaAplicar.ValorOferta}";
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
    private string AplicarPagueDosYLleveTres(ListaCompra compra, Oferta oferta, string descripcion,
        ref decimal valorDescuentoAplicado)
    {
        var gruposDeTres = compra.Cantidad / (int)oferta.ValorOferta!;
        if (gruposDeTres <= 0) return descripcion;
        descripcion = "Pague dos y lleve tres";
        valorDescuentoAplicado = gruposDeTres * compra.Producto.PrecioUnitario;

        return descripcion;
    }

    private decimal AplicarDescuento(decimal valorTotal, Oferta oferta, out string descripcion)
    {
        var valorDescuento = valorTotal * (oferta.ValorOferta.GetValueOrDefault() / 100);
        descripcion = $"Dto del {oferta.ValorOferta}%";
        return valorDescuento;
    }
}