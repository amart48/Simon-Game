using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Project2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<int> gameSequence = new List<int>(); // List to store the game sequence
        private int currentStep = 0; // Initialize currentStep variable
        private int score = 0; // Initialize score variable
        private Random R = new Random();
        private int currentDisplayIndex = 0; // Initialize currentDisplayIndex variable

        public MainWindow()
        {
            InitializeComponent();
            InitializeGame();
        }

        private void InitializeGame()
        {
            lblCongrats.Visibility = Visibility.Hidden; // Hide the congratulations label
            btnPlayAgain.Visibility = Visibility.Visible; // Make the start button visible
            lblScore.Content = "0"; // Set the lblScore to 0
            gameSequence.Clear(); // Clear the list
            currentStep = 0;
            currentDisplayIndex = 0;
            score = 0; // Variable to keep track of the score
        }

        private void btnPlayAgain_Click(object sender, RoutedEventArgs e)
        {
            StartGame(); // Click start button to start the game
        }

        private void StartGame()
        {
            gameSequence.Clear(); // Clear the list
            score = 0;
            currentStep = 0;
            currentDisplayIndex = 0;
            lblScore.Content = score.ToString(); // Set lblScore equal to the score variable
            lblCongrats.Visibility = Visibility.Hidden; // Hide the congratulations label
            btnPlayAgain.Visibility = Visibility.Hidden; // Hide the start button
            gameSequence.Add(R.Next(1, 5)); // Add initial random color
            ShowNextColor(); // Call ShowNextColor
        }

        private async void ShowNextColor()
        {
            if (currentDisplayIndex < gameSequence.Count)
            {
                int color = gameSequence[currentDisplayIndex];
                ToggleButtonVisibility(color); // Modified to use single method for toggling
                currentDisplayIndex++;
                await Task.Delay(1000);
                HideAllButtons();
                await Task.Delay(1000);
            }
        }

        private void ToggleButtonVisibility(int color)
        {
            HideAllButtons(); // Ensure all buttons are reset first
            switch (color)
            {
                case 1:
                    redButton.Visibility = Visibility.Visible;
                    redButton2.Visibility = Visibility.Hidden;
                    break;
                case 2:
                    yellowButton.Visibility = Visibility.Visible;
                    yellowButton2.Visibility = Visibility.Hidden;
                    break;
                case 3:
                    greenButton.Visibility = Visibility.Visible;
                    greenButton2.Visibility = Visibility.Hidden;
                    break;
                case 4:
                    blueButton.Visibility = Visibility.Visible;
                    blueButton2.Visibility = Visibility.Hidden;
                    break;
            }
        }

        private void HideAllButtons()
        {
            // Set all "on" buttons to hidden
            redButton.Visibility = yellowButton.Visibility = greenButton.Visibility = blueButton.Visibility = Visibility.Hidden;
            // Set all "off" buttons to visible
            redButton2.Visibility = yellowButton2.Visibility = greenButton2.Visibility = blueButton2.Visibility = Visibility.Visible;
        }

        // Clicking the buttons 
        private void ColorButton_Click(object sender, MouseButtonEventArgs e)
        {
            var button = sender as Image;
            if (button != null && button.Tag != null)
            {
                int selectedColor = Convert.ToInt32(button.Tag);
                CheckUserInput(selectedColor); // Call CheckUserInput with selectedColor as the parameter
            }
        }

        private void CheckUserInput(int selectedColor)
        {
            if (gameSequence[currentStep] == selectedColor)
            {
                currentStep++;
                if (currentStep == currentDisplayIndex)
                {
                    if (currentStep == gameSequence.Count)
                    {
                        score++;
                        lblScore.Content = score.ToString();
                        if (score == 20)
                        {
                            lblCongrats.Visibility = Visibility.Visible;
                            btnPlayAgain.Visibility = Visibility.Visible;
                        }
                        else
                        {
                            gameSequence.Add(R.Next(1, 5)); // Add new color
                            currentStep = 0;
                            currentDisplayIndex = 0;
                            ShowNextColor(); // Start showing from the first color
                        }
                    }
                    else
                    {
                        ShowNextColor(); // Show next color in sequence
                    }
                }
            }
            else
            {
                MessageBox.Show("Wrong sequence! Game over.");
                btnPlayAgain.Visibility = Visibility.Visible;
                InitializeGame();
            }
        }
    }
}
