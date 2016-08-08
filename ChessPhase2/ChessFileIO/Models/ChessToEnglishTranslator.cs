using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ChessFileIO.Models
{
    class ChessToEnglishTranslator
    {

        

        /// <summary>
        /// translates a move(c1 d2) to english, piece on c1 moved to d2
        /// // 4 groups, [0] = all, [1] = start rank-file, [2] = end rank-file, [3] = optional capture
        /// </summary>
        /// <param name="match"></param>
        /// <returns></returns>
        public string MoveToEnglish(Match match)
        {
            string start = match.Groups[1].Value;
            string end = match.Groups[2].Value;
            string moveInEnglish = "Piece on " + start + " moved to " + end;
            if (match.Groups[3].Value.Equals("*"))
            {
                moveInEnglish = moveInEnglish + " and captures piece at " + end;
            }
            return moveInEnglish;
        }

        /// <summary>
        /// translates castle move(e1 g1 h1 f1), king on e1 moves to g1, castling with rook on h1, which moves to f1
        /// 5 groups, [0] = all, [1] = king start r-f, [2] = king end r-f, [3] = rook start r-f, [4] = rook end r-f
        /// </summary>
        /// <param name="match"></param>
        /// <returns></returns>
        public string CastleToEnglish(Match match)
        {
            string kingColor = "";
            string kingStart = match.Groups[1].Value;
            string kingEnd = match.Groups[2].Value;
            string rookStart = match.Groups[3].Value;
            string rookEnd = match.Groups[4].Value;
            string side = "";
            if (kingStart.Contains("1"))
            {
                kingColor = "Light";
                if (rookStart.Contains("h"))
                {
                    side = "Kingside";
                }
                else
                {
                    side = "Queenside";
                }
            }
            else if (kingStart.Contains("8"))
            {
                kingColor = "Dark";
                if (rookStart.Contains("h"))
                {
                    side = "Queenside";
                }
                else
                {
                    side = "Kingside";
                }
            }
            else
            {
                side = "invalid castle";
            }
            string moveInEnglish = kingColor + " King on " + kingStart + " performs a " + side + " castle with rook on " + rookStart
            + ". King ends on " + kingEnd + " and rook ends on " + rookEnd;

            return moveInEnglish;
        }

        public string CharToPiece(string pieceChar)
        {
            string piece = pieceChar;

            if (piece.Equals("K"))
            {
                piece = "King";
            }
            else if (piece.Equals("Q"))
            {
                piece = "Queen";
            }
            else if (piece.Equals("B"))
            {
                piece = "Bishop";
            }
            else if (piece.Equals("N"))
            {
                piece = "Knight";
            }
            else if (piece.Equals("R"))
            {
                piece = "Rook";
            }
            else if (piece.Equals("P"))
            {
                piece = "Pawn";
            }
            else
            {
                Console.WriteLine("Invalid piece type");
            }
            return piece;
        }
    }
}
