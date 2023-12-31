﻿using System;

namespace ScrabbleGame
{
	class Program
	{
		// Fungsi untuk menggambar papan permainan
		static void DrawBoard(GameRunner game)
		{
			int boardSize = game.GetBoardSize();
			Console.WriteLine("+-----------------------------------------------------------+");
			for (int y = 0; y < boardSize; y++)
			{
				for (int x = 0; x < boardSize; x++)
				{
					string letter = game.GetBoardLetter(x, y) ?? " "; // Handle null letters
					Console.Write($"| {letter} ");
				}
				Console.WriteLine("|");
				Console.WriteLine("+-----------------------------------------------------------+");
			}
			Console.WriteLine("");
		}

		static void Main(string[] args)
		{
			Console.WriteLine("Welcome to Scrabble Game!\n");

			// Inisialisasi permainan dengan papan berukuran 15x15 dan huruf awal
			GameRunner game = new GameRunner(15, "ABCDEFGHIJKLMNOPQRSTUVWXYZ");

			// Tambahkan pemain ke permainan
			IPlayer player1 = new Player(1, "Player 1");
			IPlayer player2 = new Player(2, "Player 2");

			game.AddPlayer(player1);
			game.AddPlayer(player2);
			
			game.AddRack(player1);
			game.AddRack(player2);

			Console.WriteLine("Let's start the game!");

			while (!game.IsGameEnd())
			{
				IPlayer currentPlayer = game.GetCurrentPlayer();
				Console.WriteLine($"Current Player: {currentPlayer.GetName()}");
				DrawBoard(game); // Menampilkan papan permainan
				
				Console.Write("Enter the coordinates (x y) where you want to place the letter: ");
				int x = int.Parse(Console.ReadLine());
				int y = int.Parse(Console.ReadLine());

				Console.Write("Enter the letter you want to place: ");
				string letter = Console.ReadLine();

				if (game.SetWord(x, y, letter))
				{
					Console.WriteLine("Word placed successfully!\n");
					//To do Tambah logic hapus kata dari rack
				}
				else
				{
					Console.WriteLine("Invalid placement. Try again.\n");
				}
				
				Console.WriteLine("Choose an action:");
				Console.WriteLine("1. Continue placing letters");
				Console.WriteLine("2. Complate turn");
				
				int choice = int .Parse(Console.ReadLine());
				if (choice == 1)
				{
					continue; // to do implement logic
				}
				else if (choice == 2)
				{
					Console.WriteLine("Melakukan pengecekan kata");
					if (game.CheckWord())
					{
						Console.WriteLine("Kata Benar");
					}
					else 
					{
						Console.WriteLine("Kata Salah");
					} 
					continue;
				}

				
			}

			Console.WriteLine("Game Over!");
			IPlayer winner = game.CheckWinner();
			if (winner != null)
			{
				Console.WriteLine($"Congratulations! {winner.GetName()} wins!");
			}
			else
			{
				Console.WriteLine("It's a tie!");
			}
		}
	}
}