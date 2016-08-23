using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessFileIO.Models
{
    class Rook : Piece
    {
        public override bool IsValidMove(ChessCoordinate destination, ChessBoard board)
        {
            bool isValid = false;
            if ((destination.Rank > 8 || destination.Rank < 1) && destination.FileAsInt() > 7 || destination.FileAsInt() < 0) // move this to chesscoordinate
            {
                // destination is out of bounds
                Console.WriteLine("dest out of bounds"); 
                return false; // not bad
            }
            if (destination.Rank == Position.Rank && destination.File == Position.File)
            {
                // can't move to the space it is already occupying
                Console.WriteLine("already in this space");
                return false;
            }
            else if (Position.Rank - destination.Rank == 0) // check for left/right move
            {
                for (int j = Position.FileAsInt(); j < destination.FileAsInt(); ++j)
                {
                    Piece piece = board.Board[Position.Rank - 1, j].Piece;
                    if (piece == null)
                    {
                        isValid = true;
                    }
                    else
                    {
                        isValid = false;
                        break; // this is the right thing to do
                    }
                }
                for (int j = Position.FileAsInt(); j > destination.FileAsInt(); --j)
                {
                    Piece piece = board.Board[Position.Rank - 1, j].Piece;
                    if (piece == null)
                    {
                        isValid = true;
                    }
                    else
                    {
                        isValid = false;
                        break;
                    }
                }
            }

            else if (Position.FileAsInt() - destination.FileAsInt() == 0) // vertical checks
            {
                for (int j = destination.Rank - 1; j < Position.Rank - 1; ++j)
                {
                    Piece piece = board.Board[j, Position.FileAsInt()].Piece;
                    if (piece == null)
                    {
                        isValid = true;
                    }
                    else
                    {
                        isValid = false;
                        break;
                    }
                }
                //bool doDownLoop = true;
                for (int j = destination.Rank - 1; j > Position.Rank - 1; --j)
                {
                    Piece piece = board.Board[j, Position.FileAsInt()].Piece;
                    if (piece == null)
                    {
                        isValid = true;
                    }
                    else
                    {
                        isValid = false;
                        break;
                    }
                }
            }
            if (!isValid)
            {
                Console.WriteLine("Invalid Rook Move");
            }

            return isValid; // change this!!!!!!!!!!!!
        }

        public override string ToString()
        {
            string piece = "r";
            if (this.Color == Enums.Color.Dark)
            {
                piece = piece.ToUpper();
            }
            return piece;
        }
    }
}
