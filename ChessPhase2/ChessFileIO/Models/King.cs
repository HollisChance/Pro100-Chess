using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessFileIO.Models
{
    class King : Piece
    {
        //public override bool IsValidMove(ChessCoordinate destination)
        //{
        //    bool isValid = false;

        //    if ((destination.Rank > 8 || destination.Rank < 1) && destination.FileAsInt() > 7 || destination.FileAsInt() < 0)
        //    {
        //        // destination is out of bounds
        //        Console.WriteLine("dest out of bounds");
        //    }
        //    if (destination.Rank == Position.Rank && destination.File == Position.File)
        //    {
        //        // can't move to the space it is already occupying
        //        Console.WriteLine("already in this space");
        //    }
        //    else if (Math.Sqrt(Math.Pow(Math.Abs((Position.FileAsInt() - destination.FileAsInt())), 2)) + Math.Pow(Math.Abs((Position.Rank - destination.Rank)), 2) != Math.Sqrt(2))
        //    {
        //        //isValid = true;
        //    }
        //    else
        //    {
        //        //isValid = true;
        //    }
        //    return isValid;
        //}

        public override bool IsValidMove(ChessCoordinate destination, ChessBoard board)
        {
            bool isValid = false;

            if ((destination.Rank > 8 || destination.Rank < 1) && destination.FileAsInt() > 7 || destination.FileAsInt() < 0)
            {
                // destination is out of bounds
                Console.WriteLine("dest out of bounds");
            }
            else if (destination.Rank == Position.Rank && destination.File == Position.File)
            {
                // can't move to the space it is already occupying
                Console.WriteLine("already in this space");
            }
            else if ( (Math.Abs(destination.Rank - Position.Rank) <= 1) && (Math.Abs(destination.FileAsInt() - Position.FileAsInt()) <= 1) )
            {
                Console.WriteLine("valid move");
                isValid = true;
            }
            else
            {
                Console.WriteLine("invalid move for King at " + Position.ToString());
            }
            return isValid;
        }

        public override string ToString()
        {
            string piece = "k";
            if (this.Color == Enums.Color.Dark)
            {
                piece = piece.ToUpper();
            }
            return piece;
        }
    }
}
