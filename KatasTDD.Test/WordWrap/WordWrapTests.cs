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
}

public static class WordsWrap
{
    public static object Wrap(string word, int col)
    {
        if (string.IsNullOrEmpty(word))
            return "";
        
        if (word.Length <= col)
            return word;

        if (col != 0)
            return string.Join("\n", word.Chunk(col).Select(chars => new string(chars)));
        
        throw new Exception();
    }
}