using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Linq;

namespace slot_machine;

class Program
{
    static void Main(string[] args)
    {
        const int WIN_AMOUNT = 2;
        const int JACKPOT_WIN = 100;
        const int SINGLE_LINE_COST = 3;
        const int DOUBLE_LINE_COST = 5;
        const int ALL_LINE_COST = 9;
        int totalAmountDeposited=0;
        int totalAmountWon=0;
       
        int amountDeposited;
        

        while (true)
        {
            int amountWon = 0;
            Console.WriteLine("*********************************|Welcome to Slot Machine|*********************************\n");
            Console.WriteLine($"Option 1: - Pay ${SINGLE_LINE_COST} to play for all rows and win ${WIN_AMOUNT} for each line that matches");
            Console.WriteLine($"Option 2: - Pay ${SINGLE_LINE_COST} to play for all columns and win ${WIN_AMOUNT} for each line that matches");
            Console.WriteLine($"Option 3: - Pay ${DOUBLE_LINE_COST} to play for all rows and two diagonals and win ${WIN_AMOUNT} for each line that matches");
            Console.WriteLine($"Option 4: - Pay ${DOUBLE_LINE_COST} to play for all columns and two diagonals and win ${WIN_AMOUNT} for each line that matches");
            Console.WriteLine($"Option 5: - Pay ${ALL_LINE_COST} to play for any line and win ${WIN_AMOUNT} for each line that matches, and ${JACKPOT_WIN} Jackpot if all rows and columns matches\n");

            Console.Write("Please choose the option you want to play and press ENTER:\t");
            int cashDepositSelection = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("\n");

            if (cashDepositSelection == 1 || cashDepositSelection == 2)
            {
                amountDeposited = SINGLE_LINE_COST;
                Console.WriteLine($"\nAmount deposited = ${amountDeposited}\n");
                totalAmountDeposited += amountDeposited;
                Console.WriteLine("Total Amount deposited = $" + totalAmountDeposited);
                Console.WriteLine("\n");
            }
            if (cashDepositSelection == 3 || cashDepositSelection == 4)
            {
                amountDeposited = DOUBLE_LINE_COST;
                Console.WriteLine($"\nAmount deposited = ${amountDeposited}\n");
                totalAmountDeposited += amountDeposited;
                Console.WriteLine("Total Amount deposited = $" + totalAmountDeposited);
                Console.WriteLine("\n");
            }
            if (cashDepositSelection == 5)
            {
                amountDeposited = ALL_LINE_COST;
                Console.WriteLine($"\nAmount deposited = ${amountDeposited}\n");
                totalAmountDeposited += amountDeposited;
                Console.WriteLine("Total Amount deposited = $" + totalAmountDeposited);
                Console.WriteLine("\n");
            }

            Random rng = new Random();
            List<char> listOfChars = new List<char>() { 'a', 'b', 'c', };
            char[,] slotMachine = new char[3, 3];

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    int randomIndex = rng.Next(0, listOfChars.Count);
                    slotMachine[i, j] = listOfChars[randomIndex];
                    //Console.WriteLine($"Element at position ({i},{j}): {slotMachine[i, j]}");
                    Console.Write(slotMachine[i, j] + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine("Press ENTER to see amount won...");
            Console.WriteLine("\n");
            Console.ReadKey();

            if (cashDepositSelection == 1)
            {
                for (int i = 0; i < 3; i++)
                {
                    char firstRowElement = slotMachine[i, 0];
                    bool allSame = true;

                    for (int j = 1; j < 3; j++)
                    {
                        if (!firstRowElement.Equals(slotMachine[i, j]))
                        {
                            allSame = false;
                            break;
                        }
                    }
                    if (allSame)
                    {
                        amountWon += WIN_AMOUNT;
                        for (int j = 0; j < 3; j++)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write(slotMachine[i, j] + " ");
                            Console.ResetColor();
                        }
                        Console.WriteLine();
                    }
                    else
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            Console.Write(slotMachine[i, j] + " ");
                        }
                        Console.WriteLine();
                    }
                }
                Console.WriteLine("you won $" + amountWon);
                totalAmountWon += amountWon;
                Console.WriteLine("Total Amount won so far = $" + totalAmountWon);
            }

            if (cashDepositSelection == 2)
            {
                for (int j = 0; j < 3; j++)
                {
                    char firstColumnElement = slotMachine[0, j];
                    bool allSame = true;

                    for (int i = 1; i < 3; i++)
                    {
                        if (!firstColumnElement.Equals(slotMachine[i, j]))
                        {
                            allSame = false;
                            break;
                        }
                    }

                    if (allSame)
                    {
                        amountWon += WIN_AMOUNT;
                        for (int i = 0; i < 3; i++)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write(slotMachine[i, j] + " ");
                            Console.ResetColor();
                        }
                        Console.WriteLine();
                    }
                    else
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            Console.Write(slotMachine[i, j] + " ");
                        }
                        Console.WriteLine();
                    }
                }
                Console.WriteLine("you won $" + amountWon);
                totalAmountWon += amountWon;
                Console.WriteLine("Total Amount won so far = $" + totalAmountWon);
            }

            if (cashDepositSelection == 3)
            {
                char center = slotMachine[1, 1];
                char upRight = slotMachine[0, 2];
                char downLeft = slotMachine[2, 0];
                char downRight = slotMachine[2, 2];
                char upLeft = slotMachine[0, 0];
                char upMiddle = slotMachine[0, 1];
                char downMiddle = slotMachine[2, 1];
                char leftMiddle = slotMachine[1, 0];
                char rightMiddle = slotMachine[1, 2];

                bool diagonal1Same = upLeft == center && downRight == center;
                bool diagonal2Same = upRight == center && downLeft == center;
                bool row1Same = upLeft == upMiddle && upMiddle == upRight;
                bool row2Same = leftMiddle == center && center == rightMiddle;
                bool row3Same = downLeft == downMiddle && downMiddle == downRight;
                // Print the grid
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        bool highlight = false;
                        // Check if current cell is part of diagonal1 or diagonal2
                        if ((i == j && i == 1) || (i == j && i == 0) || (i == j && i == 2))
                        {
                            if (diagonal1Same)
                            {
                                highlight = true;
                            }
                        }
                        if ((i == j && i == 1) || (i == 0 && j == 2) || (i == 2 && j == 0))
                        {
                            if (diagonal2Same)
                            {
                                highlight = true;
                            }
                        }
                        // Check if current cell is part of row1, row2, or row3
                        if ((i == 0 && row1Same) || (i == 1 && row2Same) || (i == 2 && row3Same))
                        {
                            highlight = true;
                        }
                        if (highlight)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                        }
                        Console.Write(slotMachine[i, j] + " ");
                        Console.ResetColor();
                    }
                    Console.WriteLine();
                }
                if (row1Same) { amountWon += WIN_AMOUNT; }
                if (row2Same) { amountWon += WIN_AMOUNT; }
                if (row3Same) { amountWon += WIN_AMOUNT; }
                if (diagonal1Same) { amountWon += WIN_AMOUNT; }
                if (diagonal2Same) { amountWon += WIN_AMOUNT; }
                Console.WriteLine("you won $" + amountWon);
                totalAmountWon += amountWon;
                Console.WriteLine("Total Amount won so far = $" + totalAmountWon);
            }

            if (cashDepositSelection == 4)
            {
                char center = slotMachine[1, 1];
                char upRight = slotMachine[0, 2];
                char downLeft = slotMachine[2, 0];
                char downRight = slotMachine[2, 2];
                char upLeft = slotMachine[0, 0];
                char upMiddle = slotMachine[0, 1];
                char downMiddle = slotMachine[2, 1];
                char leftMiddle = slotMachine[1, 0];
                char rightMiddle = slotMachine[1, 2];

                bool column1Same = upLeft == leftMiddle && leftMiddle == downLeft;
                bool column2Same = upMiddle == center && center == downMiddle;
                bool column3Same = upRight == rightMiddle && rightMiddle == downRight;
                bool diagonal1Same = upLeft == center && downRight == center;
                bool diagonal2Same = upRight == center && downLeft == center;
                // Print the grid
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        bool highlight = false;
                        // Check if current cell is part of diagonal1 or diagonal2
                        if ((i == j && i == 1) || (i == j && i == 0) || (i == j && i == 2))
                        {
                            if (diagonal1Same)
                            {
                                highlight = true;
                            }
                        }
                        if ((i == j && i == 1) || (i == 0 && j == 2) || (i == 2 && j == 0))
                        {
                            if (diagonal2Same)
                            {
                                highlight = true;
                            }
                        }
                        // Check if current cell is part of column1, column2, or column3
                        if ((j == 0 && column1Same) || (j == 1 && column2Same) || (j == 2 && column3Same))
                        {
                            highlight = true;
                        }
                        if (highlight)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                        }
                        Console.Write(slotMachine[i, j] + " ");
                        Console.ResetColor();
                    }
                    Console.WriteLine();
                }
                if (column1Same) { amountWon += WIN_AMOUNT; }
                if (column2Same) { amountWon += WIN_AMOUNT; }
                if (column3Same) { amountWon += WIN_AMOUNT; }
                if (diagonal1Same) { amountWon += WIN_AMOUNT; }
                if (diagonal2Same) { amountWon += WIN_AMOUNT; }
                Console.WriteLine("you won $" + amountWon);
                totalAmountWon += amountWon;
                Console.WriteLine("Total Amount won so far = $" + totalAmountWon);
            }

            if (cashDepositSelection == 5)
            {
                char center = slotMachine[1, 1];
                char upRight = slotMachine[0, 2];
                char downLeft = slotMachine[2, 0];
                char downRight = slotMachine[2, 2];
                char upLeft = slotMachine[0, 0];
                char upMiddle = slotMachine[0, 1];
                char downMiddle = slotMachine[2, 1];
                char leftMiddle = slotMachine[1, 0];
                char rightMiddle = slotMachine[1, 2];

                bool column1Same = upLeft == leftMiddle && leftMiddle == downLeft;
                bool column2Same = upMiddle == center && center == downMiddle;
                bool column3Same = upRight == rightMiddle && rightMiddle == downRight;
                bool diagonal1Same = upLeft == center && downRight == center;
                bool diagonal2Same = upRight == center && downLeft == center;
                bool row1Same = upLeft == upMiddle && upMiddle == upRight;
                bool row2Same = leftMiddle == center && center == rightMiddle;
                bool row3Same = downLeft == downMiddle && downMiddle == downRight;

                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        bool highlight = false;
                        // Check if current cell is part of diagonal1 or diagonal2
                        if ((i == j && i == 1) || (i == j && i == 0) || (i == j && i == 2))
                        {
                            if (diagonal1Same)
                            {
                                highlight = true;
                            }
                        }
                        if ((i == j && i == 1) || (i == 0 && j == 2) || (i == 2 && j == 0))
                        {
                            if (diagonal2Same)
                            {
                                highlight = true;
                            }
                        }
                        // Check if current cell is part of row1, row2, or row3
                        if ((i == 0 && row1Same) || (i == 1 && row2Same) || (i == 2 && row3Same))
                        {
                            highlight = true;
                        }
                        if (highlight)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                        }
                        // Check if current cell is part of column1, column2, or column3
                        if ((j == 0 && column1Same) || (j == 1 && column2Same) || (j == 2 && column3Same))
                        {
                            highlight = true;
                        }
                        if (highlight)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                        }
                        Console.Write(slotMachine[i, j] + " ");
                        Console.ResetColor();
                    }
                    Console.WriteLine();
                }
                if (row1Same) { amountWon += WIN_AMOUNT; }
                if (row2Same) { amountWon += WIN_AMOUNT; }
                if (row3Same) { amountWon += WIN_AMOUNT; }
                if (column1Same) { amountWon += WIN_AMOUNT; }
                if (column2Same) { amountWon += WIN_AMOUNT; }
                if (column3Same) { amountWon += WIN_AMOUNT; }
                if (diagonal1Same) { amountWon += WIN_AMOUNT; }
                if (diagonal2Same) { amountWon += WIN_AMOUNT; }
                if (row1Same && row2Same && row3Same && column1Same && column2Same && column3Same && diagonal1Same && diagonal2Same)
                {
                    amountWon += JACKPOT_WIN;
                }
                Console.WriteLine("you won $" + amountWon);
                totalAmountWon += amountWon;
                Console.WriteLine("Total Amount won so far = $" + totalAmountWon);
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

    }
}

