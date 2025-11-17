using AwesomeAssertions;

namespace KatasTDD.Test.WordWrap;

public class WordWrapTests
{
    [Fact]
    public void If_WordIsEmpty_Must_ReturnEmptyString()
    {
        var result = WordsWrap.Wrap("", 1);

        result.Should().Be("");
    }
}

public static class WordsWrap
{
    public static object Wrap(string word, int col)
    {
        return "";
    }
}