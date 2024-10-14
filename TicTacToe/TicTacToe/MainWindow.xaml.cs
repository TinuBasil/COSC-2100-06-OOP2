//Name: Tinu Basil
//Date Created : 2024 - 10 - 14
//Last Modified : 2024 - 10 - 14
//Description: A game of Tic Tac Toe in which two players, X and O, alternately make moves, keep score, and reset or quit the game.

// Import necessary namespaces for the Windows and Controls functionality
using System.Windows;
using System.Windows.Controls;

// Define the namespace for the Tic-Tac-Toe game
namespace TicTacToe
{
    // Define the partial class for the main window of the application, inheriting from Window
    public partial class MainWindow : Window
    {
        // Variables to store the names of Player X and Player Y
        private string playerXName = string.Empty;
        private string playerYName = string.Empty;

        // Variables to store the scores for Player X, Player Y, and draws
        private int playerXScore = 0;
        private int playerYScore = 0;
        private int drawsScore = 0;

        // Variable to track the current player (X or O)
        private string currentPlayer;

        // Variable to count the number of moves made in the game
        private int moveCount;

        // Constructor for the MainWindow class
        public MainWindow()
        {
            InitializeComponent(); // Initialize the components of the window
            AskForPlayerNames(); // Prompt players for their names
            currentPlayer = "X"; // Start the game with Player X
        }

        // Method to prompt players for their names
        private void AskForPlayerNames()
        {
            // Use an input box to get Player X's name, defaulting to "Player X" if left blank
            playerXName = Microsoft.VisualBasic.Interaction.InputBox("Enter Player X Name:", "Player X Name", "Player X");

            // Use an input box to get Player Y's name, defaulting to "Player Y" if left blank
            playerYName = Microsoft.VisualBasic.Interaction.InputBox("Enter Player Y Name:", "Player Y Name", "Player Y");

            // Display the entered names in the text boxes for Player X and Player Y
            PlayerXNameTextBox.Text = playerXName;
            PlayerYNameTextBox.Text = playerYName;
        }

        // Event handler for when a button in the Tic-Tac-Toe grid is clicked
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = sender as Button; // Identify the clicked button

            // If the clicked button doesn't already have content (is empty)
            if (clickedButton.Content == null)
            {
                clickedButton.Content = currentPlayer; // Set the content of the button to the current player
                moveCount++; // Increment the move count

                // If there's a winner
                if (CheckForWinner())
                {
                    MessageBox.Show($"Player {currentPlayer} wins!"); // Display a message announcing the winner
                    UpdateScores(currentPlayer == "X" ? playerXName : playerYName); // Update the scores based on the winner
                    ResetBoard(); // Reset the game board for a new game
                }
                // If all moves are made but no winner (it's a draw)
                else if (moveCount == 9)
                {
                    MessageBox.Show("It's a draw!"); // Display a draw message
                    UpdateScores("Draw"); // Update the draw score
                    ResetBoard(); // Reset the board
                }
                else
                {
                    // Switch the current player from X to O or vice versa
                    currentPlayer = currentPlayer == "X" ? "O" : "X";
                }
            }
        }

        // Method to check if there's a winner
        private bool CheckForWinner()
        {
            // Check all rows, columns, and diagonals to see if they have the same player's marks
            return (CheckRow(0) || CheckRow(1) || CheckRow(2) || // Check all rows
                    CheckColumn(0) || CheckColumn(1) || CheckColumn(2) || // Check all columns
                    CheckDiagonal()); // Check diagonals
        }

        // Method to check if all buttons in a row are marked by the same player
        private bool CheckRow(int row)
        {
            return (GetButtonContent(row, 0) == currentPlayer && // Compare the content of the first button in the row
                    GetButtonContent(row, 1) == currentPlayer && // Compare the second button
                    GetButtonContent(row, 2) == currentPlayer);  // Compare the third button
        }

        // Method to check if all buttons in a column are marked by the same player
        private bool CheckColumn(int column)
        {
            return (GetButtonContent(0, column) == currentPlayer && // Compare the first button in the column
                    GetButtonContent(1, column) == currentPlayer && // Compare the second button
                    GetButtonContent(2, column) == currentPlayer);  // Compare the third button
        }

        // Method to check if either diagonal is marked by the same player
        private bool CheckDiagonal()
        {
            return (GetButtonContent(0, 0) == currentPlayer && GetButtonContent(1, 1) == currentPlayer && GetButtonContent(2, 2) == currentPlayer) || // Check first diagonal
                   (GetButtonContent(0, 2) == currentPlayer && GetButtonContent(1, 1) == currentPlayer && GetButtonContent(2, 0) == currentPlayer);   // Check second diagonal
        }

        // Method to get the content of a button based on its row and column in the grid
        private string GetButtonContent(int row, int column)
        {
            string buttonName = $"Button{row * 3 + column + 1}"; // Create the button's name dynamically based on its position
            Button button = (Button)this.FindName(buttonName); // Find the button by its name
            return button?.Content?.ToString(); // Return the content of the button, or null if it's empty
        }

        // Event handler for when the Reset button is clicked
        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            playerXScore = 0; // Reset Player X's score to 0
            playerYScore = 0; // Reset Player Y's score to 0
            drawsScore = 0; // Reset the draw score to 0

            // Update the score text boxes with the reset values
            PlayerXScoreTextBox.Text = playerXScore.ToString();
            PlayerYScoreTextBox.Text = playerYScore.ToString();
            DrawsScoreTextBox.Text = drawsScore.ToString();

            ResetBoard(); // Reset the game board
        }

        // Event handler for when the Exit button is clicked
        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close(); // Close the application window
        }

        // Method to update the scores based on the result of the game
        private void UpdateScores(string winner)
        {
            // If Player X wins, increment their score and update the score display
            if (winner == playerXName)
            {
                playerXScore++;
                PlayerXScoreTextBox.Text = playerXScore.ToString();
            }
            // If Player Y wins, increment their score and update the score display
            else if (winner == playerYName)
            {
                playerYScore++;
                PlayerYScoreTextBox.Text = playerYScore.ToString();
            }
            // If it's a draw, increment the draw score and update the draw display
            else
            {
                drawsScore++;
                DrawsScoreTextBox.Text = drawsScore.ToString();
            }
        }

        // Method to reset the game board for a new game
        private void ResetBoard()
        {
            moveCount = 0; // Reset the move count to 0
            currentPlayer = "X"; // Set the current player back to Player X

            // Loop through all the buttons and clear their content
            for (int i = 1; i <= 9; i++)
            {
                Button button = (Button)this.FindName($"Button{i}"); // Find each button by its name
                button.Content = null; // Clear the content of the button
            }
        }
    }
}
