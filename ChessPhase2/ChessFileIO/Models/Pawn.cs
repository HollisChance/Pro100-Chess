using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessFileIO.Models
{
    class Pawn : Piece
    {
        public bool IsFirst { get; private set; }

        public override bool IsValidMove(ChessCoordinate destination, ChessBoard board)
        {
            bool isValid = false;
            if ((destination.Rank > 8 || destination.Rank < 1) && destination.FileAsInt() > 7 || destination.FileAsInt() < 0)
            {
                // destination is out of bounds
                Console.WriteLine("dest out of bounds");
            }
            if (destination.Rank == Position.Rank && destination.File == Position.File)
            {
                // can't move to the space it is already occupying
                Console.WriteLine("already in this space");
            }
            if (Color == Enums.Color.Dark)
            {
                if (Math.Abs(Position.Rank - destination.Rank) == 2)
                {
                    if (IsFirst)
                    {
                        isValid = true;
                        IsFirst = false;
                    }
                }
                else if (Math.Abs(Position.Rank - destination.Rank) == 1)
                {
                    isValid = true;
                    IsFirst = false;
                }
            }
            else if (Color == Enums.Color.Light)
            {
                if (Math.Abs(Position.Rank - destination.Rank) == 2)
                {
                    if (IsFirst)
                    {
                        isValid = true;
                        IsFirst = false;
                    }
                }
                else if (Math.Abs(Position.Rank - destination.Rank) == 1)
                {
                    isValid = true;
                    IsFirst = false;
                }
            }
            return isValid; 
        }

        public override string ToString()
        {
            string piece = "p";
            if (this.Color == Enums.Color.Dark)
            {
                piece = piece.ToUpper();
            }
            return piece;
        }
    }
}
