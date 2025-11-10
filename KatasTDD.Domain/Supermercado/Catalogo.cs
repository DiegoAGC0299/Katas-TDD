using System.Data;
using KatasTDD.Domain.Supermercado.DTO;

namespace KatasTDD.Domain.Supermercado;

public class Catalogo
{
    public List<Producto> Productos { get; } = [];
    
    public void RegistrarProductos(List<Producto> productos){
        foreach (var producto in productos)
            RegistrarProducto(producto);
    }
    public void RegistrarProducto(Producto producto)
    {
        LanzarExcepcionSiProductoYaExisteConElMismoNombre(producto);
        Productos.Add(producto);
    }
    
    public void LanzarExcepcionSiProductoNoExisteEnElCatalogo(string nombreProducto)
    {
        if (Productos.All(p => p.Nombre != nombreProducto))
            throw new NullReferenceException($"El producto con nombre {nombreProducto} no existe.");
    }

    private void LanzarExcepcionSiProductoYaExisteConElMismoNombre(Producto producto)
    {
        if(Productos.Any(a => a.Nombre.ToLower().Equals(producto.Nombre.ToLower())))
            throw new DuplicateNameException("Ya existe un producto con el mismo nombre.");
    }
}