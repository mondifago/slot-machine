﻿using System;
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

        public static int CalculateAmountWonBasedOnModeSelected(int gameModeSelected, char[,] slotMachine)
        {
            bool[] uniformRows = new bool[slotMachine.GetLength(0)];
            bool[] uniformColumns = new bool[slotMachine.GetLength(1)];
            switch ((GameMode)gameModeSelected)
            {
                case GameMode.mode1:
                    return CheckRowWin(slotMachine);
                case GameMode.mode2:
                    return CheckColumnWin(slotMachine);
                case GameMode.mode3:
                    return CheckRowOrDiagonalWin(slotMachine);
                case GameMode.mode4:
                    return CheckColumnOrDiagonalWin(slotMachine);
                case GameMode.mode5:
                    return CheckRowOrColumnDiagonalWin(slotMachine, uniformRows, uniformColumns);
                default:
                    return 0;
            }
        }

        public static string GetModeDescription(GameMode modeDescription)
        {
            return modeDescription switch
            {
                GameMode.mode1 => "Pay $10 to play for all rows and win $40 for each line that matches",
                GameMode.mode2 => "Pay $10 to play for all columns and win $40 for each line that matches",
                GameMode.mode3 => "Pay $20 to play for all rows and two diagonals and win $40 for each line that matches",
                GameMode.mode4 => "Pay $20 to play for all columns and two diagonals and win $40 for each line that matches",
                GameMode.mode5 => "Pay $50 to play for any line and win $40 for each line that matches, and $200 Jackpot if all rows and columns match",
                _ => throw new ArgumentOutOfRangeException(nameof(modeDescription), modeDescription, null)
            };
        }

        public static int GetDepositAmount(GameMode gameModeCost)
        {
            return gameModeCost switch
            {
                GameMode.mode1 => SmConstants.SINGLE_LINE_COST,
                GameMode.mode2 => SmConstants.SINGLE_LINE_COST,
                GameMode.mode3 => SmConstants.DOUBLE_LINE_COST,
                GameMode.mode4 => SmConstants.DOUBLE_LINE_COST,
                GameMode.mode5 => SmConstants.ALL_LINE_COST,
                _ => 0
            };
        }
    }
}

