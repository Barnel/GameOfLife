using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLifev2 {
    class Program {
        static void Main(string[] args) {
            const int rowNum = 25, colNum = 50;
            Random rnd = new Random();
            int greens = 0;
            int blues = 0;
            int[,] cells = new int[rowNum, colNum];

            while (true) {
                Console.BackgroundColor = ConsoleColor.Black;
                Console.Clear();

                Console.BackgroundColor = ConsoleColor.Black;
                Console.SetCursorPosition(0, 0);
                Console.WriteLine("Welcome to Game Of Life v2");
                Console.WriteLine("Greens and BLues fight to stay alive and kill the enemy.");
                Console.WriteLine("Hit any key to start a game.");
                Console.WriteLine("Press Q during game, to quit to this menu.");
                Console.WriteLine("Press Q now, to quit game.");
                Console.WriteLine("Feel free to change my code.");
                Console.WriteLine("Have fun playing! :)");

                var input = Console.ReadKey();
                if (input.Key == ConsoleKey.Q) {
                    break;
                }

                Console.BackgroundColor = ConsoleColor.Black;
                Console.Clear();

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

                // Greens
                for (int i = 0; i < aliveAtStart; i++) {
                    rowA = rnd.Next(0, rowNum);
                    colA = rnd.Next(0, colNum);
                    cells[rowA, colA] = 1;
                }
                //Blues
                for (int i = 0; i < aliveAtStart; i++) {
                    rowA = rnd.Next(0, rowNum);
                    colA = rnd.Next(0, colNum);
                    cells[rowA, colA] = 2;
                }

                // Starting map
                for (int a = 0; a < rowNum; a++) {
                    for (int b = 0; b < colNum; b++) {
                        Console.SetCursorPosition(b, a);
                        if (cells[a, b] == 1) {
                            Console.BackgroundColor = ConsoleColor.Green;
                        } else if (cells[a, b] == 2) {
                            Console.BackgroundColor = ConsoleColor.Blue;
                        } else {
                            Console.BackgroundColor = ConsoleColor.Red;
                        }
                        Console.Write(" ");
                    }
                }

                Console.BackgroundColor = ConsoleColor.Black;
                Console.SetCursorPosition(0, rowNum + 2);
                Console.WriteLine("This is your map, press any key to start playing.");

                Console.ReadKey();
                Console.SetCursorPosition(0, rowNum + 2);
                Console.WriteLine("                                                  ");

                while (!(Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Q)) {
                    // Check for neighbours and optionally revive/kill cells
                    for (int i = 0; i < rowNum; i++) {
                        for (int j = 0; j < colNum; j++) {
                            greens = 0;
                            greens = Greens(i, j, cells, rowNum, colNum);
                            blues = 0;
                            blues = Blues(i, j, cells, rowNum, colNum);

                            if (cells[i, j] == 0) {
                                if (greens == 3 && blues != 3) {
                                    cells[i, j] = 1;
                                    Console.SetCursorPosition(j, i);
                                    Console.BackgroundColor = ConsoleColor.Green;
                                    Console.Write(" ");
                                } else if (blues == 3 && greens != 3) {
                                    cells[i, j] = 2;
                                    Console.SetCursorPosition(j, i);
                                    Console.BackgroundColor = ConsoleColor.Blue;
                                    Console.Write(" ");
                                }
                            } else if (cells[i, j] == 1) {
                                if (blues == 3) {
                                    cells[i, j] = 2;
                                    Console.SetCursorPosition(j, i);
                                    Console.BackgroundColor = ConsoleColor.Blue;
                                    Console.Write(" ");
                                } else if (greens < 2 || greens > 3) {
                                    cells[i, j] = 0;
                                    Console.SetCursorPosition(j, i);
                                    Console.BackgroundColor = ConsoleColor.Red;
                                    Console.Write(" ");
                                }
                            } else if (cells[i, j] == 2) {
                                if (greens == 3) {
                                    cells[i, j] = 1;
                                    Console.SetCursorPosition(j, i);
                                    Console.BackgroundColor = ConsoleColor.Green;
                                    Console.Write(" ");
                                } else if (blues < 2 || blues > 3) {
                                    cells[i, j] = 0;
                                    Console.SetCursorPosition(j, i);
                                    Console.BackgroundColor = ConsoleColor.Red;
                                    Console.Write(" ");
                                }
                            }
                        }
                    }
                    // Next round after 200 miliseconds
                    System.Threading.Thread.Sleep(100);

                    // Next round after keypress
                    // Console.ReadKey();
                }
            }

        }

        public static int Greens(int row, int col, int[,] cells, int rowNum, int colNum) {
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

        public static int Blues(int row, int col, int[,] cells, int rowNum, int colNum) {
            int n = 0;
            // Horizontal && Vertical check for neighbours
            if ((row > 0) && (cells[row - 1, col] == 2)) // UP
                n++;
            if ((col < colNum - 1) && (cells[row, col + 1] == 2)) // RIGHT
                n++;
            if ((row < rowNum - 1) && (cells[row + 1, col] == 2)) // DOWN
                n++;
            if ((col > 0) && (cells[row, col - 1] == 2)) // LEFT
                n++;

            //Diagonal check for neighbours
            if ((row > 0 && col > 0) && (cells[row - 1, col - 1] == 2)) // UP LEFT
                n++;
            if ((row > 0 && col < colNum - 1) && (cells[row - 1, col + 1] == 2)) // UP RIGHT
                n++;
            if ((row < rowNum - 1 && col < colNum - 1) && (cells[row + 1, col + 1] == 2)) // DOWN RIGHT
                n++;
            if ((row < rowNum - 1 && col > 0) && (cells[row + 1, col - 1] == 2)) // DOWN LEFT
                n++;

            return n;
        }
    }
}
