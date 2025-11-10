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
        _productos = new List<Producto>()
        {
            new("Manzana", 0.99),
            new("Arroz", 2.49),
        };
        
        var catalogo = new Catalogo();
        catalogo.RegistrarProductos(_productos);

        _carritoCompras = new CarritoCompras(catalogo);
    }
    
    [Fact]
    public void Si_AgregoUnProductoAlCarrito_Debe_ElValorTotalDeLaCompraSerIgualAlPrecioUnitarioDelProducto()
    {
        _carritoCompras.AgregarProductoALaLista(_productos[0].Nombre);
        var recibo = new Recibo();
        
        var valorTotal = recibo.ObtenerValorTotalCompra();

        valorTotal.Should().Be(_productos[0].PrecioUnitario);

    }
}

public class Recibo
{
    public double ObtenerValorTotalCompra()
    {
        return 0.99;
    }
}