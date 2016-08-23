using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessFileIO.Models
{
    class Bishop : Piece
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
            // makes sure the move is diagonal
            if (Position.Rank - destination.Rank == Position.FileAsInt() - destination.FileAsInt() || destination.Rank - Position.Rank == Position.FileAsInt() - destination.FileAsInt() || Position.Rank - destination.Rank == destination.FileAsInt() - Position.FileAsInt() || destination.Rank - Position.Rank == destination.FileAsInt() - Position.FileAsInt())
            {
                if (Position.Rank > destination.Rank)
                {
                    if (Position.FileAsInt() > destination.FileAsInt()) // Down and Right
                    {
                        bool doLoop = true;
                        for (int j = destination.Rank - 1; j < Position.Rank - 1 && doLoop; ++j)
                        {
                            for (int k = destination.FileAsInt(); k < Position.FileAsInt(); ++k)
                            {
                                if (Position.Rank - j - 1 == Position.FileAsInt() - k)
                                {
                                    Piece piece = board.Board[j, k].Piece;

                                    if (piece == null)
                                    {
                                        isValid = true;
                                    }
                                    else
                                    {
                                        doLoop = false;
                                    }
                                }
                            }
                        }
                    }
                    else if (Position.FileAsInt() < destination.FileAsInt()) // 1 Down and Right
                    {
                        bool doLoop = true;
                        for (int j = destination.Rank - 1; j < Position.Rank - 1 && doLoop; ++j)
                        {
                            for (int k = destination.FileAsInt(); k > Position.FileAsInt(); --k)
                            {
                                if (Position.Rank - j - 1 == k - Position.FileAsInt()) // making sure the position being checked is diagonal
                                {
                                    Piece piece = board.Board[j, k].Piece;

                                    if (piece == null)
                                    {
                                        isValid = true;
                                    }
                                    else
                                    {
                                        doLoop = false;
                                    }
                                }
                            }
                        }
                    }
                }
                else if (Position.Rank < destination.Rank) // 2
                {
                    if (Position.FileAsInt() > destination.FileAsInt())
                    {
                        bool doLoop = true;
                        for (int j = destination.Rank - 1; j > Position.Rank - 1 && doLoop; --j)
                        {
                            for (int k = destination.FileAsInt(); k < Position.FileAsInt() && doLoop; ++k)
                            {
                                if (j - (Position.Rank - 1) == Position.FileAsInt() - k)
                                {
                                    Piece piece = board.Board[j, k].Piece;
                                    if (piece == null)
                                    {
                                        isValid = true;
                                    }
                                    else
                                    {
                                        doLoop = false;
                                    }
                                }
                            }
                        }
                    }
                    else if (Position.FileAsInt() < destination.FileAsInt()) // 3
                    {
                        bool doLoop = true;
                        for (int j = destination.Rank - 1; j > Position.Rank - 1 && doLoop; --j)
                        {
                            for (int k = destination.FileAsInt(); k > Position.FileAsInt() && doLoop; --k)
                            {
                                int test1 = j - (Position.Rank - 1);
                                int test2 = k - Position.FileAsInt();
                                bool testbool = test1 == test2;
                                if (j - (Position.Rank - 1) == k - Position.FileAsInt())
                                {
                                    Piece piece = board.Board[j, k].Piece;

                                    if (piece == null)
                                    {
                                        isValid = true;
                                    }
                                    else
                                    {
                                        doLoop = false;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            if (!isValid)
            {
                Console.WriteLine("BISHOP move invalid");
            }
            return isValid;
        }

        public override string ToString()
        {
            string piece = "b";
            if (this.Color == Enums.Color.Dark)
            {
                piece = piece.ToUpper();
            }
            return piece;
        }
    }
}
