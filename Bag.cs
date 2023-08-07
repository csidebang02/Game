public class Bag
{
    private List<char> letters; //string

    public Bag()
    {
        letters = new List<char>();
        // Initialize the bag with letters
        for (char letter = 'A'; letter <= 'Z'; letter++)
        {
            letters.Add(letter);
        }
    }

    public void AddLetter(char letter)
    {
        letters.Add(letter);
    }

    public bool RemoveLetter(char letter)
    {
        if (letters.Contains(letter))
        {
            letters.Remove(letter);
            return true;
        }
        return false;
    }
}
