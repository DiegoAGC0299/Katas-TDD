using AwesomeAssertions;

namespace KatasTDD.Test.Supermercado;

public class CarritoComprasTests
{
    [Fact]
    public void Si_AgregoUnProducto_Debe_AgregarloEnLaListaDeCompra()
    {
        var producto = new Producto("Manzana", 1.00);
        var catalogo = new Catalogo();
        catalogo.AgregarProducto(producto);
        var carritoCompras = new CarritoCompras(catalogo);

        carritoCompras.AgregarProducto(producto.Nombre);
        
        carritoCompras.ObtenerLista()[0].Producto.Should().Be(producto);
    }
}

public class CarritoCompras(Catalogo catalogo)
{
    private readonly List<ListaCompra> _listaCompra = [];
    public void AgregarProducto(string nombreProducto) => _listaCompra.Add(new ListaCompra(ConsultarProducto(nombreProducto), 1));
    public List<ListaCompra> ObtenerLista() => _listaCompra;

    public record ListaCompra(Producto? Producto, double Cantidad);
    
    private Producto? ConsultarProducto(string nombreProducto) => catalogo.Productos.FirstOrDefault(f => f.Nombre == nombreProducto);
}