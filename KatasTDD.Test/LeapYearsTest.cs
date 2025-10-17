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
}

public static class LeapYear
{
    public static bool AnioBisiesto(int anio)
    {
        return true;
    }
}