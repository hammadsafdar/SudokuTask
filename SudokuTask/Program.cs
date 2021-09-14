using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuTask
{
    class Program
    {
        static void Main(string[] args)
        {
            int[][] goodSudoku1 = {
                new int[] {7,8,4,  1,5,9,  3,2,6},
                new int[] {5,3,9,  6,7,2,  8,4,1},
                new int[] {6,1,2,  4,3,8,  7,5,9},

                new int[] {9,2,8,  7,1,5,  4,6,3},
                new int[] {3,5,7,  8,4,6,  1,9,2},
                new int[] {4,6,1,  9,2,3,  5,8,7},

                new int[] {8,7,6,  3,9,4,  2,1,5},
                new int[] {2,4,3,  5,6,1,  9,7,8},
                new int[] {1,9,5,  2,8,7,  6,3,4}
            };

            int[][] goodSudoku2 = {
                new int[] {1,4, 2,3},
                new int[] {3,2, 4,1},

                new int[] {4,1, 3,2},
                new int[] {2,3, 1,4}
            };

            int[][] badSudoku1 =  {
                new int[] {1,2,3, 4,5,6, 7,8,9},
                new int[] {1,2,3, 4,5,6, 7,8,9},
                new int[] {1,2,3, 4,5,6, 7,8,9},

                new int[] {1,2,3, 4,5,6, 7,8,9},
                new int[] {1,2,3, 4,5,6, 7,8,9},
                new int[] {1,2,3, 4,5,6, 7,8,9},

                new int[] {1,2,3, 4,5,6, 7,8,9},
                new int[] {1,2,3, 4,5,6, 7,8,9},
                new int[] {1,2,3, 4,5,6, 7,8,9}
            };

            int[][] badSudoku2 = {
                new int[] {1,2,3,4,5},
                new int[] {1,2,3,4},
                new int[] {1,2,3,4},
                new int[] {1}
            };
            
            Debug.Assert(ValidateSudoku(goodSudoku1, 3), "It's a good sudoku!");
            Debug.Assert(ValidateSudoku(goodSudoku2, 2), "It's a good sudoku!");
            Debug.Assert(!ValidateSudoku(badSudoku1, 3), "It's a bad sudoku!");
            Debug.Assert(!ValidateSudoku(badSudoku2, 5), "It's a bad sudoku!");

            Console.ReadKey();
        }
       
        static bool ValidateSudoku(int[][] puzzle,int subSquareSize)
        {
            int size = puzzle.Length;

            // First make sure all the values are in the right range.
            if (!checkValues(puzzle, 1, size))
            {
                Console.WriteLine("Bad Sudoku");
                return false;
            }

            // Check that the rows contain no duplicate values
            for (int row = 0; row < size; ++row)
            {
                if (!checkRow(puzzle, row))
                {
                    Console.WriteLine("Bad Sudoku");
                    return false;
                }
            }

            // Check that the columns contain no duplicate values
            for (int col = 0; col < size; ++col)
            {
                if (!checkColumn(puzzle, col))
                {
                    Console.WriteLine("Bad Sudoku");
                    return false;
                }
            }

            // Check that the subsquares contain no duplicate values
            for (int baseRow = 0; baseRow < size; baseRow += subSquareSize)
            {
                for (int baseCol = 0; baseCol < size; baseCol += subSquareSize)
                {
                    if (!checkSquare(puzzle, baseRow, baseCol, subSquareSize))
                    {
                        Console.WriteLine("Bad Sudoku");
                        return false;
                    }
                }
            }
            Console.WriteLine("Good Sudoku");
            return true;

        }

        private static bool checkSquare(int[][] puzzle, int baseRow, int baseCol, int subSquareSize)
        {
            bool[] found = new bool[puzzle.Length];
            for (int row = baseRow; row < (baseRow + subSquareSize); ++row)
            {
                for (int col = baseCol; col < (baseCol + subSquareSize); ++col)
                {
                    // set found[x - 1] to be true if we find x in the row
                    int index = puzzle[row][col] - 1;
                    if (!found[index])
                    {
                        found[index] = true;
                    }
                    else
                    {
                        // found it twice, so return false
                        return false;
                    }
                }
            }

            return true;
        }

        private static bool checkColumn(int[][] puzzle, int whichCol)
        {
            int size = puzzle.Length;
            bool[] found = new bool[size];
            for (int row = 0; row < size; ++row)
            {
                // set found[x - 1] to be true if we find x in the row
                int index = puzzle[row][whichCol] - 1;
                if (!found[index])
                {
                    found[index] = true;
                }
                else
                {
                    // found it twice, so return false
                    return false;
                }
            }

            // didn't find any number twice, so return true
            return true;
        }

        private static bool checkRow(int[][] puzzle, int whichRow)
        {
            int size = puzzle.Length;
            bool[] found = new bool[size];
            for (int col = 0; col < size; ++col)
            {
                // set found[x - 1] to be true if we find x in the row
                int index = puzzle[whichRow][col] - 1;
                if (!found[index])
                {
                    found[index] = true;
                }
                else
                {
                    // found it twice, so return false
                    return false;
                }
            }

            // didn't find any number twice, so return true
            return true;
        }

        private static bool checkValues(int[][] puzzle, int min, int max)
        {
            for (int row = 0; row < puzzle.Length; ++row)
            {
                for (int col = 0; col < puzzle[0].Length; ++col)
                {
                    if (puzzle[row][col] < min || puzzle[row][col] > max)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
