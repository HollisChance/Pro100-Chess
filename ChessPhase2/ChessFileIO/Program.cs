using ChessFileIO.Controllers;
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
            PieceController controller = new PieceController();
            Game game = new Game();

            board.InitBoard(); // puts a default '-' in every space for display purposes
            controller.PlacePiecesInStartPosition(board);
            board.PrintBoard();
            IO.ReadChessFile(args[0], controller, board, game);
            
        }
    }
}
