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

    [Fact]
    public void If_WordIsShorterThanCol_Must_ReturnSameWord()
    {
        var result = WordsWrap.Wrap("this", 10);

        result.Should().Be("this");
    }
}

public static class WordsWrap
{
    public static object Wrap(string word, int col)
    {
        return word.Length < col ? word : "";
    }
}