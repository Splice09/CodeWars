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
                                3, 4, 1, 3};

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
            return null;
        }

        public static Building[,] CreateGrid(int gridSize)
        {
            
            Building[,] grid = new Building[gridSize, gridSize];
            for(int i = 0; i < gridSize; i++)
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
                    for(int k = 0; k < gridSize; k++)
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
                if(topClues[i] == 1)
                {
                    grid[0, i].height = gridSize;
                    grid[0, i].possibleHeights.Clear();
                }
                else if(topClues[i] == gridSize - 1)
                {

                }
                else if (topClues[i] == gridSize)
                {
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
                    grid[i, gridSize - 1].height = gridSize;
                    grid[i, gridSize - 1].possibleHeights.Clear();
                }
                else if (rightClues[i] == gridSize - 1)
                {

                }
                else if (rightClues[i] == gridSize)
                {
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
                    grid[gridSize - 1, i].height = gridSize;
                    grid[gridSize - 1, i].possibleHeights.Clear();
                }
                else if (bottomClues[i] == gridSize - 1)
                {

                }
                else if (bottomClues[i] == gridSize)
                {
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
                    grid[i, 0].height = gridSize;
                    grid[i, 0].possibleHeights.Clear();
                }
                else if (leftClues[i] == gridSize - 1)
                {

                }
                else if (leftClues[i] == gridSize)
                {
                    for (int j = 0; j < gridSize; j++)
                    {
                        grid[i, j].height = j + 1;
                        grid[i, j].possibleHeights.Clear();
                    }
                }
            }
            return grid;
        }

        public static void PrintClues(IEnumerable<int> topClues, IEnumerable<int> rightClues, IEnumerable<int> bottomClues, IEnumerable<int> leftClues)
        {
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
