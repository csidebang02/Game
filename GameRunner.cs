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
			// Implement word placement logic here
			return true;
		}

		public void SubmitTurn()
		{
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