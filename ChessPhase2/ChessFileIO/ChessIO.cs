using ChessFileIO.Controllers;
using ChessFileIO.Enums;
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
        private ChessToEnglishTranslator _translator = new ChessToEnglishTranslator();

        public void ReadChessFile(string fileName, PieceController controller, ChessBoard board, Game game)
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
                            ReadInput(controller, line, board, game);
                            board.PrintBoard();
                            Console.WriteLine();
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("file [" + fileName + "] could not be read");
                Console.WriteLine(e.Message);
            }
        }

        public void ReadInput(PieceController controller, string input, ChessBoard board, Game game)
        {
            Match placePiece = RegexTest(input, PLACE_PIECE_PATTERN);
            Match movePiece = RegexTest(input, MOVE_PIECE_PATTERN);
            Match castleMove = RegexTest(input, CASTLE_MOVE_PATTERN);
            string move = "";

            if (castleMove.Success)                                 // if the match for castle move was successful, it translates the castle move to english and perfoms it on the board
            {
                move = _translator.CastleToEnglish(castleMove);     // gets an english translation of a castle move
                CastleToBoard(controller, castleMove, board, game); // performs the castle move on the console board
            }
            else if (movePiece.Success)                             // if the movePiece match succeeded, then it is translated and the piece is moved
            {

                move = _translator.MoveToEnglish(movePiece);        // translates to english
                MoveToBoard(controller, movePiece, board, game);    // takes the match info and moves a piece on the board
            }
            else if (placePiece.Success)                        // if the placePiece match succeeded it translates and places a piece
            {
                move = _translator.PlacementToEnglish(placePiece); //takes the match info and converts to english
                PlacementToBoard(controller, placePiece, board);              // takes the match info and places piece on board
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

        public void PlacementToBoard(PieceController controller, Match match, ChessBoard board)
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

            Piece newPiece = CharToPiece(pieceChar);

            controller.PlacePiece(board, placement, newPiece);
        }

        public void MoveToBoard(PieceController controller, Match match, ChessBoard board, Game game)
        {
            string start = match.Groups[1].Value;
            string end = match.Groups[2].Value;

            ChessCoordinate startCoord = StringToChessCoordinate(start);
            ChessCoordinate endCoord = StringToChessCoordinate(end);

            controller.MovePiece(board, game, startCoord, endCoord);
        }

        public void CastleToBoard(PieceController controller, Match match, ChessBoard board, Game game)
        {
            string start1 = match.Groups[1].Value;
            string end1 = match.Groups[2].Value;
            string start2 = match.Groups[3].Value;
            string end2 = match.Groups[4].Value;

            ChessCoordinate startCoord1 = StringToChessCoordinate(start1); // kings start coordinate
            ChessCoordinate endCoord1 = StringToChessCoordinate(end1); // king ends up

            ChessCoordinate startCoord2 = StringToChessCoordinate(start2); // rook starts
            ChessCoordinate endCoord2 = StringToChessCoordinate(end2); // rook ends

            controller.MovePiece(board, game, startCoord2, endCoord2);

            controller.MovePiece(board, game, startCoord1, endCoord1);
        }

        public ChessCoordinate StringToChessCoordinate(string coordinate)
        {
            char file = coordinate.ElementAt(0);
            string rank = "" + coordinate.ElementAt(1);
            int rankNum;
            int.TryParse(rank, out rankNum);
            //Console.WriteLine("File = " + file + ", Rank = " + rank);

            ChessCoordinate cc = new ChessCoordinate() { File = file, Rank = rankNum };
            return cc;
        }

        public Piece CharToPiece(char pieceChar)
        {
            Piece piece = new Bishop();

            if (pieceChar == 'k' || pieceChar == 'K')
            {
                piece = new King();
            }
            else if (pieceChar == 'q' || pieceChar == 'Q')
            {
                piece = new Queen();
            }
            else if (pieceChar == 'r' || pieceChar == 'R')
            {
                piece = new Rook();
            }
            else if(pieceChar == 'b' || pieceChar == 'B')
            {
                piece = new Bishop();
            }
            else if (pieceChar == 'n' || pieceChar == 'N')
            {
                piece = new Knight();
            }
            else if (pieceChar == 'p' || pieceChar == 'P')
            {
                piece = new Pawn();
            }
            if (pieceChar < 90) // light vs dark
            {
                piece.Color = Color.Dark;
            }
            else
            {
                piece.Color = Color.Light;
            }
            return piece;
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
