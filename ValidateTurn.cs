using System.Text;
using ScrabbleGame;

public class ValidateTurn
{
	private List<string> wordHistory;// _
	private Dictionary<Position, string> tilesHistory;
	private Dictionary<Position, string> playerSetLetter;
	private Dictionary dictionary;
	private IBoard board;

	public ValidateTurn(IBoard board, Dictionary<Position, string> playerSetLetter, Dictionary dictionary)
	{
		this.board = board; //// Initialize the board field
		this.playerSetLetter = playerSetLetter;
		this.dictionary = dictionary;
		wordHistory = new List<string>();
		tilesHistory = new Dictionary<Position, string>();
	}

	public bool CheckConnectTiles()
	{
		// Iterate through all placed letters on the boadrd
		foreach (var position in playerSetLetter.Keys)
		{
			int x = position.X;
			int y = position.Y;
			
			//Check if
			if((x > 0 && !board.IsPositionEmpty(x - 1, y)) ||
				(x < board.GetBoardSize() - 1 && !board.IsPositionEmpty(x + 1, y)) ||
				(y > 0 && !board.IsPositionEmpty(x, y - 1)) ||
				(y < board.GetBoardSize() - 1 && !board.IsPositionEmpty(x, y + 1)))
			{
				return true; // At least on adjacent tile is not empty
			}
		}
		return false; // No adjacent tiles with letters found
	}

	public bool CheckValidPlacement()
	{
		// Implement valid placement logic here
		foreach (var position in playerSetLetter.Keys)
		{
			int x = position.X;
			int y = position.Y;
			
			//Check if adjacent tiles contain letters
			if ((x > 0 && board.IsPositionEmpty(x - 1, y)) &&
				(x < board.GetBoardSize() - 1 && board.IsPositionEmpty(x + 1, y)) &&
				(y > 0 && board.IsPositionEmpty(x, y - 1)) &&
				(y < board.GetBoardSize() - 1 && board.IsPositionEmpty(x, y + 1)))
			{
				return false; // APlaced letter has no adjacent letters
			}
			if (!playerSetLetter.ContainsKey(new Position(x + 1, y)) &&
				!playerSetLetter.ContainsKey(new Position(x - 1, y)) &&
				!playerSetLetter.ContainsKey(new Position(x, y + 1)) &&
		   		!playerSetLetter.ContainsKey(new Position(x, y - 1)))
			{
				return false; //Placed letter is not adjacent to any playerSetLetter
			}
		}
		return true; // Valid placement
	}

	public bool ValidateWord()
	{
		StringBuilder wordBuilder = new StringBuilder();
		
		//Collect letters placed on the board
		foreach (var kvp in playerSetLetter)
		{
			Position position = kvp.Key;
			string letter = kvp.Value;
			wordBuilder.Append(letter);
		}
		
		string word = wordBuilder.ToString();
		
		//Check if the word is in the dictionary
		bool isValidWord = dictionary.LookUp(word);
		
		return isValidWord;
	}
}
