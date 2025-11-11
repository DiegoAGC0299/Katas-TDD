using System.Data;
using AwesomeAssertions;
using KatasTDD.Domain.Supermercado;
using KatasTDD.Domain.Supermercado.DTO;

namespace KatasTDD.Test.Supermercado;

public class CatalogoTests
{
    [Fact]
    public void Si_AgregoUnProductoAlCatalogo_Debe_ProductoEstarRegistrado()
    {
        var catalogo = new Catalogo();
        var producto = new Producto("Manzana",0.99M);
        
        catalogo.RegistrarProducto(producto);

        catalogo.Productos[0].Should().NotBeNull();
        catalogo.Productos[0].Should().BeEquivalentTo(producto);
    }

    [Fact]
    public void Si_AgregoDosProductosAlCatalogoConElMismoNombre_Debe_LanzarExcepcion()
    {
        var catalogo = new Catalogo();
        
        catalogo.RegistrarProducto(new Producto("Manzana",0.99M));
        var caller = () => catalogo.RegistrarProducto(new Producto("Manzana",0.99M));
        
        caller.Should().ThrowExactly<DuplicateNameException>().WithMessage("Ya existe un producto con el mismo nombre.");
    }
}