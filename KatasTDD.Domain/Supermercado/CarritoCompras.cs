using KatasTDD.Domain.Supermercado.DTO;

namespace KatasTDD.Domain.Supermercado;

public class CarritoCompras(Catalogo catalogo)
{
    public List<ListaCompra> ProductosAgregados { get; } = [];
    private const int CantidadPorDefecto = 1;

    public void AgregarProductoALaLista(string nombreProducto)
    {
        catalogo.LanzarExcepcionSiProductoNoExisteEnElCatalogo(nombreProducto);
        AgregarOModificarProductoDeLaLista(nombreProducto, ConsultarProducto(nombreProducto));
    }

    public List<ListaCompra> ObtenerLista() => ProductosAgregados;
    
    private Producto? ConsultarProducto(string nombreProducto) => catalogo.Productos.FirstOrDefault(f => f.Nombre == nombreProducto);
    
    private void AgregarOModificarProductoDeLaLista(string nombreProducto, Producto productoEnCatalogo)
    {
        var productoEnLista = ProductosAgregados.FirstOrDefault(f => f.Producto?.Nombre == nombreProducto);

        if (productoEnLista is not null)
            productoEnLista.Cantidad++;
        else
            ProductosAgregados.Add(new ListaCompra(productoEnCatalogo, CantidadPorDefecto));
        
    }
}