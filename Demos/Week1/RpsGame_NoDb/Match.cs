using System;
using System.Collections.Generic;

namespace RpsGame_NoOb
{
    class Match
    {
        private Guid matchId = Guid.NewGuid();
        public Guid MatchId { get {return matchId;} }
        public Player Player1 { get; set; } // always computer
        public Player Player2 { get; set; } // always user
        public List<Round> Rounds = new List<Round>();
        private int p1RoundWins { get; set; } // how many rounds the player won
        private int p2RoundWins { get; set; }
        private int ties { get; set; }

        // below are just methods

        /// <summary>
        /// This method taks an optional Player parameter and increments the amount of wins for that player
        /// </summary>
        /// <param name="p"></param>
        public void RoundWinner(Player p = null) {
            if (p.PlayerId == Player1.PlayerId) {
                p1RoundWins++;
            } else if (p.PlayerId == Player2.PlayerId) {
                p2RoundWins++;
            } else {
                ties++;
            }
        }

        public Player MatchWinner() {
            if (p1RoundWins == 2) {
                return Player1;
            } else if (p2RoundWins == 2) {
                return Player2;
            } else {
                return null;
            }
        }
    }
}