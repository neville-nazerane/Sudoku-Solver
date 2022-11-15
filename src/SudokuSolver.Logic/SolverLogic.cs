using SudokuSolver.Logic.Model;
using System;
using System.Collections.Generic;
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

            for (int column = 0; column < 9; column++)
            {
                var list = new List<Square>();
                squares.Add(list);
                for (int row = 0; row < 9; row++)
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

    }
}
