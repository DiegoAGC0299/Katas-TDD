using System.Data;
using AwesomeAssertions;

namespace KatasTDD.Test.Supermercado;

public class CatalogoTests
{
    [Fact]
    public void Si_AgregoUnProductoAlCatalogo_Debe_ProductoEstarRegistrado()
    {
        var catalogo = new Catalogo();
        var producto = new Producto("Manzana",0.99);
        
        catalogo.AgregarProducto(producto);

        catalogo.Productos[0].Should().NotBeNull();
        catalogo.Productos[0].Should().BeEquivalentTo(producto);
    }

    [Fact]
    public void Si_AgregoDosProductosAlCatalogoConElMismoNombre_Debe_LanzarExcepcion()
    {
        var catalogo = new Catalogo();
        
        catalogo.AgregarProducto(new Producto("Manzana",0.99));
        var caller = () => catalogo.AgregarProducto(new Producto("Manzana",1.99));
        
        caller.Should().ThrowExactly<DuplicateNameException>().WithMessage("Ya existe un producto con el mismo nombre.");
    }
}

public class Catalogo
{
    public List<Producto> Productos { get; } = [];
    public void AgregarProducto(Producto producto)
    {
        LanzarExcepcionSiProductoYaExisteConElMismoNombre(producto);
        Productos.Add(producto);
    }

    private void LanzarExcepcionSiProductoYaExisteConElMismoNombre(Producto producto)
    {
        if(Productos.Any(a => a.Nombre.ToLower().Equals(producto.Nombre.ToLower())))
            throw new DuplicateNameException("Ya existe un producto con el mismo nombre.");
    }
}

public record Producto(string Nombre, double Precio);