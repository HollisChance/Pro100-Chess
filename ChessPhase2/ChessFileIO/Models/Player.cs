using ChessFileIO.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessFileIO.Models
{
    class Player
    {
        public Color Color { get; set; }

        public List<Piece> Pieces { get; set; }
    }
}
