using AwesomeAssertions;
using KatasTDD.Domain.Supermercado;
using KatasTDD.Domain.Supermercado.DTO;

namespace KatasTDD.Test.Supermercado;

public class CajaTest
{
    private readonly Catalogo _catalogo;

    public CajaTest()
    {
        var productos = new List<Producto>
        {
            new("Manzana", 0.99M),
            new("Arroz", 2.49M)
        };
        
        _catalogo = new Catalogo();
        _catalogo.RegistrarProductos(productos);
    }

    [Fact]
    public void Si_SeAgregaUnaOfertaDeTipoDescuentoDe10PorCientoAlProductoManzana_Debe_LaCajaMostrarOfertaDe10PorCiertoEnManzana()
    {
        var caja = new Caja(_catalogo);
        var valorAplicado = 10;
        var productoAplicado = _catalogo.Productos[0];
        
        caja.AregarOferta(TipoOferta.Descuento, productoAplicado, valorAplicado);
        var primeraOferta = caja.Ofertas[0];

        primeraOferta.Tipo.Should().Be(TipoOferta.Descuento);
        primeraOferta.ProductoAplicado.Should().Be(productoAplicado);
        primeraOferta.ValorOferta.Should().Be(10);

    }
    
    [Fact]
    public void Si_SeAgregaUnaOfertaAUnProductoQueNoExiste_Debe_LanzarExcepcion()
    {
        var caja = new Caja(_catalogo);
        var productoAplicado = new Producto("Piña", 1.49M);
        
        Action caller =() => caja.AregarOferta(TipoOferta.Descuento, productoAplicado);
        
        caller.Should().ThrowExactly<NullReferenceException>().WithMessage($"El producto con nombre {productoAplicado.Nombre} no existe.");

    }
    
    [Fact]
    public void Si_SeAgregaUnaOfertaAUnProductoQueYaCuentaConOfertaExistente_Debe_LanzarExcepcion()
    {
        var caja = new Caja(_catalogo);
        var productoAplicado = _catalogo.Productos[1];
        
        caja.AregarOferta(TipoOferta.Descuento, productoAplicado);
        Action caller =() => caja.AregarOferta(TipoOferta.Descuento, productoAplicado);
        
        caller.Should().ThrowExactly<InvalidOperationException>().WithMessage($"El producto con nombre {productoAplicado.Nombre} ya cuenta con una oferta existente.");

    }
}