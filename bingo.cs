// See https://aka.ms/new-console-template for more information
using System;

class BingoGame
{
    static int[,] board = new int[5, 5];
    static bool[,] marked = new bool[5, 5];
    static Random rand = new Random();

    static void Main(string[] args)
    {
        GenerateBoard();
        PlayGame();
    }

    // Tạo bảng bingo 5x5
    static void GenerateBoard()
    {
        int[] numbers = new int[25];
        for (int i = 0; i < 25; i++) numbers[i] = i + 1;

        // Shuffle mảng numbers
        for (int i = 0; i < numbers.Length; i++)
        {
            int j = rand.Next(numbers.Length);
            int temp = numbers[i];
            numbers[i] = numbers[j];
            numbers[j] = temp;
        }

        int index = 0;
        for (int r = 0; r < 5; r++)
        {
            for (int c = 0; c < 5; c++)
            {
                board[r, c] = numbers[index++];
                marked[r, c] = false;
            }
        }

        // Free space ở giữa
        marked[2, 2] = true;
    }

    // In bảng Bingo
    static void PrintBoard()
    {
        Console.Clear();
        Console.WriteLine("===== BINGO =====");
        for (int r = 0; r < 5; r++)
        {
            for (int c = 0; c < 5; c++)
            {
                if (marked[r, c])
                    Console.Write(" X \t");
                else
                    Console.Write(board[r, c] + "\t");
            }
            Console.WriteLine("\n");
        }
    }

    // Kiểm tra thắng
    static bool CheckWin()
    {
        // Hàng ngang
        for (int r = 0; r < 5; r++)
        {
            bool win = true;
            for (int c = 0; c < 5; c++)
            {
                if (!marked[r, c]) { win = false; break; }
            }
            if (win) return true;
        }

        // Hàng dọc
        for (int c = 0; c < 5; c++)
        {
            bool win = true;
            for (int r = 0; r < 5; r++)
            {
                if (!marked[r, c]) { win = false; break; }
            }
            if (win) return true;
        }

        // Chéo chính
        bool diag1 = true, diag2 = true;
        for (int i = 0; i < 5; i++)
        {
            if (!marked[i, i]) diag1 = false;
            if (!marked[i, 4 - i]) diag2 = false;
        }
        return diag1 || diag2;
    }

    static void PlayGame()
    {
        while (true)
        {
            PrintBoard();
            Console.Write("Nhập số bạn muốn đánh dấu (1-25): ");
            string input = Console.ReadLine();
            if (int.TryParse(input, out int number))
            {
                MarkNumber(number);
                if (CheckWin())
                {
                    PrintBoard();
                    Console.WriteLine("BINGO!");
                    break;
                }
            }
            else
            {
                Console.WriteLine("Vui lòng nhập số hợp lệ!");
            }
        }
    }

    // Đánh dấu số
    static void MarkNumber(int num)
    {
        for (int r = 0; r < 5; r++)
        {
            for (int c = 0; c < 5; c++)
            {
                if (board[r, c] == num)
                {
                    marked[r, c] = true;
                    return;
                }
            }
        }
    }
}

