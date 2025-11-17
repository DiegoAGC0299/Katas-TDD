namespace KatasTDD.Domain.WordWrap;

public static class WordsWrap
{
    private const string BreakLine = "\n";
    private const string WhiteSpace = " ";
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
        return string.Join(BreakLine, result);
    }

    private static bool AllowedColumnValue(int col) => col != 0;

    private static bool TextIsEmptyOrIsShorterThanCol(string text, int col, out string result)
    {
        result = text;
        return string.IsNullOrEmpty(text) || TextIsShorterThanOrEqualTo(text, col);
    }

    private static bool TextIsShorterThanOrEqualTo(string text, int col) => text.Length <= col;

    private static List<string> GroupTextPerColumn(string text, int col)
    {
        var groupWords = new List<string>();
        var wordsWithoutSpaces = text.Split(WhiteSpace);
        var currentLine = string.Empty;

        foreach (var word in wordsWithoutSpaces)
        {
            var newLine = TextIsNotEmpty(currentLine) ? currentLine + WhiteSpace + word : word;

            if (TextIsShorterThanOrEqualTo(newLine, col))
                currentLine = newLine;
            
            else
            {
                if (TextIsNotEmpty(currentLine))
                    groupWords.Add(currentLine);

                currentLine = word;
            }
        }

        if (TextIsNotEmpty(currentLine))
            groupWords.Add(currentLine);

        return groupWords;
    }

    private static bool TextIsNotEmpty(string currentLine) => !string.IsNullOrEmpty(currentLine);

    private static string ChunkWord(int col, string item) =>
        string.Join(BreakLine, item.Chunk(col).Select(chars => new string(chars)));
}