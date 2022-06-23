using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper
{
    class GameBoard
    {
        private Tile[,] tiles;
        private int flags;
        /*  
          Chef Steward
              ___
              | |
             (^_^)
            \__|__/
               |
              / \
                  */
        public GameBoard(int tilesInLine, int bombs)
        {
            this.tiles = new Tile[tilesInLine, tilesInLine];
            this.flags = 0;

            //Initialize new tiles
            for (int i = 0; i < tilesInLine; i++)
            {
                for (int j = 0; j < tilesInLine; j++)
                {
                    this.tiles[i, j] = new Tile(' ');
                }
            }

            InitBombsAndNumbers(bombs, tilesInLine);

            PutNumInSymbolSoThatShrekCouldLiveHappilyEverAfterTheEnd(tilesInLine);
        }

        public Tile[,] GetTiles()
        {
            return this.tiles;
        }
        public int GetFlags()
        {
            return this.flags;
        }

        public void SetTiles(Tile[,] tiles)
        {
            this.tiles = tiles;
        }
        public void SetFlags(int flags)
        {
            this.flags = flags;
        }

        //Gabi's magic function revised for lengths that are larger than 10
        public override string ToString()
        {
            string str = "";
            int size = this.tiles.GetLength(0);
            int i = 0;

            for (i = 0; i < size; i++)
            {
                if (i < 10)
                {
                    str += "   " + (i + 1);
                }
                else
                {
                    str += "  " + (i + 1);
                }
            }

            str += "\n  ";

            for (i = 0; i < size; i++)
            {
                str += "----";
            }
            str += "-\n";

            for (i = 0; i < size; i++)
            {
                if(i < size - 1)
                {
                    if (i < 9)
                    {
                        str += " " + (i + 1) + "|";
                    }
                    else
                    {
                        str += "" + (i + 1) + "|";
                    }
                    
                }
                else if (size < 10)
                {
                    str += " " + (i + 1) + "|";
                }
                else
                {
                    str += "" + (i + 1) + "|";
                }
                
                for (int j = 0; j < size; j++)
                {
                    str += " ";

                    if (this.tiles[i, j].IsShown())
                    {
                        str += this.tiles[i, j].GetSymbol();
                    }
                    else if (this.tiles[i, j].IsFlagged())
                    {
                        str += "?";
                    }
                    else
                    {
                        str += " ";
                    }

                    str += " |";
                }
                str += "\n  ";
                
                for (int j = 0; j < size; j++)
                {
                    str += "----";
                }

                str += "-\n";
            }

            return str;
        }

        /*
        Function that initializes bombs in random tiles and calculates the numbers around them
        Input: bombs, tilesInLine
        Output: none
        */
        void InitBombsAndNumbers(int bombs, int tilesInLine)
        {
            Random rand = new Random();
            int enoughBombs = 0;
            int willBecomeBomb;

            //Repeat as long as there aren't enough bombs
            while (enoughBombs < bombs)
            {
                //Go through the whole board
                for (int i = 0; i < tilesInLine; i++)
                {
                    for (int j = 0; j < tilesInLine; j++)
                    {
                        //Randomizes bomb location
                        willBecomeBomb = rand.Next(1, tilesInLine * tilesInLine);
                        //If it's 1 then it'll be a bomb otherwise not
                        if (willBecomeBomb == 1 && this.tiles[i, j].GetSymbol() != '*')
                        {
                            this.tiles[i, j].SetSymbol('*');
                            enoughBombs++;
                        }
                        if (enoughBombs == bombs)
                        {
                            goto break_nested_loop;
                        }
                    }
                }
            }

            break_nested_loop:

            //Goes through all of the tiles
            for (int i = 0; i < tilesInLine; i++)
            {
                for (int j = 0; j < tilesInLine; j++)
                {
                    //If tile is bomb
                    if (this.tiles[i, j].GetSymbol() == '*')
                    {
                        AddNumToAroundBomb(i, j, tilesInLine);
                    }
                }

            }
        }


        /*
        Function goes through the 8 tiles around a bomb and adds them 1
        Input: i, j, tilesInLine
        Output: none
        */
        void AddNumToAroundBomb(int i, int j, int tilesInLine)
        {
            //Go through all the tiles around it
            for (int y = i - 1; y < i + 2; y++)
            {
                for (int x = j - 1; x < j + 2; x++)
                {
                    //Checks if the tile around it is in the board
                    if (!(y < 0 || y >= tilesInLine || x < 0 || x >= tilesInLine))
                    {
                        //If the tile around it isn't a bomb
                        if (!this.tiles[y, x].IsBomb())
                        {
                            this.tiles[y, x].SetNum(this.tiles[y, x].GetNum() + 1);
                        }
                    }
                }
            }
        }

        /*
        Function puts a tile's number in its symbol if it's not a bomb
        Input: tilesInLine
        Output: none
        */
        void PutNumInSymbolSoThatShrekCouldLiveHappilyEverAfterTheEnd(int tilesInLine)
        {
            for (int i = 0; i < tilesInLine; i++)
            {
                for (int j = 0; j < tilesInLine; j++)
                {
                    if (!(this.tiles[i, j].IsBomb()))
                    {
                        this.tiles[i, j].SetSymbol((char)(this.tiles[i, j].GetNum() + '0')); 
                    }    
                }
            }
        }

        /*
        Function shows bombs
        Input: none
        Output: none
        */
        public void ShowBombs()
        {
            int len = this.tiles.GetLength(0); 

            //Goes through the tiles
            for (int i = 0; i < len; i++)
            {
                for (int j = 0; j < len; j++)
                {
                    //If it's a bomb then show tile
                    if (this.tiles[i, j].IsBomb())
                    {
                        this.tiles[i, j].SetIsShown(true);
                    }
                }
            }
        }

        /*
        Function recursively shows all the connected zeros of a tile
        Input: i, j
        Output: none
        */
        public void ShowZero(int i, int j)
        {
            int len = this.tiles.GetLength(0);

            //Go through all the tiles around it
            for (int y = i - 1; y < i + 2; y++)
            {
                for (int x = j - 1; x < j + 2; x++)
                {
                    //Checks if the tile around it is in the board
                    if (!(y < 0 || y >= len || x < 0 || x >= len))
                    {
                        //skip shown tiles
                        if (!this.tiles[y, x].IsShown())
                        {
                            this.tiles[y, x].SetIsShown(true);

                            if (this.tiles[y, x].IsZero())
                            {
                                ShowZero(y, x);
                            }
                        }
                    }
                }
            }

        }
    }
}