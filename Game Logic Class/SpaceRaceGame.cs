using System.Drawing;
using System.ComponentModel;
using Object_Classes;
using System;

namespace Game_Logic_Class
{
    public static class SpaceRaceGame
    {
        // Minimum and maximum number of players.
        public const int MIN_PLAYERS = 2;
        public const int MAX_PLAYERS = 6;

        private static int numberOfPlayers = 2;  //default value for test purposes only 
        public static int NumberOfPlayers
        {
            get
            {
                return numberOfPlayers;
            }
            set
            {
                numberOfPlayers = value;
            }
        }

        public static string[] names = { "One", "Two", "Three", "Four", "Five", "Six" };  // default values

        // Only used in Part B - GUI Implementation, the colours of each player's token
        private static Brush[] playerTokenColours = new Brush[MAX_PLAYERS] { Brushes.Yellow, Brushes.Red,
                                                                       Brushes.Orange, Brushes.White,
                                                                      Brushes.Green, Brushes.DarkViolet};
        /// <summary>
        /// A BindingList is like an array which grows as elements are added to it.
        /// </summary>
        private static BindingList<Player> players = new BindingList<Player>();
        public static BindingList<Player> Players
        {
            get
            {
                return players;
            }
        }

        // The pair of die
        private static Die die1 = new Die(), die2 = new Die();

        /// <summary>
        /// Set up the conditions for this game as well as
        ///   creating the required number of players, adding each player 
        ///   to the Binding List and initialize the player's instance variables
        ///   except for playerTokenColour and playerTokenImage in Console implementation.
        ///   
        ///     
        /// Pre:  none
        /// Post:  required number of players have been initialsed for start of a game.
        /// </summary>
        public static void SetUpPlayers()
        {

            //      create a new player object
            //      initialize player's instance variables for start of a game
            //      add player to the binding list

            for (int i = 0; i < numberOfPlayers; i++)
            {
                Player P = new Player("")
                {
                    Name = names[i],
                    Position = 0,
                    PlayerTokenColour = playerTokenColours[i],
                    RocketFuel = Player.INITIAL_FUEL_AMOUNT,
                    HasPower = true,
                    Location = Board.Squares[Board.START_SQUARE_NUMBER]
                };
                players.Add(P);
            }
        }

        /// <summary>
        ///  Plays one round of a game
        /// </summary>

        public static bool won = false;
        public static BindingList<Player> winningPlayers = new BindingList<Player>();
        public static void PlayOneRound()
        {
            foreach (Player o in players)
            {
                //Check if a player reached the finish point
                if (o.AtFinish == true)
                {
                    won = true;
                    winningPlayers.Add(o);
                }

                else if (o.HasPower == false)
                {
                    Console.WriteLine("OHHH NOOOO.. {0} has run out of power...", o.Name);
                }
                //if player has not reached finish and still has power, then roll
                else
                {
                    if (won != true || o.HasPower == true)
                    {
                        o.Play(die1, die2);
                    }
                }

                //Displays player stats at any point throughout the game
                Console.WriteLine("Player {0} is at {1} location with fuel remaining {2}", o.Name, o.Position, o.RocketFuel);
                
            }
            Console.WriteLine("**************************************************************************");
            //Print the name of the winners at the end of the game
            if (won == true)
            {
                Console.WriteLine("The winners are :");
                foreach(Player winners in winningPlayers)
                { Console.WriteLine(winners.Name); }              
            }          
        }

        public static void PlayOneRound(Player o)
        {
                //Check if a player reached the finish point
                if (o.AtFinish == true)
                {
                    won = true;
                    winningPlayers.Add(o);
                }

                else if (o.HasPower == false)
                {
                    Console.WriteLine("OHHH NOOOO.. {0} has run out of power...", o.Name);
                }
                //if player has not reached finish and still has power, then roll
                else
                {
                    if (won != true || o.HasPower == true)
                    {
                        o.Play(die1, die2);
                    }
                }

                //Displays player stats at any point throughout the game
                Console.WriteLine("Player {0} is at {1} location with fuel remaining {2}", o.Name, o.Position, o.RocketFuel);
            }  

        //Function to initialise Player class to starting conditions. 
            public static void Reset()
        {
            won = false;
            players.Clear();
            winningPlayers.Clear();            
            SetUpPlayers();
        }//end SnakesAndLadders
    }
}