using SudokuSolver.Logic;
using SudokuSolver.Logic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver.Website.Pages
{
    public partial class Index
    {

        Puzzle puzzle = SolverLogic.CreatePuzzle();

        public Index()
        {
            Console.WriteLine(puzzle);
        }

    }
}
