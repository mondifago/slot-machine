using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Linq;
using Microsoft.VisualBasic.FileIO;

namespace slot_machine;

public class Program
{
    
    
    static int CheckRowWin(char[,] slotMachine)
    {
        int amountWon = 0;
        for (int i = 0; i < slotMachine.GetLength(0); i++)
        {
            if (RowIsUniform(slotMachine, i))
                amountWon += SmConstants.WIN_AMOUNT;
        }
        return amountWon;
    }

    static int CheckColumnWin(char[,] slotMachine)
    {
        int amountWon = 0;
        for (int j = 0; j < slotMachine.GetLength(1); j++)
        {
            if (ColumnIsUniform(slotMachine, j))
                amountWon += SmConstants.WIN_AMOUNT;
        }
        return amountWon;
    }

    static int CheckRowOrDiagonalWin(char[,] slotMachine)
    {
        int amountWon = 0;
        for (int i = 0; i < slotMachine.GetLength(0); i++)
        {
            if (RowIsUniform(slotMachine, i))
                amountWon += SmConstants.WIN_AMOUNT;
        }
        if (CheckDiagonalUniform(slotMachine, true))
            amountWon += SmConstants.WIN_AMOUNT;
        if (CheckDiagonalUniform(slotMachine, false))
            amountWon += SmConstants.WIN_AMOUNT;
        return amountWon;
    }

    static int CheckColumnOrDiagonalWin(char[,] slotMachine)
    {
        int amountWon = 0;
        for (int j = 0; j < slotMachine.GetLength(1); j++)
        {
            if (ColumnIsUniform(slotMachine, j))
                amountWon += SmConstants.WIN_AMOUNT;
        }
        if (CheckDiagonalUniform(slotMachine, true))
            amountWon += SmConstants.WIN_AMOUNT;
        if (CheckDiagonalUniform(slotMachine, false))
            amountWon += SmConstants.WIN_AMOUNT;
        return amountWon;
    }

    static int CheckRowOrColumnDiagonalWin(char[,] slotMachine, bool[] uniformRows, bool[] uniformColumns)
    {
        int amountWon = 0;
        bool jackpotWon = false;

        if (uniformRows.All(x => x) && uniformColumns.All(x => x))
        {
            amountWon = SmConstants.JACKPOT_WIN;
            jackpotWon = true;
        }

        if (!jackpotWon)
        {
            for (int i = 0; i < slotMachine.GetLength(0); i++)
            {
                if (RowIsUniform(slotMachine, i))
                    amountWon += SmConstants.WIN_AMOUNT;
            }
            for (int j = 0; j < slotMachine.GetLength(1); j++)
            {
                if (ColumnIsUniform(slotMachine, j))
                    amountWon += SmConstants.WIN_AMOUNT;
            }
            if (CheckDiagonalUniform(slotMachine, true))
                amountWon += SmConstants.WIN_AMOUNT;
            if (CheckDiagonalUniform(slotMachine, false))
                amountWon += SmConstants.WIN_AMOUNT;
        }

        return amountWon;
    }

    static int CalculateWinnings(int cashDepositSelection, char[,] slotMachine)
    {
        int amountWon = 0;
        bool[] uniformRows = new bool[slotMachine.GetLength(0)]; 
        bool[] uniformColumns = new bool[slotMachine.GetLength(1)]; 
        switch (cashDepositSelection)
        {
            case SmConstants.CHECK_ROW_MODE:
                amountWon = CheckRowWin(slotMachine);
                break;
            case SmConstants.CHECK_COLUMN_MODE:
                amountWon = CheckColumnWin(slotMachine);
                break;
            case SmConstants.CHECK_ROW_AND_DIAGONAL_MODE:
                amountWon = CheckRowOrDiagonalWin(slotMachine);
                break;
            case SmConstants.CHECK_COLUMN_AND_DIAGONAL_MODE:
                amountWon = CheckColumnOrDiagonalWin(slotMachine);
                break;
            case SmConstants.CHECK_ALL_LINE_MODE:
                amountWon = CheckRowOrColumnDiagonalWin(slotMachine, uniformRows, uniformColumns);
                break;
        }
        return amountWon;
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

    static void CashOutPrompt()
    {
        while (true)
        {
            Console.Write("Are you sure you want to CASH OUT?  y/n: ");
            try
            {
                char input = char.ToUpper(Console.ReadKey().KeyChar);
                if (input == 'Y')
                {
                    Environment.Exit(0);
                }
                else if (input == 'N')
                {
                    break;
                }
                else
                {
                    throw new InvalidOperationException("Invalid input. Please enter either 'y' or 'n'.");
                }
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine("\n" + e.Message);
            }
            Console.WriteLine("\n");
        }
    }

    static void Main(string[] args)
    {
        int totalAmountDeposited = 0;
        int totalAmountWon = 0;
        
        List<char> listOfChars = new List<char>() { SmConstants.GRID_ITEM_1, SmConstants.GRID_ITEM_2, SmConstants.GRID_ITEM_3, };
        char[,] slotMachine = new char[SmConstants.GRID_ROW_DIM, SmConstants.GRID_COLUMN_DIM];

        while (true)
        {
            SmUiMethods.DisplayWelcomeMessage();

            int cashDepositSelection = SmUiMethods.PromptUserToSelectGameMode();

            SmUiMethods.PrintDepositBasedOnModeSelected(cashDepositSelection, ref totalAmountDeposited);

            SmUiMethods.PrintSlotMachineWithRandomEntries(slotMachine, listOfChars);

            SmUiMethods.PromptUserToDisplayAmountWon();

            if (cashDepositSelection == SmConstants.CHECK_ROW_MODE)
            {
                bool[] uniformRows = Enumerable.Range(0, slotMachine.GetLength(0))
                                      .Select(i => RowIsUniform(slotMachine, i))
                                      .ToArray();

                PrintOptionOneWithHighlighting(slotMachine, uniformRows);
            }

            if (cashDepositSelection == SmConstants.CHECK_COLUMN_MODE)
            {
                bool[] uniformColumns = Enumerable.Range(0, slotMachine.GetLength(1))
                                           .Select(j => ColumnIsUniform(slotMachine, j))
                                           .ToArray();

                PrintOptionTwoWithHighlighting(slotMachine, uniformColumns);
            }

            if (cashDepositSelection == SmConstants.CHECK_ROW_AND_DIAGONAL_MODE)
            {
                bool[] uniformRows = Enumerable.Range(0, slotMachine.GetLength(0))
                                      .Select(i => RowIsUniform(slotMachine, i))
                                      .ToArray();
                bool isLeftDiagonalUniform = CheckDiagonalUniform(slotMachine, true);
                bool isRightDiagonalUniform = CheckDiagonalUniform(slotMachine, false);

                PrintOptionThreeWithHighlighting(slotMachine, uniformRows, isLeftDiagonalUniform, isRightDiagonalUniform);
            }

            if (cashDepositSelection == SmConstants.CHECK_COLUMN_AND_DIAGONAL_MODE)
            {
                bool[] uniformColumns = Enumerable.Range(0, slotMachine.GetLength(1))
                                           .Select(j => ColumnIsUniform(slotMachine, j))
                                           .ToArray();
                bool isLeftDiagonalUniform = CheckDiagonalUniform(slotMachine, true);
                bool isRightDiagonalUniform = CheckDiagonalUniform(slotMachine, false);

                PrintOptionFourWithHighlighting(slotMachine, uniformColumns, isLeftDiagonalUniform, isRightDiagonalUniform);
            }

            if (cashDepositSelection == SmConstants.CHECK_ALL_LINE_MODE)
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

            int amountWon = CalculateWinnings(cashDepositSelection, slotMachine);
            Console.WriteLine("you won $" + amountWon);
            totalAmountWon += amountWon;
            Console.WriteLine("Total Amount won so far = $" + totalAmountWon);
            Console.WriteLine("\n");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            Console.Clear();
            Console.WriteLine("You have spent a total of $" + totalAmountDeposited + "\t and Won a total of $" + totalAmountWon);
            Console.WriteLine("Press Enter to play again or any other key to CASHOUT.");
            Console.WriteLine("\n");
            if (Console.ReadKey().Key != ConsoleKey.Enter)
            {
                CashOutPrompt();
            }
            Console.WriteLine("\n");
        }
    }

}

