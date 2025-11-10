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
}

public class Catalogo
{
    public List<Producto> Productos { get; } = [];
    public void AgregarProducto(Producto producto)
    {
        Productos.Add(producto);
    }
}

public record Producto(string Nombre, double Precio);