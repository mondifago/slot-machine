using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Linq;

namespace slot_machine;

class Program
{
    static void Main(string[] args)
    {
        const int FIRST_ROW_INDEX = 0;
        const int SECOND_ROW_INDEX = 1;
        const int THIRD_ROW_INDEX = 2;
        const int FOUTH_ROW_INDEX = 3;
        const int FIRST_COLUMN_INDEX = 0;
        const int SECOND_COLUMN_INDEX = 1;
        const int THIRD_COLUMN_INDEX = 2;
        const int FOUTH_COLUMN_INDEX = 3;
        const int GRID_ROW_DIM = 3;
        const int GRID_COLUMN_DIM = 3;
        const int LIST_START_INDEX = 0;
        const char LIST_ITEM_1 = 'a';
        const char LIST_ITEM_2 = 'b';
        const char LIST_ITEM_3 = 'c';
        const int FIRST_OPTION = 1;
        const int SECOND_OPTION = 2;
        const int THIRD_OPTION = 3;
        const int FOURTH_OPTION = 4;
        const int FIFTH_OPTION = 5;
        const int WIN_AMOUNT = 40;
        const int JACKPOT_WIN = 200;
        const int SINGLE_LINE_COST = 10;
        const int DOUBLE_LINE_COST = 20;
        const int ALL_LINE_COST = 50;
        int totalAmountDeposited=0;
        int totalAmountWon=0;
        int amountDeposited;
        Random rng = new Random();
        List<char> listOfChars = new List<char>() { LIST_ITEM_1, LIST_ITEM_2, LIST_ITEM_3, };
        char[,] slotMachine = new char[GRID_ROW_DIM, GRID_COLUMN_DIM];

        while (true)
        {
            int amountWon = 0;
            Console.WriteLine("*********************************|Welcome to Slot Machine|*********************************\n");
            Console.WriteLine($"Option {FIRST_OPTION}: - Pay ${SINGLE_LINE_COST} to play for all rows and win ${WIN_AMOUNT} for each line that matches");
            Console.WriteLine($"Option {SECOND_OPTION}: - Pay ${SINGLE_LINE_COST} to play for all columns and win ${WIN_AMOUNT} for each line that matches");
            Console.WriteLine($"Option {THIRD_OPTION}: - Pay ${DOUBLE_LINE_COST} to play for all rows and two diagonals and win ${WIN_AMOUNT} for each line that matches");
            Console.WriteLine($"Option {FOURTH_OPTION}: - Pay ${DOUBLE_LINE_COST} to play for all columns and two diagonals and win ${WIN_AMOUNT} for each line that matches");
            Console.WriteLine($"Option {FIFTH_OPTION}: - Pay ${ALL_LINE_COST} to play for any line and win ${WIN_AMOUNT} for each line that matches, and ${JACKPOT_WIN} Jackpot if all rows and columns matches\n");

            Console.Write("Please choose the option you want to play and press ENTER:\t");
            int cashDepositSelection = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("\n");

            if (cashDepositSelection == FIRST_OPTION || cashDepositSelection == SECOND_OPTION)
            {
                amountDeposited = SINGLE_LINE_COST;
                Console.WriteLine($"\nAmount deposited = ${amountDeposited}\n");
                totalAmountDeposited += amountDeposited;
                Console.WriteLine("Total Amount deposited = $" + totalAmountDeposited);
                Console.WriteLine("\n");
            }
            if (cashDepositSelection == THIRD_OPTION || cashDepositSelection == FOURTH_OPTION)
            {
                amountDeposited = DOUBLE_LINE_COST;
                Console.WriteLine($"\nAmount deposited = ${amountDeposited}\n");
                totalAmountDeposited += amountDeposited;
                Console.WriteLine("Total Amount deposited = $" + totalAmountDeposited);
                Console.WriteLine("\n");
            }
            if (cashDepositSelection == FIFTH_OPTION)
            {
                amountDeposited = ALL_LINE_COST;
                Console.WriteLine($"\nAmount deposited = ${amountDeposited}\n");
                totalAmountDeposited += amountDeposited;
                Console.WriteLine("Total Amount deposited = $" + totalAmountDeposited);
                Console.WriteLine("\n");
            }

            for (int i = FIRST_ROW_INDEX; i < FOUTH_ROW_INDEX; i++)
            {
                for (int j = FIRST_COLUMN_INDEX; j < FOUTH_COLUMN_INDEX; j++)
                {
                    int randomIndex = rng.Next(LIST_START_INDEX, listOfChars.Count);
                    slotMachine[i, j] = listOfChars[randomIndex];
                    //Console.WriteLine($"Element at position ({i},{j}): {slotMachine[i, j]}");
                    Console.Write(slotMachine[i, j] + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine("Press ENTER to see amount won...");
            Console.WriteLine("\n");
            Console.ReadKey();

            if (cashDepositSelection == FIRST_OPTION)
            {
                for (int i = 0; i < slotMachine.GetLength(0); i++)
                {
                    if (RowIsUniform(slotMachine, i))
                    {
                        amountWon += WIN_AMOUNT;
                        PrintRowInGreen(slotMachine, i);
                    }
                    else
                    {
                        PrintRow(slotMachine, i);
                    }
                }
                Console.WriteLine("you won $" + amountWon);
                totalAmountWon += amountWon;
                Console.WriteLine("Total Amount won so far = $" + totalAmountWon);
            }

            static bool RowIsUniform(char[,] slotMachine, int rowIndex)
            {
                char[] rowElements = new char[slotMachine.GetLength(1)]; // Number of columns
                for (int j = 0; j < slotMachine.GetLength(1); j++)
                {
                    rowElements[j] = slotMachine[rowIndex, j];
                }
                return rowElements.Distinct().Count() == 1;
            }

            static void PrintRow(char[,] slotMachine, int rowIndex)
            {
                for (int j = 0; j < slotMachine.GetLength(1); j++)
                {
                    Console.Write(slotMachine[rowIndex, j] + " ");
                }
                Console.WriteLine();
            }

            static void PrintRowInGreen(char[,] slotMachine, int rowIndex)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                PrintRow(slotMachine, rowIndex);
                Console.ResetColor();
            }

            if (cashDepositSelection == SECOND_OPTION)
            {
                bool[] uniformColumns = new bool[slotMachine.GetLength(1)];
                for (int j = 0; j < slotMachine.GetLength(1); j++)
                {
                    uniformColumns[j] = ColumnIsUniform(slotMachine, j);
                }

                for (int i = 0; i < slotMachine.GetLength(0); i++)
                if (ColumnIsUniform(slotMachine, i))
                {
                    amountWon += WIN_AMOUNT;
                }
                // Print the grid, highlighting uniform columns.
                PrintGridWithHighlighting(slotMachine, uniformColumns);
                Console.WriteLine("you won $" + amountWon);
                totalAmountWon += amountWon;
                Console.WriteLine("Total Amount won so far = $" + totalAmountWon);
            }
                
                static bool ColumnIsUniform(char[,] slotMachine, int columnIndex)
                {
                    char[] columnElements = new char[slotMachine.GetLength(0)]; // Number of rows
                    for (int i = 0; i < slotMachine.GetLength(0); i++)
                    {
                        columnElements[i] = slotMachine[i, columnIndex];
                    }
                    return columnElements.Distinct().Count() == 1;
                }

                static void PrintGridWithHighlighting(char[,] grid, bool[] uniformColumns)
                {
                    for (int i = 0; i < grid.GetLength(0); i++)
                    {
                        for (int j = 0; j < grid.GetLength(1); j++)
                        {
                        // Highlight the column if it has uniform elements.
                            if (uniformColumns[j])
                            {
                            Console.ForegroundColor = ConsoleColor.Green;
                            }
                            Console.Write(grid[i, j] + " ");
                            Console.ResetColor();
                        }
                        Console.WriteLine();
                    }
                }

            if (cashDepositSelection == THIRD_OPTION)
            {
                char center = slotMachine[SECOND_ROW_INDEX, SECOND_COLUMN_INDEX];
                char upRight = slotMachine[FIRST_ROW_INDEX, THIRD_COLUMN_INDEX];
                char downLeft = slotMachine[THIRD_ROW_INDEX, FIRST_COLUMN_INDEX];
                char downRight = slotMachine[THIRD_ROW_INDEX, THIRD_COLUMN_INDEX];
                char upLeft = slotMachine[FIRST_ROW_INDEX, FIRST_COLUMN_INDEX];
                char upMiddle = slotMachine[FIRST_ROW_INDEX, SECOND_COLUMN_INDEX];
                char downMiddle = slotMachine[THIRD_ROW_INDEX, SECOND_COLUMN_INDEX];
                char leftMiddle = slotMachine[SECOND_ROW_INDEX, FIRST_COLUMN_INDEX];
                char rightMiddle = slotMachine[SECOND_ROW_INDEX, THIRD_COLUMN_INDEX];

                bool diagonal1Same = upLeft == center && downRight == center;
                bool diagonal2Same = upRight == center && downLeft == center;
                bool row1Same = upLeft == upMiddle && upMiddle == upRight;
                bool row2Same = leftMiddle == center && center == rightMiddle;
                bool row3Same = downLeft == downMiddle && downMiddle == downRight;
                // Print the grid
                for (int i = FIRST_ROW_INDEX; i < FOUTH_ROW_INDEX; i++)
                {
                    for (int j = FIRST_COLUMN_INDEX; j < FOUTH_COLUMN_INDEX; j++)
                    {
                        bool highlight = false;
                        // Check if current cell is part of diagonal1 or diagonal2
                        if ((i == j && i == SECOND_ROW_INDEX) || (i == j && i == FIRST_ROW_INDEX) || (i == j && i == THIRD_ROW_INDEX))
                        {
                            if (diagonal1Same)
                            {
                                highlight = true;
                            }
                        }
                        if ((i == j && i == SECOND_ROW_INDEX) || (i == FIRST_ROW_INDEX && j == THIRD_COLUMN_INDEX) || (i == THIRD_ROW_INDEX && j == FIRST_COLUMN_INDEX))
                        {
                            if (diagonal2Same)
                            {
                                highlight = true;
                            }
                        }
                        // Check if current cell is part of row1, row2, or row3
                        if ((i == FIRST_ROW_INDEX && row1Same) || (i == SECOND_ROW_INDEX && row2Same) || (i == THIRD_ROW_INDEX && row3Same))
                        {
                            highlight = true;
                        }
                        if (highlight)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                        }
                        Console.Write(slotMachine[i, j] + " ");
                        Console.ResetColor();
                    }
                    Console.WriteLine();
                }
                if (row1Same) { amountWon += WIN_AMOUNT; }
                if (row2Same) { amountWon += WIN_AMOUNT; }
                if (row3Same) { amountWon += WIN_AMOUNT; }
                if (diagonal1Same) { amountWon += WIN_AMOUNT; }
                if (diagonal2Same) { amountWon += WIN_AMOUNT; }
                Console.WriteLine("you won $" + amountWon);
                totalAmountWon += amountWon;
                Console.WriteLine("Total Amount won so far = $" + totalAmountWon);
            }

            if (cashDepositSelection == FOURTH_OPTION)
            {
                char center = slotMachine[SECOND_ROW_INDEX, SECOND_COLUMN_INDEX];
                char upRight = slotMachine[FIRST_ROW_INDEX, THIRD_COLUMN_INDEX];
                char downLeft = slotMachine[THIRD_ROW_INDEX, FIRST_COLUMN_INDEX];
                char downRight = slotMachine[THIRD_ROW_INDEX, THIRD_COLUMN_INDEX];
                char upLeft = slotMachine[FIRST_ROW_INDEX, FIRST_COLUMN_INDEX];
                char upMiddle = slotMachine[FIRST_ROW_INDEX, SECOND_COLUMN_INDEX];
                char downMiddle = slotMachine[THIRD_ROW_INDEX, SECOND_COLUMN_INDEX];
                char leftMiddle = slotMachine[SECOND_ROW_INDEX, FIRST_COLUMN_INDEX];
                char rightMiddle = slotMachine[SECOND_ROW_INDEX, THIRD_COLUMN_INDEX];

                bool column1Same = upLeft == leftMiddle && leftMiddle == downLeft;
                bool column2Same = upMiddle == center && center == downMiddle;
                bool column3Same = upRight == rightMiddle && rightMiddle == downRight;
                bool diagonal1Same = upLeft == center && downRight == center;
                bool diagonal2Same = upRight == center && downLeft == center;
                
                for (int i = FIRST_ROW_INDEX; i < FOUTH_ROW_INDEX; i++)
                {
                    for (int j = FIRST_COLUMN_INDEX; j < FOUTH_COLUMN_INDEX; j++)
                    {
                        bool highlight = false;
                        if ((i == j && i == SECOND_ROW_INDEX) || (i == j && i == FIRST_ROW_INDEX) || (i == j && i == THIRD_ROW_INDEX))
                        {
                            if (diagonal1Same)
                            {
                                highlight = true;
                            }
                        }
                        if ((i == j && i == SECOND_ROW_INDEX) || (i == FIRST_ROW_INDEX && j == THIRD_COLUMN_INDEX) || (i == THIRD_ROW_INDEX && j == FIRST_COLUMN_INDEX))
                        {
                            if (diagonal2Same)
                            {
                                highlight = true;
                            }
                        }
                        // Check if current cell is part of column1, column2, or column3
                        if ((j == FIRST_COLUMN_INDEX && column1Same) || (j == SECOND_COLUMN_INDEX && column2Same) || (j == THIRD_COLUMN_INDEX && column3Same))
                        {
                            highlight = true;
                        }
                        if (highlight)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                        }
                        Console.Write(slotMachine[i, j] + " ");
                        Console.ResetColor();
                    }
                    Console.WriteLine();
                }
                if (column1Same) { amountWon += WIN_AMOUNT; }
                if (column2Same) { amountWon += WIN_AMOUNT; }
                if (column3Same) { amountWon += WIN_AMOUNT; }
                if (diagonal1Same) { amountWon += WIN_AMOUNT; }
                if (diagonal2Same) { amountWon += WIN_AMOUNT; }
                Console.WriteLine("you won $" + amountWon);
                totalAmountWon += amountWon;
                Console.WriteLine("Total Amount won so far = $" + totalAmountWon);
            }

            if (cashDepositSelection == FIFTH_OPTION)
            {
                char center = slotMachine[SECOND_ROW_INDEX, SECOND_COLUMN_INDEX];
                char upRight = slotMachine[FIRST_ROW_INDEX, THIRD_COLUMN_INDEX];
                char downLeft = slotMachine[THIRD_ROW_INDEX, FIRST_COLUMN_INDEX];
                char downRight = slotMachine[THIRD_ROW_INDEX, THIRD_COLUMN_INDEX];
                char upLeft = slotMachine[FIRST_ROW_INDEX, FIRST_COLUMN_INDEX];
                char upMiddle = slotMachine[FIRST_ROW_INDEX, SECOND_COLUMN_INDEX];
                char downMiddle = slotMachine[THIRD_ROW_INDEX, SECOND_COLUMN_INDEX];
                char leftMiddle = slotMachine[SECOND_ROW_INDEX, FIRST_COLUMN_INDEX];
                char rightMiddle = slotMachine[SECOND_ROW_INDEX, THIRD_COLUMN_INDEX];

                bool column1Same = upLeft == leftMiddle && leftMiddle == downLeft;
                bool column2Same = upMiddle == center && center == downMiddle;
                bool column3Same = upRight == rightMiddle && rightMiddle == downRight;
                bool diagonal1Same = upLeft == center && downRight == center;
                bool diagonal2Same = upRight == center && downLeft == center;
                bool row1Same = upLeft == upMiddle && upMiddle == upRight;
                bool row2Same = leftMiddle == center && center == rightMiddle;
                bool row3Same = downLeft == downMiddle && downMiddle == downRight;

                for (int i = FIRST_ROW_INDEX; i < FOUTH_ROW_INDEX; i++)
                {
                    for (int j = FIRST_COLUMN_INDEX; j < FOUTH_COLUMN_INDEX; j++)
                    {
                        bool highlight = false;
                        if ((i == j && i == SECOND_ROW_INDEX) || (i == j && i == FIRST_ROW_INDEX) || (i == j && i == THIRD_ROW_INDEX))
                        {
                            if (diagonal1Same)
                            {
                                highlight = true;
                            }
                        }
                        if ((i == j && i == SECOND_ROW_INDEX) || (i == FIRST_ROW_INDEX && j == THIRD_COLUMN_INDEX) || (i == THIRD_ROW_INDEX && j == FIRST_COLUMN_INDEX))
                        {
                            if (diagonal2Same)
                            {
                                highlight = true;
                            }
                        }
                        if ((i == FIRST_ROW_INDEX && row1Same) || (i == SECOND_ROW_INDEX && row2Same) || (i == THIRD_ROW_INDEX && row3Same))
                        {
                            highlight = true;
                        }
                        if (highlight)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                        }
                        if ((j == FIRST_COLUMN_INDEX && column1Same) || (j == SECOND_COLUMN_INDEX && column2Same) || (j == THIRD_COLUMN_INDEX && column3Same))
                        {
                            highlight = true;
                        }
                        if (highlight)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                        }
                        Console.Write(slotMachine[i, j] + " ");
                        Console.ResetColor();
                    }
                    Console.WriteLine();
                }
                if (row1Same) { amountWon += WIN_AMOUNT; }
                if (row2Same) { amountWon += WIN_AMOUNT; }
                if (row3Same) { amountWon += WIN_AMOUNT; }
                if (column1Same) { amountWon += WIN_AMOUNT; }
                if (column2Same) { amountWon += WIN_AMOUNT; }
                if (column3Same) { amountWon += WIN_AMOUNT; }
                if (diagonal1Same) { amountWon += WIN_AMOUNT; }
                if (diagonal2Same) { amountWon += WIN_AMOUNT; }
                if (row1Same && row2Same && row3Same && column1Same && column2Same && column3Same && diagonal1Same && diagonal2Same)
                {
                    amountWon += JACKPOT_WIN;
                }
                Console.WriteLine("you won $" + amountWon);
                totalAmountWon += amountWon;
                Console.WriteLine("Total Amount won so far = $" + totalAmountWon);
            }
            Console.WriteLine("\n");
            Console.WriteLine("Press any to continue...");
            Console.ReadKey();
            Console.Clear();
            Console.WriteLine("You have spent a total of $" + totalAmountDeposited + "\t and Won a total of $" + totalAmountWon);
            Console.WriteLine("Press Enter to play again or any other key to CASHOUT.");
            if (Console.ReadKey().Key != ConsoleKey.Enter)
            {
                break;
            }
            Console.WriteLine("\n");
        }

    }
}

