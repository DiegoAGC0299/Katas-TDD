using AwesomeAssertions;
using KatasTDD.Domain;

namespace KatasTDD.Test.NumerosRomanos;

public class NumeracionRomanaTests
{
    [Fact]
    public void Si_NumeroEsUno_Debe_RetornarI()
    {
        var numeroRomano = NumeracionRomana.ConvertirNumeroARomano(1);

        numeroRomano.Should().BeEquivalentTo("I");
    }
    
    [Fact]
    public void Si_NumeroEsDos_Debe_RetornarII()
    {
        var numeroRomano = NumeracionRomana.ConvertirNumeroARomano(2);

        numeroRomano.Should().BeEquivalentTo("II");
    }
    
    [Fact]
    public void Si_NumeroEsTres_Debe_RetornarIII()
    {
        var numeroRomano = NumeracionRomana.ConvertirNumeroARomano(3);

        numeroRomano.Should().BeEquivalentTo("III");
    }
    
    [Theory]
    [InlineData(0)]
    [InlineData(4000)]
    public void Si_NumeroEstaFueraDelRango_Debe_RetornarExcepcionDeTipoFueraDeRango(int numero)
    {
        var caller = () => NumeracionRomana.ConvertirNumeroARomano(numero);

        caller.Should().ThrowExactly<ArgumentOutOfRangeException>().WithMessage("El número debe estar entre 1 y 3999. (Parameter 'numero')");
    }
    
    [Fact]
    public void Si_NumeroEsCuatro_Debe_RetornarIV()
    {
        var numeroRomano = NumeracionRomana.ConvertirNumeroARomano(4);

        numeroRomano.Should().BeEquivalentTo("IV");
    }
    
    [Fact]
    public void Si_NumeroEsCinco_Debe_RetornarV()
    {
        var numeroRomano = NumeracionRomana.ConvertirNumeroARomano(5);

        numeroRomano.Should().BeEquivalentTo("V");
    }
    
    [Theory]
    [InlineData(6, "VI")]
    [InlineData(7, "VII")]
    [InlineData(8, "VIII")]
    public void Si_NumeroEsMayorASeisOMenorA8_Debe_RetornarNumeroRomanoCorrecto(int numero, string numeroRomanoEsperado)
    {
        var numeroRomano = NumeracionRomana.ConvertirNumeroARomano(numero);

        numeroRomano.Should().BeEquivalentTo(numeroRomanoEsperado);
    }
    
    [Fact]
    public void Si_NumeroEsNueve_Debe_RetornarIX()
    {
        var numeroRomano = NumeracionRomana.ConvertirNumeroARomano(9);

        numeroRomano.Should().BeEquivalentTo("IX");
    }
    
    [Fact]
    public void Si_NumeroEsDiez_Debe_RetornarX()
    {
        var numeroRomano = NumeracionRomana.ConvertirNumeroARomano(10);

        numeroRomano.Should().BeEquivalentTo("X");
    }
    
    [Theory]
    [InlineData(25, "XXV")]
    [InlineData(166, "CLXVI")]
    [InlineData(901, "CMI")]
    [InlineData(1986, "MCMLXXXVI")]
    [InlineData(2820, "MMDCCCXX")]
    public void Si_NumeroEsEntreUnoYCuatroMil_Debe_RetornarElNumeroRomanoCorrespondiente(int numero, string numeroRomanoEsperado)
    {
        var numeroRomano = NumeracionRomana.ConvertirNumeroARomano(numero);

        numeroRomano.Should().BeEquivalentTo(numeroRomanoEsperado);
    }
}