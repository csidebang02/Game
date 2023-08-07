public class Dictionary
{
    private List<string> words;

    public Dictionary()
    {
        words = new List<string>();
        // You can manually add valid words here if needed
    }

    public void AddWord(string word)
    {
        words.Add(word);
    }

    public bool LookUp(string word)
    {
        return words.Contains(word);
    }
}
