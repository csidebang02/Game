public class Rack
{
    private char[] letters;

    public Rack()
    {
        letters = new char[7];
    }

    public bool AddLetter(char letter)
    {
        for (int i = 0; i < letters.Length; i++)
        {
            if (letters[i] == '\0')
            {
                letters[i] = letter;
                return true;
            }
        }
        return false;
    }

    public bool RemoveLetter(char letter)
    {
        for (int i = 0; i < letters.Length; i++)
        {
            if (letters[i] == letter)
            {
                letters[i] = '\0';
                return true;
            }
        }
        return false;
    }
}
