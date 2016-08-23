using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessFileIO.Models
{
    public class ChessBoard
    {
        const int BOARD_SIZE = 8;
        private List<Piece> allPieces = new List<Piece>();

        public ChessSquare[,] Board { get; private set; }

        public void InitBoard()
        {
            Board = new ChessSquare[BOARD_SIZE, BOARD_SIZE];
            for (int j = 0; j < Board.GetLength(0); j++)
            {
                for (int k = 0; k < Board.GetLength(0); k++)
                {
                    Board[j, k] = new ChessSquare();
                    char c = (char)(j + 65);
                    Board[j, k].Position = new ChessCoordinate() { File = c, Rank = k };
                }
            }
        }

        public void PrintBoard()
        {
            //Console.WriteLine("--------------------------");
            Console.WriteLine("  A B C D E F G H");

            for (int j = Board.GetLength(0) - 1; j >= 0; --j)
            {
                Console.Write(j + 1);
                for (int k = 0; k < Board.GetLength(0); ++k)
                {
                    if (Board[j, k].Piece == null)
                    {
                        Console.Write(" -");
                    }
                    else
                    {
                        Console.Write(" " + Board[j, k].Piece.ToString());
                    }
                }
                //Console.WriteLine("|");
                //Console.WriteLine("--------------------------");
                Console.WriteLine();
            }
        }
      
    }
}
