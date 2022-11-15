using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver.Logic.Model
{
    public class Square
    {

        public int? Value { get; set; }

        required public int Column { get; init; }

        required public int Region { get; init; }

        required public int Row { get; init; }

        public bool IsEnforced { get; set; }

        internal Square()
        {

        }

    }
}
