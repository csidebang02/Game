using System;

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
				
				//Menampilkan papan permainan
				Console.WriteLine(game.ShowBoard());
				
				Console.WriteLine($"Current Player: {currentPlayer.GetName()}");
				Console.WriteLine($"Rack Letters: {string.Join(", ", game.GetPlayerRack(currentPlayer).Letters)}");
				Console.WriteLine("Do you want to continue placing letters or complete your turn?");
				Console.WriteLine("1. Continue placing letters");
				Console.WriteLine("2. Complete turn and check for valid words");

				int choice = int.Parse(Console.ReadLine());
				//DrawBoard(game); // Menampilkan papan permainan
				if (choice == 1)
				{
					Console.Write("Enter the coordinates (x y) where you want to place the letter: ");
					int x = int.Parse(Console.ReadLine());
					int y = int.Parse(Console.ReadLine());

					Console.Write("Enter the letter you want to place: ");
					string letter = Console.ReadLine();
					
					bool success = game.SetWord(x,y, letter);
					
					if(success)
					{
						Console.WriteLine("Word placed successfully!\n");
					}
					else
					{
						Console.WriteLine("Invalid placement. Try again.\n");
					}
				}
				else if (choice == 2)
				{
					// game.CompleteTurn();
					
					//Check if the word is valid and submit if it is
					Console.Write("Enter the word you formed: ");
					string formedWord = Console.ReadLine(). ToUpper();
					bool isValidWord = game.CheckWord(formedWord);
					
					if (isValidWord)
					{
						Console.WriteLine("Word is valid! Submitting the turn.");
						// game.CompleteTurn();
					}
					else
					{
						Console.WriteLine("Word is not valid. Continuing the turn.");
					}
				}
				else
				{
					Console.WriteLine("Invalid choice. Please Try again.");
				}

                // game.SubmitTurn();
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