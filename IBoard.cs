namespace ScrabbleGame
{
	public interface IBoard
	{
		int GetBoardSize();
		string GetLetterAtPosition(int x, int y);
		Position GetLetterPosition(string letter);
		void PlaceLetterAtPosition(int x, int y, string letter);
		bool IsPositionEmpty(int x, int y);
	}
}
