using System.Collections.Generic;
using ScrabbleGame;

public class ScoreCounter
{
	private Dictionary<char, int> pointValues;
	private GameRunner game;
	
	public ScoreCounter(GameRunner game)
	{
		this.game = game;
		
		pointValues = new Dictionary<char, int>
		{
			{ 'A', 1 }, { 'B', 3 }, { 'C', 3 }, { 'D', 2 }, { 'E', 1 }, { 'F', 4 }, { 'G', 2 }, { 'H', 4 },
			{ 'I', 1 }, { 'J', 8 }, { 'K', 5 }, { 'L', 1 }, { 'M', 3 }, { 'N', 1 }, { 'O', 1 }, { 'P', 3 },
			{ 'Q', 10 }, { 'R', 1 }, { 'S', 1 }, { 'T', 1 }, { 'U', 1 }, { 'V', 4 }, { 'W', 4 }, { 'X', 8 },
			{ 'Y', 4 }, { 'Z', 10}
		};
	}

	public int CalculateScore(IPlayer player)
	{
		// Implement your scoring logic here
		int score = 0;
		Rack rack = playerRacks[player];
		foreach (var position in playerSetLetter.Keys)
		{
			string letter = playerSetLetter[position];
			char uppercaseLetter = char.ToUpper(letter[0]); //Ensure uppercase for consistency
			
			//Calculate the score for the letter based on its point value and any
			int letterScore = pointValues.ContainsKey(uppercaseLetter) ? pointValues[uppercaseLetter] : 0;
			int wordMultiplier = GetWordMultiplier(position);
			int letterMultiplier = GetLetterMultiplier(position);
			
			score += letterScore * letterMultiplier * wordMultiplier;
		}
		// Calculate the score based on the player's letters and board state
		return score;
	}
}
