using System;
namespace slot_machine
{
	public static class SmUiMethods
	{
        public static readonly Random rng = new Random();

        public static void DisplayWelcomeMessage()
        {
            Console.WriteLine("*********************************|Welcome to Slot Machine|*********************************\n");
            Console.WriteLine($"Mode {SmConstants.CHECK_ROW_MODE}: - Pay ${SmConstants.SINGLE_LINE_COST} to play for all rows and win ${SmConstants.WIN_AMOUNT} for each line that matches");
            Console.WriteLine($"Mode {SmConstants.CHECK_COLUMN_MODE}: - Pay ${SmConstants.SINGLE_LINE_COST} to play for all columns and win ${SmConstants.WIN_AMOUNT} for each line that matches");
            Console.WriteLine($"Mode {SmConstants.CHECK_ROW_AND_DIAGONAL_MODE}: - Pay ${SmConstants.DOUBLE_LINE_COST} to play for all rows and two diagonals and win ${SmConstants.WIN_AMOUNT} for each line that matches");
            Console.WriteLine($"Mode {SmConstants.CHECK_COLUMN_AND_DIAGONAL_MODE}: - Pay ${SmConstants.DOUBLE_LINE_COST} to play for all columns and two diagonals and win ${SmConstants.WIN_AMOUNT} for each line that matches");
            Console.WriteLine($"Mode {SmConstants.CHECK_ALL_LINE_MODE}: - Pay ${SmConstants.ALL_LINE_COST} to play for any line and win ${SmConstants.WIN_AMOUNT} for each line that matches, and ${SmConstants.JACKPOT_WIN} Jackpot if all rows and columns matches\n");

            Console.WriteLine();
        }

        public static int PromptUserToSelectGameMode()
        {
            int gameModeSelected;
            do
            {
                Console.Write("Please choose the Mode you want to play and press ENTER:\t");
                gameModeSelected = HandleInvalidEntry();
            } while (gameModeSelected < SmConstants.CHECK_ROW_MODE || gameModeSelected > SmConstants.CHECK_ALL_LINE_MODE);

            Console.WriteLine("\n");
            return gameModeSelected;
        }

        public static int HandleInvalidEntry()
        {
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out int input))
                {
                    return input;
                }
                Console.Write($"Error: Please enter a number between {SmConstants.CHECK_ROW_MODE} and {SmConstants.CHECK_ALL_LINE_MODE}:\t");
            }
        }

        public static void PrintDepositBasedOnModeSelected(int gameModeSelected, ref int totalAmountDeposited)
        {
            int amountDeposited;

            if (gameModeSelected == SmConstants.CHECK_ROW_MODE || gameModeSelected == SmConstants.CHECK_COLUMN_MODE)
            {
                amountDeposited = SmConstants.SINGLE_LINE_COST;
                Console.WriteLine($"\nAmount deposited = ${amountDeposited}\n");
                totalAmountDeposited += amountDeposited;
                Console.WriteLine("Total Amount deposited = $" + totalAmountDeposited);
                Console.WriteLine("\n");
            }
            if (gameModeSelected == SmConstants.CHECK_ROW_AND_DIAGONAL_MODE || gameModeSelected == SmConstants.CHECK_COLUMN_AND_DIAGONAL_MODE)
            {
                amountDeposited = SmConstants.DOUBLE_LINE_COST;
                Console.WriteLine($"\nAmount deposited = ${amountDeposited}\n");
                totalAmountDeposited += amountDeposited;
                Console.WriteLine("Total Amount deposited = $" + totalAmountDeposited);
                Console.WriteLine("\n");
            }
            if (gameModeSelected == SmConstants.CHECK_ALL_LINE_MODE)
            {
                amountDeposited = SmConstants.ALL_LINE_COST;
                Console.WriteLine($"\nAmount deposited = ${amountDeposited}\n");
                totalAmountDeposited += amountDeposited;
                Console.WriteLine("Total Amount deposited = $" + totalAmountDeposited);
                Console.WriteLine("\n");
            }
        }

        public static void PrintSlotMachineWithRandomEntries(char[,] slotMachine, List<char> listOfChars)
        {
            for (int i = 0; i < SmConstants.GRID_ROW_DIM; i++)
            {
                for (int j = 0; j < SmConstants.GRID_COLUMN_DIM; j++)
                {
                    int randomIndex = rng.Next(listOfChars.Count);
                    slotMachine[i, j] = listOfChars[randomIndex];
                    Console.Write(slotMachine[i, j] + " ");
                }
                Console.WriteLine();
            }
        }

        public static void PromptUserToDisplayAmountWon()
        {
            while (true)
            {
                Console.WriteLine("Press ENTER to see amount won...");
                Console.WriteLine();
                if (Console.ReadKey(true).Key == ConsoleKey.Enter)
                {
                    break;
                }
                Console.WriteLine("\nInvalid key pressed.");
            }
        }

        public static void PrintGridHighlightingUniformLinesBasedOnModeSelected(int gameModeSelected, char[,] slotMachine)
        {
            if (gameModeSelected == SmConstants.CHECK_ROW_MODE)
            {
                bool[] uniformRows = Enumerable.Range(0, slotMachine.GetLength(0))
                                      .Select(i => SmLogic.RowIsUniform(slotMachine, i))
                                      .ToArray();

                PrintOptionOneWithHighlighting(slotMachine, uniformRows);
            }

            if (gameModeSelected == SmConstants.CHECK_COLUMN_MODE)
            {
                bool[] uniformColumns = Enumerable.Range(0, slotMachine.GetLength(1))
                                           .Select(j => SmLogic.ColumnIsUniform(slotMachine, j))
                                           .ToArray();

                PrintOptionTwoWithHighlighting(slotMachine, uniformColumns);
            }

            if (gameModeSelected == SmConstants.CHECK_ROW_AND_DIAGONAL_MODE)
            {
                bool[] uniformRows = Enumerable.Range(0, slotMachine.GetLength(0))
                                      .Select(i => SmLogic.RowIsUniform(slotMachine, i))
                                      .ToArray();
                bool isLeftDiagonalUniform = SmLogic.CheckDiagonalUniform(slotMachine, true);
                bool isRightDiagonalUniform = SmLogic.CheckDiagonalUniform(slotMachine, false);

                PrintOptionThreeWithHighlighting(slotMachine, uniformRows, isLeftDiagonalUniform, isRightDiagonalUniform);
            }

            if (gameModeSelected == SmConstants.CHECK_COLUMN_AND_DIAGONAL_MODE)
            {
                bool[] uniformColumns = Enumerable.Range(0, slotMachine.GetLength(1))
                                           .Select(j => SmLogic.ColumnIsUniform(slotMachine, j))
                                           .ToArray();
                bool isLeftDiagonalUniform = SmLogic.CheckDiagonalUniform(slotMachine, true);
                bool isRightDiagonalUniform = SmLogic.CheckDiagonalUniform(slotMachine, false);

                PrintOptionFourWithHighlighting(slotMachine, uniformColumns, isLeftDiagonalUniform, isRightDiagonalUniform);
            }

            if (gameModeSelected == SmConstants.CHECK_ALL_LINE_MODE)
            {
                bool[] uniformRows = Enumerable.Range(0, slotMachine.GetLength(0))
                                      .Select(i => SmLogic.RowIsUniform(slotMachine, i))
                                      .ToArray();
                bool[] uniformColumns = Enumerable.Range(0, slotMachine.GetLength(1))
                                           .Select(j => SmLogic.ColumnIsUniform(slotMachine, j))
                                           .ToArray();
                bool isLeftDiagonalUniform = SmLogic.CheckDiagonalUniform(slotMachine, true);
                bool isRightDiagonalUniform = SmLogic.CheckDiagonalUniform(slotMachine, false);

                PrintOptionFiveWithHighlighting(slotMachine, uniformRows, uniformColumns, isLeftDiagonalUniform, isRightDiagonalUniform);
            }
        }

        public static void PrintOptionOneWithHighlighting(char[,] slotMachine, bool[] uniformRows)
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

        public static void PrintOptionTwoWithHighlighting(char[,] slotMachine, bool[] uniformColumns)
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

        public static void PrintOptionThreeWithHighlighting(char[,] slotMachine, bool[] uniformRows, bool isLeftDiagonalUniform, bool isRightDiagonalUniform)
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

        public static void PrintOptionFourWithHighlighting(char[,] slotMachine, bool[] uniformColumns, bool isLeftDiagonalUniform, bool isRightDiagonalUniform)
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

        public static void PrintOptionFiveWithHighlighting(char[,] slotMachine, bool[] uniformRows, bool[] uniformColumns, bool isLeftDiagonalUniform, bool isRightDiagonalUniform)
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

        public static void PromptUserToCashoutOrContinue(int totalAmountDeposited, int totalAmountWon)
        {
            Console.WriteLine("You have spent a total of $" + totalAmountDeposited + "\t and Won a total of $" + totalAmountWon);
            Console.WriteLine("Press Enter to play again or any other key to CASHOUT.");
            Console.WriteLine("\n");
            if (Console.ReadKey().Key != ConsoleKey.Enter)
            {
                CashOutPrompt();
            }
            Console.WriteLine("\n");
        }

        public static void CashOutPrompt()
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
                    if (input == 'N')
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

        public static void AddandDisplayTotalAmountDepositedandWon(int amountWon, ref int totalAmountWon)
        {
            Console.WriteLine("you won $" + amountWon);
            totalAmountWon += amountWon;
            Console.WriteLine("Total Amount won so far = $" + totalAmountWon);
            Console.WriteLine("\n");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            Console.Clear();
        }
    }
}

