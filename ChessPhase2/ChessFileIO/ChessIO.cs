using ChessFileIO.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ChessFileIO
{
    class ChessIO
    {
        // 4 groups, 1st = full statement, 2nd = piece, 3rd = color, 4th = placement file-rank
        const string PLACE_PIECE_PATTERN = @"([QKBNRP])([ld])([a-h][1-8])"; 
        // 4 groups, 1st = all, 2nd = start rank-file, 3rd = end rank-file, 4th = optional capture
        const string MOVE_PIECE_PATTERN = @"([a-h][1-8])\s+([a-h][1-8])(\*)?"; 
        // 5 groups, 1st = all, 2nd = piece 1 start r-f, 3rd = p1 end r-f, 4th = p2 start r-f, 5th = p2 end r-f
        const string CASTLE_MOVE_PATTERN = @"([a-h][1-8])\s([a-h][1-8])\s([a-h][1-8])\s([a-h][1-8])";
        private ChessToEnglishTranslator translator = new ChessToEnglishTranslator();

        public void ReadChessFile(string fileName, ChessBoard board)
        {
            try
            {
                using (StreamReader reader = new StreamReader(fileName))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (!(line.Equals(""))) // if line isn't empty
                        {
                            ReadInput(line, board);
                            board.PrintBoard();
                            Console.WriteLine();
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("file [" +fileName + "] could not be read");
                Console.WriteLine(e.Message);
            }
        }

        public void ReadInput(string input, ChessBoard board)
        {
            Match placePiece = RegexTest(input, PLACE_PIECE_PATTERN);
            Match movePiece = RegexTest(input, MOVE_PIECE_PATTERN);
            Match castleMove = RegexTest(input, CASTLE_MOVE_PATTERN);

            string move = "";

            if (castleMove.Success)                     // if the match for castle move was successful, then it translates the castle move to english and to the board
            {
                move = RegexToTranslation(castleMove, board);
            }
            else if (movePiece.Success)                 // if the movePiece match succeeded, then it is translated and the piece is moved
            {
                move = RegexToTranslation(movePiece, board);
            }
            else if (placePiece.Success)                // if the placePiece match succeeded it translates and places a piece
            {
                move = RegexToTranslation(placePiece, board);
            }
            else
            {
                move = "Invalid/Unrecognized Move";
            }
            Console.WriteLine(move);
        }

        public Match RegexTest(string line, string pattern)
        {
            Match match = Regex.Match(line, pattern);            
            return match;
        }

        public string RegexToTranslation(Match match, ChessBoard board)
        {
            string moveInEnglish = "";
            if (match.Success)
            {
                moveInEnglish = TranslateMove(match, board); // translates the line into english and a board move, if the line matched
            }
            return moveInEnglish;
        }

        public string TranslateMove(Match match, ChessBoard board)
        {
            string translation = "";
            int groupCount = match.Groups.Count;
            
            if (groupCount == 5) // means that the match is a castle move
            {
                translation = translator.CastleToEnglish(match);
                CastleToBoard(match, board);
            }
            else if (groupCount == 4)
            {
                if (match.Groups[1].Length == 1)                // if group[1] (which would be a K, Q, etc..) is 1 character long then it is a placement command
                {
                    PlacementToBoard(match, board);             // takes the match info and places piece on board
                    translation = PlacementToEnglish(match);    //takes the match info and converts to english
                }
                else                                            // group[1] isn't 1 in length, which means it is a movement command
                {
                    MoveToBoard(match, board);                  // takes the match info and moves a piece on the board
                    translation = translator.MoveToEnglish(match);         // translates to english
                }
            }
            else
            {
                translation = "ERROR: INVALID MOVE";
            }

            return translation;
        }

        /// <summary>
        /// translates a placement move (Kle1) into english(light King placed on e1)
        /// uses groups from matcher. [0] = full statement, [1] = piece, [2] = color, [3] = placement file-rank
        /// </summary>
        /// <param name="match"></param>
        /// <returns></returns>
        public string PlacementToEnglish(Match match)
        {
            string piece = match.Groups[1].Value;
            string color = match.Groups[2].Value;
            string position = match.Groups[3].Value;

            piece = translator.CharToPiece(piece);

            if (color.Equals("l"))
            {
                piece = "Light " + piece;
            } else if (color.Equals("d"))
            {
                piece = "Dark " + piece;
            }
            string moveInEnglish = piece + " placed on " + position;
            return moveInEnglish;
        }

        public void PlacementToBoard(Match match, ChessBoard board)
        {
            string piece = match.Groups[1].Value;
            string color = match.Groups[2].Value;
            string position = match.Groups[3].Value;
            ChessCoordinate placement = StringToChessCoordinate(position);

            if (color.Equals("l"))
            {
                piece = piece.ToLower(); // lowercase for light
            }
            char pieceChar;
            char.TryParse(piece, out pieceChar);

            board.PlacePiece(placement, pieceChar);
        }

        public void MoveToBoard(Match match, ChessBoard board)
        {
            string start = match.Groups[1].Value;
            string end = match.Groups[2].Value;

            ChessCoordinate startCoord = StringToChessCoordinate(start);
            ChessCoordinate endCoord = StringToChessCoordinate(end);

            board.MovePiece(startCoord, endCoord);
        }
        
        public void CastleToBoard(Match match, ChessBoard board)
        {
            string start1 = match.Groups[1].Value;
            string end1 = match.Groups[2].Value;
            string start2 = match.Groups[3].Value;
            string end2 = match.Groups[4].Value;

            ChessCoordinate startCoord1 = StringToChessCoordinate(start1);
            ChessCoordinate endCoord1 = StringToChessCoordinate(end1);

            ChessCoordinate startCoord2 = StringToChessCoordinate(start2);
            ChessCoordinate endCoord2 = StringToChessCoordinate(end2);

            board.MovePiece(startCoord2, endCoord2);

            board.MovePiece(startCoord1, endCoord1);
        }

        ///// <summary>
        ///// translates a move(c1 d2) to english, piece on c1 moved to d2
        ///// // 4 groups, [0] = all, [1] = start rank-file, [2] = end rank-file, [3] = optional capture
        ///// </summary>
        ///// <param name="match"></param>
        ///// <returns></returns>
        //public string MoveToEnglish(Match match)
        //{
        //    string start = match.Groups[1].Value;
        //    string end = match.Groups[2].Value;
        //    string moveInEnglish = "Piece on " + start + " moved to " + end;
        //    if (match.Groups[3].Value.Equals("*"))
        //    {
        //        moveInEnglish = moveInEnglish + " and captures piece at " + end;
        //    }
        //    return moveInEnglish;
        //}

        ///// <summary>
        ///// translates castle move(e1 g1 h1 f1), king on e1 moves to g1, castling with rook on h1, which moves to f1
        ///// 5 groups, [0] = all, [1] = king start r-f, [2] = king end r-f, [3] = rook start r-f, [4] = rook end r-f
        ///// </summary>
        ///// <param name="match"></param>
        ///// <returns></returns>
        //public string CastleToEnglish(Match match)
        //{
        //    string kingColor = "";
        //    string kingStart = match.Groups[1].Value;
        //    string kingEnd = match.Groups[2].Value;
        //    string rookStart = match.Groups[3].Value;
        //    string rookEnd = match.Groups[4].Value;
        //    string side = "";
        //    if (kingStart.Contains("1"))
        //    {
        //        kingColor = "Light";
        //        if (rookStart.Contains("h"))
        //        {
        //            side = "Kingside";
        //        }
        //        else
        //        {
        //            side = "Queenside";
        //        }
        //    }
        //    else if (kingStart.Contains("8"))
        //    {
        //        kingColor = "Dark";
        //        if (rookStart.Contains("h"))
        //        {
        //            side = "Queenside";
        //        }
        //        else
        //        {
        //            side = "Kingside";
        //        }
        //    }
        //    else
        //    {
        //        side = "invalid castle";
        //    }
        //        string moveInEnglish = kingColor + " King on " + kingStart + " performs a " + side + " castle with rook on " + rookStart
        //        + ". King ends on " + kingEnd + " and rook ends on " + rookEnd;

        //    return moveInEnglish;
        //}

        //public string CharToPiece(string pieceChar)
        //{
        //    string piece = pieceChar;

        //    if (piece.Equals("K"))
        //    {
        //        piece = "King";
        //    }
        //    else if (piece.Equals("Q"))
        //    {
        //        piece = "Queen";
        //    }
        //    else if (piece.Equals("B"))
        //    {
        //        piece = "Bishop";
        //    }
        //    else if (piece.Equals("N"))
        //    {
        //        piece = "Knight";
        //    }
        //    else if (piece.Equals("R"))
        //    {
        //        piece = "Rook";
        //    }
        //    else if (piece.Equals("P"))
        //    {
        //        piece = "Pawn";
        //    }
        //    else
        //    {
        //        Console.WriteLine("Invalid piece type");
        //    }
        //    return piece;
        //}

        public ChessCoordinate StringToChessCoordinate(string coordinate)
        {
            char file = coordinate.ElementAt(0);
            string rank = "" + coordinate.ElementAt(1);
            int rankNum;
            int.TryParse(rank, out rankNum);
            //Console.WriteLine("File = " + file + ", Rank = " + rank);

            ChessCoordinate cc = new ChessCoordinate { File = file, Rank = rankNum };
            return cc;
        }

        /// <summary>
        /// takes input string and a regex pattern. tests the pattern and if successful prints the results
        /// </summary>
        /// <param name="input"></param>
        /// <param name="pattern"></param>
        public void TestPrint(string input, string pattern) 
        {
            Match match = Regex.Match(input, pattern, RegexOptions.IgnoreCase);

            if (match.Success)
            {
                Console.WriteLine(match.Groups.Count);
                for (int j = 0; j < match.Groups.Count; ++j)
                {
                    string key = match.Groups[j].Value;
                    Console.WriteLine(key);
                }
            }
        }
    }
}
