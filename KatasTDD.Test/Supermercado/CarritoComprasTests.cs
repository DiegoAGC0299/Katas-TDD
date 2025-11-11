using AwesomeAssertions;
using KatasTDD.Domain.Supermercado;
using KatasTDD.Domain.Supermercado.DTO;

namespace KatasTDD.Test.Supermercado;

public class CarritoComprasTests
{
    [Fact]
    public void Si_AgregoUnProducto_Debe_AgregarloEnLaListaDeCompra()
    {
        var producto = new Producto("Manzana", 1.00M);
        var catalogo = new Catalogo();
        catalogo.RegistrarProducto(producto);
        var carritoCompras = new CarritoCompras(catalogo);

        carritoCompras.AgregarProductoALaLista(producto.Nombre);
        
        carritoCompras.ProductosAgregados[0].Producto.Should().Be(producto);
    }
    
    [Fact]
    public void Si_AgregoUnProductoQueNoExiste_Debe_LanzarExcepcion()
    {
        var producto = new Producto("Manzana", 1.00M);
        var catalogo = new Catalogo();
        catalogo.RegistrarProducto(producto);
        var carritoCompras = new CarritoCompras(catalogo);

        var caller = () => carritoCompras.AgregarProductoALaLista("Naranja");
        
        caller.Should().ThrowExactly<NullReferenceException>().WithMessage("El producto con nombre Naranja no existe.");
    }
    
    [Fact]
    public void Si_AgregoElMismoProductoDosVeces_Debe_ExistirUnSoloProductoConCantidadDosEnLaListaDeCompra()
    {
        var producto = new Producto("Manzana", 1.00M);
        var catalogo = new Catalogo();
        catalogo.RegistrarProducto(producto);
        var carritoCompras = new CarritoCompras(catalogo);

        carritoCompras.AgregarProductoALaLista(producto.Nombre);
        carritoCompras.AgregarProductoALaLista(producto.Nombre);
        
        carritoCompras.ProductosAgregados.Should().HaveCount(1);
        carritoCompras.ProductosAgregados[0].Cantidad.Should().Be(2);
        
    }
}