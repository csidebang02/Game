public class Rack
{
    private string[] letters;

    public Rack()
    {
        letters = new string[7];
    }

    public bool AddLetter(string letter)
    {
        for (int i = 0; i < letters.Length; i++)
        {
            if (letters[i] == null)
            {
                letters[i] = letter;
                return true;
            }
        }
        return false;
    }

    public bool RemoveLetter(string letter)
    {
        // for (int i = 0; i < letters.Length; i++)
        // {
        //     if (letters[i] == letter)
        //     {
        //         letters[i] = '\0';
        //         return true;
        //     }
        // }
        return false;
    }

    public bool ContainsLetter(string letter)
    {
        return letters.Contains(letter);
    }

    public string[] Letters // Properti untuk mengakses huruf-huruf dalam rack
    {
        get { return letters; }
    }
}
