using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver.Logic.Model
{
    public class Square
    {

        //public string Id => $"{Row},{Column},{Region}";

        public int? Value { get; set; }

        required public int Column { get; init; }

        required public int Region { get; init; }

        required public int Row { get; init; }

        public bool IsUserEntered { get; set; }
        public bool IsBroken { get; internal set; }

        internal Square()
        {

        }

        //public override bool Equals(object? obj) => obj is Square sq && Id == sq.Id;


    }
}
