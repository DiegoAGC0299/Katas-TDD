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
    private string letra { get; set; }
    public void ConstruirCancion(int i)
    {
        letra = "El primer día de navidad \n Mi verdadero amor me regaló \n Una perdiz en un arbol de peras";
    }

    public string ImprimirLetra()
    {
        return letra;
    }
}