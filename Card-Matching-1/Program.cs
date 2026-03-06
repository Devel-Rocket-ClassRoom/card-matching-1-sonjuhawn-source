using System;
using System.Linq;
using System.Threading;

Card card = new Card();

card.Play();

class Card
{
    public string[,] deck = new string[5, 5]
    {
        {"   ", "1열", "2열", "3열", "4열" },
        {"1행", "[1]", "[1]", "[2]", "[2]"},
        {"2행", "[3]", "[3]", "[4]", "[4]"},
        {"3행", "[5]", "[5]", "[6]", "[6]"},
        {"4행", "[7]", "[7]", "[8]", "[8]"}
    };
    bool[,] checkcard = new bool[5, 5];

    public int trycount = 0;
    public int successcount = 0;

    public void CardSuffle()
    {
        Console.WriteLine("=== 카드 짝 맞추기 게임 ===\n");

        Console.WriteLine($"카드를 섞는 중...\n");
        Thread.Sleep(2000);
        string[,] newdeck = new string[4, 4];
        for (int i = 1; i < 5; i++)
        {
            for (int j = 1; j < 5; j++)
            {
                newdeck[i - 1, j - 1] = deck[i, j];
            }
        }
        Random random = new Random();
        for (int i = 15; i > 1; i--)
        {
            int j = random.Next(i + 1);

            int x1 = i / 4;
            int y1 = i % 4;
            int x2 = j / 4;
            int y2 = j % 4;

            string temp = newdeck[x1, y1];
            newdeck[x1, y1] = newdeck[x2, y2];
            newdeck[x2, y2] = temp;

        }
        for (int i = 1; i < 5; i++)
        {
            for (int j = 1; j < 5; j++)
            {
                deck[i, j] = newdeck[i - 1, j - 1];
            }
        }
        for (int i = 0; i < deck.GetLength(0); i++)
        {
            for (int j = 0; j < deck.GetLength(1); j++)
            {
                checkcard[i, j] = false;
            }
        }
    }

    public string FirstCard()
    {
        Console.Write("첫 번째 카드를 선택하세요 (행, 열): ");
        string position = Console.ReadLine();
        string[] pos = position.Split(' ');
        checkcard[int.Parse(pos[0]), int.Parse(pos[1])] = true;
        ShowCard();
        return deck[int.Parse(pos[0]), int.Parse(pos[1])];
    }

    public string SecondCard()
    {
        Console.Write("두 번째 카드를 선택하세요 (행, 열): ");
        string position = Console.ReadLine();
        string[] pos = position.Split(' ');
        checkcard[int.Parse(pos[0]), int.Parse(pos[1])] = true;
        ShowCard();
        return deck[int.Parse(pos[0]), int.Parse(pos[1])];
    }

    public void CheckCard()
    {
        string firstcard = FirstCard();
        string secondcard = SecondCard();
        if (firstcard == secondcard)
        {
            Console.WriteLine("짝을 찾았습니다!\n");
            trycount++;
            successcount++;
            Thread.Sleep(1000);
        }
        else
        {
            Console.WriteLine("짝이 맞지 않습니다!\n");
            trycount++;
            for (int i = 0; i < deck.GetLength(0); i++)
            {
                for (int j = 0; j < deck.GetLength(1); j++)
                {
                    if (deck[i, j] == firstcard || deck[i, j] == secondcard)
                    {
                        checkcard[i, j] = false;
                    }
                }
            }
            Thread.Sleep(3000);
        }

    }
    public void ShowCard()
    {
        for (int i = 0; i < deck.GetLength(0); i++)
        {
            for (int j = 0; j < deck.GetLength(1); j++)
            {
                if (i > 0 && j > 0)
                {
                    if (checkcard[i, j] == true)
                    {
                        Console.Write($"{deck[i, j]} ");
                    }
                    else
                    {
                        Console.Write("** ");
                    }
                }
                else
                {
                    Console.Write(deck[i, j]);
                }
            }
            Console.WriteLine();
        }
        Console.WriteLine($"시도 횟수: {trycount} | 찾은 쌍: {successcount}/8 \n");
    }
    public void Play()
    {
        CardSuffle();
        while (true)
        {
            ShowCard();
            CheckCard();

            if (successcount == 8)
            {
                Console.WriteLine("=== 게임 클리어! ===");
                Console.WriteLine($"시도 횟수: {trycount}");
                Console.WriteLine("새 게임을 하시겠습니까 (Y/N)");
                string game = Console.ReadLine();

                if (game == "Y")
                {
                    Console.Clear();
                    CardSuffle();
                    continue;
                }
                else if (game == "N")
                {
                    break;
                }
            }
        }
    }
}