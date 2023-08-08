namespace ScrabbleGame
{
	class Board : IBoard
	{
		private int _boardSize;
		private char[,] _boardLetters;

		public Board(int size)
		{
			_boardSize = size;
			_boardLetters = new char[size, size];
		}

		public int GetBoardSize()
		{
			return _boardSize;
		}

		public char GetLetterAtPosition(int x, int y)
		{
			return _boardLetters[x, y];
		}

		public void PlaceLetterAtPosition(int x, int y, char letter)
		{
			_boardLetters[x, y] = letter;
		}

		public bool IsPositionEmpty(int x, int y)
		{
			return _boardLetters[x, y] == '\0';
		}
		public Position GetLetterPosition(char letter)
		{
			for (int y = 0; y < _boardSize; y++)
			{
				for (int x = 0; x < _boardSize; x++)
				{
					if (_boardLetters[x, y] == letter)
					{
						return new Position(x, y);
					}
				}
			}
			return null;
		}
		// ... (implementasi lainnya)
	}
}