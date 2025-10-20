using AwesomeAssertions;

namespace KatasTDD.Test.DoceDiasDeNavidad;

public class DoceDiasDeNavidadTest
{
    [Fact]
    public void Si_DiaEsUno_Debe_RetornarLetraCancion_Con_LineaCorrespondienteYRegalos()
    {
        var cancion = new Cancion();
        
        cancion.ConstruirCancion(1);

        cancion.ImprimirLetra().Should().BeEquivalentTo("El primer día de navidad \n Mi verdadero amor me regaló \n Una perdiz en un arbol de peras.");
    }

    [Fact]
    public void Si_DiaEsDos_Debe_RetornarLetraCancion_Con_LineasCorrespondientesYRegalos()
    {
        var cancion = new  Cancion();
        
        cancion.ConstruirCancion(2);
        
        cancion.ImprimirLetra().Should().BeEquivalentTo("En el segundo día de navidad \n Mi verdadero amor me regaló \n Dos tórtolas, \n y Una perdiz en un arbol de peras.");
    }

    [Theory]
    [InlineData(0)]
    [InlineData(13)]
    public void Si_DiaEsMenorAUnoOMayorADoce_Debe_RetornarExcepcionDeTipoFueraDeRango(int dia)
    {
        var cancion = new Cancion();
        
        Action accion = () => cancion.ConstruirCancion(dia);
        
        accion.Should().Throw<ArgumentOutOfRangeException>().WithMessage("El día debe estar entre 1 y 12.*");
        
        
    }
}

public class Cancion
{
    private string _letra = string.Empty;
    public void ConstruirCancion(int dia)
    {
        if (dia < 1 || dia > 12)
            throw new ArgumentOutOfRangeException(nameof(dia), "El día debe estar entre 1 y 12.");
        
        if(dia == 1)
            _letra = "El primer día de navidad \n Mi verdadero amor me regaló \n Una perdiz en un arbol de peras.";

        if (dia == 2)
            _letra =
                "En el segundo día de navidad \n Mi verdadero amor me regaló \n Dos tórtolas, \n y Una perdiz en un arbol de peras.";
        
    }

    public string ImprimirLetra()
    {
        return _letra;
    }
}