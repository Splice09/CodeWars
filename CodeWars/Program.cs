using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CodeWars
{
    class Program
    {
        static void Main(string[] args)
        {
            var clues1 = new[]{ 2, 2, 1, 3,
                                2, 2, 3, 1,
                                1, 2, 2, 3,
                                3, 2, 1, 3};

            var clues2 = new[]{ 0, 0, 1, 2,
                                0, 2, 0, 0,
                                0, 3, 0, 0,
                                0, 1, 0, 0};

            Skyscrapers.SolvePuzzle(clues1);
        }
    }

    public class Skyscrapers
    {
        public static int[][] SolvePuzzle(int[] clues)
        {
            int gridSize = (int)Math.Sqrt(clues.Length);

            // separate clues
            var topClues = clues.Take(gridSize).ToArray();
            var rightClues = clues.Skip(gridSize).Take(gridSize).ToArray();
            var bottomClues = clues.Skip(gridSize * 2).Take(gridSize).Reverse().ToArray();
            var leftClues = clues.Skip(gridSize * 3).Take(gridSize).Reverse().ToArray();

            PrintClues(topClues, rightClues, bottomClues, leftClues);

            Building[,] grid = CreateGrid(gridSize);
            PrintGrid(grid);

            grid = InitializeEdgeClues(grid, topClues, rightClues, bottomClues, leftClues);
            PrintGrid(grid);

            //var isPossible = Possible(grid, 2, 2, 4);
            //Console.WriteLine($"Is Possible Check for input 2, 2, 4: {isPossible}");
            return null;
        }

        public static Building[,] CreateGrid(int gridSize)
        {

            Building[,] grid = new Building[gridSize, gridSize];
            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    Building building = new Building
                    {
                        height = 0,
                        row = i,
                        col = j,
                        possibleHeights = new ArrayList()
                    };
                    for (int k = 0; k < gridSize; k++)
                    {
                        building.possibleHeights.Add(k);
                    }
                    grid[i, j] = building;
                }
            }
            return grid;
        }

        private static Building[,] InitializeEdgeClues(Building[,] grid, int[] topClues, int[] rightClues, int[] bottomClues, int[] leftClues)
        {
            int gridSize = topClues.Length;
            for (int i = 0; i < gridSize; i++)
            {
                if (topClues[i] == 1)
                {
                    //set first index of row to gridSize
                    grid[0, i].height = gridSize;
                    grid[0, i].possibleHeights.Clear();
                }
                else if (topClues[i] == gridSize - 1)
                {
                    //any clue of n-1 means you can put the Nth digit in the last or second to last cell
                }
                else if (topClues[i] == gridSize)
                {
                    //set row in sequential order
                    for (int j = 0; j < gridSize; j++)
                    {
                        grid[j, i].height = j + 1;
                        grid[j, i].possibleHeights.Clear();
                    }
                }
            }

            for (int i = 0; i < gridSize; i++)
            {
                if (rightClues[i] == 1)
                {
                    //set first index of row to gridSize
                    grid[i, gridSize - 1].height = gridSize;
                    grid[i, gridSize - 1].possibleHeights.Clear();
                }
                else if (rightClues[i] == gridSize - 1)
                {
                    //any clue of n-1 means you can put the Nth digit in the last or second to last cell
                }
                else if (rightClues[i] == gridSize)
                {
                    //set row in sequential order
                    for (int j = 0; j < gridSize; j++)
                    {
                        grid[i, j].height = gridSize - j;
                        grid[i, j].possibleHeights.Clear();
                    }
                }
            }

            for (int i = 0; i < gridSize; i++)
            {
                if (bottomClues[i] == 1)
                {
                    //set first index of row to gridSize
                    grid[gridSize - 1, i].height = gridSize;
                    grid[gridSize - 1, i].possibleHeights.Clear();
                }
                else if (bottomClues[i] == gridSize - 1)
                {
                    //any clue of n-1 means you can put the Nth digit in the last or second to last cell
                }
                else if (bottomClues[i] == gridSize)
                {
                    //set row in sequential order
                    for (int j = 0; j < gridSize; j++)
                    {
                        grid[j, i].height = gridSize - j;
                        grid[j, i].possibleHeights.Clear();
                    }
                }
            }

            for (int i = 0; i < gridSize; i++)
            {
                if (leftClues[i] == 1)
                {
                    //set first index of row to gridSize
                    grid[i, 0].height = gridSize;
                    grid[i, 0].possibleHeights.Clear();
                }
                else if (leftClues[i] == gridSize - 1)
                {
                    //any clue of n-1 means you can put the Nth digit in the last or second to last cell
                }
                else if (leftClues[i] == gridSize)
                {
                    //set row in sequential order
                    for (int j = 0; j < gridSize; j++)
                    {
                        grid[i, j].height = j + 1;
                        grid[i, j].possibleHeights.Clear();
                    }
                }
            }
            return grid;
        }

        public static bool Possible(Building[,] grid, int[] topClues, int[] rightClues, int[] bottomClues, int[] leftClues, int row, int column, int height)
        {
            int gridSize = grid.GetLength(0);
            //sudoku scanning
            for (int i = 0; i < gridSize; i++)
            {
                if (grid[row, i].height == height)
                {
                    return false;
                }
            }
            for (int i = 0; i < gridSize; i++)
            {
                if (grid[i, column].height == height)
                {
                    return false;
                }
            }
            //check if placement violates edge rules
            
            return true;
        }

        /* Efficiency Enhancement */
        //public static Building[,] SetPossibleHeights(Building[,] grid)
        //{
        //    int gridSize = grid.GetLength(0);
        //    for (int i = 0; i < gridSize; i++)
        //    {
        //        for(int j = 0; j < gridSize; j++)
        //        {

        //        }
        //    }
        //    return grid;
        //}

        public static void PrintClues(IEnumerable<int> topClues, IEnumerable<int> rightClues, IEnumerable<int> bottomClues, IEnumerable<int> leftClues)
        {
            Console.WriteLine("==================");
            Console.WriteLine("Printing Clues");
            Console.WriteLine("==================");
            foreach (var clue in topClues)
            {
                Console.Write(clue);
            }
            Console.WriteLine("");
            foreach (var clue in rightClues)
            {
                Console.Write(clue);
            }
            Console.WriteLine("");
            foreach (var clue in bottomClues)
            {
                Console.Write(clue);
            }
            Console.WriteLine("");
            foreach (var clue in leftClues)
            {
                Console.Write(clue);
            }
            Console.WriteLine("");
        }
        public static void PrintGrid(Building[,] grid)
        {
            Console.WriteLine("==================");
            Console.WriteLine("Printing Grid");
            Console.WriteLine("==================");
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    Console.Write(grid[i, j].height);
                }
                Console.WriteLine();
            }
        }
    }

    public class Building
    {
        public int height { get; set; }
        public int col { get; set; }
        public int row { get; set; }
        public ArrayList possibleHeights { get; set; }
    }
}
