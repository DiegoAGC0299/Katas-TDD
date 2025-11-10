using AwesomeAssertions;
using KatasTDD.Domain.Supermercado;
using KatasTDD.Domain.Supermercado.DTO;

namespace KatasTDD.Test.Supermercado;

public class SupermercadoTests
{
    private readonly Catalogo _catalogo;
    private readonly CarritoCompras _carritoCompras; 
    private readonly Caja _caja;

    public SupermercadoTests()
    {
        _catalogo = new Catalogo();
        _catalogo.RegistrarProductos(GenerarProductosPorDefecto());
        _carritoCompras = new CarritoCompras(_catalogo);
        _caja = new Caja(_catalogo);
        
    }
    
    [Fact]
    public void Si_HayUnProductoAgregadoAlCarrito_Debe_GenerarUnReciboConUnSoloItemDelProducto()
    {
        var productoAgregado = _catalogo.Productos[0];
        _carritoCompras.AgregarProductoALaLista(productoAgregado.Nombre);
        
        var recibo = _caja.GenerarRecibo(_carritoCompras);

        recibo.Items.Should().HaveCount(1);
        recibo.Items[0].Producto.Should().Be(productoAgregado);

    }
    
    [Fact]
    public void Si_HayDosProductosAgregadoAlCarrito_Debe_GenerarUnReciboConDosItemsDeLosProductos()
    {
        var producto1 = _catalogo.Productos[0];
        var producto2 = _catalogo.Productos[1];
        
        _carritoCompras.AgregarProductoALaLista(producto1.Nombre);
        _carritoCompras.AgregarProductoALaLista(producto2.Nombre);
        
        var recibo = _caja.GenerarRecibo(_carritoCompras);

        recibo.Items.Should().HaveCount(2);
        recibo.Items[0].Producto.Should().Be(producto1);
        recibo.Items[1].Producto.Should().Be(producto2);

    }
    
    [Fact]
    public void Si_HayUnProductoQueSeAgregoDosVecesAlCarrito_Debe_ElValorTotalParaElItemSerElProductoEntreElValorUnitarioYLaCantidad()
    {
        var productoAgregado = _catalogo.Productos[0];
        
        _carritoCompras.AgregarProductoALaLista(productoAgregado.Nombre);
        _carritoCompras.AgregarProductoALaLista(productoAgregado.Nombre);
        
        var recibo = _caja.GenerarRecibo(_carritoCompras);
        
        recibo.Items[0].ValorTotal.Should().Be(productoAgregado.PrecioUnitario * 2);
    }

    [Fact]
    public void Si_ExisteUnaManzanaEnElCarritoYSeAgregoUnaOfertaDelVeintePorCiento_Debe_GenerarElReciboConProductoYValorDescuentoAplicado()
    {
        var productoManzana = _catalogo.Productos[1];
        _carritoCompras.AgregarProductoALaLista(productoManzana.Nombre);
        _caja.AregarOferta(TipoOferta.Descuento, productoManzana, 20);
        
        var recibo = _caja.GenerarRecibo(_carritoCompras);
        
        recibo.Descuentos.Should().HaveCount(1);
        recibo.Descuentos[0].Producto.Should().Be(productoManzana);
        recibo.Descuentos[0].ValorDescuento.Should().Be(1.192);

    }
    
    private List<Producto> GenerarProductosPorDefecto()
        =>
        [
            new("Cepillo de dientes", 0.99),
            new("Manzana por kilo", 1.49),
            new("Arroz en bolsa", 2.49),
            new("Tubo de pasta de dientes", 1.79),
            new("Caja de tomates cherry", 0.99)
        ];
}