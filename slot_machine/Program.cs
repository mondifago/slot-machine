using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Linq;
using Microsoft.VisualBasic.FileIO;

namespace slot_machine;

public class Program
{ 
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

            SmUiMethods.PrintGridHighlightingUniformLinesBasedOnModeSelected(cashDepositSelection, slotMachine);

            int amountWon = SmUiMethods.CalculateWinnings(cashDepositSelection, slotMachine);

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

