using AwesomeAssertions;

namespace KatasTDD.Test;

public class LeapYearsTest
{
    [Fact]
    public void Si_ElAnioEs2004_Debo_RetornarTrue()
    {
        LeapYear.EsAnioBisiesto(2004).Should().BeTrue();
    }

    [Fact]
    public void Si_ElAnioEs2017_Debo_RetornarFalse()
    {
        LeapYear.EsAnioBisiesto(2017).Should().BeFalse();
    }

    [Theory]
    [InlineData(2005)]
    [InlineData(2006)]
    [InlineData(2007)]
    public void Si_ElAnioNoEsDivisiblePor4_Debo_RetornarFalse(int anio)
    {
        LeapYear.EsAnioBisiesto(anio).Should().BeFalse();
    }

    [Theory]
    [InlineData(2008)]
    [InlineData(2012)]
    [InlineData(1976)]
    public void Si_ElAnioEsDivisiblePor4_YNoEsDivisiblePor100_Debo_RetornarTrue(int anio)
    {
        LeapYear.EsAnioBisiesto(anio).Should().BeTrue();
    }
    
    [Theory]
    [InlineData(1600)]
    [InlineData(2000)]
    public void Si_ElAnioEsDivisiblePor400_Debo_RetornarTrue(int anio)
    {
        LeapYear.EsAnioBisiesto(anio).Should().BeTrue();
    }
}

public static class LeapYear
{
    public static bool EsAnioBisiesto(int anio)
        => ValidarAnioBisiesto(anio);
    

    private static bool ValidarAnioBisiesto(int anio)
    {
        if (anio % 400 == 0)
            return true;
        
        if (anio % 4 != 0)
            return false;
        
        return anio % 10 != 0;
    }
}