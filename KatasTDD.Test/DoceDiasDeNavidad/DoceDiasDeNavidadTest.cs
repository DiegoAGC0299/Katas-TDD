using AwesomeAssertions;

namespace KatasTDD.Test.DoceDiasDeNavidad;

public class DoceDiasDeNavidadTest
{
    [Fact]
    public void Si_DiaEsUno_Debe_RetornarLetraCancion_Con_LineaCorrespondienteYRegalos()
    {
        var cancion = new Cancion();
        
        cancion.ConstruirCancion(1);

        cancion.ImprimirLetra().Should().BeEquivalentTo("El primer día de navidad \n Mi verdadero amor me regaló \n Una perdiz en un arbol de peras");
    }
}

public class Cancion
{
    public void ConstruirCancion(int i)
    {
        throw new NotImplementedException();
    }

    public string ImprimirLetra()
    {
        throw new NotImplementedException();
    }
}