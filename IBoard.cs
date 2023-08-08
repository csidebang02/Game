namespace ScrabbleGame
{
	interface IBoard
	{
		int GetBoardSize();
		char GetLetterAtPosition(int x, int y);
		Position GetLetterPosition(char letter);

	}
}
