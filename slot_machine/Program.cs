using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Linq;
using Microsoft.VisualBasic.FileIO;

namespace slot_machine;

public class Program
{ 
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

            int amountWon = SmLogic.CalculateAmountWonBasedOnModeSelected(cashDepositSelection, slotMachine);

            SmUiMethods.AddandDisplayTotalAmountDepositedandWon(amountWon, ref totalAmountWon);

            SmUiMethods.PromptUserToCashoutOrContinue(totalAmountDeposited, totalAmountWon);
        }
    }
}

