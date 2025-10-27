using AwesomeAssertions;

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
    
    [Fact]
    public void Si_NumeroEsNegativo_Debe_RetornarExcepcionDeTipoFueraDeRango()
    {
        var caller = () => NumeracionRomana.ConvertirNumeroARomano(-1);

        caller.Should().ThrowExactly<ArgumentOutOfRangeException>().WithMessage("El número debe ser mayor a cero. (Parameter 'numero')");
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
}

public static class NumeracionRomana
{
    public static string ConvertirNumeroARomano(int numero)
    {
        ValidarRangoPermitido(numero);
        return CalcularNumeroRomano(numero);
    }
    
    private static void ValidarRangoPermitido(int numero)
    {
        if(numero < 0)
            throw new ArgumentOutOfRangeException(nameof(numero), "El número debe ser mayor a cero.");
    }

    private static string CalcularNumeroRomano(int numero)
    {
        return numero switch
        {
            10 => "X",
            9 => "IX",
            >=6 => $"V{new string('I', numero - 5)}",
            5 => "V",
            4 => "IV",
            _ => new string('I', numero)
        };
    }

    
}