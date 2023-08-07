using System;

namespace ScrabbleGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Scrabble Game!\n");

            // Inisialisasi permainan dengan papan berukuran 15x15
            GameRunner game = new GameRunner(15);

            // Tambahkan pemain ke permainan
            IPlayer player1 = new Player(1, "Player 1");
            IPlayer player2 = new Player(2, "Player 2");

            game.AddPlayer(player1);
            game.AddPlayer(player2);

            while (!game.IsGameEnd())
            {
                IPlayer currentPlayer = game.GetCurrentPlayer();
                Console.WriteLine($"Current Player: {currentPlayer.GetName()}");
                Console.WriteLine(game.ShowBoard());

                Console.Write("Enter the coordinates (x y) where you want to place the letter: ");
				string[] coordinates = Console.ReadLine().Split(' ');
				if (coordinates.Length != 2 || !int.TryParse(coordinates[0], out int x) || !int.TryParse(coordinates[1], out int y))
				{
					Console.WriteLine("Invalid input. Please enter valid coordinates.");
					continue; // Continue to the next iteration of the loop
				}

				Console.Write("Enter the letter you want to place: ");
				char letter = char.ToUpper(Console.ReadLine()[0]);

				if (game.SetWord(x, y, letter))
				{
					Console.WriteLine("Word placed successfully!\n");
				}
				else
				{
					Console.WriteLine("Invalid placement. Try again.\n");
				}


                game.SubmitTurn();
            }

            Console.WriteLine("Game Over!");
        }
    }
}
