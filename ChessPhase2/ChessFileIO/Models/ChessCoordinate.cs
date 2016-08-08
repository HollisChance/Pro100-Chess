using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessFileIO.Models
{
    class ChessCoordinate
    {
        public char File { get; set; }

        public int Rank { get; set; }

        public override string ToString()
        {
            return File + "" + Rank;
        }

        public int FileAsInt() // need to check this
        {
            // 65 - 90 capitol letters
            string s = File.ToString().ToUpper();
            char upperFile;
            char.TryParse(s, out upperFile);
            int num = upperFile;
            num -= 65;
            return num;
        }
    }
}
