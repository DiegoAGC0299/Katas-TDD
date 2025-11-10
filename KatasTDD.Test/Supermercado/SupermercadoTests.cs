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
    public void Si_GeneroUnReciboConUnCepilloDeDientesAgregado_Debe_ContarConUnItemDelProductoAgregado()
    {
        var carritoCompras = new CarritoCompras(_catalogo);
        var productoAgregado = _catalogo.Productos[0];
        carritoCompras.AgregarProductoALaLista(productoAgregado.Nombre);

        var caja = new Caja(_catalogo);
        var recibo = caja.GenerarRecibo(carritoCompras);

        recibo.Items.Should().HaveCount(1);
        recibo.Items[0].Producto.Should().Be(productoAgregado);

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