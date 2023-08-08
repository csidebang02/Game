using System;
using System.Collections.Generic;

namespace ScrabbleGame
{
	public class GameRunner
	{
		private Dictionary<int, IPlayer> players;
		private Dictionary<IPlayer, Rack> playerRacks;
		private IBoard board;
		private IPlayer currentPlayer;
		private Dictionary<Position, char> playerSetLetter;
		private Bag bag;
		private ScoreCounter scoreCounter;
		private Dictionary dictionary;
		private Rack rack;

		public GameRunner(int boardSize, string initialLetters)
		{
			players = new Dictionary<int, IPlayer>();
			playerRacks = new Dictionary<IPlayer, Rack>();
			board = new Board(boardSize);
			currentPlayer = null;
			playerSetLetter = new Dictionary<Position, char>();
			bag = new Bag(initialLetters);
			scoreCounter = new ScoreCounter();
			dictionary = new Dictionary();
		}
		
		public char GetBoardLetter(int x, int y)
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
			playerRacks.Add(player, new Rack());
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

		public bool SetWord(int x, int y, char letter)
		{
			//tambah validasi terkait 
				//cek posisi yg diinput itu valid atau ngga? dicek dengan cara bandingkan dengan size
			int boardSize = board.GetBoardSize();
			if (x < 0 || x >= boardSize || y < 0 || y >= boardSize);
			{
				return false;
			}
				//cek sudah ada huruf di situ atau belum? kalau sudah ada, return false
			if (!board.IsPositionEmpty(x, y))
			{
				return false;
			}
				//cek playerRacks, dia punya huruf yg diinput ga? kalo ga, return false
			Rack rack = playerRacks[currentPlayer];
			if (!rack.ContainsLetter(letter))
			{
				return false;
			}
				//kalo melewati itu semua, maka simpan ke dalam Board -> BoardLetters
			board.PlaceLetterAtPosition(x, y, letter);
			playerSetLetter[new Position(x, y)] = letter;
			rack.RemoveLetter(letter);
				//kalo melewati itu semua, maka simpan ke dalam Board -> BoardLetters, lalu return true
			// Implement word placement logic here
			return true;
		}

		public void SubmitTurn()
		{
			// mengganti currentplayer dari player lain di dalam list Players
			// ambil currentPlayer
			// ambil currentplayer tsb di dalam list Players dengan menggunakan player id
			// set currentPlayer menjadi player selanjutnya (ambil dari list Players dengan menggunakan player id dari currentPlayer sebelumnya, lalu + 1)
			// kalau player id + 1 dari currentPlayer tidak ada, maka kembali lagi set player id 1 sebagai currentPlayer
			// tidak perlu return apa-apa, karena void 
			// Implement submitting turn logic here
		}

		public void SkipTurn()
		{
			// Implement skipping a turn
		}

		public string ShowTurnStatus()
		{
			// Implement showing turn status logic here
			return string.Empty;
		}

		public string ShowLeaderBoard()
		{
			// Implement showing leaderboard logic here
			return string.Empty;
		}

		public string ShowBoard()
		{
			// Implement showing board logic here
			return string.Empty;
		}

		public string ShowBag()
		{
			// Implement showing bag logic here
			return string.Empty;
		}

		public IPlayer CheckWinner()
		{
			// Implement checking winner logic here
			return null;
		}
		public Position GetLetterPosition(char letter)
		{
			return board.GetLetterPosition(letter);
		}

	}
}