namespace slot_machine;

public class Program
{
    static void Main(string[] args)
    {
        int totalAmountDeposited = 0;
        int totalAmountWon = 0;
        int playerBalance = SmConstants.STARTING_BALANCE;

        List<char> listOfChars = new List<char>()
        { SmConstants.GRID_ITEM_1,
          SmConstants.GRID_ITEM_2,
          SmConstants.GRID_ITEM_3,
        };

        char[,] slotMachine = new char[SmConstants.GRID_ROW_DIM, SmConstants.GRID_COLUMN_DIM];

        bool continuePlaying = true;
        while (continuePlaying)
        {
            SmUiMethods.DisplayWelcomeMessage(playerBalance);

            int gameModeSelected = SmUiMethods.PromptUserToSelectGameMode(playerBalance);

            if (gameModeSelected == -1)
            {
                SmUiMethods.CashOutPrompt(playerBalance);
                break;
            }

            SmUiMethods.PrintDepositBasedOnModeSelected(gameModeSelected, ref totalAmountDeposited, ref playerBalance);

            SmUiMethods.PrintSlotMachineWithRandomEntries(slotMachine, listOfChars);

            SmUiMethods.PromptUserToDisplayAmountWon();

            SmUiMethods.PrintGridHighlightingUniformLinesBasedOnModeSelected(gameModeSelected, slotMachine);

            int amountWon = SmLogic.CalculateAmountWonBasedOnModeSelected(gameModeSelected, slotMachine);

            SmUiMethods.AddandDisplayTotalAmountDepositedandWon(amountWon, ref totalAmountWon, ref playerBalance);

            continuePlaying = SmUiMethods.PromptUserToCashoutOrContinue(totalAmountDeposited, totalAmountWon, playerBalance);
        }
    }
}

