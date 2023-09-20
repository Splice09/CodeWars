using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

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

            var clues3 = new[]{ 3, 2, 2, 3, 2, 1,
                                1, 2, 3, 3, 2, 2,
                                5, 1, 2, 2, 4, 3,
                                3, 2, 1, 2, 2, 4};

            var clues4 = new[] { 7, 0, 0, 0, 2, 2, 3,
                                 0, 0, 3, 0, 0, 0, 0,
                                 3, 0, 3, 0, 0, 5, 0,
                                 0, 0, 0, 0, 5, 0, 4 };

            var skyscrapers = new Skyscrapers();
            skyscrapers.SolvePuzzle(clues1);
            
            //int[] buildingArray = { 7, 0, 8, 2, 9 };

            //Console.Write(skyscrapers.CountBuildingsVisible(buildingArray));
        }
    }

    public class Skyscrapers
    {
        public List<Building[,]> answers = new List<Building[,]>();
        public int[][] SolvePuzzle(int[] clues)
        {
            int gridSize = clues.Length / 4;

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

            //var isPossible = Possible(grid, topClues, rightClues, bottomClues, leftClues, 2, 1, 4);
            //Console.WriteLine($"Is Possible Check for input 2, 1, 4: {isPossible}");

            Solve(grid, topClues, rightClues, bottomClues, leftClues);

            Console.WriteLine("In SolvePuzzle()");
            //foreach (var answer in answers)
            //{
            //    PrintGrid(answer);
            //}
            //grid = Solve(grid, topClues, rightClues, bottomClues, leftClues);
            //PrintGrid(grid);
            return null;
        }

        public Building[,] CreateGrid(int gridSize)
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

        private Building[,] InitializeEdgeClues(Building[,] grid, int[] topClues, int[] rightClues, int[] bottomClues, int[] leftClues)
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

        public void Solve(Building[,] grid, int[] topClues, int[] rightClues, int[] bottomClues, int[] leftClues)
        {
            int gridSize = grid.GetLength(0);
            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    if (grid[i, j].height == 0)
                    {
                        for (int n = 1; n <= gridSize; n++)
                        {
                            if (Possible(grid, topClues, rightClues, bottomClues, leftClues, i, j, n))
                            {
                                grid[i, j].height = n;
                                Solve(grid, topClues, rightClues, bottomClues, leftClues);
                                //backtracking
                                grid[i, j].height = 0;
                            }
                        }
                        return;
                    }
                }
            }
            Console.WriteLine("In recursive function");
            PrintGrid(grid);

            //Building[,] solvedGrid = new Building[gridSize, gridSize];
            //solvedGrid =  grid.Clone() as Building[,];
            //this.answers.Add(grid);
            
            //return grid;
        }

        public bool Possible(Building[,] grid, int[] topClues, int[] rightClues, int[] bottomClues, int[] leftClues, int row, int column, int height)
        {
            int gridSize = grid.GetLength(0);
            #region SUDOKU_SCANNING
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
            #endregion

            /*=========================================
            check if placement violates edge clues
            =========================================*/
            var leftClue = leftClues[row];
            var rightClue = rightClues[row];
            var topClue = topClues[column];
            var bottomClue = bottomClues[column];

            #region ROW_RULES
            //Any clue of N-1 means you can put the Nth digit in the last or second to last cell
            if ((leftClue == gridSize - 1) && (height == gridSize) && (column < gridSize - 2))
            {
                return false;
            }

            if ((rightClue == gridSize - 1) && (height == gridSize) && (column > 1))
            {
                return false;
            }

            //Any clue >2 you can 'X' out N and N-1 in the first cell
            if ((leftClue > 2) && (height == gridSize || height == gridSize - 1) && (column == 0))
            {
                return false;
            }

            if ((rightClue > 2) && (height == gridSize || height == gridSize - 1) && (column == gridSize - 1))
            {
                return false;
            }
            #endregion
            
            #region COLUMN_RULES
            //Any clue of N-1 means you can put the Nth digit in the last or second to last cell
            if ((topClue == gridSize - 1) && (height == gridSize) && (row < gridSize - 2))
            {
                return false;
            }

            if ((bottomClue == gridSize - 1) && (height == gridSize) && (row > 1))
            {
                return false;
            }

            //Any clue >2 you can 'X' out N and N-1 in the first cell
            if ((topClue > 2) && (height == gridSize || height == gridSize - 1) && (row == 0))
            {
                return false;
            }

            if ((bottomClue > 2) && (height == gridSize || height == gridSize - 1) && (row == gridSize - 1))
            {
                return false;
            }
            #endregion

            #region CALCULATE VISIBLE

            grid[row, column].height = height;
            //PrintGrid(grid);
            if (topClue > 0)
            {
                int[] gridColumn = new int[gridSize];
                for (int i = 0; i < gridSize; i++)
                {
                    gridColumn[i] = grid[i, column].height;
                }
                if (!gridColumn.Contains(0))
                {
                    int buildingsVisible = CountBuildingsVisible(gridColumn);
                    if (buildingsVisible > topClue)
                    {
                        grid[row, column].height = 0;
                        return false;
                    }
                }
            }
            if (rightClue > 0)
            {
                int[] gridRow = new int[gridSize];
                for (int i = 0; i < gridSize; i++)
                {
                    gridRow[i] = grid[row, i].height;
                }
                if (!gridRow.Contains(0))
                {
                    gridRow = gridRow.Reverse().ToArray();
                    int buildingsVisible = CountBuildingsVisible(gridRow);
                    if (buildingsVisible > rightClue)
                    {
                        grid[row, column].height = 0;
                        return false;
                    }
                }
                
            }
            if (bottomClue > 0)
            {
                int[] gridColumn = new int[gridSize];
                for (int i = 0; i < gridSize; i++)
                {
                    gridColumn[i] = grid[i, column].height;
                }
                if (!gridColumn.Contains(0))
                {
                    gridColumn = gridColumn.Reverse().ToArray();
                    int buildingsVisible = CountBuildingsVisible(gridColumn);
                    if (buildingsVisible > bottomClue)
                    {
                        grid[row, column].height = 0;
                        return false;
                    }
                }
                
            }
            if (leftClue > 0)
            {
                int[] gridRow = new int[gridSize];
                for (int i = 0; i < gridSize; i++)
                {
                    gridRow[i] = grid[row, i].height;
                }
                if (!gridRow.Contains(0))
                {
                    int buildingsVisible = CountBuildingsVisible(gridRow);
                    if (buildingsVisible > leftClue)
                    {
                        grid[row, column].height = 0;
                        return false;
                    }
                }
            }


            #endregion

            return true;
        }

        public int CountBuildingsVisible(int[] buildingArray)
        {
            int buildingCount = 0;
            int currentMaximum = buildingArray[0];

            for (int i = 0; i < buildingArray.Length; i++)
            {
                if (buildingArray[i] != 0 && (buildingArray[i] > currentMaximum || buildingArray[i] == currentMaximum))
                {
                    buildingCount++;
                    currentMaximum = buildingArray[i];
                }
            }

            return buildingCount;
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

        public void PrintClues(IEnumerable<int> topClues, IEnumerable<int> rightClues, IEnumerable<int> bottomClues, IEnumerable<int> leftClues)
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
        public void PrintGrid(Building[,] grid)
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
