using AwesomeAssertions;
using KatasTDD.Domain.Supermercado;
using KatasTDD.Domain.Supermercado.DTO;

namespace KatasTDD.Test.Supermercado;

public class SupermercadoTests
{
    private readonly Catalogo _catalogo;

    public SupermercadoTests()
    {
        var productos = 
        _catalogo = new Catalogo();
        _catalogo.RegistrarProductos(GenerarProductosPorDefecto());
        
    }
    
    [Fact]
    public void Si_HayUnProductoAgregadoAlCarrito_Debe_GenerarUnReciboConUnSoloItemDelProducto()
    {
        var carritoCompras = new CarritoCompras(_catalogo);
        var productoAgregado = _catalogo.Productos[0];
        carritoCompras.AgregarProductoALaLista(productoAgregado.Nombre);

        var caja = new Caja(_catalogo);
        var recibo = caja.GenerarRecibo(carritoCompras);

        recibo.Items.Should().HaveCount(1);
        recibo.Items[0].Producto.Should().Be(productoAgregado);

    }
    
    [Fact]
    public void Si_HayDosProductosAgregadoAlCarrito_Debe_GenerarUnReciboConDosItemsDeLosProductos()
    {
        var carritoCompras = new CarritoCompras(_catalogo);
        var producto1 = _catalogo.Productos[0];
        var producto2 = _catalogo.Productos[1];
        
        carritoCompras.AgregarProductoALaLista(producto1.Nombre);
        carritoCompras.AgregarProductoALaLista(producto2.Nombre);

        var caja = new Caja(_catalogo);
        var recibo = caja.GenerarRecibo(carritoCompras);

        recibo.Items.Should().HaveCount(2);
        recibo.Items[0].Producto.Should().Be(producto1);
        recibo.Items[1].Producto.Should().Be(producto2);

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