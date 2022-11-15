using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver.Logic.Model
{
    public class Puzzle
    {
        required public IEnumerable<IEnumerable<Square>> AllSquares { get; set; }

        public Dictionary<int, IEnumerable<Square>> Rows { get; private set; }
        public Dictionary<int, IEnumerable<Square>> Columns { get; private set; }
        public Dictionary<int, IEnumerable<Square>> Regions { get; private set; }

        public IEnumerable<Dictionary<int, IEnumerable<Square>>> Collections { get; private set; }

        internal Puzzle()
        {
            Rows = new();
            Columns = new();
            Regions = new();

            Collections = new[]
            {
                Rows, Columns, Regions
            };
        }

    }
}
