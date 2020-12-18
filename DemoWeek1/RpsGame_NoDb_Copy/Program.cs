using System;
using System.Collections.Generic;

namespace RpsGame_NoOb
{
    class Program
    {
        static void Main(string[] args)
        {
            
            string userResponse;
            int userChoice;
            bool userResponseParsed;
            int numOfGamesWon = 0;
            int numOfGamesLost = 0;
            int numOfGamesPlayed = 0;
            bool newGame = false;
            string continueYorN = "";
            string userName = "";
            string changeUsername = "";

            // prompts user to pick a username
            Console.WriteLine("Enter a username.");
            userName = Console.ReadLine();
            Console.Clear();

            Console.WriteLine("This is the Official Batch Rock-Paper-Scissors Game!");
            do {

                // resets the game
                if (newGame == true) {
                    numOfGamesWon = 0;
                    numOfGamesLost = 0;
                    numOfGamesPlayed = 0;
                    newGame = false; // resets boolean so if statement isn't continuously entered
                }
                if (changeUsername.ToLower() == "y" || changeUsername.ToLower() == "yes") { // prompts user to change username
                    Console.WriteLine("Enter a username.");
                    userName = Console.ReadLine();
                    Console.Clear();
                    changeUsername = ""; // resets string so if statement isn't continuously entered
                }  

                Console.WriteLine("Please choose Rock, Paper, or Scissors by typing 1, 2, or 3 and hitting enter.");
                Console.WriteLine("\t1. Rock \n\t2. Paper \n\t3. Scissors");

                userResponse = Console.ReadLine(); // read user input

                userResponseParsed = int.TryParse(userResponse, out userChoice); // parse user input to int

                if (userResponseParsed == false || userChoice > 3 || userChoice < 1) { // give message if user input is invalid
                    Console.WriteLine("\nYour response is invalid\n");
                }

                Random randomNumber = new Random(10); // create random number object
                int computerChoice = randomNumber.Next(1, 4); // get random number between 1 and 3

                // compare numbers to see who won
                if (userChoice == 2 && computerChoice == 1) { // user won with paper vs. rock
                    Console.WriteLine("\nYou chose paper and the Computer chose rock.");
                    Console.WriteLine($"Congrats {userName} WON!\n");
                    numOfGamesWon++;
                    numOfGamesPlayed++;
                } else if (userChoice == 3 && computerChoice == 2) { // user won with scissors vs. paper
                    Console.WriteLine("\nYou chose scissors and the Computer chose paper.");
                    Console.WriteLine($"Congrats {userName} WON!\n");
                    numOfGamesWon++;
                    numOfGamesPlayed++;
                } else if (userChoice == 1 && computerChoice == 3) { // user won with rock vs. scissors
                    Console.WriteLine("\nYou chose rock and the Computer chose scissors.");
                    Console.WriteLine($"Congrats {userName} WON!\n");
                    numOfGamesWon++;
                    numOfGamesPlayed++;
                } else if (userChoice == computerChoice) { // if players tied
                    Console.WriteLine("\nThis game was a tie.\n");
                    numOfGamesPlayed++;
                } else if (userChoice == 1 && computerChoice == 2) { // computer won with rock vs. paper
                    Console.WriteLine("\nYou chose rock and the Computer chose paper.");
                    Console.WriteLine("We're sorry, the Computer won.\n");
                    numOfGamesLost++;
                    numOfGamesPlayed++;
                } else if (userChoice == 2 && computerChoice == 3) { // computer won with paper vs. scissors
                    Console.WriteLine("\nYou chose paper and the Computer chose scissors.");
                    Console.WriteLine("We're sorry, the Computer won.\n");
                    numOfGamesLost++;
                    numOfGamesPlayed++;
                } else if (userChoice == 3 && computerChoice == 1) { // computer won with scissors vs. rock
                    Console.WriteLine("\nYou chose scissors and the Computer chose rock.");
                    Console.WriteLine("We're sorry, the Computer won.\n");
                    numOfGamesLost++;
                    numOfGamesPlayed++;
                }

                // names winner of best 2 out of 3 games
                if (numOfGamesWon == 2) {
                    Console.WriteLine($"Congrats {userName} won 2 out of 3 games!\n");
                    Console.WriteLine($"The total number of games played is {numOfGamesPlayed}");
                    Console.WriteLine("Would you like to continue playing? (y/n)");
                    continueYorN = Console.ReadLine();

                    // allows user to continue playing or not
                    if (continueYorN.ToLower() == "y" || continueYorN.ToLower() == "yes") {
                        Console.WriteLine("Would you like to log in as a different user? (y/n)"); // prompts user to change username
                        changeUsername = Console.ReadLine();
                        Console.Clear();
                        newGame = true;
                        continue;
                    } else {
                        Console.Clear();
                        break;
                    }

                } else if (numOfGamesLost == 2) {
                    Console.WriteLine("We're sorry, the Computer won 2 out of 3 games.\n");
                    Console.WriteLine($"The total number of games played is {numOfGamesPlayed}");
                    Console.WriteLine("Would you like to continue playing? (y/n)");
                    continueYorN = Console.ReadLine();
                    
                    // allows user to continue playing or not
                    if (continueYorN.ToLower() == "y" || continueYorN.ToLower() == "yes") {
                        Console.WriteLine("Would you like to log in as a different user? (y/n)"); // prompts user to change username
                        changeUsername = Console.ReadLine();
                        Console.Clear();
                        newGame = true;
                        continue;
                    } else {
                        Console.Clear();
                        break;
                    }
                }

            } while (true); // state conditions for repeating loop

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
