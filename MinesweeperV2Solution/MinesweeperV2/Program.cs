using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper
{
    class Program
    {
        static void Main(string[] args)
        {
            bool gameOver = false;
            int choice = 0;
            int x = 0;
            int y = 0;

            int tilesInLine = 0;
            int bombs = -1;
            GameBoard board;

            while (tilesInLine < 1)
            {
                Console.Write("Enter the number of tiles in line/column: ");
                tilesInLine = int.Parse(Console.ReadLine());
            }

            while (bombs < 0)
            {
                Console.Write("Enter the number of bombs: ");
                bombs = int.Parse(Console.ReadLine());
            }

            board = new GameBoard(tilesInLine, bombs);

            while (!gameOver)
            {
                Console.WriteLine(board.ToString());

                //User chooses what to do
                do
                {
                    Menu(board.GetFlags());
                    choice = int.Parse(Console.ReadLine());
                }while (choice > 3 || choice < 1);

                //Get x and y from user
                do
                {
                    Console.Write("Enter row (1-" + tilesInLine + "): ");
                    x = int.Parse(Console.ReadLine()) - 1;
                } while (x < 0 || x >= tilesInLine);
                do
                {
                    Console.Write("Enter column (1-" + tilesInLine + "): ");
                    y = int.Parse(Console.ReadLine()) - 1;
                } while (y < 0 || y >= tilesInLine);

                Console.WriteLine(); //go down one line

                switch (choice)
                {
                    case 1:
                        gameOver = Guess(board, x, y);
                        break;
                    case 2:
                        //If tile is not shown nor flagged
                        if (!(board.GetTiles()[y, x].IsShown() || board.GetTiles()[y, x].IsFlagged()))
                        {
                            board.GetTiles()[y, x].SetIsFlagged(true);
                            board.SetFlags(board.GetFlags() + 1);
                        }
                        
                        break;
                    case 3:
                        //If tile is flagged but not shown
                        if (board.GetTiles()[y, x].IsShown() || board.GetTiles()[y, x].IsFlagged())
                        {
                            board.GetTiles()[y, x].SetIsFlagged(false);
                            board.SetFlags(board.GetFlags() - 1);
                        }
                            
                        break;
                    default:
                        Console.WriteLine("\"To be or not to be, that is the question\" - Obama while creating Disney");
                        break;
                }

                //If you lost
                if (gameOver)
                {
                    board.ShowBombs();
                    Console.WriteLine(board.ToString());
                    Console.WriteLine("bad lol\n");
                }
                else
                {
                    gameOver = HasWonGame(board); //Check if you won
                    if (gameOver) //If you won
                    {
                        Console.WriteLine(board.ToString());

                        //Become a millioner with one simple trick! Bankers hate this!
                        Console.WriteLine("Congratulations! You won and it was so impressive that a Nigerian prince decided to give you his inheritence of 170M dollars!");
                        Console.WriteLine("Just pay a simple fee of 2300 dollars to claim the money! 100% legit!\n");
                    }
                }
            }
        }

        /*
        Prints the menu
        Input: flags
        Output: none
        */
        static void Menu(int flags)
        {
            Console.WriteLine("Amount of flags: " + flags);
            Console.WriteLine("1 - Guess \n2 - Sign tile \n3 - Unsign tile");
        }

        /*
        Function reveals a tile if it isn't flagged
        Input: board, x, y
        Output: gameOver
        */
        static bool Guess(GameBoard board, int x, int y)
        {
            bool gameOver = false;

            if (board.GetTiles()[y, x].IsFlagged()) //If tile is flagged
            {
                Console.WriteLine("You have a flag there!");
                return gameOver;
            }
            if (board.GetTiles()[y, x].IsBomb()) //If tile is bomb
            {
                gameOver = true;
                board.GetTiles()[y, x].SetIsShown(true); 
            }
            else if (!board.GetTiles()[y, x].IsZero()) //If tile is not 0
            {
                board.GetTiles()[y, x].SetIsShown(true);
            }
            else //If tile is 0
            {
                board.ShowZero(y, x); //make recursion function 
            }

            return gameOver;
        }

        /*
        Function checks if we won the game
        Input: board
        Output: gameOver
        */
        static bool HasWonGame(GameBoard board)
        {
            bool gameOver = true;
            int len = board.GetTiles().GetLength(0);

            //Go though all the tiles
            for (int i = 0; i < len; i++)
            {
                for (int j = 0; j < len; j++)
                {
                    //If at least 1 tile that isn't a bomb is hidden, then you didn't win
                    if (!board.GetTiles()[i, j].IsBomb() && !board.GetTiles()[i, j].IsShown())
                    {
                        gameOver = false;
                        goto break_nested_loop;
                    }
                }
            }
        break_nested_loop:

            return gameOver;
        }
    }
}
