using System;

namespace RpsGame_NoOb
{
    class Player
    {
        public Player(string firstname = "null", string lastname = "null")
        {
            this.firstName = firstname;
            this.LastName = lastname;
        }

        private Guid playerId =  Guid.NewGuid();
        public Guid PlayerId 
        { 
            get
            {
                return playerId;
            } 
        }   

        private int numWins;
        private int numLosses;
        
        private string firstName;
        public string FirstName
        {
            get { return firstName; }
            set 
            { 
                if (value is string && value.Length < 20 && value.Length > 0) {
                    firstName = value;
                } else {
                    throw new Exception("The name you sent is not valid");
                }
            }
        }

        private string lastName;
        public string LastName
        {
            get { return lastName; }
            set 
            { 
                if (value is string && value.Length < 20 && value.Length > 0) {
                    lastName = value;
                } else {
                    throw new Exception("The name you sent is not valid");
                }
            }
        }
        
        
        public void AddWin() {
            numWins++;
        }

        public void AddLoss() {
            numLosses++;
        }

        public int[] GetWinLossRecord() { // create an array to hold the number of wins and losses
            int[] winsAndLosses = new int[2];

            winsAndLosses[0] = numWins; // gets wins
            winsAndLosses[1] = numLosses; // gets losses

            return winsAndLosses; // returns array
        }
        
    }
}