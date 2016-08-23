using ChessFileIO.Enums;
using ChessFileIO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessFileIO.Controllers
{
    public class PieceController
    {

        public void PlacePiece(ChessBoard chessBoard, ChessCoordinate coord, Piece piece)
        {
            int FileIndex = coord.FileAsInt();              // file is a char, must be converted to an int coordinate
            int RankIndex = coord.Rank - 1;                 // must subtract one to convert to array index
            chessBoard.Board[RankIndex, FileIndex].Piece = piece; // rank goes first in array indexing (row, col)/(rank, file)
            piece.Position = coord;
        }

        public void MovePiece(ChessBoard chessBoard, Game game, ChessCoordinate start, ChessCoordinate end)
        {
            Console.WriteLine(start.ToString());
            int startFile = start.FileAsInt();  // start file (a-h) converts to an integer that corresponds to the array index
            int startRank = start.Rank - 1;     // the rank is (1-8), subtracts 1 because array indexes of the board are 0-7
            int endFile = end.FileAsInt();
            int endRank = end.Rank - 1;
            
            Piece piece = chessBoard.Board[startRank, startFile].Piece;     // gets the piece from the start coordinate
            Console.WriteLine("it is " + game.PlayerTurn + "'s turn");

            // makes sure the start is a piece and that the end is null
            if (!(piece == null) && chessBoard.Board[endRank, endFile].Piece == null)                 // validates that the "piece" isn't an empty space
            {
                if (piece.Color == game.PlayerTurn)
                {
                    if (piece.IsValidMove(end, chessBoard))
                    {
                        PlacePiece(chessBoard, end, piece);  // places the piece on the new position
                        chessBoard.Board[startRank, startFile].Piece = null;   // removes the piece from the original position
                        game.SwitchTurn();
                    }
                    else
                    {
                        Console.WriteLine("Invalid Piece Movement");
                    }
                }
                else
                {
                    Console.WriteLine("can't move a piece that isn't yours, it is " + game.PlayerTurn + "'s turn");
                }
            }
            else
            {
                Console.WriteLine("Can't move a piece that isn't yours. it is " + game.PlayerTurn + "'s turn");
            }
        }

        public void PlacePiecesInStartPosition(ChessBoard chessBoard)
        {
            List<Piece> pieces = new List<Piece>();

            pieces.Add(new Rook() { Color = Color.Light, Position = new ChessCoordinate('a', 1) } );
            pieces.Add(new Rook() { Color = Color.Dark, Position = new ChessCoordinate('a', 8) } );
            pieces.Add(new Knight() { Color = Color.Light, Position = new ChessCoordinate('b', 1) });
            pieces.Add(new Knight() { Color = Color.Dark, Position = new ChessCoordinate('b', 8) });
            pieces.Add(new Bishop() { Color = Color.Light, Position = new ChessCoordinate('c', 1) });
            pieces.Add(new Bishop() { Color = Color.Dark, Position = new ChessCoordinate('c', 8) });
            pieces.Add(new Queen() { Color = Color.Light, Position = new ChessCoordinate('d', 1) });
            pieces.Add(new Queen() { Color = Color.Dark, Position = new ChessCoordinate('d', 8) });
            pieces.Add(new King() { Color = Color.Light, Position = new ChessCoordinate('e', 1) });
            pieces.Add(new King() { Color = Color.Dark, Position = new ChessCoordinate('e', 8) });
            pieces.Add(new Bishop() { Color = Color.Light, Position = new ChessCoordinate('f', 1) });
            pieces.Add(new Bishop() { Color = Color.Dark, Position = new ChessCoordinate('f', 8) });
            pieces.Add(new Knight() { Color = Color.Light, Position = new ChessCoordinate('g', 1) });
            pieces.Add(new Knight() { Color = Color.Dark, Position = new ChessCoordinate('g', 8) });
            pieces.Add(new Rook() { Color = Color.Light, Position = new ChessCoordinate('h', 1) });
            pieces.Add(new Rook() { Color = Color.Dark, Position = new ChessCoordinate('h', 8) });

            //for (int j = 0; j < chessBoard.Board.GetLength(0); ++j)
            //{
            //    pieces.Add(new Pawn() { Color = Color.Light, Position = new ChessCoordinate((char)(j + 65), 2) });
            //    pieces.Add(new Pawn() { Color = Color.Dark, Position = new ChessCoordinate((char)(j + 65), 7) });
            //}

            foreach (Piece piece in pieces)
            {
                chessBoard.Board[piece.Position.Rank - 1, piece.Position.FileAsInt()].Piece = piece;
            }


        }
    }
}
