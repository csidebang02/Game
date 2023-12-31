using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScrabbleGame
{
	public class GameRunner
	{
		private Dictionary<int, int> players;
		private Dictionary<int, Rack> playerRacks;
		private IBoard board;
		private IPlayer currentPlayer;
		private Dictionary<Position, string> playerSetLetter;
		private Bag bag;
		private ScoreCounter scoreCounter;
		private Dictionary dictionary;
		private List<int> playerKeys;
		private int _boardSize;
		private string[,] _boardLetters;
		private List<Position> listPosition;

		public GameRunner(int boardSize, string initialLetters)
		{
			players = new Dictionary<int, int>();
			playerRacks = new Dictionary<int, Rack>();
			board = new Board(boardSize);
			currentPlayer = null;
			playerSetLetter = new Dictionary<Position, string>();
			bag = new Bag(initialLetters);
			scoreCounter = new ScoreCounter();
			dictionary = new Dictionary();
			playerKeys = new List<int>();
			_boardSize = boardSize;
			_boardLetters  = new string[boardSize, boardSize];
			listPosition = new List<Position>();
			
			for (int i = 1; i <= 2; i++)
			{
				playerKeys.Add(i);
			}
			dictionary.SetWord("word.txt");
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
			players.Add(player.GetId(), 0);
			if (currentPlayer == null)
			{
				currentPlayer = player;
			}
		}

		public void CompleteTurn()
		{
			IPlayer currentPlayer = GetCurrentPlayer();
			Rack rack = GetPlayerRack(currentPlayer);

			Console.WriteLine("Do you want to continue placing letters or complete your turn?");
			Console.WriteLine("1. Continue placing letters");
			Console.WriteLine("2. Complete turn and check for valid words");

			int choice = int.Parse(Console.ReadLine());

			if (choice == 1)
			{
				// Continue placing letters
				Console.Write("Enter the coordinates (x y) where you want to place the letter: ");
				int x = int.Parse(Console.ReadLine());
				int y = int.Parse(Console.ReadLine());

				Console.Write("Enter the letter you want to place: ");
				string letter = Console.ReadLine();

				ValidateTurn validator = new ValidateTurn(board, playerSetLetter, dictionary);

				if (!validator.CheckValidPlacement())
				{
					Console.WriteLine("Invalid placement. Letters must be placed adjacent to existing letters.");
					return;
				}

				if (!rack.ContainsLetter(letter))
				{
					Console.WriteLine("Invalid letter. You don't have this letter in your rack.");
					return;
				}

				// Place the letter on the board
				if (board.IsPositionEmpty(x, y))
				{
					board.PlaceLetterAtPosition(x, y, letter);
					playerSetLetter[new Position(x, y)] = letter;
					rack.RemoveLetter(letter);

					// Optionally, display the updated board
					Console.WriteLine(ShowBoard());
				}
				else
				{
					Console.WriteLine("Invalid placement. The position is already occupied.");
				}
			}
			else if (choice == 2)
			{
				// Complete the turn and check for valid words
				ValidateTurn validator = new ValidateTurn(board, playerSetLetter, dictionary);
				
				if (!validator.CheckConnectTiles())
				{
					Console.WriteLine("Invalid placement. Letters must be connected to existing tiles.");
					return;
				}

				if (!validator.CheckValidPlacement())
				{
					Console.WriteLine("Invalid placement. Letters must be placed adjacent to existing letters.");
					return;
				}

				bool isValidWord = validator.ValidateWord();
				if (isValidWord)
				{
					// Calculate the score for the player and update their score
					int score = CalculatePlayerScore(currentPlayer);
					players[currentPlayer.GetId()] = score;

					// Remove letters from the player's rack
					foreach (var position in playerSetLetter.Keys)
					{
						string letter = playerSetLetter[position];
						rack.RemoveLetter(letter);
					}

					// Refill the player's rack from the bag
					string discardedLetters = string.Join("", playerSetLetter.Values);
					bag.RefillLetters(discardedLetters);
					AddRack(currentPlayer);

					// Clear the placed letters from the board
					playerSetLetter.Clear();

					// Move to the next player's turn
					SubmitTurn();
				}
				else
				{
					Console.WriteLine("Invalid word. Please try again.");
					// Optionally, allow the player to modify their word and try again
					// ...
				}
			}
			else
			{
				Console.WriteLine("Invalid choice.");
			}
		}
		
		public string Brach()
		{
			int Length = 7;
			string randomString = GenerateRandomString(Length);
			return randomString;
		}
		
		static string GenerateRandomString(int length)
		{
			const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
			Random random = new Random();
			char[] stringChars = new char[length];

			for (int i = 0; i < length; i++)
			{
				stringChars[i] = chars[random.Next(length)];
			}

			return new string(stringChars);
		}
		
		public void AddRack(IPlayer player)
		{
			Rack isiRack = new Rack();
			string karakter = Brach();
			foreach (char huruf in karakter)
			{
				isiRack.AddLetter(huruf.ToString());
				Console.Write(huruf.ToString());
			}
				Console.WriteLine("|");
			playerRacks.Add(player.GetId(), isiRack);
		}
		
		public void DeleteWordFromRacks(IPlayer currentPlayer, string removedWord)
		{
			//Implementasi
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
			return playerRacks[player.GetId()];
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
			Rack rack = playerRacks[currentPlayer.GetId()];
			if (!rack.ContainsLetter(letter))
			{
				Console.WriteLine("Player tidak mempunya huruf tersebut");
				return false;
			}
				//kalo melewati itu semua, maka simpan ke dalam Board -> BoardLetters
			board.PlaceLetterAtPosition(x, y, letter);
			playerSetLetter[new Position(x, y)] = letter;
			Position posisi = new Position(x, y);
			listPosition.Add(posisi);
			
			// rack.RemoveLetter(letter);
				//kalo melewati itu semua, maka simpan ke dalam Board -> BoardLetters, lalu return true
			// Implement word placement logic here
			return true;
		}
		
		public bool CheckWord()
		{
			string word = board.GetWordFormPosition(listPosition);
			return dictionary.LookUp(word);
		}

		public void SubmitTurn()
		{
			// mengganti currentplayer dari player lain di dalam list Players
			// ambil currentPlayer
			// IPlayer currentPlayer = GetCurrentPlayer();
			// // ambil currentplayer tsb di dalam list Players dengan menggunakan player id
			// int currentPlayerIndex = playerKeys.IndexOf(currentPlayer.GetId());
			
			// if (currentPlayerIndex != -1)
			// {
			// 	// set currentPlayer menjadi player selanjutnya (ambil dari list Players dengan menggunakan player id dari currentPlayer sebelumnya, lalu + 1)
			// 	int nextPlayerIndex = (currentPlayerIndex + 1) % playerKeys.Count;
			// 	int nextPlayerKey = playerKeys[nextPlayerIndex];
			// 	IPlayer nextPlayer = players[nextPlayerKey];
				
			// 	// Mengubah currentPlayer menjadi nextPlayer
			// 	currentPlayer = nextPlayer;
				
			// 	// Memperbarui currentPlayer dalam objek GameRunner
			// 	this.currentPlayer = currentPlayer;
			// }
		}

		public void SkipTurn()
		{
			// int currentPlayerId = currentPlayer.GetId();
			// int currentPlayerIndex = playerKeys.IndexOf(currentPlayerId);

			// if (currentPlayerIndex != -1)
			// {
			// 	// Calculate the index of the next player to take a turn
			// 	int nextPlayerIndex = (currentPlayerIndex + 1) % playerKeys.Count;
			// 	int nextPlayerKey = playerKeys[nextPlayerIndex];
			// 	IPlayer nextPlayer = players[nextPlayerKey];

			// 	// Set the current player to the next player
			// 	currentPlayer = nextPlayer;

			// 	Console.WriteLine($"{currentPlayer.GetName()} skips their turn.");
			// }
		}

		public string ShowTurnStatus()
		{
			string currentPlayerName = currentPlayer.GetName();
			string rackLetters = string.Join(", ", playerRacks[currentPlayer.GetId()].Letters);

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

			// foreach (var player in players.Values)
			// {
			// 	int playerScore = scoreCounter.CalculateScore(player);

			// 	if (playerScore > highestScore)
			// 	{
			// 		highestScore = playerScore;
			// 		winner = player;
			// 	}
			// }

			return winner;
		}

		private int CalculatePlayerScore(IPlayer player)
		{
			// Implement your logic to calculate the score for the given player
			int score = 0;

			// Retrieve the letters placed by the player and calculate their score
			// Rack rack = playerRacks[player];
			// foreach (var position in playerSetLetter.Keys)
			// {
			// 	string letter = playerSetLetter[position];
			// 	// Calculate the score for the letter based on the board position and any multipliers
			// 	// Add the score to the player's total score
			// 	// score += ...

			// 	// You can use methods from your ScoreCounter class to calculate the letter score
			// }

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