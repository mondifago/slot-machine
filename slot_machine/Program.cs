using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Linq;
using Microsoft.VisualBasic.FileIO;

namespace slot_machine;

class Program
{
    const int GRID_ROW_DIM = 3;
    const int GRID_COLUMN_DIM = 3;
    const int ZERO_BASED_INDEX = 0;
    const char GRID_ITEM_1 = 'a';
    const char GRID_ITEM_2 = 'b';
    const char GRID_ITEM_3 = 'c';
    const int CHECK_ROW_MODE = 1;
    const int CHECK_COLUMN_MODE = 2;
    const int CHECK_ROW_AND_DIAGONAL_MODE = 3;
    const int CHECK_COLUMN_AND_DIAGONAL_MODE = 4;
    const int CHECK_ALL_LINE_MODE = 5;
    const int WIN_AMOUNT = 40;
    const int JACKPOT_WIN = 200;
    const int SINGLE_LINE_COST = 10;
    const int DOUBLE_LINE_COST = 20;
    const int ALL_LINE_COST = 50;
    
    static int CheckRowWin(char[,] slotMachine)
    {
        int amountWon = 0;
        for (int i = 0; i < slotMachine.GetLength(0); i++)
        {
            if (RowIsUniform(slotMachine, i))
                amountWon += WIN_AMOUNT;
        }
        return amountWon;
    }

    static int CheckColumnWin(char[,] slotMachine)
    {
        int amountWon = 0;
        for (int j = 0; j < slotMachine.GetLength(1); j++)
        {
            if (ColumnIsUniform(slotMachine, j))
                amountWon += WIN_AMOUNT;
        }
        return amountWon;
    }

    static int CheckRowOrDiagonalWin(char[,] slotMachine)
    {
        int amountWon = 0;
        for (int i = 0; i < slotMachine.GetLength(0); i++)
        {
            if (RowIsUniform(slotMachine, i))
                amountWon += WIN_AMOUNT;
        }
        if (CheckDiagonalUniform(slotMachine, true))
            amountWon += WIN_AMOUNT;
        if (CheckDiagonalUniform(slotMachine, false))
            amountWon += WIN_AMOUNT;
        return amountWon;
    }

    static int CheckColumnOrDiagonalWin(char[,] slotMachine)
    {
        int amountWon = 0;
        for (int j = 0; j < slotMachine.GetLength(1); j++)
        {
            if (ColumnIsUniform(slotMachine, j))
                amountWon += WIN_AMOUNT;
        }
        if (CheckDiagonalUniform(slotMachine, true))
            amountWon += WIN_AMOUNT;
        if (CheckDiagonalUniform(slotMachine, false))
            amountWon += WIN_AMOUNT;
        return amountWon;
    }

    static int CheckRowOrColumnDiagonalWin(char[,] slotMachine, bool[] uniformRows, bool[] uniformColumns)
    {
        int amountWon = 0;
        bool jackpotWon = false;

        if (uniformRows.All(x => x) && uniformColumns.All(x => x))
        {
            amountWon = JACKPOT_WIN;
            jackpotWon = true;
        }

        if (!jackpotWon)
        {
            for (int i = 0; i < slotMachine.GetLength(0); i++)
            {
                if (RowIsUniform(slotMachine, i))
                    amountWon += WIN_AMOUNT;
            }
            for (int j = 0; j < slotMachine.GetLength(1); j++)
            {
                if (ColumnIsUniform(slotMachine, j))
                    amountWon += WIN_AMOUNT;
            }
            if (CheckDiagonalUniform(slotMachine, true))
                amountWon += WIN_AMOUNT;
            if (CheckDiagonalUniform(slotMachine, false))
                amountWon += WIN_AMOUNT;
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
            case CHECK_ROW_MODE:
                amountWon = CheckRowWin(slotMachine);
                break;
            case CHECK_COLUMN_MODE:
                amountWon = CheckColumnWin(slotMachine);
                break;
            case CHECK_ROW_AND_DIAGONAL_MODE:
                amountWon = CheckRowOrDiagonalWin(slotMachine);
                break;
            case CHECK_COLUMN_AND_DIAGONAL_MODE:
                amountWon = CheckColumnOrDiagonalWin(slotMachine);
                break;
            case CHECK_ALL_LINE_MODE:
                amountWon = CheckRowOrColumnDiagonalWin(slotMachine, uniformRows, uniformColumns);
                break;
        }
        return amountWon;
    }

        static bool RowIsUniform(char[,] slotMachine, int rowIndex)
        {
            char[] rowElements = Enumerable.Range(ZERO_BASED_INDEX, slotMachine.GetLength(1))
                                           .Select(j => slotMachine[rowIndex, j])
                                           .ToArray();
            return rowElements.Distinct().Count() == 1;
        }

        static bool ColumnIsUniform(char[,] slotMachine, int columnIndex)
        {
            char[] columnElements = Enumerable.Range(ZERO_BASED_INDEX, slotMachine.GetLength(0))
                                              .Select(i => slotMachine[i, columnIndex])
                                              .ToArray();
            return columnElements.Distinct().Count() == 1;
        }

        static bool CheckDiagonalUniform(char[,] slotMachine, bool leftDiagonal)
        {
            char[] diagonalElements = Enumerable.Range(ZERO_BASED_INDEX, slotMachine.GetLength(0))
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

    public static int HandleInvalidEntry(int min = CHECK_ROW_MODE, int max = CHECK_ALL_LINE_MODE)
    {
        while (true)
        {
            
            string input = Console.ReadLine();

            if (int.TryParse(input, out int cashDepositSelection))
            {
                if (cashDepositSelection >= min && cashDepositSelection <= max)
                {
                    return cashDepositSelection;
                }
                else
                {
                    Console.Write($"Error: Please enter a number between {min} and {max}:\t");
                }
            }
            else if (Console.KeyAvailable)
            {
                Console.ReadKey(true); 
                Console.WriteLine("Error: Please enter a valid number:");
            }
            else
            {
                Console.Write("Error: You must enter a number:\t");
            }
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
        string startPromptMessage = "Please choose the Mode you want to play and press ENTER:\t";
        int cashDepositSelection;
        int totalAmountDeposited = 0;
        int totalAmountWon = 0;
        int amountDeposited;

        Random rng = new Random();
        List<char> listOfChars = new List<char>() { GRID_ITEM_1, GRID_ITEM_2, GRID_ITEM_3, };
        char[,] slotMachine = new char[GRID_ROW_DIM, GRID_COLUMN_DIM];

        while (true)
        {
            Console.WriteLine("*********************************|Welcome to Slot Machine|*********************************\n");
            Console.WriteLine($"Mode {CHECK_ROW_MODE}: - Pay ${SINGLE_LINE_COST} to play for all rows and win ${WIN_AMOUNT} for each line that matches");
            Console.WriteLine($"Mode {CHECK_COLUMN_MODE}: - Pay ${SINGLE_LINE_COST} to play for all columns and win ${WIN_AMOUNT} for each line that matches");
            Console.WriteLine($"Mode {CHECK_ROW_AND_DIAGONAL_MODE}: - Pay ${DOUBLE_LINE_COST} to play for all rows and two diagonals and win ${WIN_AMOUNT} for each line that matches");
            Console.WriteLine($"Mode {CHECK_COLUMN_AND_DIAGONAL_MODE}: - Pay ${DOUBLE_LINE_COST} to play for all columns and two diagonals and win ${WIN_AMOUNT} for each line that matches");
            Console.WriteLine($"Mode {CHECK_ALL_LINE_MODE}: - Pay ${ALL_LINE_COST} to play for any line and win ${WIN_AMOUNT} for each line that matches, and ${JACKPOT_WIN} Jackpot if all rows and columns matches\n");

            Console.WriteLine();
            do
            {
                Console.Write(startPromptMessage);
                cashDepositSelection = HandleInvalidEntry(CHECK_ROW_MODE, CHECK_ALL_LINE_MODE);
            } while (cashDepositSelection < CHECK_ROW_MODE || cashDepositSelection > CHECK_ALL_LINE_MODE); 

            Console.WriteLine("\n");

            if (cashDepositSelection == CHECK_ROW_MODE || cashDepositSelection == CHECK_COLUMN_MODE)
            {
                amountDeposited = SINGLE_LINE_COST;
                Console.WriteLine($"\nAmount deposited = ${amountDeposited}\n");
                totalAmountDeposited += amountDeposited;
                Console.WriteLine("Total Amount deposited = $" + totalAmountDeposited);
                Console.WriteLine("\n");
            }
            if (cashDepositSelection == CHECK_ROW_AND_DIAGONAL_MODE || cashDepositSelection == CHECK_COLUMN_AND_DIAGONAL_MODE)
            {
                amountDeposited = DOUBLE_LINE_COST;
                Console.WriteLine($"\nAmount deposited = ${amountDeposited}\n");
                totalAmountDeposited += amountDeposited;
                Console.WriteLine("Total Amount deposited = $" + totalAmountDeposited);
                Console.WriteLine("\n");
            }
            if (cashDepositSelection == CHECK_ALL_LINE_MODE)
            {
                amountDeposited = ALL_LINE_COST;
                Console.WriteLine($"\nAmount deposited = ${amountDeposited}\n");
                totalAmountDeposited += amountDeposited;
                Console.WriteLine("Total Amount deposited = $" + totalAmountDeposited);
                Console.WriteLine("\n");
            }

            for (int i = 0; i < GRID_ROW_DIM; i++)
            {
                for (int j = 0; j < GRID_COLUMN_DIM; j++)
                {
                    int randomIndex = rng.Next(ZERO_BASED_INDEX, listOfChars.Count);
                    slotMachine[i, j] = listOfChars[randomIndex];
                    Console.Write(slotMachine[i, j] + " ");
                }
                Console.WriteLine();
            }
            while (true) 
            {
                Console.WriteLine("Press ENTER to see amount won...");
                Console.WriteLine(); 
                if (Console.ReadKey(true).Key == ConsoleKey.Enter) 
                { 
                    break; 
                }
                else
                {
                    Console.WriteLine("\nInvalid key pressed. Please press Enter to continue.");
                }
            }

            if (cashDepositSelection == CHECK_ROW_MODE)
            {
                bool[] uniformRows = Enumerable.Range(ZERO_BASED_INDEX, slotMachine.GetLength(0))
                                      .Select(i => RowIsUniform(slotMachine, i))
                                      .ToArray();

                PrintOptionOneWithHighlighting(slotMachine, uniformRows);
            }

            if (cashDepositSelection == CHECK_COLUMN_MODE)
            {
                bool[] uniformColumns = Enumerable.Range(ZERO_BASED_INDEX, slotMachine.GetLength(1))
                                           .Select(j => ColumnIsUniform(slotMachine, j))
                                           .ToArray();

                PrintOptionTwoWithHighlighting(slotMachine, uniformColumns);
            }

            if (cashDepositSelection == CHECK_ROW_AND_DIAGONAL_MODE)
            {
                bool[] uniformRows = Enumerable.Range(ZERO_BASED_INDEX, slotMachine.GetLength(0))
                                      .Select(i => RowIsUniform(slotMachine, i))
                                      .ToArray();
                bool isLeftDiagonalUniform = CheckDiagonalUniform(slotMachine, true);
                bool isRightDiagonalUniform = CheckDiagonalUniform(slotMachine, false);

                PrintOptionThreeWithHighlighting(slotMachine, uniformRows, isLeftDiagonalUniform, isRightDiagonalUniform);
            }

            if (cashDepositSelection == CHECK_COLUMN_AND_DIAGONAL_MODE)
            {
                bool[] uniformColumns = Enumerable.Range(ZERO_BASED_INDEX, slotMachine.GetLength(1))
                                           .Select(j => ColumnIsUniform(slotMachine, j))
                                           .ToArray();
                bool isLeftDiagonalUniform = CheckDiagonalUniform(slotMachine, true);
                bool isRightDiagonalUniform = CheckDiagonalUniform(slotMachine, false);

                PrintOptionFourWithHighlighting(slotMachine, uniformColumns, isLeftDiagonalUniform, isRightDiagonalUniform);
            }

            if (cashDepositSelection == CHECK_ALL_LINE_MODE)
            {
                bool[] uniformRows = Enumerable.Range(ZERO_BASED_INDEX, slotMachine.GetLength(0))
                                      .Select(i => RowIsUniform(slotMachine, i))
                                      .ToArray();
                bool[] uniformColumns = Enumerable.Range(ZERO_BASED_INDEX, slotMachine.GetLength(1))
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

