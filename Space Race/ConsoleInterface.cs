using System;
//DO NOT DELETE the two following using statements *********************************
using Game_Logic_Class;
using Object_Classes;


namespace Space_Race
{
    class Console_Class
    {
        /// <summary>
        /// Algorithm below currently plays only one game
        /// 
        /// when have this working correctly, add the abilty for the user to 
        /// play more than 1 game if they choose to do so.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            DisplayIntroductionMessage();
            Board.SetUpBoard();
            /*                    
             Set up the board in Board class (Board.SetUpBoard)        
             Determine number of players - initally play with 2 for testing purposes 
             Create the required players in Game Logic class
              and initialize players for start of a game             

             loop  until game is finished           
                call PlayGame in Game Logic class to play one round
                Output each player's details at end of round
             end loop
             Determine if anyone has won
             Output each player's details at end of the game
           */
            bool playagain = true;
            do
            {
            Console.WriteLine("Press Enter to play a round \n");

                //Clear players bindinglist
                //Clear all previous winners from winningPlayers
                //setup number of players
                //setup each pieces
                SpaceRaceGame.Players.Clear();
                SpaceRaceGame.winningPlayers.Clear();
                SpaceRaceGame.NumberOfPlayers = EnterNumberOfPlayers();
                SpaceRaceGame.SetUpPlayers();          
                        
                //loop until somebody wins the game
                //the game is displayed one round at a time
                //loops everytime the user types Y to playagain
                do { SpaceRaceGame.PlayOneRound();Console.ReadKey(); } while (SpaceRaceGame.won == false);
                playagain = PlayAgain();
            } while (playagain == true);      
           

            PressEnter();



        }//end Main

   
        /// <summary>
        /// Display a welcome message to the console
        /// Pre:    none.
        /// Post:   A welcome message is displayed to the console.
        /// </summary>
        static void DisplayIntroductionMessage()
        {
            Console.WriteLine("Welcome to Space Race.\n");
        } //end DisplayIntroductionMessage

        /// <summary>
        /// Displays a prompt and waits for a keypress.
        /// Pre:  none
        /// Post: a key has been pressed.
        /// </summary>
        static void PressEnter()
        {
            Console.Write("\nPress Enter to terminate program ...");
            Console.ReadLine();
        } // end PressAny

        static int EnterNumberOfPlayers()
        {
            Console.Write("How many players are going to play?");
            string input;
            int output;
            input = Console.ReadLine();
            if (int.TryParse(input, out output))
            {
                if (output > 6 || output < 2)
                {
                    Console.WriteLine("Please enter value within the range of 2 and 6");
                    EnterNumberOfPlayers();
                }
            }
            else { Console.WriteLine("Please enter a valid number"); EnterNumberOfPlayers(); }
            return output;
        }

        static bool PlayAgain()
        {
            Console.WriteLine("Do you want to play again? Y/y for yes OR any other key for no.");
            string input;
            input = Console.ReadLine();
            bool yesNo = false;
            if (input == "Y" || input == "y") { yesNo = true; SpaceRaceGame.Reset(); }
            else{ yesNo = false; }
            return yesNo;
        }


    }//end Console class
}
