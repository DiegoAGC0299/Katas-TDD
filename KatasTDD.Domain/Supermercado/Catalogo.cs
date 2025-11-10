using System.Data;
using KatasTDD.Domain.Supermercado.DTO;

namespace KatasTDD.Domain.Supermercado;

public class Catalogo
{
    private readonly List<Producto> _productos = [];
    
    public void RegistrarProductos(List<Producto> productos){
        foreach (var producto in productos)
            RegistrarProducto(producto);
    }
    public void RegistrarProducto(Producto producto)
    {
        LanzarExcepcionSiProductoYaExisteConElMismoNombre(producto);
        _productos.Add(producto);
    }
    
    public List<Producto> ConsultarProductos()
        => _productos;

    private void LanzarExcepcionSiProductoYaExisteConElMismoNombre(Producto producto)
    {
        if(_productos.Any(a => a.Nombre.ToLower().Equals(producto.Nombre.ToLower())))
            throw new DuplicateNameException("Ya existe un producto con el mismo nombre.");
    }
}