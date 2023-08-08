namespace ScrabbleGame
{
	interface IBoard
	{
		int GetBoardSize();
		char GetLetterAtPosition(int x, int y);
		Position GetLetterPosition(char letter);
		void PlaceLetterAtPosition(int x, int y, char letter);
		bool IsPositionEmpty(int x, int y);
	}
}
