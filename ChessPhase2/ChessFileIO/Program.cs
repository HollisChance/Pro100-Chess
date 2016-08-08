using ChessFileIO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessFileIO
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Run(args);
        }

        public static void Run(string[] args)
        {
            ChessIO IO = new ChessIO();
            ChessBoard board = new ChessBoard();
            ChessCoordinate cc = new ChessCoordinate { File = 'a', Rank = 4 };
            ChessCoordinate end = new ChessCoordinate { File = 'd', Rank = 2 };
            board.InitBoard();
            //board.SetBoardForGame();
            board.PrintBoard();

            IO.ReadChessFile(args[0], board);
            //IO.ReadInput("a2 d4", board);
            
            //board.PlacePiece(cc, 'n');
            //board.PrintBoard();
            //Console.WriteLine();
            //Console.WriteLine();
            //board.MovePiece(cc, end);
            //IO.ReadInput("f1 d1");
            //IO.ReadInput("f1 d1");
            //IO.ReadInput("f1 d1*");
            //IO.ReadInput("Kdb4");
            //IO.ReadInput("e1 g1 h1 f1");
        }
    }
}
