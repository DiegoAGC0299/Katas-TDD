using AwesomeAssertions;
using KatasTDD.Domain.WordWrap;

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

    [Fact]
    public void If_ColIs2_Must_ReturnStringWithLineBreakEvery2Characters()
    {
        var result = WordsWrap.Wrap("word", 2);

        result.Should().Be("wo\nrd");
    }

    [Fact]
    public void If_ColIs3_Must_ReturnStringWithLineBreakEvery3Characters()
    {
        var result = WordsWrap.Wrap("abcdefghij", 3);

        result.Should().Be("abc\ndef\nghi\nj");
    }

    [Fact]
    public void If_TextContainsTwoWordsAndColIs3_Must_ReturnStringWithLineBreakEveryWordAnd3Characters()
    {
        var result = WordsWrap.Wrap("word word", 3);

        result.Should().Be("wor\nd\nwor\nd");
    }

    [Fact]
    public void If_TextContainsTwoWordsAndColIs6_Must_ReturnStringWithLineBreakEveryWord()
    {
        var result = WordsWrap.Wrap("word word", 6);

        result.Should().Be("word\nword");
    }

    [Fact]
    public void If_TextContainsTwoWordsAndColIs5_Must_ReturnStringWithLineBreakEveryWord()
    {
        var result = WordsWrap.Wrap("word word", 5);

        result.Should().Be("word\nword");
    }

    [Fact]
    public void If_TextContainsThreeWordsAndColIs6_Must_ReturnStringWithLineBreakEveryWord()
    {
        var result = WordsWrap.Wrap("word word word", 6);

        result.Should().Be("word\nword\nword");
    }

    [Fact]
    public void If_TextContainsThreeWordsAndColIs11_Must_ReturnStringWithLineBreakBetweenTwoWordsAndTheLast()
    {
        var result = WordsWrap.Wrap("word word word", 11);

        result.Should().Be("word word\nword");
    }
}