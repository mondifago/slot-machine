using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Linq;

namespace slot_machine;

class Program
{
    static void Main(string[] args)
    {

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
        int totalAmountDeposited = 0;
        int totalAmountWon = 0;
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

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
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
                bool[] uniformRows = Enumerable.Range(0, slotMachine.GetLength(0))
                                      .Select(i => RowIsUniform(slotMachine, i))
                                      .ToArray();

                PrintOptionOneWithHighlighting(slotMachine, uniformRows);
            }

            if (cashDepositSelection == SECOND_OPTION)
            {
                bool[] uniformColumns = Enumerable.Range(0, slotMachine.GetLength(1))
                                           .Select(j => ColumnIsUniform(slotMachine, j))
                                           .ToArray();

                PrintOptionTwoWithHighlighting(slotMachine, uniformColumns);
            }

            if (cashDepositSelection == THIRD_OPTION)
            {
                bool[] uniformRows = Enumerable.Range(0, slotMachine.GetLength(0))
                                      .Select(i => RowIsUniform(slotMachine, i))
                                      .ToArray();
                bool isLeftDiagonalUniform = CheckDiagonalUniform(slotMachine, true);
                bool isRightDiagonalUniform = CheckDiagonalUniform(slotMachine, false);

                PrintOptionThreeWithHighlighting(slotMachine, uniformRows, isLeftDiagonalUniform, isRightDiagonalUniform);
            }

            if (cashDepositSelection == FOURTH_OPTION)
            {
                bool[] uniformColumns = Enumerable.Range(0, slotMachine.GetLength(1))
                                           .Select(j => ColumnIsUniform(slotMachine, j))
                                           .ToArray();
                bool isLeftDiagonalUniform = CheckDiagonalUniform(slotMachine, true);
                bool isRightDiagonalUniform = CheckDiagonalUniform(slotMachine, false);

                PrintOptionFourWithHighlighting(slotMachine, uniformColumns, isLeftDiagonalUniform, isRightDiagonalUniform);
            }

            if (cashDepositSelection == FIFTH_OPTION)
            {
                bool[] uniformRows = Enumerable.Range(0, slotMachine.GetLength(0))
                                      .Select(i => RowIsUniform(slotMachine, i))
                                      .ToArray();
                bool[] uniformColumns = Enumerable.Range(0, slotMachine.GetLength(1))
                                           .Select(j => ColumnIsUniform(slotMachine, j))
                                           .ToArray();
                bool isLeftDiagonalUniform = CheckDiagonalUniform(slotMachine, true);
                bool isRightDiagonalUniform = CheckDiagonalUniform(slotMachine, false);

                PrintOptionFiveWithHighlighting(slotMachine, uniformRows, uniformColumns, isLeftDiagonalUniform, isRightDiagonalUniform);
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

        static bool RowIsUniform(char[,] slotMachine, int rowIndex)
        {
            char[] rowElements = Enumerable.Range(0, slotMachine.GetLength(1))
                                           .Select(j => slotMachine[rowIndex, j])
                                           .ToArray();
            return rowElements.Distinct().Count() == 1;
        }

        static bool ColumnIsUniform(char[,] slotMachine, int columnIndex)
        {
            char[] columnElements = Enumerable.Range(0, slotMachine.GetLength(0))
                                              .Select(i => slotMachine[i, columnIndex])
                                              .ToArray();
            return columnElements.Distinct().Count() == 1;
        }

        static bool CheckDiagonalUniform(char[,] slotMachine, bool leftDiagonal)
        {
            char[] diagonalElements = Enumerable.Range(0, slotMachine.GetLength(0))
                                                .Select(i => leftDiagonal ? slotMachine[i, i] : slotMachine[i, slotMachine.GetLength(1) - 1 - i])
                                                .ToArray();
            return diagonalElements.Distinct().Count() == 1;
        }

        static void PrintOptionFiveWithHighlighting(char[,] slotMachine, bool[] uniformRows, bool[] uniformColumns, bool isLeftDiagonalUniform, bool isRightDiagonalUniform)
        {
            for (int i = 0; i < slotMachine.GetLength(0); i++)
            {
                for (int j = 0; j < slotMachine.GetLength(1); j++)
                { 
                    bool isRowUniform = uniformRows[i];
                    bool isColumnUniform = uniformColumns[j];
                    bool isDiagonal = (i == j && isLeftDiagonalUniform) || (i == slotMachine.GetLength(1) - 1 - j && isRightDiagonalUniform);
                    if (isRowUniform || isColumnUniform || isDiagonal)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    Console.Write(slotMachine[i, j] + " ");
                    Console.ResetColor();
                }
                Console.WriteLine();
            }
        }

        static void PrintOptionFourWithHighlighting(char[,] slotMachine, bool[] uniformColumns, bool isLeftDiagonalUniform, bool isRightDiagonalUniform)
        {
            for (int i = 0; i < slotMachine.GetLength(0); i++)
            {
                for (int j = 0; j < slotMachine.GetLength(1); j++)
                {
                    bool isDiagonal = (i == j && isLeftDiagonalUniform) || (i == slotMachine.GetLength(1) - 1 - j && isRightDiagonalUniform);
                    if (uniformColumns[j] || isDiagonal)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    Console.Write(slotMachine[i, j] + " ");
                    Console.ResetColor();
                }
                Console.WriteLine();
            }
        }

        static void PrintOptionThreeWithHighlighting(char[,] slotMachine, bool[] uniformRows, bool isLeftDiagonalUniform, bool isRightDiagonalUniform)
        {
            for (int i = 0; i < slotMachine.GetLength(0); i++)
            {
                for (int j = 0; j < slotMachine.GetLength(1); j++)
                {
                    bool isDiagonal = (i == j && isLeftDiagonalUniform) || (i == slotMachine.GetLength(1) - 1 - j && isRightDiagonalUniform);
                    if (uniformRows[i] || isDiagonal)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    Console.Write(slotMachine[i, j] + " ");
                    Console.ResetColor();
                }
                Console.WriteLine();
            }
        }

        static void PrintOptionTwoWithHighlighting(char[,] slotMachine, bool[] uniformColumns)
        {
            for (int i = 0; i < slotMachine.GetLength(0); i++)
            {
                for (int j = 0; j < slotMachine.GetLength(1); j++)
                {
                    if (uniformColumns[j])
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    Console.Write(slotMachine[i, j] + " ");
                    Console.ResetColor();
                }
                Console.WriteLine();
            }
        }

        static void PrintOptionOneWithHighlighting(char[,] slotMachine, bool[] uniformRows)
        {
            for (int i = 0; i < slotMachine.GetLength(0); i++)
            {
                for (int j = 0; j < slotMachine.GetLength(1); j++)
                {
                    if (uniformRows[i])
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    Console.Write(slotMachine[i, j] + " ");
                    Console.ResetColor();
                }
                Console.WriteLine();
            }
        }
    }
       
}

