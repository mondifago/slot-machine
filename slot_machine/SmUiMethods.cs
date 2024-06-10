using System;
namespace slot_machine
{
	public static class SmUiMethods
	{
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
            int cashDepositSelection;
            do
            {
                Console.Write("Please choose the Mode you want to play and press ENTER:\t");
                cashDepositSelection = HandleInvalidEntry();
            } while (cashDepositSelection < SmConstants.CHECK_ROW_MODE || cashDepositSelection > SmConstants.CHECK_ALL_LINE_MODE);

            Console.WriteLine("\n");
            return cashDepositSelection;
        }

        public static int HandleInvalidEntry()
        {
            while (true)
            {

                string input = Console.ReadLine();

                if (int.TryParse(input, out int cashDepositSelection))
                {
                    if (cashDepositSelection >= SmConstants.CHECK_ROW_MODE && cashDepositSelection <= SmConstants.CHECK_ALL_LINE_MODE)
                    {
                        return cashDepositSelection;
                    }
                    else
                    {
                        Console.Write($"Error: Please enter a number between {SmConstants.CHECK_ROW_MODE} and {SmConstants.CHECK_ALL_LINE_MODE}:\t");
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

        public static void PrintDepositBasedOnModeSelected(int cashDepositSelection, ref int totalAmountDeposited)
        {
            int amountDeposited;

            if (cashDepositSelection == SmConstants.CHECK_ROW_MODE || cashDepositSelection == SmConstants.CHECK_COLUMN_MODE)
            {
                amountDeposited = SmConstants.SINGLE_LINE_COST;
                Console.WriteLine($"\nAmount deposited = ${amountDeposited}\n");
                totalAmountDeposited += amountDeposited;
                Console.WriteLine("Total Amount deposited = $" + totalAmountDeposited);
                Console.WriteLine("\n");
            }
            if (cashDepositSelection == SmConstants.CHECK_ROW_AND_DIAGONAL_MODE || cashDepositSelection == SmConstants.CHECK_COLUMN_AND_DIAGONAL_MODE)
            {
                amountDeposited = SmConstants.DOUBLE_LINE_COST;
                Console.WriteLine($"\nAmount deposited = ${amountDeposited}\n");
                totalAmountDeposited += amountDeposited;
                Console.WriteLine("Total Amount deposited = $" + totalAmountDeposited);
                Console.WriteLine("\n");
            }
            if (cashDepositSelection == SmConstants.CHECK_ALL_LINE_MODE)
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
                    int randomIndex = SmConstants.RANDOM.Next(listOfChars.Count);
                    slotMachine[i, j] = listOfChars[randomIndex];
                    Console.Write(slotMachine[i, j] + " ");
                }
                Console.WriteLine();
            }
        }
    }
}

