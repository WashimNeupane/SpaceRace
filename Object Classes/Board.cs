using System.Diagnostics;
using System;

namespace Object_Classes {
    /// <summary>
    /// Models a game board for Space Race consisting of three different types of squares
    /// 
    /// Ordinary squares, Wormhole squares and Blackhole squares.
    /// 
    /// landing on a Wormhole or Blackhole square at the end of a player's move 
    /// results in the player moving to another square
    /// 
    /// </summary>
    public static class Board {
        /// <summary>
        /// Models a game board for Space Race consisting of three different types of squares
        /// Ordinary squares, Wormhole squares and Blackhole squares.
        /// landing on a Wormhole or Blackhole square at the end of a player's move 
        /// results in the player moving to another square
        /// </summary>
        
        public const int NUMBER_OF_SQUARES = 56;
        public const int START_SQUARE_NUMBER = 0;
        public const int FINISH_SQUARE_NUMBER = NUMBER_OF_SQUARES - 1;

        private static Square[] squares = new Square[NUMBER_OF_SQUARES];

        public static Square[] Squares {
            get {
                Debug.Assert(squares != null, "squares != null",
                   "The game board has not been instantiated");
                return squares;
            }
        }

        public static Square StartSquare {
            get {
                return squares[START_SQUARE_NUMBER];
            }
        }


        /// <summary>
        ///  Eight Wormhole squares.
        /// Each row represents a Wormhole square number, the square to jump forward to and the amount of fuel consumed in that jump.
        /// For example {2, 22, 10} is a Wormhole on square 2, jumping to square 22 and using 10 units of fuel
        /// </summary>
        private static int[,] wormHoles =
        {
            {2, 22, 10},
            {3, 9, 3},
            {5, 17, 6},
            {12, 24, 6},
            {16, 47, 15},
            {29, 38, 4},
            {40, 51, 5},
            {45, 54, 4}
        };

        /// <summary>
        ///  Eight Blackhole squares.
        ///  
        /// Each row represents a Blackhole square number, the square to jump back to and the amount of fuel consumed in that jump.
        /// 
        /// For example {10, 4, 6} is a Blackhole on square 10, jumping to square 4 and using 6 units of fuel
        /// 
        /// </summary>
        private static int[,] blackHoles =
        {
            {10, 4, 6},
            {26, 8, 18},
            {30, 19, 11},
            {35,11, 24},
            {36, 34, 2},
            {49, 13, 36},
            {52, 41, 11},
            {53, 42, 11}
        };


        /// <summary>
        /// Parameterless Constructor
        /// Initialises a board consisting of a mix of Ordinary Squares,
        ///     Wormhole Squares and Blackhole Squares.
        /// 
        /// Pre:  none
        /// Post: board is constructed
        /// </summary>
        public static void SetUpBoard() {

            // Create the 'start' square where all players will start.
            squares[START_SQUARE_NUMBER] = new Square("Start", START_SQUARE_NUMBER);

            //  Create the main part of the board, squares 1 .. 54

            //Define two arrays whose elements represents the index of blackhole/wormhole square. 
            int[] blkSquare = { 10, 26, 30, 35, 36, 49, 52, 53}, wrmSquare = { 2, 3, 5, 12, 16, 29, 40, 45 } ;
           
            //Attribute each index positions as either Square, Blackhole Square or Wormhole Square. 
            //If blackhole, the information of this particular blackhole is retrived by searching the blackHoles array. 
            //Same with worm holes. 
            //If index does not belong to either, then the square is a normal square with specific properties. 
            for (int i = START_SQUARE_NUMBER+1; i < FINISH_SQUARE_NUMBER; i++)
            {
                if (Array.IndexOf(blkSquare, i) > -1)                //checks if index falls in blkSquare array
                {
                    int index = Array.IndexOf(blkSquare, i);          
                    squares[i] = new BlackholeSquare(i.ToString(), blackHoles[index,0],blackHoles[index,1],blackHoles[index,2]);
                }
                else if (Array.IndexOf(wrmSquare, i) > -1)          //checks if index falls in wrmSquare array
                {
                    int index = Array.IndexOf(wrmSquare, i);     
                    squares[i] = new WormholeSquare(i.ToString(), wormHoles[index,0],wormHoles[index,1],wormHoles[index,2]);
                }
                else
                {
                    squares[i] = new Square(i.ToString(), i);
                }                
                }
           
            //

            // Create the 'finish' square.
            squares[FINISH_SQUARE_NUMBER] = new Square("Finish", FINISH_SQUARE_NUMBER);
        } // end SetUpBoard

        /// <summary>
        /// Finds the destination square and the amount of fuel used for either a 
        /// Wormhole or Blackhole Square.
        /// 
        /// pre: squareNum is either a Wormhole or Blackhole square number
        /// post: destNum and amount are assigned correct values.
        /// </summary>
        /// <param name="holes">a 2D array representing either the Wormholes or Blackholes squares information</param>
        /// <param name="squareNum"> a square number of either a Wormhole or Blackhole square</param>
        /// <param name="destNum"> destination square's number</param>
        /// <param name="amount"> amont of fuel used to jump to the deestination square</param>
        private static void FindDestSquare(int[,] holes, int squareNum, out int destNum, out int amount) {
            //const int start = 0, exit = 1, fuel = 2;
            destNum = 0; amount = 0;

            // takes a hole, either blackhole or wormhole and return the destination and amount of fuel consumed
            // for the particular square number. 

            destNum = holes[squareNum,1];
            amount = holes[squareNum, 2];
        } //end FindDestSquare

    } //end class Board
}