using System;
namespace slot_machine
{
	public static class SmLogic
	{
        public static bool RowIsUniform(char[,] slotMachine, int rowIndex)
        {
            char[] rowElements = Enumerable.Range(0, slotMachine.GetLength(1))
                                           .Select(j => slotMachine[rowIndex, j])
                                           .ToArray();
            return rowElements.Distinct().Count() == 1;
        }

        public static bool ColumnIsUniform(char[,] slotMachine, int columnIndex)
        {
            char[] columnElements = Enumerable.Range(0, slotMachine.GetLength(0))
                                              .Select(i => slotMachine[i, columnIndex])
                                              .ToArray();
            return columnElements.Distinct().Count() == 1;
        }

        public static bool CheckDiagonalUniform(char[,] slotMachine, bool leftDiagonal)
        {
            char[] diagonalElements = Enumerable.Range(0, slotMachine.GetLength(0))
                                                .Select(i => leftDiagonal ? slotMachine[i, i] : slotMachine[i, slotMachine.GetLength(1) - 1 - i])
                                                .ToArray();
            return diagonalElements.Distinct().Count() == 1;
        }

        public static int CheckRowWin(char[,] slotMachine)
        {
            int amountWon = 0;
            for (int i = 0; i < slotMachine.GetLength(0); i++)
            {
                if (RowIsUniform(slotMachine, i))
                    amountWon += SmConstants.WIN_AMOUNT;
            }
            return amountWon;
        }

        public static int CheckColumnWin(char[,] slotMachine)
        {
            int amountWon = 0;
            for (int j = 0; j < slotMachine.GetLength(1); j++)
            {
                if (ColumnIsUniform(slotMachine, j))
                    amountWon += SmConstants.WIN_AMOUNT;
            }
            return amountWon;
        }

        public static int CheckRowOrDiagonalWin(char[,] slotMachine)
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

        public static int CheckColumnOrDiagonalWin(char[,] slotMachine)
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

        public static int CheckRowOrColumnDiagonalWin(char[,] slotMachine, bool[] uniformRows, bool[] uniformColumns)
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

        public static int CalculateAmountWonBasedOnModeSelected(int cashDepositSelection, char[,] slotMachine)
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
    }
}

