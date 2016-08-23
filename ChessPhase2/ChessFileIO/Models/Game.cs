using ChessFileIO.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessFileIO.Models
{
    public class Game
    {
        public Color PlayerTurn { get; set; }
        public int TurnCounter { get; set; }
        public bool IsBlackInCheck { get; set; }
        public bool IsWhiteInCheck { get; set; }

        public void SwitchTurn()
        {
            TurnCounter++;
            if (PlayerTurn == Color.Dark)
            {
                PlayerTurn = Color.Light;
            }
            else if (PlayerTurn == Color.Light)
            {
                PlayerTurn = Color.Dark;
            }
        }

        public Game()
        {
            PlayerTurn = Color.Light;
        }
    }
}
