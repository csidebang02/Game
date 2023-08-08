namespace ScrabbleGame
{
	class Board : IBoard
	{
		private int _boardSize;
		private string[,] _boardLetters;

		public Board(int size)
		{
			_boardSize = size;
			_boardLetters = new string[size, size];
		}

		public int GetBoardSize()
		{
			return _boardSize;
		}

		public string GetLetterAtPosition(int x, int y)
		{
			return _boardLetters[x, y];
		}

		public void PlaceLetterAtPosition(int x, int y, string letter)
		{
			_boardLetters[x, y] = letter;
		}

		public bool IsPositionEmpty(int x, int y)
		{
			return _boardLetters[x, y] == null;
		}
		public Position GetLetterPosition(string letter)
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