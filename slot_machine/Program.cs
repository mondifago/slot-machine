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
        int amountWon = 0;
        int amountDeposited;

        Console.WriteLine("*********************************|Welcome to Slot Machine|*********************************\n");
        Console.WriteLine($"Option 1: - Pay ${SINGLE_LINE_COST} to play for all rows and win $2 for each line that matches");
        Console.WriteLine($"Option 2: - Pay ${SINGLE_LINE_COST} to play for all columns and win $2 for each line that matches");
        Console.WriteLine($"Option 3: - Pay ${DOUBLE_LINE_COST} to play for all rows and two diagonals and win $2 for each line that matches");
        Console.WriteLine($"Option 4: - Pay ${DOUBLE_LINE_COST} to play for all columns and two diagonals and win $2 for each line that matches");
        Console.WriteLine($"Option 5: - Pay ${ALL_LINE_COST} to play for any line and win ${WIN_AMOUNT} for each line that matches, and $100 Jackpot if all rows and columns matches\n");

        Console.Write("Please choose the option you want to play and press ENTER:\t");
        int cashDepositSelection = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("\n");

        if (cashDepositSelection == 1 || cashDepositSelection==2)
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

        List<char> listOfChars = new List<char>(){'a','b','c',};
        char[,] slotMachine = new char[3,3];

        for (int i = 0; i <3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                Random rng = new Random();
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


            }

            if (cashDepositSelection == 4)
            {


            }

            if (cashDepositSelection == 5)
            {


            }


        



    }
}

