using System;

namespace TicTacToe
{
    internal class Program
    {
        static void Main(string[] args)
        {
            HowToPlay();
            char[,] matrix = CreateMatrix();
            bool isGameOver = false;    
            int availablePositions = 9;
            bool isPlayersTurn = false;
            char playerSymbol = ' ';
            char aiSymbol = ' ';

            while (true)
            {
                try
                {
                    Console.Write("Do you want to play with [X] or [O]: ");
                    char choice = Convert.ToChar(Console.ReadLine().ToUpper());
                    if (choice == 'X')
                    {
                        PrintMatrixCurrentState(matrix);
                        isPlayersTurn = true;
                        playerSymbol = 'X';
                        aiSymbol = 'O';
                        break;
                    }
                    else if (choice == 'O')
                    {
                        isPlayersTurn = false;
                        playerSymbol = 'O';
                        aiSymbol = 'X';
                        break;
                    }
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Invalid Input. Please enter [X] or [O].");
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                catch (Exception)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Invalid Input. Please enter [X] or [O].");
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
            }

            while (!isGameOver)
            {
                if (isPlayersTurn && availablePositions > 0)
                {
                    int position = 0;
                    while (true)
                    {
                        try
                        {
                            Console.WriteLine("Choose a position between (1 - 9)");
                            Console.Write("Position: ");
                            position = Convert.ToInt32(Console.ReadLine());
                            if (position >= 1 && position <= 9)
                            {
                                break;
                            }
                            else
                            {
                                position = 0;
                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                Console.WriteLine("Invalid Input or Position. Try Again.");
                                Console.ForegroundColor = ConsoleColor.Gray;
                            }
                        }
                        catch (Exception)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine("Invalid Input or Position. Try Again.");
                            Console.ForegroundColor = ConsoleColor.Gray;
                        }
                    }
                    
                    int row = RowPosition(position);
                    int col = ColPosition(position);
                    bool isPlaceBusy = true;
                    if (matrix[row, col] == ' ')
                    {
                        matrix[row, col] = playerSymbol;
                        PrintMatrixCurrentState(matrix);
                        availablePositions--;
                    }
                    else
                    {
                        while (isPlaceBusy)
                        {
                            try
                            {
                                if (position >= 1 && position <= 9)
                                {
                                    Console.WriteLine("This place is busy. Try another one.");
                                    Console.Write("Position: ");
                                }
                                else
                                {
                                    Console.Write("Position: ");
                                }
                                
                                position = Convert.ToInt32(Console.ReadLine());
                                row = RowPosition(position);
                                col = ColPosition(position);

                                if (matrix[row, col] == ' ')
                                {
                                    isPlaceBusy = false;
                                    matrix[row, col] = playerSymbol;
                                    PrintMatrixCurrentState(matrix);
                                    availablePositions--;
                                    break;
                                }
                            }
                            catch (Exception)
                            {
                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                Console.WriteLine("Invalid Input or Position. Try Again.");
                                Console.ForegroundColor = ConsoleColor.Gray;
                            }
                        }
                    }
                    isPlayersTurn = false;
                    if (IsWinning01(matrix, playerSymbol) ||
                        IsWinning02(matrix, playerSymbol) ||
                        IsWinning03(matrix, playerSymbol) ||
                        IsWinning04(matrix, playerSymbol) ||
                        IsWinning05(matrix, playerSymbol) ||
                        IsWinning06(matrix, playerSymbol) ||
                        IsWinning07(matrix, playerSymbol) ||
                        IsWinning08(matrix, playerSymbol))
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("GAME OVER! Player wins.");
                        Console.ForegroundColor = ConsoleColor.Gray;
                        PrintMatrixCurrentState(matrix);
                        Environment.Exit(0);
                    }
                }
                else if (!isPlayersTurn && availablePositions > 0)
                {
                    Console.WriteLine("AI's turn");
                    bool isPlaceBusy = true;
                    Random random = new Random();
                    int randomRow = random.Next(0, 3);
                    int randomCol = random.Next(0, 3);
                    if (matrix[randomRow, randomCol] == ' ')
                    {
                        matrix[randomRow, randomCol] = aiSymbol;
                        PrintMatrixCurrentState(matrix);
                        availablePositions--;
                    }
                    else
                    {
                        while (isPlaceBusy)
                        {
                            randomRow = random.Next(0, 3);
                            randomCol = random.Next(0, 3);
                            if (matrix[randomRow, randomCol] == ' ')
                            {
                                isPlaceBusy = false;
                                matrix[randomRow, randomCol] = aiSymbol;
                                PrintMatrixCurrentState(matrix);
                                availablePositions--;
                            }
                        }
                    }

                    isPlayersTurn = true;
                    if (IsWinning01(matrix, aiSymbol) ||
                        IsWinning02(matrix, aiSymbol) ||
                        IsWinning03(matrix, aiSymbol) ||
                        IsWinning04(matrix, aiSymbol) ||
                        IsWinning05(matrix, aiSymbol) ||
                        IsWinning06(matrix, aiSymbol) ||
                        IsWinning07(matrix, aiSymbol) ||
                        IsWinning08(matrix, aiSymbol))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("GAME OVER! AI wins.");
                        Console.ForegroundColor = ConsoleColor.Gray;
                        PrintMatrixCurrentState(matrix);
                        Environment.Exit(0);
                    }
                }
                else if (availablePositions == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("GAME OVER! Draw.");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    PrintMatrixCurrentState(matrix);
                    Environment.Exit(0);
                }
            }
        }
        static char[,] CreateMatrix()
        {
            char[,] matrix = new char[3, 3];
            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    matrix[row, col] = ' ';
                }
            }
            return matrix;
        }
        static void PrintMatrixCurrentState(char[,] matrix)
        {
            Console.WriteLine();
            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    if (col < 2)
                    {
                        Console.Write($"{matrix[row, col]}|");
                    }
                    else
                    {
                        Console.Write($"{matrix[row, col]}");
                    }

                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
        static int RowPosition(int position)
        {
            int row = -1;
            switch (position)
            {
                case 1: row = 0; break;
                case 2: row = 0; break;
                case 3: row = 0; break;
                case 4: row = 1; break;
                case 5: row = 1; break;
                case 6: row = 1; break;
                case 7: row = 2; break;
                case 8: row = 2; break;
                case 9: row = 2; break;
            }
            return row;
        }
        static int ColPosition(int position)
        {
            int col = -1;
            switch (position)
            {
                case 1: col = 0; break;
                case 2: col = 1; break;
                case 3: col = 2; break;
                case 4: col = 0; break;
                case 5: col = 1; break;
                case 6: col = 2; break;
                case 7: col = 0; break;
                case 8: col = 1; break;
                case 9: col = 2; break;
            }
            return col;
        }
        static bool IsWinning01(char[,] matrix, char symbol)
        {
            bool isWinning = false;
            if (matrix[0, 0] == symbol && matrix[1, 0] == symbol &&
                matrix[2, 0] == symbol)
            {
                isWinning = true;
            }
            return isWinning;
        }
        static bool IsWinning02(char[,] matrix, char symbol)
        {
            bool isWinning = false;
            if (matrix[0, 0] == symbol && matrix[0, 1] == symbol &&
                matrix[0, 2] == symbol)
            {
                isWinning = true;
            }
            return isWinning;
        }
        static bool IsWinning03(char[,] matrix, char symbol)
        {
            bool isWinning = false;
            if (matrix[0, 2] == symbol && matrix[1, 2] == symbol &&
                matrix[2, 2] == symbol)
            {
                isWinning = true;
            }
            return isWinning;
        }
        static bool IsWinning04(char[,] matrix, char symbol)
        {
            bool isWinning = false;
            if (matrix[2, 0] == symbol && matrix[2, 1] == symbol &&
                matrix[2, 2] == symbol)
            {
                isWinning = true;
            }
            return isWinning;
        }
        static bool IsWinning05(char[,] matrix, char symbol)
        {
            bool isWinning = false;
            if (matrix[1, 0] == symbol && matrix[1, 1] == symbol &&
                matrix[1, 2] == symbol)
            {
                isWinning = true;
            }
            return isWinning;
        }
        static bool IsWinning06(char[,] matrix, char symbol)
        {
            bool isWinning = false;
            if (matrix[0, 0] == symbol && matrix[1, 1] == symbol &&
                matrix[2, 2] == symbol)
            {
                isWinning = true;
            }
            return isWinning;
        }
        static bool IsWinning07(char[,] matrix, char symbol)
        {
            bool isWinning = false;
            if (matrix[0, 2] == symbol && matrix[1, 1] == symbol &&
                matrix[2, 0] == symbol)
            {
                isWinning = true;
            }
            return isWinning;
        }
        static bool IsWinning08(char[,] matrix, char symbol)
        {
            bool isWinning = false;
            if (matrix[0, 1] == symbol && matrix[1, 1] == symbol &&
                matrix[2, 1] == symbol)
            {
                isWinning = true;
            }
            return isWinning;
        }
        static void HowToPlay()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Welcome to Tic-Tac-Toe!");
            Console.WriteLine("Below you can see how to play the game.\n");
            int position = 1;
            int[,] matrix = new int[3, 3];
            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    matrix[row, col] = position;
                    position++;
                    if (col < 2)
                    {
                        Console.Write($"{matrix[row, col]}|");
                    }
                    else
                    {
                        Console.Write($"{matrix[row, col]}");
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine();
            Console.WriteLine("Type the number where you want to place your MARK.\nEnjoy!\n");
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }
}
