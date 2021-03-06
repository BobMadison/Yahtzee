﻿using System;
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
            Console.Clear();
            for (int i = 1; i <= playerNum; i++)
            {
                string name;
                Console.Write($"  Player {i} enter your name: ");
                name = Console.ReadLine();
                ScoreCard card = new ScoreCard();
                Player player = new Player(i, name, 0, card);
                players.Add(player);
            }

            Console.Clear();
            for (int i = 1; i <= 13; i++)
            {
                for (int play = 0; play < playerNum; play++)
                {
                    Console.WriteLine($"  {players[play].PlayerName} It's your turn...");

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
                        Console.Write("Do you want to keep this second roll? (y/n)? ");
                        response = Console.ReadLine().ToLower();

                        if (response == "y")
                        {
                            ShowAvailable(players[play].Card, dice);
                            DisplayScoreCardTotals(players[play].PlayerName, players[play].Card);
                        }
                        else
                        {
                            RollAgain(dice);
                            Array.Sort(dice);
                            DisplayScoreCardTotals(players[play].PlayerName, players[play].Card);
                            ShowDice(dice);
                            ShowAvailable(players[play].Card, dice);
                            DisplayScoreCardTotals(players[play].PlayerName, players[play].Card);
                        }

                    }
                    Console.Clear();
                    // Console.WriteLine($"{players[play].PlayerName} has {players[play].Card.Total} points");
                }

            }

            for (int play = 0; play < playerNum; play++)
            {
                players[play].PlayerScore = players[play].Card.Total;

            }

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
                DisplayScoreCardTotals(pl[i].PlayerName, pl[i].Card);
            }
            Console.WriteLine($"\n\nWay to go {winner} you won the game with a score of {testScore}!");
        }


        public static void ShowDice(int[] dc)
        {
            Console.WriteLine("\n Top row represents the die position.\n Bottom number is the value of the die.\n");

            Console.WriteLine(" 1 2 3 4 5\n");

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
            string choice = "";
            int playerNumber;
            Console.Write("\n\n Please enter the number of players: 1, 2, 3, or 4: ");

            do
            {
                choice = Console.ReadLine();
                if (choice != "1" && choice != "2" && choice != "3" && choice != "4")
                    Console.Write($"\n Sorry. We need a 1, 2, 3, or 4. Try again: ");
            }
            while (choice != "1" && choice != "2" && choice != "3" && choice != "4");
            playerNumber = Convert.ToInt32(choice);
            return playerNumber;

        }

        public static void DisplayScoreCardTotals(string name, ScoreCard sc)
        {
            Console.WriteLine($"\n  {name}'s current score card.\n\n\n");


            Console.Write($"{"  Aces:",-22}{sc.Aces,3}");
            if (sc.Aces == 0 && sc.AcesUsed)
            {
                Console.WriteLine(" scratched");
            }
            else
            {
                Console.WriteLine();
            }

            Console.Write($"{"  Twos:",-22}{sc.Twos,3}");
            if (sc.Twos == 0 && sc.TwosUsed)
            {
                Console.WriteLine(" scratched");
            }
            else
            {
                Console.WriteLine();
            }

            Console.Write($"{"  Threes:",-22}{sc.Threes,3}");
            if (sc.Threes == 0 && sc.ThreesUsed)
            {
                Console.WriteLine(" scratched");
            }
            else
            {
                Console.WriteLine();
            }

            Console.Write($"{"  Fours:",-22}{sc.Fours,3}");
            if (sc.Fours == 0 && sc.FoursUsed)
            {
                Console.WriteLine(" scratched");
            }
            else
            {
                Console.WriteLine();
            }

            Console.Write($"{"  Fives:",-22}{sc.Fives,3}");
            if (sc.Fives == 0 && sc.FivesUsed)
            {
                Console.WriteLine(" scratched");
            }
            else
            {
                Console.WriteLine();
            }

            Console.Write($"{"  Sixes:",-22}{sc.Sixes,3}");
            if (sc.Sixes == 0 && sc.SixesUsed)
            {
                Console.WriteLine(" scratched");
            }
            else
            {
                Console.WriteLine();
            }

            Console.WriteLine(new String('-', 50));

            sc.UpperHalfSub = sc.Aces + sc.Twos + sc.Threes + sc.Fours + sc.Fives + sc.Sixes;
            Console.WriteLine($"{"  Total of Above:",-22}{sc.UpperHalfSub,3}");

            sc.Bonus = sc.UpperHalfSub >= 63 ? 35 : 0;
            Console.WriteLine($"{"  Bonus:",-22}{sc.Bonus,3}");

            sc.UpperHalfTotal = sc.Bonus + sc.UpperHalfSub;
            Console.WriteLine($"{"  Upper Half Total:",-22}{sc.UpperHalfTotal,3}");
            Console.WriteLine(new String('-', 50));
            Console.WriteLine(new String('-', 50));

            Console.Write($"{"  3 of a Kind:",-22}{sc.ThreeOfKind,3}");
            if (sc.ThreeOfKind == 0 && sc.ThreeOfKindUsed)
            {
                Console.WriteLine(" scratched");
            }
            else
            {
                Console.WriteLine();
            }

            Console.Write($"{"  4 of a kind:",-22}{sc.FourOfKind,3}");
            if (sc.FourOfKind == 0 && sc.FourOfKindUsed)
            {
                Console.WriteLine(" scratched");
            }
            else
            {
                Console.WriteLine();
            }

            Console.Write($"{"  Full House:",-22}{sc.FullHouse,3}");
            if (sc.FullHouse == 0 && sc.FullHouseUsed)
            {
                Console.WriteLine(" scratched");
            }
            else
            {
                Console.WriteLine();
            }

            Console.Write($"{"  Small Straight:",-22}{sc.SmallStraight,3}");
            if (sc.SmallStraight == 0 && sc.SmallStraightUsed)
            {
                Console.WriteLine(" scratched");
            }
            else
            {
                Console.WriteLine();
            }

            Console.Write($"{"  Large Straight:",-22}{sc.LargeStraight,3}");
            if (sc.LargeStraight == 0 && sc.LargeStraightUsed)
            {
                Console.WriteLine(" scratched");
            }
            else
            {
                Console.WriteLine();
            }

            Console.Write($"{"  Chance:",-22}{sc.Chance,3}");
            if (sc.Chance == 0 && sc.ChanceUsed)
            {
                Console.WriteLine(" scratched");
            }
            else
            {
                Console.WriteLine();
            }

            Console.Write($"{"  Yahtzee:",-22}{sc.Yahtzee,3}");
            if (sc.Yahtzee == 0 && sc.YahtzeeUsed)
            {
                Console.WriteLine(" scratched");
            }
            else
            {
                Console.WriteLine();
            }

            Console.WriteLine(new String('-', 50));

            sc.LowerHalfTotal = sc.ThreeOfKind + sc.FourOfKind + sc.FullHouse +
                                sc.SmallStraight + sc.LargeStraight + sc.Chance + sc.Yahtzee;
            Console.WriteLine($"{"  Lower Half Total:",-22}{sc.LowerHalfTotal,3}");
            Console.WriteLine($"{"  Upper Half Total:",-22}{sc.UpperHalfTotal,3}");

            sc.Total = sc.UpperHalfTotal + sc.LowerHalfTotal;
            Console.WriteLine($"{"  Grand Total:",-22}{sc.Total,3}");
            Console.WriteLine(new String('-', 50));
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
            int[] choices = new int[14];
            int choice;
            int counter = 0;

            if (sc.Aces == 0 && IsNumberThere(dice, 1) && !sc.AcesUsed)
            {
                Console.WriteLine("  1. Aces");
                choices[counter++] = 1;
            }

            if (sc.Twos == 0 && IsNumberThere(dice, 2) && !sc.TwosUsed)
            {
                Console.WriteLine("  2. Twos");
                choices[counter++] = 2;
            }

            if (sc.Threes == 0 && IsNumberThere(dice, 3) && !sc.ThreesUsed)
            {
                Console.WriteLine("  3. Threes");
                choices[counter++] = 3;
            }

            if (sc.Fours == 0 && IsNumberThere(dice, 4) && !sc.FoursUsed)
            {
                Console.WriteLine("  4. Fours");
                choices[counter++] = 4;
            }

            if (sc.Fives == 0 && IsNumberThere(dice, 5) && !sc.FivesUsed)
            {
                Console.WriteLine("  5. Fives");
                choices[counter++] = 5;
            }

            if (sc.Sixes == 0 && IsNumberThere(dice, 6) && !sc.SixesUsed)
            {
                Console.WriteLine("  6. Sixes");
                choices[counter++] = 6;
            }

            if (sc.ThreeOfKind == 0 && GotThreeOfKind(dice) && !sc.ThreeOfKindUsed)
            {
                Console.WriteLine("  7. 3 of a Kind");
                choices[counter++] = 7;
            }

            if (sc.FourOfKind == 0 && GotFourOfKind(dice) && !sc.FourOfKindUsed)
            {
                Console.WriteLine("  8. 4 of a Kind");
                choices[counter++] = 8;
            }

            if (sc.FullHouse == 0 && GotFullHouse(dice) && !sc.FullHouseUsed)
            {
                Console.WriteLine("  9. Full House");
                choices[counter++] = 9;
            }

            if (sc.SmallStraight == 0 && GotSmallStraight(dice) && !sc.SmallStraightUsed)
            {
                Console.WriteLine("  10. Small Straight");
                choices[counter++] = 10;
            }

            if (sc.LargeStraight == 0 && GotLargeStraight(dice) && !sc.LargeStraightUsed)
            {
                Console.WriteLine("  11. Large Straight");
                choices[counter++] = 11;
            }

            if (sc.Chance == 0 && !sc.ChanceUsed)
            {
                Console.WriteLine("  12. Chance");
                choices[counter++] = 12;
            }

            if (sc.Yahtzee == 0 && dice[0] == dice[4] && !sc.YahtzeeUsed)
            {
                Console.WriteLine("  13. Yahtzee");
                choices[counter++] = 13;
            }
            choices[counter] = 14;

            Console.WriteLine($"  14. Scratch a score");

            choice = SelectFromMenu(choices, counter, "");

            switch (choice)
            {

                case 1: sc.Aces = AddThemUp(dice, 1); sc.AcesUsed = true; break;
                case 2: sc.Twos = AddThemUp(dice, 2); sc.TwosUsed = true; break;
                case 3: sc.Threes = AddThemUp(dice, 3); sc.ThreesUsed = true; break;
                case 4: sc.Fours = AddThemUp(dice, 4); sc.FoursUsed = true; break;
                case 5: sc.Fives = AddThemUp(dice, 5); sc.FivesUsed = true; break;
                case 6: sc.Sixes = AddThemUp(dice, 6); sc.SixesUsed = true; break;

                case 7: sc.ThreeOfKind = AddAll(dice); sc.ThreeOfKindUsed = true; break;
                case 8: sc.FourOfKind = AddAll(dice); sc.FourOfKindUsed = true; break;
                case 9: sc.FullHouse = 25; sc.FullHouseUsed = true; break;
                case 10: sc.SmallStraight = 30; sc.SmallStraightUsed = true; break;
                case 11: sc.LargeStraight = 40; sc.LargeStraightUsed = true; break;
                case 12: sc.Chance = AddAll(dice); sc.ChanceUsed = true; break;
                case 13: sc.Yahtzee = 50; sc.YahtzeeUsed = true; break;
                case 14: ScratchAScore(sc); break;
                default:
                    Console.WriteLine("Not an option");
                    break;
            }
            sc.LowerHalfTotal = sc.ThreeOfKind + sc.FourOfKind + sc.FullHouse +
                                sc.SmallStraight + sc.LargeStraight + sc.Chance + sc.Yahtzee;

            sc.Total = sc.UpperHalfTotal + sc.LowerHalfTotal;

        }

        public static bool InArray(int[] choices, int count, int choice)
        {
            bool found = false;
            for (int i = 0; i <= count; i++)
            {
                if (choices[i] == choice)
                {
                    found = true;
                    break;
                }
            }
            return found;
        }

        public static void ScratchAScore(ScoreCard sc)
        {
            int[] choices = new int[14];
            int choice;
            int counter = 0;

            if (!sc.AcesUsed)
            {
                Console.WriteLine("  1. Aces");
                choices[counter++] = 1;
            }

            if (!sc.TwosUsed)
            {
                Console.WriteLine("  2. Twos");
                choices[counter++] = 2;
            }

            if (!sc.ThreesUsed)
            {
                Console.WriteLine("  3. Threes");
                choices[counter++] = 3;
            }

            if (!sc.FoursUsed)
            {
                Console.WriteLine("  4. Fours");
                choices[counter++] = 4;
            }

            if (!sc.FivesUsed)
            {
                Console.WriteLine("  5. Fives");
                choices[counter++] = 5;
            }

            if (!sc.SixesUsed)
            {
                Console.WriteLine("  6. Sixes");
                choices[counter++] = 6;
            }

            if (!sc.ThreeOfKindUsed)
            {
                Console.WriteLine("  7. 3 of a Kind");
                choices[counter++] = 7;
            }

            if (!sc.FourOfKindUsed)
            {
                Console.WriteLine("  8. 4 of a Kind");
                choices[counter++] = 8;
            }

            if (!sc.FullHouseUsed)
            {
                Console.WriteLine("  9. Full House");
                choices[counter++] = 9;
            }

            if (!sc.SmallStraightUsed)
            {
                Console.WriteLine("  10. Small Straight");
                choices[counter++] = 10;
            }

            if (!sc.LargeStraightUsed)
            {
                Console.WriteLine("  11. Large Straight");
                choices[counter++] = 11;
            }

            if (!sc.ChanceUsed)
            {
                Console.WriteLine("  12. Chance");
                choices[counter++] = 12;
            }

            if (!sc.YahtzeeUsed)
            {
                Console.WriteLine("  13. Yahtzee");
                choices[counter++] = 13;
            }

            choice = SelectFromMenu(choices, counter, " to scratch");

            switch (choice)
            {
                case 1: sc.AcesUsed = true; break;
                case 2: sc.TwosUsed = true; break;
                case 3: sc.ThreesUsed = true; break;
                case 4: sc.FoursUsed = true; break;
                case 5: sc.FivesUsed = true; break;
                case 6: sc.SixesUsed = true; break;

                case 7: sc.ThreeOfKindUsed = true; break;
                case 8: sc.FourOfKindUsed = true; break;
                case 9: sc.FullHouseUsed = true; break;
                case 10: sc.SmallStraightUsed = true; break;
                case 11: sc.LargeStraightUsed = true; break;
                case 12: sc.ChanceUsed = true; break;
                case 13: sc.YahtzeeUsed = true; break;
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
            int choice;

            Console.Write("How many dice do you want to replace? ");
            do
            {
                reroll = Convert.ToInt32(Console.ReadLine());
                if (reroll < 1 || reroll > 5)
                    Console.Write($"{reroll} is not an option. 1 to 5 only, please. Try again: ");
            } while (reroll < 1 || reroll > 5);


            switch (reroll)
            {
                case 1:
                case 2:
                case 3:
                case 4:
                    for (int i = 0; i < reroll; i++)
                    {
                        Console.Write($"Enter which die to reroll: ");
                        do
                        {
                            choice = Convert.ToInt32(Console.ReadLine());
                            if (choice < 1 || choice > 5)
                                Console.Write($"{choice} is not an option. 1 to 5 only, please. Try again: ");
                        } while (choice < 1 || choice > 5);

                        dice[choice - 1] = GetDiceRoll();
                    }
                    break;
                case 5:
                    for (int i = 0; i < 5; i++)
                    {
                        dice[i] = GetDiceRoll();
                    }
                    break;
                default:
                    Console.WriteLine("You messed up!");
                    break;
            }
            Console.Clear();
        }

        public static int SelectFromMenu(int[] ch, int cntr, string prompt)
        {
            string pick = "";
            int choice;
            bool check;

            // ShowArray(ch, cntr);
            Console.Write($"Please enter your choice{prompt}: ");
            do
            {    
                do
                {
                    pick = Console.ReadLine();
                    check = (pick != "1" && pick != "2" && pick != "3" && pick != "4" && pick != "5" &&
                    pick != "6" && pick != "7" && pick != "8" && pick != "9" && pick != "10" &&
                    pick != "11" && pick != "12" && pick != "13" && pick != "14");
                    Console.WriteLine($"Check is {check}.");
                    if (check)
                        Console.WriteLine($"{pick} is not an option. Please choose one of the numbers available: ");
                } while (check);

                choice = Convert.ToInt32(pick);
                if (!InArray(ch, cntr, choice))
                {
                    Console.Write($"{choice} is not a valid selection. Try again: ");
                }
                else
                {
                    break;
                }

            } while (!InArray(ch, cntr, choice));

            return choice;
        }

        public static void ShowArray(int[] ch, int cntr)
        {
            for (int i = 0; i <= cntr; i++)
            {
                Console.WriteLine($"The value is {ch[i]}.");
            }
        }
    }
}