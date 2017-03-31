using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Life {

    class Program {

        static void Main(string[] args) {
            Random rnd = new Random();
            const int rowNum = 25, colNum = 50;
            int neighs = 0;
            int[,] cells = new int[rowNum, colNum];

            // Example scenarios for different sizes
            #region scenarios
            int[,] scenario1 = new int[25, 50];
            int[,] scenario2 = new int[20, 25];
            int[,] scenario3 = new int[10, 10];

            #region scenario1
            for (int i = 0; i < 25; i++) {
                for (int j = 0; j < 50; j++) {
                    scenario1[i, j] = 0;
                }
            }
            scenario1[1, 1] = 1;
            scenario1[1, 0] = 1;
            scenario1[1, 2] = 1;
            scenario1[1, 3] = 1;
            scenario1[2, 3] = 1;
            scenario1[2, 4] = 1;
            scenario1[2, 6] = 1;
            scenario1[4, 8] = 1;
            scenario1[4, 9] = 1;
            scenario1[4, 10] = 1;
            scenario1[3, 8] = 1;
            scenario1[3, 7] = 1;
            scenario1[5, 10] = 1;
            scenario1[5, 8] = 1;
            scenario1[6, 6] = 1;
            scenario1[6, 5] = 1;
            scenario1[6, 4] = 1;
            scenario1[7, 5] = 1;
            scenario1[7, 7] = 1;
            scenario1[7, 9] = 1;
            #endregion
            #region scenario2
            for (int i = 0; i < 20; i++) {
                for (int j = 0; j < 25; j++) {
                    scenario2[i, j] = 0;
                }
            }
            scenario2[1, 1] = 1;
            scenario2[1, 0] = 1;
            scenario2[1, 2] = 1;
            scenario2[1, 3] = 1;
            scenario2[2, 3] = 1;
            scenario2[2, 4] = 1;
            scenario2[2, 6] = 1;
            scenario2[3, 6] = 1;
            scenario2[3, 5] = 1;
            scenario2[3, 4] = 1;
            scenario2[4, 1] = 1;
            scenario2[4, 2] = 1;
            scenario2[4, 3] = 1;
            scenario2[4, 5] = 1;
            scenario2[4, 4] = 1;
            #endregion
            #region scenario3
            for (int i = 0; i < 10; i++) {
                for (int j = 0; j < 10; j++) {
                    scenario3[i, j] = 0;
                }
            }
            scenario3[1, 1] = 1;
            scenario3[1, 0] = 1;
            scenario3[1, 2] = 1;
            scenario3[1, 3] = 1;
            scenario3[2, 3] = 1;
            scenario3[2, 0] = 1;
            scenario3[3, 3] = 1;
            scenario3[3, 2] = 1;
            scenario3[3, 1] = 1;
            #endregion

            #endregion

            while (true) {
                Console.BackgroundColor = ConsoleColor.Black;
                Console.Clear();
                
                Console.BackgroundColor = ConsoleColor.Black;
                Console.SetCursorPosition(0, 0);
                Console.WriteLine("Welcome to Game Of Life");
                Console.WriteLine("Hit any key to start a game.");
                Console.WriteLine("Press Q during game, to quit to this menu.");
                Console.WriteLine("Press Q now, to quit game.");
                Console.WriteLine("Feel free to use different scenarios or change my code.");
                Console.WriteLine("Have fun playing! :)");

                var input = Console.ReadKey();
                if(input.Key == ConsoleKey.Q) {
                    break;
                }

                // Choose scenario (be sure that rowNum and colNum are suitable for scenario)
                // If you uncomment a scenario, comment random generation for cells position
                //cells = scenario1;
                //cells = scenario2;
                //cells = scenario3;

                // Random generation of alive cells
                // 0 to All
                for (int i = 0; i < rowNum; i++) {
                    for (int j = 0; j < colNum; j++) {
                        cells[i, j] = 0;
                    }
                }

                // Generate positions of alive cells
                int aliveAtStart = rowNum * colNum / 10;
                int rowA = 0, colA = 0;
                for (int i = 0; i < aliveAtStart; i++) {
                    rowA = rnd.Next(0, rowNum);
                    colA = rnd.Next(0, colNum);
                    cells[rowA, colA] = 1;
                }


                // Starting map
                for (int a = 0; a < rowNum; a++) {
                    for (int b = 0; b < colNum; b++) {
                        Console.SetCursorPosition(b, a);
                        if (cells[a, b] == 1) {
                            Console.BackgroundColor = ConsoleColor.Green;
                        } else {
                            Console.BackgroundColor = ConsoleColor.Red;
                        }
                        Console.Write(" ");
                    }
                }

                Console.BackgroundColor = ConsoleColor.Black;
                Console.Clear();

                while (!(Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Q)) {
                    // Check for neighbours and optionally revive/kill cells
                    for (int i = 0; i < rowNum; i++) {
                        for (int j = 0; j < colNum; j++) {
                            neighs = 0;
                            neighs = Neighs(i, j, cells, rowNum, colNum);

                            if ((cells[i, j] == 0) && (neighs == 3)) {
                                cells[i, j] = 1;
                            } else if ((cells[i, j] == 1) && ((neighs < 2) || neighs > 3)) {
                                cells[i, j] = 0;
                            }
                        }
                    }

                    // New map
                    for (int a = 0; a < rowNum; a++) {
                        for (int b = 0; b < colNum; b++) {
                            Console.SetCursorPosition(b, a);
                            if (cells[a, b] == 1) {
                                Console.BackgroundColor = ConsoleColor.Green;
                            } else {
                                Console.BackgroundColor = ConsoleColor.Red;
                            }
                            Console.Write(" ");
                        }
                    }

                    // Next round after 250 miliseconds
                    System.Threading.Thread.Sleep(250);

                    // Next round after keypress
                    // Console.ReadKey();
                }
            }
        }

        // Checks for number of neighbours of each cell
        public static int Neighs(int row, int col, int[,] cells, int rowNum, int colNum) {
            int n = 0;
            // Horizontal && Vertical check for neighbours
            if ((row > 0) && (cells[row - 1, col] == 1)) // UP
                n++;
            if ((col < colNum - 1) && (cells[row, col + 1] == 1)) // RIGHT
                n++;
            if ((row < rowNum - 1) && (cells[row + 1, col] == 1)) // DOWN
                n++;
            if ((col > 0) && (cells[row, col - 1] == 1)) // LEFT
                n++;

            //Diagonal check for neighbours
            if ((row > 0 && col > 0) && (cells[row - 1, col - 1] == 1)) // UP LEFT
                n++;
            if ((row > 0 && col < colNum - 1) && (cells[row - 1, col + 1] == 1)) // UP RIGHT
                n++;
            if ((row < rowNum - 1 && col < colNum - 1) && (cells[row + 1, col + 1] == 1)) // DOWN RIGHT
                n++;
            if ((row < rowNum - 1 && col > 0) && (cells[row + 1, col - 1] == 1)) // DOWN LEFT
                n++;

            return n;
        }
    }
}
