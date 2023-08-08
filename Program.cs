using System;

namespace ScrabbleGame
{
    class Program
    {
        // Fungsi untuk menggambar papan permainan ini perubahan 
        static void DrawBoard(GameRunner game)
        {
            int boardSize = game.GetBoardSize();
            Console.WriteLine("+----+----+----+----+----+----+----+----+----+");
            for (int y = 0; y < boardSize; y++)
            {
                for (int x = 0; x < boardSize; x++)
                {
                    char letter = game.GetBoardLetter(x, y);
                    Console.Write($"| {letter} ");
                }
                Console.WriteLine("|");
                Console.WriteLine("+----+----+----+----+----+----+----+----+----+");
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
