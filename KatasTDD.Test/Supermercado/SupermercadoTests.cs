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
        
        recibo.Items[0].ValorTotal.Should().Be(productoAgregado.PrecioUnitario * 2M);
        recibo.Items[0].ValorTotal.Should().Be(productoAgregado.PrecioUnitario * 2);
    }

    [Fact]
    public void Si_ExisteUnKiloDeManzanaEnElCarritoYSeAgregoUnaOfertaDelVeintePorCiento_Debe_GenerarElReciboConProductoYValorDescuentoAplicado()
    {
        var productoManzana = _catalogo.Productos[1];
        _carritoCompras.AgregarProductoALaLista(productoManzana.Nombre);
        _caja.AregarOferta(TipoOferta.Descuento, productoManzana, 20);
        
        var recibo = _caja.GenerarRecibo(_carritoCompras);
        
        recibo.Descuentos.Should().HaveCount(1);
        recibo.Descuentos[0].Producto.Should().Be(productoManzana);
        recibo.Descuentos[0].Valor.Should().Be(0.298M);

    }
    
    [Fact]
    public void Si_ExisteUnaBolsaDeArrozEnElCarritoYSeAgregoUnaOfertaDelDiezPorCiento_Debe_GenerarElReciboConProductoYValorDescuentoAplicado()
    {
        var productoArroz = _catalogo.Productos[2];
        var porcentajeDescuento = 10;
        _carritoCompras.AgregarProductoALaLista(productoArroz.Nombre);
        _caja.AregarOferta(TipoOferta.Descuento, productoArroz, porcentajeDescuento);
        
        var recibo = _caja.GenerarRecibo(_carritoCompras);
        
        recibo.Descuentos.Should().HaveCount(1);
        recibo.Descuentos[0].Producto.Should().Be(productoArroz);
        recibo.Descuentos[0].Descripcion.Should().BeEquivalentTo($"Dto del {porcentajeDescuento}%");
        recibo.Descuentos[0].Valor.Should().Be(0.249M);

    }
    
    [Fact]
    public void Si_ExistenTresCepillosDeDientesEnElCarrito_Debe_AplicarOfertaPagueDosYLleveTresYGenerarElReciboConDescuentoAplicado()
    {
        var productoCepilloDientes = _catalogo.Productos[0];
        _carritoCompras.AgregarProductoALaLista(productoCepilloDientes.Nombre);
        _carritoCompras.AgregarProductoALaLista(productoCepilloDientes.Nombre);
        _carritoCompras.AgregarProductoALaLista(productoCepilloDientes.Nombre);
        _caja.AregarOferta(TipoOferta.PagueDosLleveTres, productoCepilloDientes, 3);
        
        var recibo = _caja.GenerarRecibo(_carritoCompras);
        
        recibo.Descuentos[0].Producto.Should().Be(productoCepilloDientes);
        recibo.Descuentos[0].Descripcion.Should().BeEquivalentTo("Pague dos y lleve tres");
        recibo.Descuentos[0].Valor.Should().Be(0.99M);

    }
    
    [Fact]
    public void Si_ExistenSeisCepillosDeDientesEnElCarrito_Debe_AplicarOfertaPagueDosYLleveTresYGenerarElReciboConDescuentoAplicado()
    {
        var productoCepilloDientes = _catalogo.Productos[0];
        _carritoCompras.AgregarProductoALaLista(productoCepilloDientes.Nombre);
        _carritoCompras.AgregarProductoALaLista(productoCepilloDientes.Nombre);
        _carritoCompras.AgregarProductoALaLista(productoCepilloDientes.Nombre);
        _carritoCompras.AgregarProductoALaLista(productoCepilloDientes.Nombre);
        _carritoCompras.AgregarProductoALaLista(productoCepilloDientes.Nombre);
        _carritoCompras.AgregarProductoALaLista(productoCepilloDientes.Nombre);
        
        _caja.AregarOferta(TipoOferta.PagueDosLleveTres, productoCepilloDientes, 3);
        
        var recibo = _caja.GenerarRecibo(_carritoCompras);
        
        recibo.Descuentos[0].Producto.Should().Be(productoCepilloDientes);
        recibo.Descuentos[0].Descripcion.Should().BeEquivalentTo("Pague dos y lleve tres");
        recibo.Descuentos[0].Valor.Should().Be(1.98M);

    }
    
    [Fact]
    public void Si_ExistenDosCajasDeTomatesCherryElCarrito_Debe_AplicarOfertaPagueDosPorCeroPuntoNoventaYNueveYGenerarElReciboConDescuentoAplicado()
    {
        var productoTomatesCherry = _catalogo.Productos[4];
        var precioFijo = 0.99M;
        _carritoCompras.AgregarProductoALaLista(productoTomatesCherry.Nombre);
        _carritoCompras.AgregarProductoALaLista(productoTomatesCherry.Nombre);
        
        _caja.AregarOferta(TipoOferta.PagueDosPorPrecioFijo, productoTomatesCherry, precioFijo);
        
        var recibo = _caja.GenerarRecibo(_carritoCompras);
        
        recibo.Descuentos[0].Producto.Should().Be(productoTomatesCherry);
        recibo.Descuentos[0].Descripcion.Should().BeEquivalentTo($"Pague 2 por ${precioFijo}");
        recibo.Descuentos[0].Valor.Should().Be(0.39M);

    }
    
    [Fact]
    public void Si_ExistenCuatroCajasDeTomatesCherryEnElCarrito_Debe_AplicarOfertaPagueDosPorCeroPuntoNoventaYNueveYGenerarElReciboConDescuentoAplicado()
    {
        var productoTomatesCherry = _catalogo.Productos[4];
        var precioFijo = 0.99M;
        
        _carritoCompras.AgregarProductoALaLista(productoTomatesCherry.Nombre);
        _carritoCompras.AgregarProductoALaLista(productoTomatesCherry.Nombre);
        _carritoCompras.AgregarProductoALaLista(productoTomatesCherry.Nombre);
        _carritoCompras.AgregarProductoALaLista(productoTomatesCherry.Nombre);
        
        _caja.AregarOferta(TipoOferta.PagueDosPorPrecioFijo, productoTomatesCherry, precioFijo);
        
        var recibo = _caja.GenerarRecibo(_carritoCompras);
        
        recibo.Descuentos[0].Producto.Should().Be(productoTomatesCherry);
        recibo.Descuentos[0].Descripcion.Should().BeEquivalentTo($"Pague 2 por ${precioFijo}");
        recibo.Descuentos[0].Valor.Should().Be(0.78M);

    }
    
    [Fact]
    public void Si_ExistenCincoTubosDePastaDeDientesEnElCarrito_Debe_AplicarOfertaPagueCincoPorSietePuntoCuarentaYNueveYGenerarElReciboConDescuentoAplicado()
    {
        var productoPastaDientes = _catalogo.Productos[3];
        var precioFijo = 7.49M;
        
        _carritoCompras.AgregarProductoALaLista(productoPastaDientes.Nombre);
        _carritoCompras.AgregarProductoALaLista(productoPastaDientes.Nombre);
        _carritoCompras.AgregarProductoALaLista(productoPastaDientes.Nombre);
        _carritoCompras.AgregarProductoALaLista(productoPastaDientes.Nombre);
        _carritoCompras.AgregarProductoALaLista(productoPastaDientes.Nombre);
        
        _caja.AregarOferta(TipoOferta.PagueCincoPorPrecioFijo, productoPastaDientes, precioFijo);
        
        var recibo = _caja.GenerarRecibo(_carritoCompras);
        
        recibo.Descuentos[0].Producto.Should().Be(productoPastaDientes);
        recibo.Descuentos[0].Descripcion.Should().BeEquivalentTo($"Pague 5 por ${precioFijo}");
        recibo.Descuentos[0].Valor.Should().Be(1.46M);

    }
    
    [Fact]
    public void Si_AgregoUnCepilloDeDientesYUnArrozEnBolsa_Debe_ElPrecioTotalDebeSerTresPuntoCuarentaYOcho()
    {
        var productoCepilloDientes = _catalogo.Productos[0];
        var productoBolsaArroz = _catalogo.Productos[2];
        
        _carritoCompras.AgregarProductoALaLista(productoCepilloDientes.Nombre);
        _carritoCompras.AgregarProductoALaLista(productoBolsaArroz.Nombre);
        
        var recibo = _caja.GenerarRecibo(_carritoCompras);
        var total = recibo.ObtenerValorTotalCompra();

        total.Should().Be(3.48M);

    }
    
    [Fact]
    public void Si_HayDosCajasDeTomatesCerryYUnKiloDeManzanas_Debe_ElPrecioTotalDebeSerLaSumatoriaDeLosProductos()
    {
        var productoTomateCherry = _catalogo.Productos[4];
        var productoKiloManzana = _catalogo.Productos[1];
        
        _carritoCompras.AgregarProductoALaLista(productoTomateCherry.Nombre);
        _carritoCompras.AgregarProductoALaLista(productoTomateCherry.Nombre);
        _carritoCompras.AgregarProductoALaLista(productoKiloManzana.Nombre);
        
        var recibo = _caja.GenerarRecibo(_carritoCompras);
        var total = recibo.ObtenerValorTotalCompra();

        total.Should().Be(2.87M);

    }
    
    private List<Producto> GenerarProductosPorDefecto()
        =>
        [
            new("Cepillo de dientes", 0.99M),
            new("Manzana por kilo", 1.49M),
            new("Arroz en bolsa", 2.49M),
            new("Tubo de pasta de dientes", 1.79M),
            new("Caja de tomates cherry", 0.69M)
        ];
}