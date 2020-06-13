using System;
//  Uncomment  this using statement after you have remove the large Block Comment below 
using System.Drawing;
using System.Windows.Forms;
using Game_Logic_Class;
//  Uncomment  this using statement when you declare any object from Object Classes, eg Board,Square etc.
using Object_Classes;

namespace GUI_Class
{
    public partial class SpaceRaceForm : Form
    {
        // The numbers of rows and columns on the screen.
        const int NUM_OF_ROWS = 7;
        const int NUM_OF_COLUMNS = 8;

        // When we update what's on the screen, we show the movement of a player 
        // by removing them from their old square and adding them to their new square.
        // This enum makes it clear that we need to do both.
        enum TypeOfGuiUpdate { AddPlayer, RemovePlayer };


        public SpaceRaceForm()
        {
            InitializeComponent();
            Board.SetUpBoard();
            ResizeGUIGameBoard();
            SetUpGUIGameBoard();
            SetupPlayersDataGridView();
            DetermineNumberOfPlayers();
            SpaceRaceGame.SetUpPlayers();
            PrepareToPlay();
        }


        /// <summary>
        /// Handle the Exit button being clicked.
        /// Pre:  the Exit button is clicked.
        /// Post: the game is terminated immediately
        /// </summary>
        private void exitButton_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
        
        /// <summary>
        /// Resizes the entire form, so that the individual squares have their correct size, 
        /// as specified by SquareControl.SQUARE_SIZE.  
        /// This method allows us to set the entire form's size to approximately correct value 
        /// when using Visual Studio's Designer, rather than having to get its size correct to the last pixel.
        /// Pre:  none.
        /// Post: the board has the correct size.
        /// </summary>
        private void ResizeGUIGameBoard()
        {
            const int SQUARE_SIZE = SquareControl.SQUARE_SIZE;
            int currentHeight = tableLayoutPanel.Size.Height;
            int currentWidth = tableLayoutPanel.Size.Width;
            int desiredHeight = SQUARE_SIZE * NUM_OF_ROWS;
            int desiredWidth = SQUARE_SIZE * NUM_OF_COLUMNS;
            int increaseInHeight = desiredHeight - currentHeight;
            int increaseInWidth = desiredWidth - currentWidth;
            this.Size += new Size(increaseInWidth, increaseInHeight);
            tableLayoutPanel.Size = new Size(desiredWidth, desiredHeight);

        }// ResizeGUIGameBoard


        /// <summary>
        /// Creates a SquareControl for each square and adds it to the appropriate square of the tableLayoutPanel.
        /// Pre:  none.
        /// Post: the tableLayoutPanel contains all the SquareControl objects for displaying the board.
        /// </summary>
        private void SetUpGUIGameBoard()
        {
            for (int squareNum = Board.START_SQUARE_NUMBER; squareNum <= Board.FINISH_SQUARE_NUMBER; squareNum++)
            {
                Square square = Board.Squares[squareNum];
                SquareControl squareControl = new SquareControl(square, SpaceRaceGame.Players);
                AddControlToTableLayoutPanel(squareControl, squareNum);
            }//endfor

        }// end SetupGameBoard

        private void AddControlToTableLayoutPanel(Control control, int squareNum)
        {
            int screenRow = 0;
            int screenCol = 0;
            MapSquareNumToScreenRowAndColumn(squareNum, out screenRow, out screenCol);
            tableLayoutPanel.Controls.Add(control, screenCol, screenRow);
        }// end Add Control


        /// <summary>
        /// For a given square number, tells you the corresponding row and column number
        /// on the TableLayoutPanel.
        /// Pre:  none.
        /// Post: returns the row and column numbers, via "out" parameters.
        /// </summary>
        /// <param name="squareNumber">The input square number.</param>
        /// <param name="rowNumber">The output row number.</param>
        /// <param name="columnNumber">The output column number.</param>
        private static void MapSquareNumToScreenRowAndColumn(int squareNum, out int screenRow, out int screenCol)
        {            
            screenRow = (55-(squareNum)) / 8;
            if (screenRow % 2 == 0)
            {
                screenCol =(squareNum) % 8;
            }
            else
            {
                screenCol = 7 - (squareNum) % 8;
            }
             
        }//end MapSquareNumToScreenRowAndColumn


        private void SetupPlayersDataGridView()
        {
            // Stop the playersDataGridView from using all Player columns.
            playersDataGridView.AutoGenerateColumns = false;
            // Tell the playersDataGridView what its real source of data is.
            playersDataGridView.DataSource = SpaceRaceGame.Players;

        }// end SetUpPlayersDataGridView



        /// <summary>
        /// Obtains the current "selected item" from the ComboBox
        ///  and
        ///  sets the NumberOfPlayers in the SpaceRaceGame class.
        ///  Pre: none
        ///  Post: NumberOfPlayers in SpaceRaceGame class has been updated
        /// </summary>
        private void DetermineNumberOfPlayers()
        {
            // Store the SelectedItem property of the ComboBox in a string
            string selected = comboBox1.SelectedItem.ToString();
            // Parse string to a number
            int numVal = Convert.ToInt32(selected);
            // Set the NumberOfPlayers in the SpaceRaceGame class to that number
            SpaceRaceGame.NumberOfPlayers = numVal;
        }//end DetermineNumberOfPlayers

        /// <summary>
        /// The players' tokens are placed on the Start square
        /// </summary>
        private void PrepareToPlay()
        {
            //Set all conditions to initial
            //
            
            DetermineNumberOfPlayers();
            UpdatePlayersGuiLocations(TypeOfGuiUpdate.RemovePlayer);
            SpaceRaceGame.Reset();
            UpdatePlayersGuiLocations(TypeOfGuiUpdate.AddPlayer);
            UpdatesPlayersDataGridView();

            //Enable combobox with reset
            //Disable roll button with reset
            //Enable data grid view to be edited with reset
            comboBox1.Enabled = true;
            RollButton.Enabled = false;
            playersDataGridView.Enabled = true;
            stepOne.Checked = false;
            stepMulti.Checked = false;
        }//end PrepareToPlay()


        /// <summary>
        /// Tells you which SquareControl object is associated with a given square number.
        /// Pre:  a valid squareNumber is specified; and
        ///       the tableLayoutPanel is properly constructed.
        /// Post: the SquareControl object associated with the square number is returned.
        /// </summary>
        /// <param name="squareNumber">The square number.</param>
        /// <returns>Returns the SquareControl object associated with the square number.</returns>
        private SquareControl SquareControlAt(int squareNum)
        {
            int screenRow;
            int screenCol;

            // Uncomment the following lines once you've added the tableLayoutPanel to your form. 
            //     and delete the "return null;" 
            //
            MapSquareNumToScreenRowAndColumn(squareNum, out screenRow, out screenCol);
            return (SquareControl)tableLayoutPanel.GetControlFromPosition(screenCol, screenRow);
            
        }


        /// <summary>
        /// Tells you the current square number of a given player.
        /// Pre:  a valid playerNumber is specified.
        /// Post: the square number of the player is returned.
        /// </summary>
        /// <param name="playerNumber">The player number.</param>
        /// <returns>Returns the square number of the player.</returns>
        private int GetSquareNumberOfPlayer(int playerNumber)
        {
            // Code needs to be added here.
            int val;
            val = SpaceRaceGame.Players[playerNumber].Position;
            //     delete the "return -1;" once body of method has been written 
            return val;
        }//end GetSquareNumberOfPlayer


        /// <summary>
        /// When the SquareControl objects are updated (when players move to a new square),
        /// the board's TableLayoutPanel is not updated immediately.  
        /// Each time that players move, this method must be called so that the board's TableLayoutPanel 
        /// is told to refresh what it is displaying.
        /// Pre:  none.
        /// Post: the board's TableLayoutPanel shows the latest information 
        ///       from the collection of SquareControl objects in the TableLayoutPanel.
        /// </summary>
        private void RefreshBoardTablePanelLayout()
        {
            // Uncomment the following line once you've added the tableLayoutPanel to your form.
            tableLayoutPanel.Invalidate(true);
        }

        /// <summary>
        /// When the Player objects are updated (location, etc),
        /// the players DataGridView is not updated immediately.  
        /// Each time that those player objects are updated, this method must be called 
        /// so that the players DataGridView is told to refresh what it is displaying.
        /// Pre:  none.
        /// Post: the players DataGridView shows the latest information 
        ///       from the collection of Player objects in the SpaceRaceGame.
        /// </summary>
        private void UpdatesPlayersDataGridView()
        {
            SpaceRaceGame.Players.ResetBindings();
        }

        /// <summary>
        /// At several places in the program's code, it is necessary to update the GUI board,
        /// so that player's tokens are removed from their old squares
        /// or added to their new squares. E.g. at the end of a round of play or 
        /// when the Reset button has been clicked.
        /// 
        /// Moving all players from their old to their new squares requires this method to be called twice: 
        /// once with the parameter typeOfGuiUpdate set to RemovePlayer, and once with it set to AddPlayer.
        /// In between those two calls, the players locations must be changed. 
        /// Otherwise, you won't see any change on the screen.
        /// 
        /// Pre:  the Players objects in the SpaceRaceGame have each players' current locations
        /// Post: the GUI board is updated to match 
        /// </summary>
        private void UpdatePlayersGuiLocations(TypeOfGuiUpdate typeOfGuiUpdate)
        {
            // Code needs to be added here which does the following:
            //
            //   for each player
            //       determine the square number of the player
            //       retrieve the SquareControl object with that square number
            //       using the typeOfGuiUpdate, update the appropriate element of 
            //          the ContainsPlayers array of the SquareControl object.
            //        
            int i = 0;
            foreach (Player o in SpaceRaceGame.Players)
            {
                
                int currentpos = GetSquareNumberOfPlayer(i);
                SquareControl Squarelocation = SquareControlAt(currentpos);
                if (typeOfGuiUpdate == TypeOfGuiUpdate.AddPlayer && i < SpaceRaceGame.Players.Count)
                {
                    Squarelocation.ContainsPlayers[i] = true;
                }
                else if (typeOfGuiUpdate == TypeOfGuiUpdate.RemovePlayer && i <= SpaceRaceGame.Players.Count)
                {
                    Squarelocation.ContainsPlayers[i] = false;
                }
                i = i + 1;
            }            
            RefreshBoardTablePanelLayout();//must be the last line in this method. Do not put inside above loop.
        } //end UpdatePlayersGuiLocations


        private void UpdatePlayersGuiLocations(TypeOfGuiUpdate typeOfGuiUpdate, Player o)
        {
            // Code needs to be added here which does the following:
            //
            //   for each player
            //       determine the square number of the player
            //       retrieve the SquareControl object with that square number
            //       using the typeOfGuiUpdate, update the appropriate element of 
            //          the ContainsPlayers array of the SquareControl object.
            //        
                 int i = SpaceRaceGame.Players.IndexOf(o);
                int currentpos = GetSquareNumberOfPlayer(i);
                SquareControl Squarelocation = SquareControlAt(currentpos);
                if (typeOfGuiUpdate == TypeOfGuiUpdate.AddPlayer && i < SpaceRaceGame.Players.Count)
                {
                    Squarelocation.ContainsPlayers[i] = true;
                }
                else if (typeOfGuiUpdate == TypeOfGuiUpdate.RemovePlayer && i <= SpaceRaceGame.Players.Count)
                {
                    Squarelocation.ContainsPlayers[i] = false;
                }
            RefreshBoardTablePanelLayout();//must be the last line in this method. Do not put inside above loop.
        }    
         //end UpdatePlayersGuiLocations


        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void NumberOfPlayersText_Click(object sender, EventArgs e)
        {

        }

        private void ResetButton_Click(object sender, EventArgs e)
        {            
           //reset function
            PrepareToPlay();

        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void playersDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void playerBindingSource_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void RollButton_Click(object sender, EventArgs e)
        {           
            //Disable data grid to be edited during game
            //Enable reset button on every round. 
            playersDataGridView.Enabled = false;
            ResetButton.Enabled = true;

            //Show name of winners of the game in a messagebox
            if (SpaceRaceGame.won == true)
            {
                foreach (var item in SpaceRaceGame.winningPlayers)
                {
                    MessageBox.Show(item.Name+ " won the game ");                                      
                }               
            }
            else
            {
                //If checkbox is checked at YES, allow multi step movement
                if (stepMulti.Checked == true)
                {                  
                    UpdatePlayersGuiLocations(TypeOfGuiUpdate.RemovePlayer);
                    SpaceRaceGame.PlayOneRound();
                    UpdatePlayersGuiLocations(TypeOfGuiUpdate.AddPlayer);                   
                }

                //else, allow single step movement
                else if(stepOne.Checked == true){
                    {
                        RollButton.Enabled = true;
                        for (int i = 0; i < SpaceRaceGame.NumberOfPlayers; i++)
                        {
                            UpdatePlayersGuiLocations(TypeOfGuiUpdate.RemovePlayer, SpaceRaceGame.Players[i]);
                            SpaceRaceGame.PlayOneRound(SpaceRaceGame.Players[i]);
                            UpdatePlayersGuiLocations(TypeOfGuiUpdate.AddPlayer, SpaceRaceGame.Players[i]);
                        }
                    }
                }
                //update the players data grid view
                UpdatesPlayersDataGridView();
                exitButton.Enabled = true;
            }            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //disable combobox when a value is selected
            if (comboBox1.SelectedItem.ToString() != "-1") { comboBox1.Enabled = false; }

        }

        private void stepOne_CheckedChanged(object sender, EventArgs e)
        {
            //enable roll button
            RollButton.Enabled = true;
        }

        private void stepMulti_CheckedChanged(object sender, EventArgs e)
        {   //enable roll button
            RollButton.Enabled = true;
        }
    }// end class
}
