using AwesomeAssertions;

namespace KatasTDD.Test.Supermercado;

public class CarritoComprasTests
{
    [Fact]
    public void Si_AgregoUnProducto_Debe_AgregarloEnLaListaDeCompra()
    {
        var producto = new Producto("Manzana", 1.00);
        var catalogo = new Catalogo();
        catalogo.RegistrarProducto(producto);
        var carritoCompras = new CarritoCompras(catalogo);

        carritoCompras.AgregarProductoALaLista(producto.Nombre);
        
        carritoCompras.ObtenerLista()[0].Producto.Should().Be(producto);
    }
    
    [Fact]
    public void Si_AgregoUnProductoQueNoExiste_Debe_LanzarExcepcion()
    {
        var producto = new Producto("Manzana", 1.00);
        var catalogo = new Catalogo();
        catalogo.RegistrarProducto(producto);
        var carritoCompras = new CarritoCompras(catalogo);

        var caller = () => carritoCompras.AgregarProductoALaLista("Naranja");
        
        caller.Should().ThrowExactly<NullReferenceException>().WithMessage("El producto con nombre Naranja no existe.");
    }
    
    [Fact]
    public void Si_AgregoElMismoProductoDosVeces_Debe_ExistirUnSoloProductoConCantidadDosEnLaListaDeCompra()
    {
        var producto = new Producto("Manzana", 1.00);
        var catalogo = new Catalogo();
        catalogo.RegistrarProducto(producto);
        var carritoCompras = new CarritoCompras(catalogo);

        carritoCompras.AgregarProductoALaLista(producto.Nombre);
        carritoCompras.AgregarProductoALaLista(producto.Nombre);
        
        var lista =  carritoCompras.ObtenerLista();
        lista.Should().HaveCount(1);
        lista[0].Cantidad.Should().Be(2);
        
    }
}

public class CarritoCompras(Catalogo catalogo)
{
    private readonly List<ListaCompra> _listaCompra = [];
    private const int CantidadPorDefecto = 1;

    public void AgregarProductoALaLista(string nombreProducto)
    {
        var productoEnCatalogo = ConsultarProducto(nombreProducto);
        LanzarExcepcionSiProductoNoExisteEnElCatalogo(nombreProducto, productoEnCatalogo);
        
        AgregarOModificarProductoDeLaLista(nombreProducto, productoEnCatalogo);
    }

    public List<ListaCompra> ObtenerLista() => _listaCompra;
    
    private Producto? ConsultarProducto(string nombreProducto) => catalogo.Productos.FirstOrDefault(f => f.Nombre == nombreProducto);
    
    private static void LanzarExcepcionSiProductoNoExisteEnElCatalogo(string nombreProducto, Producto? consultaProducto)
    {
        if (consultaProducto is null)
            throw new NullReferenceException($"El producto con nombre {nombreProducto} no existe.");
    }
    
    private void AgregarOModificarProductoDeLaLista(string nombreProducto, Producto productoEnCatalogo)
    {
        var productoEnLista = _listaCompra.FirstOrDefault(f => f.Producto?.Nombre == nombreProducto);

        if (productoEnLista is not null)
            productoEnLista.Cantidad++;
        else
            _listaCompra.Add(new ListaCompra(productoEnCatalogo, CantidadPorDefecto));
        
    }
}

public class ListaCompra(Producto producto, int cantidad)
{
    public Producto Producto { get; } = producto;
    public int Cantidad { get; set; } = cantidad;
}