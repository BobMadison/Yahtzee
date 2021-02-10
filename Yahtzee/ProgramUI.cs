using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Yahtzee
{
    public class ProgramUI
    {

        static void Main(string[] args)
        {
            List<Player> players = new List<Player>();



            int[] dice;
            dice = new int[5];

            int playerNum = GetNumberOfPlayers();
            Console.WriteLine(playerNum);

            for (int i = 1; i <= playerNum; i++)
            {
                string name;
                Console.Write($"Player {i} enter your name: ");
                name = Console.ReadLine();
                ScoreCard card = new ScoreCard();
                Player player = new Player(i, name, 0, card);
                players.Add(player);
            }

            Console.WriteLine(players[0].PlayerNumber);

            // set up a loop to execute 13 times (inner loop from 1 to numPlayers)
            for (int i = 1; i <= 13; i++)
            {
                for (int play = 0; play < playerNum; play++)
                {
                    Console.WriteLine($"{players[play].PlayerName} It's your turn...");
                    for (int j = 0; j < dice.Length; j++)
                    {
                        dice[j] = GetDiceRoll();
                    }
                    Array.Sort(dice);
                    DisplayScoreCardTotals(players[play].PlayerName, players[play].Card);
                    ShowDice(dice);
                    Console.Write("Do you want to keep this roll? (y/n)? ");
                    string response = Console.ReadLine().ToLower();

                    if (response == "y")
                    {
                        ShowAvailable(players[play].Card, dice);
                    }
                    else
                    {
                        RollAgain(dice);
                        Array.Sort(dice);
                        DisplayScoreCardTotals(players[play].PlayerName, players[play].Card);
                        ShowDice(dice);
                        Console.Write("Do you want to keep this roll? (y/n)? ");
                        response = Console.ReadLine().ToLower();
                        if (response == "y")
                        {
                            ShowAvailable(players[play].Card, dice);
                        }
                        else
                        {
                            RollAgain(dice);
                            Array.Sort(dice);
                            DisplayScoreCardTotals(players[play].PlayerName, players[play].Card);
                            ShowDice(dice);
                            ShowAvailable(players[play].Card, dice);
                        }
                    }
                    Console.Clear();
                }
                
            }

            for (int play = 0; play < playerNum; play++)
                players[play].PlayerScore = players[play].Card.Total;

            DisplayResults(players, playerNum);
        }


        public static void DisplayResults(List<Player> pl, int num)
        {
            int testScore = 0;
            string winner = "";
            Console.Clear();

            for (int i = 0; i < num; i++)
            {
                Console.WriteLine($"{pl[i].PlayerName} final score: {pl[i].PlayerScore}");
                if (pl[i].PlayerScore > testScore)
                {
                    testScore = pl[i].PlayerScore;
                    winner = pl[i].PlayerName;
                }
            }
            Console.WriteLine($"\n\nWay to go {winner} you won the game with a score of {testScore}!");
        }
                

        public static void ShowDice(int[] dc)
        {
            Console.WriteLine("\n Top row represents the die position.\n Bottom number is the value of the die.\n");

            // for (int i = 1; i <= dc.Length; i++)
            //      Console.Write($"{i} ");
            Console.WriteLine(" 1 2 3 4 5\n\n");

            for (int i = 0; i < dc.Length; i++)
                Console.Write($" {dc[i]}");

            Console.WriteLine();

        }

        public static int GetDiceRoll()
        {
            Random random = new Random();
            Thread.Sleep(20);
            return Convert.ToInt32(random.Next(1, 7));
        }

        public static int GetNumberOfPlayers()
        {
            int playerNumber;
            do
            {
                Console.Write("Please enter the number of players: 1, 2, 3, or 4: ");
                playerNumber = Convert.ToInt32(Console.ReadLine());
            }
            while (!(playerNumber <= 4 && playerNumber >= 1));
            return playerNumber;

        }

        public static void DisplayScoreCardTotals(string name, ScoreCard sc)
        {
            Console.WriteLine($"\n\n\n   {name}'s current score card.\n\n\n");


            Console.WriteLine($"{"  Aces:",-23}{sc.Aces}");
            Console.WriteLine($"{"  Twos:",-23}{sc.Twos}");
            Console.WriteLine($"{"  Threes:",-22} {sc.Threes}");
            Console.WriteLine($"{"  Fours:",-22}{sc.Fours}");
            Console.WriteLine($"{"  Fives:",-22}{sc.Fives}");
            Console.WriteLine($"{"  Sixes:",-22}{sc.Sixes}");
            Console.WriteLine(new String('-', 74));

            sc.UpperHalfSub = sc.Aces + sc.Twos + sc.Threes + sc.Fours + sc.Fives + sc.Sixes;
            Console.WriteLine($"{"  Total of Above:",-22}{sc.UpperHalfSub}");

            sc.Bonus = sc.UpperHalfSub >= 63 ? 35 : 0;
            Console.WriteLine($"{"  Bonus:",-22}{sc.Bonus}");

            sc.UpperHalfTotal = sc.Bonus + sc.UpperHalfSub;
            Console.WriteLine($"{"  Upper Half Total:",-22}{sc.UpperHalfTotal}");
            Console.WriteLine(new String('-', 74));

            Console.WriteLine($"{"  3 of a Kind:",-22}{sc.ThreeOfKind}");
            Console.WriteLine($"{"  4 of a kind:",-22}{sc.FourOfKind}");
            Console.WriteLine($"{"  Full House:",-22}{sc.FullHouse}");
            Console.WriteLine($"{"  Small Straight:",-21} {sc.SmallStraight}");
            Console.WriteLine($"{"  Large Straight:",-22}{sc.LargeStraight}");
            Console.WriteLine($"{"  Chance:",-22}{sc.Chance}");
            Console.WriteLine($"{"  Yahtzee:",-22}{sc.Yahtzee}");
            Console.WriteLine(new String('-', 74));

            sc.LowerHalfTotal = sc.ThreeOfKind + sc.FourOfKind + sc.FullHouse +
                                sc.SmallStraight + sc.LargeStraight + sc.Chance + sc.Yahtzee;
            Console.WriteLine($"{"  Lower Half Total:",-22}{sc.LowerHalfTotal}");
            Console.WriteLine($"{"  Upper Half Total:",-23}{sc.UpperHalfTotal}");

            sc.Total = sc.UpperHalfTotal + sc.LowerHalfTotal;
            Console.WriteLine($"{"  Grand Total:",-22}{sc.Total}");
            Console.WriteLine(new String('-', 74));
        }
  
        public static bool IsNumberThere(int[] dc, int number)
        {
            bool found = false;

            for (int i = 0; i < dc.Length; i++)
            {
                if (dc[i] == number)
                {
                    found = true;
                    break;
                }
            }
            return found;
        }

        public static void ShowAvailable(ScoreCard sc, int[] dice)
        {
            if (sc.Aces == 0 && IsNumberThere(dice, 1))
            {
                Console.WriteLine("  1. Aces");
            }

            if (sc.Twos == 0 && IsNumberThere(dice, 2))
            {
                Console.WriteLine("  2. Twos");
            }

            if (sc.Threes == 0 && IsNumberThere(dice, 3))
            {
                Console.WriteLine("  3. Threes");
            }

            if (sc.Fours == 0 && IsNumberThere(dice, 4))
            {
                Console.WriteLine("  4. Fours");
            }

            if (sc.Fives == 0 && IsNumberThere(dice, 5))
            {
                Console.WriteLine("  5. Fives");
            }

            if (sc.Sixes == 0 && IsNumberThere(dice, 6))
            {
                Console.WriteLine("  6. Sixes");
            }

            if (sc.ThreeOfKind == 0 && GotThreeOfKind(dice))
            {
                Console.WriteLine("  7. 3 of a Kind");
            }

            if (sc.FourOfKind == 0 && GotFourOfKind(dice))
            {
                Console.WriteLine("  8. 4 of a Kind");
            }

            if (sc.FullHouse == 0 && GotFullHouse(dice))
            {
                Console.WriteLine("  9. Full House");
            }

            if (sc.SmallStraight == 0 && GotSmallStraight(dice))
            {
                Console.WriteLine("  10. Small Straight");
            }

            if (sc.LargeStraight == 0 && GotLargeStraight(dice))
            {
                Console.WriteLine("  11. Large Straight");
            }

            if (sc.Chance == 0)
            {
                Console.WriteLine("  12. Chance");
            }

            if (sc.Yahtzee == 0 && dice[0] == dice[4])
            {
                Console.WriteLine("  13. Yahtzee");
            }

            Console.Write("Please enter your choice: ");
            int choice = Convert.ToInt32(Console.ReadLine());
            switch (choice)
            {

                case 1: sc.Aces = AddThemUp(dice, 1); break;
                case 2: sc.Twos = AddThemUp(dice, 2); break;
                case 3: sc.Threes = AddThemUp(dice, 3); break;
                case 4: sc.Fours = AddThemUp(dice, 4); break;
                case 5: sc.Fives = AddThemUp(dice, 5); break;
                case 6: sc.Sixes = AddThemUp(dice, 6); break;

                case 7: sc.ThreeOfKind = AddAll(dice); break;
                case 8: sc.FourOfKind = AddAll(dice); break;
                case 9: sc.FullHouse = 25; break;
                case 10: sc.SmallStraight = 30; break;
                case 11: sc.LargeStraight = 40; break;
                case 12: sc.Chance = AddAll(dice); break;
                case 13: sc.Yahtzee = 50; break;
                default:
                    Console.WriteLine("Not an option");
                    break;
            }
        }

        public static int AddThemUp(int[] dice, int rollType)
        {
            int sumUp = 0;

            for (int i = 0; i < dice.Length; i++)
            {
                if (rollType == dice[i])
                {
                    sumUp += rollType;
                }
            }
            return sumUp;
        }

        public static int AddAll(int[] dice)
        {
            int add = 0;
            for (int i = 0; i < dice.Length; i++)
            {
                add += dice[i];
            }
            return add;
        }

        public static bool GotThreeOfKind(int[] dc)
        {
            bool found = false;
            if (dc[0] == dc[2] || dc[1] == dc[3] || dc[2] == dc[4])
                found = true;
            return found;
        }

        public static bool GotFourOfKind(int[] dc)
        {
            bool found = false;

            if (dc[0] == dc[3] || dc[1] == dc[4])
                found = true;
            return found;
        }

        public static bool GotFullHouse(int[] dc)
        {
            bool found = false;
            if (dc[0] == dc[2] && dc[3] == dc[4] || dc[0] == dc[1] && dc[2] == dc[4])
                found = true;
            return found;
        }

        // Here are all of the possible small straights (I think)
        // 1 1 2 3 4  B 
        // 1 2 2 3 4   C
        // 1 2 3 3 4    D  
        // 1 2 3 4 4 A
        // 1 2 3 4 5 A (also a large straight) 
        // 1 2 3 4 6 A
        // 1 3 4 5 6  B
        // 2 2 3 4 5  B
        // 2 3 3 4 5   C
        // 2 3 4 4 5    D
        // 2 3 4 5 5 A
        // 2 3 4 5 6 A (also a large straight) 
        // 3 3 4 5 6  B dc[1] + 1 == dc[2] && dc[2] + 1 == dc[3] && dc[3] + 1 == dc[4]
        // 3 4 4 5 6   C dc[0] + 1 == dc[1] && dc[1] == dc[2] && dc[2] + 1 == dc[3] && dc[3] + 1 == dc[4]
        // 3 4 5 5 6    D dc[0] + 1 == dc[1] && dc[1] + 1 == dc[2] && dc[2] == dc[3] && dc[3] + 1 == dc[4]
        // 3 4 5 6 6 A   dc[0] + 1 == dc[1] && dc[1] + 1 == dc[2] && dc[2] + 1 == dc[3]

        


        public static bool GotSmallStraight(int[] dc)
        {
            bool found = false;
            if (dc[1] + 1 == dc[2] && dc[2] + 1 == dc[3] && dc[3] + 1 == dc[4])
                found = true;
            else if (dc[0] + 1 == dc[1] && dc[1] == dc[2] && dc[2] + 1 == dc[3] && dc[3] + 1 == dc[4])
                found = true;
            else if (dc[0] + 1 == dc[1] && dc[1] + 1 == dc[2] && dc[2] == dc[3] && dc[3] + 1 == dc[4])
                found = true;
            else if (dc[0] + 1 == dc[1] && dc[1] + 1 == dc[2] && dc[2] + 1 == dc[3])
                found = true;

            return found;
        }

        public static bool GotLargeStraight(int[] dc)
        {
            bool found = false;
            if (dc[0] + 1 == dc[1] && dc[1] + 1 == dc[2] && dc[2] + 1 == dc[3] && dc[3] + 1 == dc[4])
                found = true;
            return found;
        }

        public static void RollAgain(int[] dice)
        {
            int reroll;

            Console.Write("How many dice do you want to replace? ");
            reroll = Convert.ToInt32(Console.ReadLine());

            switch (reroll)
            {
                case 1:
                case 2:
                case 3:
                case 4:
                    for (int i = 0; i < reroll; i++)
                    {
                        Console.Write($"Enter which die to reroll: ");
                        int choice = Convert.ToInt32(Console.ReadLine());
                        dice[choice - 1] = GetDiceRoll();
                    }
                    break;
                case 5:
                    for(int i = 0; i < 5; i++)
                    {
                        dice[i] = GetDiceRoll();
                    }
                    break;
                default:
                    Console.WriteLine("You messed up!");
                    break;
            }
        }
    }
}
