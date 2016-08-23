using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessFileIO.Models
{
    class Knight : Piece
    {

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
            else if (Math.Abs(Position.Rank - destination.Rank) == 2)
            {
                if (Math.Abs(Position.FileAsInt() - destination.FileAsInt()) == 1)
                {
                    isValid = true;
                }
            }
            else if (Math.Abs(Position.Rank - destination.Rank) == 1)
            {
                if (Math.Abs(Position.FileAsInt() - destination.FileAsInt()) == 2)
                {
                    isValid = true;
                }
            }
            if (!isValid)
            {
                Console.WriteLine("invalid Knight move");
            }
            
            return isValid; // change this!!!!!!!!!!!!
        }

        public override string ToString()
        {
            string piece = "n";
            if (this.Color == Enums.Color.Dark)
            {
                piece = piece.ToUpper();
            }
            return piece;
        }
    }
}
