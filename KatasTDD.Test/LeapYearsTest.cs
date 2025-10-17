using AwesomeAssertions;

namespace KatasTDD.Test;

public class LeapYearsTest
{
    [Fact]
    public void Si_ElAnioEs2004_Debo_RetornarTrue()
    {
        LeapYear.AnioBisiesto(2004).Should().BeTrue();
    }

    [Fact]
    public void Si_ElAnioEs2017_Debo_RetornarFalse()
    {
        LeapYear.AnioBisiesto(2017).Should().BeFalse();
    }

    [Theory]
    [InlineData(2005)]
    [InlineData(2006)]
    [InlineData(2007)]
    public void Si_ElAnioNoEsDivisiblePor4_Debo_RetornarFalse(int anio)
    {
        LeapYear.AnioBisiesto(anio).Should().BeFalse();
    }
}

public static class LeapYear
{
    public static bool AnioBisiesto(int anio)
    {
        if(anio % 4 != 0)
            return false;
        
        return true;
    }
}