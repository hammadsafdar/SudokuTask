using System;
using System.Linq;

class Program
{
    static void Main()
    {
        int[][] goodSudoku1 =
        {
            new int[] {7, 8, 4, 1, 5, 9, 3, 2, 6},
            new int[] {5, 3, 9, 6, 7, 2, 8, 4, 1},
            new int[] {6, 1, 2, 4, 3, 8, 7, 5, 9},
            new int[] {9, 2, 8, 7, 1, 5, 4, 6, 3},
            new int[] {3, 5, 7, 8, 4, 6, 1, 9, 2},
            new int[] {4, 6, 1, 9, 2, 3, 5, 8, 7},
            new int[] {8, 7, 6, 3, 9, 4, 2, 1, 5},
            new int[] {2, 4, 3, 5, 6, 1, 9, 7, 8},
            new int[] {1, 9, 5, 2, 8, 7, 6, 3, 4}
        };

        //int[][] badSudoku1 = {
        //        new int[] {1,2,3,4,5},
        //        new int[] {1,2,3,4},
        //        new int[] {1,2,3,4},
        //        new int[] {1}
        //    };

        bool isValid = ValidateSudoku(goodSudoku1);

        Console.WriteLine(isValid
            ? "The Sudoku is valid!"
            : "The Sudoku is not valid!");
    }

    static bool ValidateSudoku(int[][] sudoku)
    {
        int N = sudoku.Length;
        int sqrtN = (int)Math.Sqrt(N);

        return Enumerable.Range(0, N).All(i =>
            IsValidSet(sudoku[i], N) &&
            IsValidSet(sudoku.Select(row => row[i]).ToArray(), N) &&
            IsValidSet(Enumerable.Range(i / sqrtN * sqrtN, sqrtN)
                .SelectMany(row => Enumerable.Range(i % sqrtN * sqrtN, sqrtN).Select(col => sudoku[row][col]))
                .ToArray(), N));
    }

    static bool IsValidSet(int[] set, int N)
    {
        bool[] seen = new bool[N + 1];

        return set.All(num => num >= 1 && num <= N && !seen[num] && (seen[num] = true));
    }
}
