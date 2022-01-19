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
            var topClues = clues.Take(gridSize);
            var rightClues = clues.Skip(gridSize).Take(gridSize);
            var bottomClues = clues.Skip(gridSize * 2).Take(gridSize);
            var leftClues = clues.Skip(gridSize * 3).Take(gridSize);
            PrintClues(topClues, rightClues, bottomClues, leftClues);

            Building[,] grid = CreateGrid(gridSize);
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

        private static Building[,] InitializeEdgeClues(Building[,] grid, IEnumerable<int> topClues, IEnumerable<int> rightClues, IEnumerable<int> bottomClues, IEnumerable<int> leftClues)
        {
            // set up buildings based on clues
            foreach (var clue in topClues)
            {
                // TODO: set buildings
            }

            foreach (var clue in rightClues)
            {
                // TODO: set buildings
            }

            foreach (var clue in bottomClues)
            {
                // TODO: set buildings
            }

            foreach (var clue in leftClues)
            {
                // TODO: set buildings
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
