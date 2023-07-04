using SudokuSolver.Logic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver.Logic.Utility
{
    internal static class PuzzleExtensions
    {

        public static IEnumerable<Square> GetAllUnique(this IEnumerable<Square> squares, Square excludeSquare = null)
            => squares.Distinct()
                      .Where(s => s != excludeSquare)
                      .ToArray();
        public static IEnumerable<Square> GetAllUnique(this IEnumerable<IEnumerable<Square>> squares, Square excludeSquare = null)
            => squares.SelectMany(s => s).GetAllUnique(excludeSquare);


        public static IEnumerable<Square> GetAllUniqueFilled(this IEnumerable<Square> squares, Square excludeSquare = null)
            => squares.Distinct()
                      .Where(s => s != excludeSquare && s.Value is not null)
                      .ToArray();


        public static IEnumerable<Square> GetAllUniqueEmpty(this IEnumerable<Square> squares, Square excludeSquare = null)
            => squares.Distinct()
                      .Where(s => s != excludeSquare && s.Value is null)
                      .ToArray();

        public static IEnumerable<Square> GetAllUniqueFilled(this IEnumerable<IEnumerable<Square>> squares, Square excludeSquare = null)
            => squares.SelectMany(s => s).GetAllUniqueFilled(excludeSquare);


        public static IEnumerable<Square> GetAllUniqueEmpty(this IEnumerable<IEnumerable<Square>> squares, Square excludeSquare = null)
            => squares.SelectMany(s => s).GetAllUniqueEmpty(excludeSquare);


        public static IEnumerable<IEnumerable<Square>> GetAllSurroundingSquares(this IEnumerable<Square> squares, Square targetSquare)
            => new[]
            {
                squares.Where(s => s.Row == targetSquare.Row && s != targetSquare),
                squares.Where(s => s.Column == targetSquare.Column && s != targetSquare),
                squares.Where(s => s.Region == targetSquare.Region && s != targetSquare),
            };



    }
}
