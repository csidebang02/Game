using System.Collections.Generic;
public class Dictionary
{
	private HashSet<string> words;

	public Dictionary()
	{
		words = new HashSet<string>();
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
	
	static HashSet<string> LoadWordSetFromFile(string filePath)
		{
			HashSet<string> wordSet = new HashSet<string>();

			try
			{
				// Baca semua baris dari file
				string[] lines = File.ReadAllLines(filePath);

				// Tambahkan setiap kata ke dalam himpunan
				foreach (string line in lines)
				{
					string word = line.Trim().ToLower();
					wordSet.Add(word);
				}
			}
			catch (Exception e)
			{
				Console.WriteLine("Error: " + e.Message);
			}

			return wordSet;
		}
	public void SetWord(string filePath)
	{
		words = LoadWordSetFromFile(filePath);
	}
}