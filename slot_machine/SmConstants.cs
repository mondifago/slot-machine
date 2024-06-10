using System;
namespace slot_machine
{
	public static class SmConstants
	{
        public static readonly Random RANDOM = new Random();
        public const char GRID_ITEM_1 = 'a';
        public const char GRID_ITEM_2 = 'b';
        public const char GRID_ITEM_3 = 'c';
        public const int GRID_ROW_DIM = 3;
        public const int GRID_COLUMN_DIM = 3;
        public const int CHECK_ROW_MODE = 1;
        public const int CHECK_COLUMN_MODE = 2;
        public const int CHECK_ROW_AND_DIAGONAL_MODE = 3;
        public const int CHECK_COLUMN_AND_DIAGONAL_MODE = 4;
        public const int CHECK_ALL_LINE_MODE = 5;
        public const int WIN_AMOUNT = 40;
        public const int JACKPOT_WIN = 200;
        public const int SINGLE_LINE_COST = 10;
        public const int DOUBLE_LINE_COST = 20;
        public const int ALL_LINE_COST = 50;
    }
}

