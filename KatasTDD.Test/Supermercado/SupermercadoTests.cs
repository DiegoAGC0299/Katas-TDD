using AwesomeAssertions;
using KatasTDD.Domain.Supermercado;
using KatasTDD.Domain.Supermercado.DTO;

namespace KatasTDD.Test.Supermercado;

public class SupermercadoTests
{
    private readonly List<Producto> _productos;
    private readonly CarritoCompras _carritoCompras;

    public SupermercadoTests()
    {
        _productos =
        [
            new Producto("Manzana", 0.99),
            new Producto("Arroz", 2.49)
        ];
        
        var catalogo = new Catalogo();
        catalogo.RegistrarProductos(_productos);

        _carritoCompras = new CarritoCompras(catalogo);
    }
    
    [Fact]
    public void Si_AgregoUnProductoAlCarrito_Debe_EnElReciboMostrarLosProductosAgregados()
    {
        _carritoCompras.AgregarProductoALaLista(_productos[0].Nombre);
        var recibo = new Supermercado(_carritoCompras);
        
        var valorTotal = recibo.ObtenerValorTotalCompra();

        valorTotal.Should().Be(_productos[0].PrecioUnitario);

    }
    
    [Fact]
    public void Si_AgregoUnProductoAlCarrito_Debe_ElValorTotalDeLaCompraSerIgualAlPrecioUnitarioDelProducto()
    {
        _carritoCompras.AgregarProductoALaLista(_productos[0].Nombre);
        var supermercado = new Supermercado(_carritoCompras);
        
        var valorTotal = supermercado.ObtenerValorTotalCompra();

        valorTotal.Should().Be(_productos[0].PrecioUnitario);

    }
    
    
}

public class Supermercado(CarritoCompras carrito)
{
    public double ObtenerValorTotalCompra()
    {
        double valorTotal = 0;
        foreach (var productoAgregado in carrito.ProductosAgregados)
        {
            valorTotal += productoAgregado.Producto.PrecioUnitario;
        }
        return valorTotal;
    }
}