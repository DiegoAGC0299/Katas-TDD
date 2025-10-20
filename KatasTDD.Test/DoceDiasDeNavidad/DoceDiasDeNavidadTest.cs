using AwesomeAssertions;

namespace KatasTDD.Test.DoceDiasDeNavidad;

public class DoceDiasDeNavidadTest
{
    [Fact]
    public void Si_DiaEsUno_Debe_RetornarLetraCancion_Con_LineaCorrespondienteYRegalos()
    {
        var cancion = new Cancion();
        
        cancion.ConstruirCancion(1);

        cancion.ImprimirLetra().Should().BeEquivalentTo("El primer día de navidad,\n Mi verdadero amor me regaló\n Una perdiz en un árbol de peras.");
    }

    [Fact]
    public void Si_DiaEsDos_Debe_RetornarLetraCancion_Con_LineasCorrespondientesYRegalos()
    {
        var cancion = new  Cancion();
        
        cancion.ConstruirCancion(2);
        
        cancion.ImprimirLetra().Should().BeEquivalentTo("En el segundo día de navidad,\n Mi verdadero amor me regaló:\n Dos tórtolas,\n Y Una perdiz en un árbol de peras.");
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

    [Theory]
    [InlineData(
        5, 
        "En el quinto día de Navidad,\n Mi verdadero amor me regaló:\n Cinco anillos de oro,\n Cuatro pájaros que llaman," +
        "\n Tres gallinas francesas,\n Dos tórtolas,\n Y una perdiz en un árbol de peras.")]
    [InlineData(
        8, 
        "En el octavo día de Navidad,\n Mi verdadero amor me regaló:\n Ocho doncellas ordeñando,\n Siete cisnes nadando,\n " +
        "Seis gansos poniendo,\n Cinco anillos de oro,\n Cuatro pájaros que llaman,\n Tres gallinas francesas,\n Dos tórtolas,\n Y una perdiz en un árbol de peras.")]
    [InlineData(
        12, 
        "En el duodécimo día de Navidad,\n Mi verdadero amor me regaló:\n Doce tamborileros tocando el tambor," +
        "\n Once gaiteros,\n Diez señores saltando,\n Nueve damas bailando,\n Ocho doncellas ordeñando,\n Siete cisnes nadando,\n " +
        "Seis gansos poniendo,\n Cinco anillos de oro,\n Cuatro pájaros que llaman,\n Tres gallinas francesas,\n Dos tórtolas,\n Y una perdiz en un árbol de peras.")]
    public void Si_DiaEstaEntreUnoYDoce_Debe_RetornarLetraCancion_Con_LineasCorrespondientesYRegalos(int dia, string cancionEsperada)
    {
        var cancion = new Cancion();
        
        cancion.ConstruirCancion(dia);
        
        cancion.ImprimirLetra().Should().BeEquivalentTo(cancionEsperada);
    }
}

public class Cancion
{
    private string _letra = string.Empty;
    public void ConstruirCancion(int dia)
    {
        if (dia < 1 || dia > 12)
            throw new ArgumentOutOfRangeException(nameof(dia), "El día debe estar entre 1 y 12.");

        ConstruirLineas(dia);
    }

    private void ConstruirLineas(int dia)
    {
        var ordinario = OrdinalNumero(dia);
        if (dia == 1)
            _letra = $"El {ordinario} día de navidad,\n Mi verdadero amor me regaló";
        else
            _letra = $"En el {ordinario} día de navidad,\n Mi verdadero amor me regaló:";
        
        for(int i = dia; i >= 1; i--)
        {
            _letra += "\n ";

            if (i == 1 && dia > 1)
                _letra += "Y ";
            
            _letra += $"{RegalosDia[i]}{(i == 1 ? "." : ",")}";
        }
    }

    public string ImprimirLetra()
    {
        return _letra;
    }

    private readonly Dictionary<int, string> RegalosDia = new()
    {
        { 1, "Una perdiz en un árbol de peras" },
        { 2, "Dos tórtolas" },
        { 3, "Tres gallinas francesas" },
        { 4, "Cuatro pájaros que llaman" },
        { 5, "Cinco anillos de oro" },
        { 6, "Seis gansos poniendo" },
        { 7, "Siete cisnes nadando" },
        { 8, "Ocho doncellas ordeñando" },
        { 9, "Nueve damas bailando" },
        { 10, "Diez señores saltando" },
        { 11, "Once gaiteros" },
        { 12, "Doce tamborileros tocando el tambor" }
    };

    public string OrdinalNumero(int numero) => numero switch
    {
        1 => "primer",
        2 => "segundo",
        3 => "tercer",
        4 => "cuarto",
        5 => "quinto",
        6 => "sexto",
        7 => "séptimo",
        8 => "octavo",
        9 => "noveno",
        10 => "décimo",
        11 => "undécimo",
        12 => "duodécimo",
        _ => numero.ToString()
    };


}