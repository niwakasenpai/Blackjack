using System;

class Program
{
    static int Main(string[] args)
    {
        int[] suit = new int[52];
        int[] rank = new int[52];
        int dd = 0;
        int i;
        bool flag = false;

        Random r = new Random();


        for (i = 0; i < 52; i++)
        {
            suit[i] = r.Next(4);
            rank[i] = r.Next(13) + 1;
            for (int j = 0; j < i; j++)
            {
                if ((suit[j] == suit[i]) && (rank[j] == rank[i]))
                {
                    i--;
                    dd++;
                }
            }
        }

        Console.WriteLine("ディーラー1枚目");
        Card(0, suit, rank);

        Console.WriteLine("プレイヤー1枚目");
        Card(1, suit, rank);

        Console.WriteLine("プレイヤー2枚目");
        Card(3, suit, rank);

        int n = 1;
        int m = 2;
        int d_sum = D_Calc(n, rank);
        Console.WriteLine("ディーラー合計");
        Console.WriteLine(d_sum);
        int p_sum = P_Calc(m, rank);
        Console.WriteLine("プレイヤー合計");
        Console.WriteLine(p_sum);

        while (true)
        {
            i = 0;

            Console.WriteLine("[1:ヒット][2:スタンド][999:終了]");
            while (true)
            {
                int.TryParse(Console.ReadLine(), out i);
                if (i == 1 || i == 2 || i == 999) break;
            }

            if (i == 1)
            {
                m++;
                Console.WriteLine("プレイヤー"+m+"枚目");
                Card(m * 2 - 1, suit, rank);
                p_sum = P_Calc(m, rank);
                Console.WriteLine("プレイヤー合計");
                Console.WriteLine(p_sum);
            }
            else if (i == 2)
            {
                while (true)
                {
                    n++;
                    Console.WriteLine("ディーラー" + n + "枚目");
                    Card(n * 2 - 2, suit, rank);
                    d_sum = D_Calc(n, rank);
                    Console.WriteLine("ディーラー合計");
                    Console.WriteLine(d_sum);
                    if (d_sum > 16) break;
                }
            }

            if (i == 2 || i == 999) break;
            if (p_sum > 21) break;
        }

        if (i == 1)
        {
            Console.WriteLine("BUST!!ディーラーWIN!!");
        }
        else if (i == 2 && d_sum < 22)
        {
            if (d_sum == p_sum)
            {
                Console.WriteLine("DRAW");
            }
            else if(d_sum > p_sum)
            {
                Console.WriteLine("ディーラーWIN!!");
            }
            else if (d_sum < p_sum)
            {
                Console.WriteLine("プレイヤーWIN!!");
            }
        }
        else if (d_sum > 21)
            Console.WriteLine("ディーラーBUST!!プレイヤーWIN!!");
        
        return 0;
    }

    static void Card(int i,int[] suit,int[] rank)
    {
        switch (suit[i])
        {
            case 0:
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Suit:クラブ");
                break;
            case 1:
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Suit:ダイヤ");
                break;
            case 2:
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Suit:ハート");
                break;
            case 3:
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Suit:スペード");
                break;
        }

        if (rank[i] == 11) Console.WriteLine("Number:J");
        else if (rank[i] == 12) Console.WriteLine("Number:Q");
        else if (rank[i] == 13) Console.WriteLine("Number:K");
        else Console.WriteLine("Number:" + rank[i]);

        Console.ForegroundColor = ConsoleColor.White;
    }


    static int D_Calc(int n, int[] rank)
    {
        int d_sum = 0;

        for (int i = 0; i < n; i++)
        {
            switch (rank[i * 2])
            {
                case 1:
                    if (d_sum < 11) d_sum += 11;
                    else d_sum += 1;
                    break;
                case 11:
                    d_sum += 10;
                    break;
                case 12:
                    d_sum += 10;
                    break;
                case 13:
                    d_sum += 10;
                    break;
                default:
                    d_sum += rank[i * 2];
                    break;
            }
        }
        return d_sum;
    }

    static int P_Calc(int m, int[] rank)
    {
        int p_sum = 0;

        for (int i = 0; i < m; i++)
        {
            switch (rank[i * 2 + 1])
            {
                case 1:
                    if (p_sum < 11) p_sum += 11;
                    else p_sum += 1;
                    break;
                case 11:
                    p_sum += 10;
                    break;
                case 12:
                    p_sum += 10;
                    break;
                case 13:
                    p_sum += 10;
                    break;
                default:
                    p_sum += rank[i * 2 + 1];
                    break;
            }
        }
        return p_sum;
    }
}