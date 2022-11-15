using Microsoft.AspNetCore.Components.Web;
using SudokuSolver.Logic;
using SudokuSolver.Logic.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver.Website.Pages
{
    public partial class Index
    {

        Puzzle puzzle = SolverLogic.CreatePuzzle();

        Square? selection = null;

        //async Task Sweet()
        //{
        //    SolverLogic.Compute(puzzle);
        //}

        void Solve()
        {
            SolverLogic.Compute(puzzle);
        }

        void ForcedManualBind(KeyboardEventArgs e, Square sq)
        {
            if (int.TryParse(e.Key, out int value))
                sq.Value = value;
        }

        void Select(Square sq)
        {
            Console.WriteLine("selected!");
            selection = sq;
        }

        void TriggerKeypress(KeyboardEventArgs e)
        {
            Console.WriteLine(e.Key);
            if (selection is not null && int.TryParse(e.Key, out int val) && val > 0 && val < 10)
            {
                selection.Value = val;
            }
        }

        string GetInputClass(Square sq)
        {
            string buffer = string.Empty;
            if (selection == sq)
                buffer = "-outline";
            return sq.Region % 2 == 0 ? $"btn{buffer}-success" : $"btn{buffer}-info";
        }

    }
}
