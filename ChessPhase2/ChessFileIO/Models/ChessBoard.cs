using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessFileIO.Models
{
    class ChessBoard
    {
        const int BOARD_SIZE = 8;
        char[,] board = new char[8, 8];

        public void InitBoard()
        {
            board = new char[BOARD_SIZE, BOARD_SIZE];
            for (int j = 0; j < board.GetLength(0); j++)
            {
                for (int k = 0; k < board.GetLength(0); k++)
                {
                    board[j, k] = '-';
                }
            }
        }

        public void PrintBoard()
        {
            //Console.WriteLine("--------------------------");
            Console.WriteLine("  A B C D E F G H");

            for (int j = board.GetLength(0) - 1; j >= 0; --j)
            {
                Console.Write(j + 1);
                for (int k = 0; k < board.GetLength(0); ++k)
                {
                    Console.Write(" " + board[j, k]);
                }
                //Console.WriteLine("|");
                //Console.WriteLine("--------------------------");
                Console.WriteLine();
            }
        }

        public void SetBoardForGame()
        {
            board[0, 7] = 'r';
            board[7, 7] = 'R';
            board[0, 6] = 'n';
            board[7, 6] = 'N';
            board[0, 5] = 'b';
            board[7, 5] = 'B';
            board[0, 4] = 'k';
            board[7, 4] = 'K';
            board[0, 3] = 'q';
            board[7, 3] = 'Q';
            board[0, 2] = 'b';
            board[7, 2] = 'B';
            board[0, 1] = 'n';
            board[7, 1] = 'N';
            board[0, 0] = 'r';
            board[7, 0] = 'R';

            for (int j = 0; j < board.GetLength(0); ++j)
            {
                board[1, j] = 'p';
                board[6, j] = 'P';
            }
        }

        public void PlacePiece(ChessCoordinate coord, char piece)
        {
            int FileIndex = coord.FileAsInt(); // file is a char, must be converted to an int coordinate
            int RankIndex = coord.Rank - 1;     // must subtract one to convert to array index
            board[RankIndex, FileIndex] = piece; // rank goes first in array indexing (row, col)/(rank, file)
        }

        public void MovePiece(ChessCoordinate start, ChessCoordinate end)
        {
            int startFile = start.FileAsInt();
            int startRank = start.Rank - 1;

            char piece = board[startRank, startFile];
            board[startRank, startFile] = '-';
            PlacePiece(end, piece);
        }
    }
}
