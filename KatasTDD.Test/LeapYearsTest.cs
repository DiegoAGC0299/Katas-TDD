using AwesomeAssertions;

namespace KatasTDD.Test;

public class LeapYearsTest
{
    [Fact]
    public void Si_ElAnioEs2004_Debo_RetornarTrue()
    {
        var resultado = LeapYear.AnioBisiesto();
        
        resultado.Should().BeTrue();
    }
}

public class LeapYear
{
    public static bool AnioBisiesto()
    {
        return true;
    }
}