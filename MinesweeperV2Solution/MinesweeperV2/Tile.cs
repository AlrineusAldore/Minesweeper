using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper
{
    class Tile
    {
        private char symbol;
        private int num;
        private bool isShown;
        private bool isFlagged;

        public Tile(char symbol)
        {
            this.symbol = symbol;
            this.num = 0;
            this.isShown = false;
            this.isFlagged = false;
        }

        public char GetSymbol()
        {
            return this.symbol;
        }
        public int GetNum()
        {
            return this.num;
        }
        public bool IsShown()
        {
            return this.isShown;
        }
        public bool IsFlagged()
        {
            return this.isFlagged;
        }

        public void SetSymbol(char symbol)
        {
            this.symbol = symbol;
        }
        public void SetNum(int num)
        {
            this.num = num;
        }
        public void SetIsShown(bool isShown)
        {
            this.isShown = isShown;
        }

        public override string ToString()
        {
            return Char.ToString(this.symbol);
        }

        /*
        Sets the tile to a flag
        Input: none
        Output: none
        */
        public void SetIsFlagged(bool isFlagged)
        {
            this.isFlagged = isFlagged;   
        }

        /*
        Returns true if the tile is a bomb
        Input: none
        Output: isbomb
        */
        public bool IsBomb()
        {
            return (this.symbol == '*');
        }

        /*
        Return true if the tile is equal 0
        Input: none
        Output: iszero
        */
        public bool IsZero()
        {
            return (this.symbol == '0');
        }


    }
}
