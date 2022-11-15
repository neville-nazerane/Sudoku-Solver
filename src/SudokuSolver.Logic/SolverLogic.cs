using SudokuSolver.Logic.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver.Logic
{
    public static class SolverLogic
    {

        public static readonly Dictionary<string, int> RegionMappings = new()
        {
            { "0,0", 0 },
            { "0,1", 0 },
            { "0,2", 0 },
            { "1,0", 0 },
            { "1,1", 0 },
            { "1,2", 0 },
            { "2,0", 0 },
            { "2,1", 0 },
            { "2,2", 0 },

            { "0,3", 1 },
            { "0,4", 1 },
            { "0,5", 1 },
            { "1,3", 1 },
            { "1,4", 1 },
            { "1,5", 1 },
            { "2,3", 1 },
            { "2,4", 1 },
            { "2,5", 1 },

            { "0,6", 2 },
            { "0,7", 2 },
            { "0,8", 2 },
            { "1,6", 2 },
            { "1,7", 2 },
            { "1,8", 2 },
            { "2,6", 2 },
            { "2,7", 2 },
            { "2,8", 2 },

            { "3,0", 3 },
            { "3,1", 3 },
            { "3,2", 3 },
            { "4,0", 3 },
            { "4,1", 3 },
            { "4,2", 3 },
            { "5,0", 3 },
            { "5,1", 3 },
            { "5,2", 3 },

            { "3,3", 4 },
            { "3,4", 4 },
            { "3,5", 4 },
            { "4,3", 4 },
            { "4,4", 4 },
            { "4,5", 4 },
            { "5,3", 4 },
            { "5,4", 4 },
            { "5,5", 4 },


            { "3,6", 5 },
            { "3,7", 5 },
            { "3,8", 5 },
            { "4,6", 5 },
            { "4,7", 5 },
            { "4,8", 5 },
            { "5,6", 5 },
            { "5,7", 5 },
            { "5,8", 5 },

            { "6,0", 6 },
            { "6,1", 6 },
            { "6,2", 6 },
            { "7,0", 6 },
            { "7,1", 6 },
            { "7,2", 6 },
            { "8,0", 6 },
            { "8,1", 6 },
            { "8,2", 6 },

            { "6,3", 7 },
            { "6,4", 7 },
            { "6,5", 7 },
            { "7,3", 7 },
            { "7,4", 7 },
            { "7,5", 7 },
            { "8,3", 7 },
            { "8,4", 7 },
            { "8,5", 7 },


            { "6,6", 8 },
            { "6,7", 8 },
            { "6,8", 8 },
            { "7,6", 8 },
            { "7,7", 8 },
            { "7,8", 8 },
            { "8,6", 8 },
            { "8,7", 8 },
            { "8,8", 8 }
        };

        public static Puzzle CreatePuzzle()
        {
            var squares = new List<List<Square>>();
            var result = new Puzzle() { AllSquares = squares };

            for (int row = 0; row < 9; row++)
            {
                var list = new List<Square>();
                squares.Add(list);
                for (int column = 0; column < 9; column++)
                {
                    int region = RegionMappings[$"{row},{column}"];

                    Square square = new()
                    {
                        Column = column,
                        Row = row,
                        Region = region
                    };
                    list.Add(square);

                    SetCollectionValue(result.Columns, column, square);
                    SetCollectionValue(result.Rows, row, square);
                    SetCollectionValue(result.Regions, region, square);
                }
            }

            return result;
        }

        private static void SetCollectionValue(Dictionary<int, IEnumerable<Square>> collection, int key, Square square)
        {
            if (!collection.TryGetValue(key, out var items))
            {
                items = new List<Square>();
                collection[key] = items;
            }
            var list = (List<Square>)items;
            list.Add(square);
        }

        public static void Compute(Puzzle puzzle)
        {
            var updatedSq = puzzle.AllSquares
                                  .SelectMany(s => s)
                                  .Distinct()
                                  .Where(s => s.Value.HasValue)
                                  .ToArray()
                                  .AsEnumerable();

            while (updatedSq.Any())
                updatedSq = Compute(puzzle, updatedSq);
                
        }
        
        private static IEnumerable<Square> Compute(Puzzle puzzle, IEnumerable<Square> updatedSqures)
        {
            var changed = new HashSet<Square>();

            //var unfilledSquares = puzzle.AllSquares
            //                         .SelectMany(s => s)
            //                         .Where(s => s.Value is null)
            //                         .ToArray();
            Dictionary<Square, HashSet<int>> nonPossibles = puzzle.AllSquares
                                     .SelectMany(s => s)
                                     .Where(s => s.Value is null)
                                     .ToDictionary(s => s, s => new HashSet<int>());

            foreach (var square in updatedSqures)
            {
                var surrounding = puzzle.Rows[square.Row]
                                        .Union(puzzle.Columns[square.Column])
                                        .Union(puzzle.Regions[square.Region])
                                        .Distinct()
                                        .Where(s => nonPossibles.ContainsKey(s) && s != square)
                                        .Select(s => nonPossibles[s])
                                        .ToArray();

                foreach (var nonPoss in surrounding)
                    if (square.Value.HasValue)
                        nonPoss.Add(square.Value.Value);
            }

            foreach (var nonPoss in nonPossibles)
            {

                switch (nonPoss.Value.Count)
                {
                    case 8:
                        nonPoss.Key.Value = 45 - nonPoss.Value.Sum();
                        changed.Add(nonPoss.Key);
                        break;
                    case > 8:
                        nonPoss.Key.IsBroken = true;
                        break;
                }

            }

            var unfilledSquares = nonPossibles.Keys
                                          .Where(s => s.Value is null)
                                          .ToArray();

            foreach (var square in unfilledSquares)
            {
                var blocks = new[]
                            {
                                puzzle.Rows[square.Row],
                                puzzle.Columns[square.Column],
                                puzzle.Regions[square.Region]
                            }
                            .Select(r => r.Where(s => nonPossibles.ContainsKey(s) && s != square).Select(s => nonPossibles[s]))
                            .ToArray();

                for (int i = 1; i < 10; i++)
                {
                    if (blocks.Any(b => b.All(p => p.Contains(i))))
                    {
                        //square.Value = i;
                        changed.Add(square);
                        break;
                    }
                }
            }

            return changed;

        }

    }
}
