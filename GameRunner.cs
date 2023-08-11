using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScrabbleGame
{
	public class GameRunner
	{
		private Dictionary<int, IPlayer> players;
		private Dictionary<IPlayer, Rack> playerRacks;
		private IBoard board;
		private IPlayer currentPlayer;
		private Dictionary<Position, string> playerSetLetter;
		private Bag bag;
		private ScoreCounter scoreCounter;
		private Dictionary dictionary;
		private List<int> playerKeys;
		private int _boardSize;
		private string[,] _boardLetters;
		private int[,] wordMultipliers;
		private int[,] letterMultipliers;

		public GameRunner(int boardSize, string initialLetters)
		{
			players = new Dictionary<int, IPlayer>();
			playerRacks = new Dictionary<IPlayer, Rack>();
			board = new Board(boardSize);
			currentPlayer = null;
			playerSetLetter = new Dictionary<Position, string>();
			bag = new Bag(initialLetters);
			scoreCounter = new ScoreCounter();
			dictionary = new Dictionary();
			playerKeys = new List<int>();
			_boardSize = boardSize;
			_boardLetters  = new string[boardSize, boardSize];
			wordMultipliers = new int[boardSize, boardSize];
			letterMultipliers = new int[boardSize, boardSize];
			
			for (int x = 0; x < boardSize; x++)
			{
				for (int y = 0; y < boardSize; y++)
				{
					if (x == y || x == boardSize - y - 1)
					{
						wordMultipliers[x, y] = 2;
					}
					else if ((x % 4 == 1 && 4 == 1) || (x % 4 == 3 && y % 4 == 3))
					{
						letterMultipliers[x, y] = 3;
					}
				}
			}
		}
		
		public int GetWordMultiplier(Position position)
		{
			return wordMultipliers[position.X, position.Y];
		}
		
		public int GetLetterMultiplier(Position position)
		{
			return letterMultipliers[position.X, position.Y];
		}
		
		public string GetBoardLetter(int x, int y)
		{
			return board.GetLetterAtPosition(x, y);
		}
		
		public int GetBoardSize()
		{
			return board.GetBoardSize();
		}

		public void AddPlayer(IPlayer player)
		{
			players.Add(player.GetId(), player);
			Rack isiRack = new Rack();
			string karakter = "D";
			isiRack.AddLetter(karakter);
			playerRacks.Add(player, isiRack);
			if (currentPlayer == null)
			{
				currentPlayer = player;
			}
		}

		public bool IsGameEnd()
		{
			// Implement game end logic here
			return false;
		}

		public IPlayer GetCurrentPlayer()
		{
			return currentPlayer;
		}

		public Rack GetPlayerRack(IPlayer player)
		{
			return playerRacks[player];
		}

		public bool SetWord(int x, int y, string letter)
		{
			//tambah validasi terkait 
				//cek posisi yg diinput itu valid atau ngga? dicek dengan cara bandingkan dengan size
			int boardSize = board.GetBoardSize();
			if (x > boardSize || y > boardSize )
			{
				Console.WriteLine("Gagal karena kordinat melebih size board");
				Console.WriteLine($"{x}");
				Console.WriteLine($"{y}");
				Console.WriteLine($"{boardSize}");
				return false;
			}
				//cek sudah ada huruf di situ atau belum? kalau sudah ada, return false
			if (!board.IsPositionEmpty(x, y))
			{
				Console.WriteLine("Kordinat sudah terdapat Huruf");
				return false;
			}
				//cek playerRacks, dia punya huruf yg diinput ga? kalo ga, return false
			Rack rack = playerRacks[currentPlayer];
			if (!rack.ContainsLetter(letter))
			{
				Console.WriteLine("Player tidak mempunya huruf tersebut");
				return false;
			}
				//kalo melewati itu semua, maka simpan ke dalam Board -> BoardLetters
			board.PlaceLetterAtPosition(x, y, letter);
			playerSetLetter[new Position(x, y)] = letter;
			// rack.RemoveLetter(letter);
				//kalo melewati itu semua, maka simpan ke dalam Board -> BoardLetters, lalu return true
			// Implement word placement logic here
			return true;
		}

		public void SubmitTurn()
		{
			// mengganti currentplayer dari player lain di dalam list Players
			// ambil currentPlayer
			IPlayer currentPlayer = GetCurrentPlayer();
			// ambil currentplayer tsb di dalam list Players dengan menggunakan player id
			int currentPlayerIndex = playerKeys.IndexOf(currentPlayer.GetId());
			
			if (currentPlayerIndex != -1)
			{
				// set currentPlayer menjadi player selanjutnya (ambil dari list Players dengan menggunakan player id dari currentPlayer sebelumnya, lalu + 1)
				int nextPlayerIndex = (currentPlayerIndex + 1) % playerKeys.Count;
				int nextPlayerKey = playerKeys[nextPlayerIndex];
				IPlayer nextPlayer = players[nextPlayerKey];
				
				// Mengubah currentPlayer menjadi nextPlayer
				currentPlayer = nextPlayer;
				
				// Memperbarui currentPlayer dalam objek GameRunner
				this.currentPlayer = currentPlayer;
			}
		}

		public void SkipTurn()
		{
			int currentPlayerId = currentPlayer.GetId();
			int currentPlayerIndex = playerKeys.IndexOf(currentPlayerId);

			if (currentPlayerIndex != -1)
			{
				// Calculate the index of the next player to take a turn
				int nextPlayerIndex = (currentPlayerIndex + 1) % playerKeys.Count;
				int nextPlayerKey = playerKeys[nextPlayerIndex];
				IPlayer nextPlayer = players[nextPlayerKey];

				// Set the current player to the next player
				currentPlayer = nextPlayer;

				Console.WriteLine($"{currentPlayer.GetName()} skips their turn.");
			}
		}

		public string ShowTurnStatus()
		{
			string currentPlayerName = currentPlayer.GetName();
			string rackLetters = string.Join(", ", playerRacks[currentPlayer].Letters);

			string turnStatus = $"Current Player: {currentPlayerName}\n";
			turnStatus += $"Rack Letters: {rackLetters}\n";

			return turnStatus;
		}

		//public string ShowLeaderBoard()
		//{
			// var sortedPlayers = players.Values.OrderByDescending(player => scoreCounter.GetPlayerScore(player)).ToList();

			// string leaderBoard = "Leaderboard:\n";
			
			// for (int i = 0; i < sortedPlayers.Count; i++)
			// {
			// 	IPlayer player = sortedPlayers[i];
			// 	int score = scoreCounter.GetPlayerScore(player);
				
			// 	leaderBoard += $"{i + 1}. {player.GetName()} - Score: {score}\n";
			// }

			// return leaderBoard;
		//}

		public string ShowBoard()
		{
			StringBuilder boardDisplay = new StringBuilder();

			for (int y = 0; y < _boardSize; y++)
			{
				for (int x = 0; x < _boardSize; x++)
				{
					string letter = _boardLetters[x, y];
					boardDisplay.Append($"| {letter} ");
				}
				boardDisplay.AppendLine("|");
				boardDisplay.AppendLine(new string('-', _boardSize * 5 + 1)); // Horizontal separator
			}

			return boardDisplay.ToString();
		}

		public string ShowBag()
		{
			return $"Remaining letters in the bag: {bag.DrawLetters(100)}"; // Replace '100' with the appropriate number of letters to display
		}

		 public IPlayer CheckWinner()
		{
			IPlayer winner = null;
			int highestScore = 0;

			foreach (var player in players.Values)
			{
				int playerScore = scoreCounter.CalculateScore(player);

				if (playerScore > highestScore)
				{
					highestScore = playerScore;
					winner = player;
				}
			}

			return winner;
		}

		private int CalculatePlayerScore(IPlayer player)
		{
			// Implement your logic to calculate the score for the given player
			int score = 0;

			// Retrieve the letters placed by the player and calculate their score
			Rack rack = playerRacks[player];
			foreach (var position in playerSetLetter.Keys)
			{
				string letter = playerSetLetter[position];
				// Calculate the score for the letter based on the board position and any multipliers
				// Add the score to the player's total score
				// score += ...

				// You can use methods from your ScoreCounter class to calculate the letter score
			}

			return score;
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
			return null; // Return null if the letter is not found on the board
		}
	}
}