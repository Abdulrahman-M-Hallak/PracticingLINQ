namespace LINQ;
internal class LINQAndStrings
{
    public static void FindOccurencesInText(string text , string searchWord)
    {
        string[] words = text.Split(new char[] {' ' , '.' ,',' , '?' , '!' , ';' , ':' } , StringSplitOptions.RemoveEmptyEntries);

        int wordOccurences =
            (from word in words
            where word.Equals(searchWord , StringComparison.InvariantCultureIgnoreCase)
            select word).Count();

        Console.WriteLine(wordOccurences);
    }

    public static string QuertySentence(string text , params string[] wordsToMatch)
    {
        string[] sentences = text.Split(new char[] { '!', '?', '.' });

        var sentenceQuery =
            from sentence in sentences
            let w = sentence.Split(new char[] { ' ', '.', ',', '?', '!', ';', ':' }, StringSplitOptions.RemoveEmptyEntries)
            where w.Distinct().Intersect(wordsToMatch).Count() == wordsToMatch.Count()
            select sentence;
        return sentenceQuery.ToString();
    }
}
