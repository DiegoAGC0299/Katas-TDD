using KatasTDD.Domain.Supermercado.DTO;

namespace KatasTDD.Domain.Supermercado;

public class Caja(Catalogo catalogo)
{
    public Catalogo Catalogo { get; } = catalogo;
    public List<Oferta> Ofertas { get; } = [];

    public void AregarOferta(TipoOferta tipo, Producto producto, double? valorAplicado = null)
        => Ofertas.Add(new Oferta(tipo, producto, valorAplicado));
}