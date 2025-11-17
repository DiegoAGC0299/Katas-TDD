using System.Text;
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
}

public static class WordsWrap
{
    public static string Wrap(string text, int col)
    {
        if (TextIsEmptyOrIsShorterThanCol(text, col, out var textResult))
            return textResult;

        if (AllowedColumnValue(col))
            return WrapText(text, col);
        
        throw new Exception();
    }

    private static string WrapText(string text, int col)
    {
        var result = new List<string>();
        var words = text.Split(' ');

        foreach (var item in words) result.Add(string.Join("\n", item.Chunk(col).Select(chars => new string(chars))));
        return string.Join("\n", result);
    }

    private static bool AllowedColumnValue(int col) => col != 0;

    private static bool TextIsEmptyOrIsShorterThanCol(string text, int col, out string result)
    {
        result = text;
        return string.IsNullOrEmpty(text) || text.Length <= col;
    }
}