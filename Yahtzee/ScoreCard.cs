using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yahtzee
{
    public class ScoreCard
    {
        public int Aces;
        public int Twos;
        public int Threes;
        public int Fours;
        public int Fives;
        public int Sixes;
        public int UpperHalfSub;
        public int Bonus;
        public int UpperHalfTotal;

        public int ThreeOfKind;
        public int FourOfKind;
        public int FullHouse;
        public int SmallStraight;
        public int LargeStraight;
        public int Chance;
        public int Yahtzee;
        public int LowerHalfTotal;
        public int Total;

        public bool AcesUsed;
        public bool TwosUsed;
        public bool ThreesUsed;
        public bool FoursUsed;
        public bool FivesUsed;
        public bool SixesUsed;
        public bool ThreeOfKindUsed;
        public bool FourOfKindUsed;
        public bool FullHouseUsed;
        public bool SmallStraightUsed;
        public bool LargeStraightUsed;
        public bool ChanceUsed;
        public bool YahtzeeUsed;

        /*public ScoreCard()
        {
            Aces = 3;
            Twos = 6;
            Threes = 9;
            Fours = 12;
            Fives = 15;
            Sixes = 18;
            UpperHalfSub = 0; // Total of 1's 2's 3's 4's 5's 6's if >= 63 add 35
            Bonus = 0;
            UpperHalfTotal = 0;

            ThreeOfKind = 21;
            FourOfKind = 17;
            FullHouse = 25;
            SmallStraight = 30;
            LargeStraight = 40;
            Chance = 14;
            Yahtzee = 50;
            LowerHalfTotal = 0;
            Total = 0;
        } */
        
        public ScoreCard()
        {
            
            Aces = 0;
            Twos = 0;
            Threes = 0;
            Fours = 0;
            Fives = 0;
            Sixes = 0;
            UpperHalfSub = 0; 
            Bonus = 0;
            UpperHalfTotal = 0;

            ThreeOfKind = 0;
            FourOfKind = 0;
            FullHouse = 0;
            SmallStraight = 0;
            LargeStraight = 0;
            Chance = 0;
            Yahtzee = 0;
            LowerHalfTotal = 0;
            Total = 0; 

            AcesUsed = false;
            TwosUsed = false;
            ThreesUsed = false;
            FoursUsed = false;
            FivesUsed = false;
            SixesUsed = false;
            ThreeOfKindUsed = false;
            FourOfKindUsed = false;
            FullHouseUsed = false;
            SmallStraightUsed = false;
            LargeStraightUsed = false;
            ChanceUsed = false;
            YahtzeeUsed = false;
        } 
    }


}
