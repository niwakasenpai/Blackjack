using System;

class Program
{
    static int Main(string[] args)
    {
        int pp = 0;
        int cnt = 0;
        bool flag = false;
        bool stn = false;
        bool nbj = false;
        int[] suit = new int[52];
        int[] rank = new int[52];

        while (true)
        {
            if (pp > 26) pp = 0;
            if (pp == 0)
            {
                Sort(cnt, suit, rank);
                Console.WriteLine("新しいカードデッキに交換しました。");
            }

            pp = Game(pp, cnt, suit, rank, flag, stn, nbj);

            Console.WriteLine("なにかキーを押して続行");
            Console.ReadKey();
        }
        return 0;
    }

    //
    static int Game(int pp, int cnt, int[] suit, int[] rank, bool flag, bool stn, bool nbj)
    {
        int i = 0;

        Console.WriteLine("ディーラー1枚目");
        Card(0 + pp, suit, rank);

        Console.WriteLine("プレイヤー1枚目");
        Card(1 + pp, suit, rank);

        Console.WriteLine("プレイヤー2枚目");
        Card(3 + pp, suit, rank);

        int m = 0;
        int n = 0;
        int d_sum = D_Calc(pp, m, n, rank, stn);
        Console.WriteLine("ディーラー合計");
        Console.WriteLine(d_sum);
        int p_sum = P_Calc(pp, m, rank);
        Console.WriteLine("プレイヤー合計");
        Console.WriteLine(p_sum);

        if (p_sum == 21) nbj = true;

        while (nbj == false)
        {
            Console.WriteLine("[1:ヒット][2:スタンド]");
            while (true)
            {
                int.TryParse(Console.ReadLine(), out i);
                if (i == 1 || i == 2) break;
            }

            if (i == 1)
            {
                m++;
                Console.WriteLine("プレイヤー" + (m + 2) + "枚目");
                Card(3 + pp + m, suit, rank);
                p_sum = P_Calc(pp, m, rank);
                Console.WriteLine("プレイヤー合計");
                Console.WriteLine(p_sum);
            }
            else if (i == 2)
            {
                stn = true;
                Console.WriteLine("ディーラー2枚目");
                Card(2 + pp, suit, rank);
                d_sum = D_Calc(pp, m, n, rank, stn);
                Console.WriteLine("ディーラー合計");
                Console.WriteLine(d_sum);

                while (true)
                {
                    if (d_sum > 16 && d_sum > p_sum) break;
                    n++;
                    Console.WriteLine("ディーラー" + (n + 2) + "枚目");
                    Card(3 + pp + m + n, suit, rank);
                    d_sum = D_Calc(pp, m, n, rank, stn);
                    Console.WriteLine("ディーラー合計");
                    Console.WriteLine(d_sum);
                }
            }

            if (i == 0) break;
            if (i == 2) break;
            if (p_sum == 21) break;
            if (p_sum > 21) break;
        }

        if (i == 0)
        {
            Console.WriteLine("ディーラー2枚目");
            Card(2 + pp, suit, rank);
            stn = true;
            d_sum = D_Calc(pp, m, n, rank, stn);
            Console.WriteLine("ディーラー合計");
            Console.WriteLine(d_sum);

            if (d_sum == 21)
            {
                Console.WriteLine("DRAW");
            }
            else
            {
                Console.WriteLine("プレイヤーWIN!!ナチュラルブラックジャック!!");
            }

        }
        else if (i == 1 && p_sum == 21)
        {
            Console.WriteLine("プレイヤーWIN!!ブラックジャック!!");
        }
        else if (i == 1 && p_sum > 21)
        {
            Console.WriteLine("BUST!!ディーラーWIN!!");
        }
        else if (i == 2 && d_sum < 22)
        {
            if (d_sum == p_sum)
            {
                Console.WriteLine("DRAW");
            }
            else if (d_sum > p_sum)
            {
                Console.WriteLine("ディーラーWIN!!");
            }
            else if (d_sum < p_sum)
            {
                Console.WriteLine("プレイヤーWIN!!");
                if (p_sum == 21)
                {
                    Console.WriteLine("ブラックジャック！");
                }
            }
        }
        else if (d_sum > 21)
            Console.WriteLine("ディーラーBUST!!プレイヤーWIN!!");

        pp = pp + 4 + m + n;

        return pp;
    }


    //
    static void Sort(int cnt, int[] suit, int[] rank)
    {
        Random r = new Random();

        for (cnt = 0; cnt < 52; cnt++)
        {
            suit[cnt] = r.Next(4);
            rank[cnt] = r.Next(13) + 1;
            for (int j = 0; j < cnt; j++)
            {
                if ((suit[j] == suit[cnt]) && (rank[j] == rank[cnt]))
                {
                    cnt--;
                }
            }
        }
    }

    //
    static void Card(int i, int[] suit, int[] rank)
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

    //
    static int P_Calc(int pp, int m, int[] rank)
    {
        bool flag = false;

        int p_sum = 0;

        p_sum = P_R_calc(1 + pp, rank, p_sum, flag);
        p_sum = P_R_calc(3 + pp, rank, p_sum, flag);

        for (int i = 0; i < m; i++)
        {
            p_sum = P_R_calc(3 + pp + i + 1, rank, p_sum, flag);
        }

        if (p_sum > 21)
        {
            if (rank[1 + pp] == 1 || rank[3 + pp] == 1)
            {
                flag = true;
            }
            for (int i = 0; i < m; i++)
            {
                if (rank[3 + pp + i + 1] == 1)
                {
                    flag = true;
                }
            }

            if (flag == true)
            {
                p_sum = 0;
                p_sum = P_R_calc(1 + pp, rank, p_sum, flag);
                p_sum = P_R_calc(3 + pp, rank, p_sum, flag);

                for (int i = 0; i < m; i++)
                {
                    p_sum = P_R_calc(3 + pp + i + 1, rank, p_sum, flag);
                }
            }

        }
        return p_sum;
    }

    //
    static int D_Calc(int pp, int m, int n, int[] rank, bool stn)
    {
        bool flag = false;

        int d_sum = 0;

        d_sum = D_R_calc(0 + pp, rank, d_sum, flag);

        if (stn == true)
        {
            d_sum = D_R_calc(2 + pp, rank, d_sum, flag);
        }

        for (int i = 0; i < n; i++)
        {
            d_sum = D_R_calc(3 + pp + m + i + 1, rank, d_sum, flag);
        }

        if (d_sum > 21)
        {
            if (rank[0 + pp] == 1)
            {
                flag = true;
            }
            if (stn == true && rank[2 + pp] == 1)
            {
                flag = true;
            }
            for (int i = 0; i < m; i++)
            {
                if (rank[3 + pp + m + i + 1] == 1)
                {
                    flag = true;
                }
            }

            if (flag == true)
            {
                d_sum = 0;

                d_sum = D_R_calc(0 + pp, rank, d_sum, flag);

                if (stn == true)
                {
                    d_sum = D_R_calc(2 + pp, rank, d_sum, flag);
                }

                for (int i = 0; i < n; i++)
                {
                    d_sum = D_R_calc(3 + pp + m + i + 1, rank, d_sum, flag);
                }
            }

        }

        return d_sum;
    }

    //
    static int P_R_calc(int j, int[] rank, int p_sum, bool flag)
    {
        switch (rank[j])
        {
            case 1:
                if (p_sum < 11 && flag == false) p_sum += 11;
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
                p_sum += rank[j];
                break;
        }
        return p_sum;
    }

    //
    static int D_R_calc(int j, int[] rank, int d_sum, bool flag)
    {
        switch (rank[j])
        {
            case 1:
                if (d_sum < 11 && flag == false) d_sum += 11;
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
                d_sum += rank[j];
                break;
        }
        return d_sum;
    }
}