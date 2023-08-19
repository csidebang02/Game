using System;

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
			return string.IsNullOrEmpty(_boardLetters[x, y]);
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
		public string GetWordFormPosition(List<Position> WordPosition)
		{
			string wordFormed = "";
			foreach (Position posisi in WordPosition)
			{
				string wordResult = GetLetterAtPosition(posisi.GetX(), posisi.GetY());
				wordFormed += wordResult;
			}
			Console.WriteLine("Kata Terbentuk :" + wordFormed);
			return wordFormed;
		}
		// ... (implementasi lainnya)
	}
}