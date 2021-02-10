using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yahtzee
{
    public class Player
    {
        public int PlayerNumber { get; set;  }
        public string PlayerName  {  get; set;  }
        public int PlayerScore { get; set; }
        public ScoreCard Card;

        public Player (int playerNumber, string playerName, int playerScore, ScoreCard card)
        {
            PlayerNumber = playerNumber;
            PlayerName = playerName;
            PlayerScore = playerScore;
            Card = card;
        }

    }
}
