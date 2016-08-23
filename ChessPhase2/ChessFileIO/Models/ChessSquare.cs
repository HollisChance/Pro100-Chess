using ChessFileIO.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessFileIO.Models
{
    public class ChessSquare
    {
        public Piece Piece { get; set; }
        public ChessCoordinate Position { get; set; }

        /// <summary>
        /// gets a single character representation of the piece(if there is one) contained in the chess square
        /// </summary>
        /// <returns>a string that represents the value containted int the chessSquare</returns>
        public string getPieceChar()
        {
            string pieceVal = "-";

            if (Piece == null)
            {

            }
            else
            {
                Type pieceType = typeof(Piece);

            if (pieceType.Equals(typeof(King)))
                {
                    pieceVal = "k";
                }
                else if (pieceType.Equals(typeof(Queen)))
                {
                    pieceVal = "q";
                }
                else if (pieceType.Equals(typeof(Rook)))
                {
                    pieceVal = "r";
                }
                else if (pieceType.Equals(typeof(Bishop)))
                {
                    pieceVal = "b";
                }
                else if (pieceType.Equals(typeof(Knight)))
                {
                    pieceVal = "n";
                }
                else if (pieceType.Equals(typeof(Pawn)))
                {
                    pieceVal = "p";
                }
                if (Piece.Color == Color.Dark && !pieceVal.Equals("-")) // converts to upper case if the piece is Dark
                {
                    pieceVal = pieceVal.ToUpper();
                }
            }
            
            return pieceVal;
        }
    }
}
