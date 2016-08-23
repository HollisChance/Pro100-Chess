using ChessFileIO.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessFileIO.Models
{
    public abstract class Piece
    {

        public ChessCoordinate Position { get; set; }
        public Color Color { get; set; }
        public bool OnBoard { get; set; }

        public abstract bool IsValidMove(ChessCoordinate destination, ChessBoard board);
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
        //    else
        //    {
        //        isValid = true;
        //    }

        //    return isValid;
        //}

        public override string ToString()
        {
            string piece = "Piece";
            return piece;
        }
    }
}
