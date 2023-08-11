using System.Collections.Generic;
public class Dictionary
{
	private HashSet<string> words;

	public Dictionary()
	{
		words = new HashSet<string>();
		// {
		// 	// Add valid word here
		// 	"APPLE",
		// 	"BANANA",
		// 	"CAT",
		// 	//...(add more words)
		// };
		// // You can manually add valid words here if needed
	}

	public void AddWord(string word)
	{
		words.Add(word);
	}

	public bool LookUp(string word)
	{
		return words.Contains(word.ToUpper());
	}
}
