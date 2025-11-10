using KatasTDD.Domain.Supermercado.DTO;

namespace KatasTDD.Domain.Supermercado;

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
    
    private Producto? ConsultarProducto(string nombreProducto) => catalogo.ConsultarProductos().FirstOrDefault(f => f.Nombre == nombreProducto);
    
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