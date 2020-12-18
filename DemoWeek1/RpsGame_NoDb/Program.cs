using System;
using System.Collections.Generic;

namespace RpsGame_NoOb
{
    class Program
    {
        static void Main(string[] args)
        {
            
            Choice userChoice;
            bool userResponseParsed;

            List<Player> players = new List<Player>();
            List<Match> matches = new List<Match>();
            List<Round> rounds = new List<Round>();

            // create the Computer that everyone plays against
            Player p1 = new Player() {
                FirstName = "Max",
                LastName = "Headroom"
            };

            players.Add(p1); // adds computer to list of players

            // log in or create a new player, unique firstName and lastName means create a new player, otherwise, grab existing player
            Console.WriteLine("Please enter your first name.\nIf you enter unique first and last name, I will create a new player");
            string userNames = Console.ReadLine();
            string[] userNamesArray = userNames.Split(' ');
            // foreach(var name in userNamesArray) {
            //     Console.WriteLine(name);
            // }

            Player p2 = new Player()
            {
                FirstName = userNamesArray[0],
                LastName = userNamesArray[1]
            };

            players.Add(p2); // adds user to list of players
            Match match = new Match();
            match.Player1 = p1;
            match.Player2 = p2;
            Round round = new Round();

            Console.WriteLine("This is the Official Batch Rock-Paper-Scissors Game!");
            do {
                Console.WriteLine($"Welcome, {p2.FirstName}. Please choose Rock, Paper, or Scissors by typing 0, 1, or 2 and hitting enter.");
                Console.WriteLine("\t0. Rock \n\t1. Paper \n\t2. Scissors");

                string userResponse = Console.ReadLine(); // read user input

                userResponseParsed = Choice.TryParse(userResponse, out userChoice); // parse user input to int

                if (userResponseParsed == false || (int) userChoice > 2 || (int) userChoice < 0) { // give message if user input is invalid
                    Console.WriteLine("Your response is invalid");
                }
            } while (userResponseParsed == false || (int) userChoice > 2 || (int) userChoice < 0); // state conditions for repeating loop

            Console.WriteLine("Starting the game...");
            Random randomNumber = new Random(10); // create random number object
            Choice computerChoice = (Choice) randomNumber.Next(0, 3); // get random number between 0 and 2

            round.Player1Choice = computerChoice;
            round.Player2Choice = userChoice;

            Console.WriteLine($"the computer choice is => {computerChoice}.");

            // compare numbers to see who won
            if (( (int) userChoice == 1 && (int) computerChoice == 0) || // if user won
                ( (int) userChoice == 2 && (int) computerChoice == 1) ||
                ( (int) userChoice == 0 && (int) computerChoice == 2)) {
                Console.WriteLine("Congrats You {the user} WON!");
                round.WinningPlayer = p2;
                rounds.Add(round);
                match.Rounds.Add(round);
                match.RoundWinner(p2);
            } else if (userChoice == computerChoice) { // if players tied
                Console.WriteLine("This game was a tie.");
                // rounds.WinningPlayer is default null
                rounds.Add(round);
                match.Rounds.Add(round);
                match.RoundWinner();
            } else { // if computer won
                Console.WriteLine("We're sorry, the Computer won.");
                round.WinningPlayer = p1;
                rounds.Add(round);
                match.Rounds.Add(round);
                match.RoundWinner(p1);
            }

            /*
            // alternate solution
            while(true){
                Console.WriteLine("\nWelcome to the Rock-Paper-Scissors Game");
                Console.WriteLine("Type \"stop\" if you want to stop.");
                Console.WriteLine("Please choose an option by typing a number: \n\t1. Rock\n\t2. Paper\n\t3. Scissors");
                Console.WriteLine("Type your answer here: ");
                string userResponse = Console.ReadLine().ToLower();
                if(userResponse=="stop"){
                    break;
                }
                if(userResponse=="1"||userResponse=="2"||userResponse=="3"){
                    Dictionary<int,string> answers = new Dictionary<int,string>();
                    //rock beats scissors
                    answers.Add(1,"3");
                    //paper beats rock
                    answers.Add(2,"1");
                    //scissors beats paper
                    answers.Add(3,"2");
                    Random rand = new Random();
                    int computerChoice = rand.Next(3)+1;
                    string response = "The computer chose ";
                    if(userResponse==computerChoice.ToString()){
                        Console.WriteLine("\nResult: You tied!");
                    }
                    else if(answers[computerChoice]==userResponse){
                        switch (computerChoice){
                            case 1: response+="Rock and you picked Scissors, so you lost...";
                            break;
                            case 2: response+="Paper and you picked Rock, so you lost...";
                            break;
                            case 3: response+="Scissors and you picked Paper, so you lost...";
                            break;
                        }
                        Console.WriteLine(response);
                        Console.WriteLine("\nResult: You fool! The computer won!\n");
                    }
                    else{
                        switch (computerChoice){
                            case 1: response+="Rock and you picked Paper, so you won!";
                            break;
                            case 2: response+="Paper and you picked Scissors, so you won!";
                            break;
                            case 3: response+="Scissors and you picked Rock, so you won!";
                            break;
                        }
                        Console.WriteLine(response);
                        Console.WriteLine("\nResult: Good job! You won!\n");
                    }
                }
                else{
                    Console.WriteLine("Ummm, try again...\n\n");
                }
            }
            */
        }

    }
}
