using System;
using System.Collections.Generic;
namespace slot_machine;

class Program
{
    static void Main(string[] args)
    {
        List<char> listOfChars = new List<char>(){'a','b','c',};
        char[,] slotMachine = new char[3,3];

        for (int i = 0; i <3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                Random rng = new Random();
                int randomIndex = rng.Next(0, listOfChars.Count);
                slotMachine[0, 0] = listOfChars[randomIndex];
                slotMachine[0, 1] = listOfChars[randomIndex];
                slotMachine[0, 2] = listOfChars[randomIndex];
                slotMachine[1, 0] = listOfChars[randomIndex];
                slotMachine[1, 1] = listOfChars[randomIndex];
                slotMachine[1, 2] = listOfChars[randomIndex];
                slotMachine[2, 0] = listOfChars[randomIndex];
                slotMachine[2, 1] = listOfChars[randomIndex];
                slotMachine[2, 2] = listOfChars[randomIndex];

                //Console.WriteLine($"Element at position ({i},{j}): {slotMachine[i, j]}");

                Console.Write(slotMachine[i, j] + " ");
            }
            Console.WriteLine();
        }

    }
}

