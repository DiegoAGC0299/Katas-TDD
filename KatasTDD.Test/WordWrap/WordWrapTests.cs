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
        var groupedWords = GroupTextPerColumn(text, col);
        foreach (var item in groupedWords) result.Add(ChunkWord(col, item));
        return string.Join("\n", result);
    }

    private static bool AllowedColumnValue(int col) => col != 0;

    private static bool TextIsEmptyOrIsShorterThanCol(string text, int col, out string result)
    {
        result = text;
        return string.IsNullOrEmpty(text) || text.Length <= col;
    }

    private static List<string> GroupTextPerColumn(string text, int col)
    {
        var group = new List<string>();
        var words = text.Split(" ");
        var currentLine = string.Empty;
        
        foreach (var wordItem in words)
        {
            var newLine = !string.IsNullOrEmpty(currentLine) ? currentLine + " " + wordItem : wordItem;
            
            if(newLine.Length <= col)
                currentLine = newLine;
            else
            {
                if(!string.IsNullOrEmpty(currentLine))
                    group.Add(currentLine);
                
                currentLine = wordItem;
            }
        }
        
        if(!string.IsNullOrEmpty(currentLine))
            group.Add(currentLine);
        
        return group;
    }
    private static string ChunkWord(int col, string item) => string.Join("\n", item.Chunk(col).Select(chars => new string(chars)));
}