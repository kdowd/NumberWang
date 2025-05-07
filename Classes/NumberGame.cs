using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace NumberWang.Classes
{


    public class NumberGame
    {
        private readonly Button? UserButton;
        private readonly TextBox? UserTextBox;
        private readonly TextBox? FeedBackTextBox;
        private readonly TextBox? DebugTextBox;
        private readonly StackPanel? UserAttemptsContainer;
        private readonly StackPanel? GameButtonsContainer;

        private readonly int Max_Attempts = 5;
        private readonly int _SecretNumber = 0;

        private int Attempts { get; set; } = 0;

        public NumberGame()
        {
            MainWindow MW = (MainWindow)Application.Current.MainWindow;
            UserButton = MW.user_button;
            UserTextBox = MW.user_number;
            DebugTextBox = MW.debug;
            FeedBackTextBox = MW.feedback;
            UserAttemptsContainer = MW.user_attempts;
            GameButtonsContainer = MW.game_choices;

            _SecretNumber = SetSecretNumber();

            if (UserButton != null)
            {
                UserButton.Click += UserButtonEvent;
            }

            if (UserTextBox != null)
            {
                // clear onscreen advice  
                UserTextBox.GotFocus += UserTextBoxEvent;
            }
        }

        public void Replay()
        {
            SetSecretNumber();
            ClearAttempts();
            HideGameButtons();
            UserTextBox!.Text = "Take another shot, Hombre!";

        }

        private int SetSecretNumber()
        {
            Random rnd = new Random();
            int r = rnd.Next(1, 100);

            if (DebugTextBox != null)
            {
                DebugTextBox.Text = r.ToString();
            }

            return r;
        }

        private void UserButtonEvent(object sender, EventArgs e)
        {


            string? UserText = UserTextBox?.Text;
            UserText = UserText ?? string.Empty;

            UserText = UserText.Trim();
            // clear users choice
            UserTextBox!.Text = string.Empty;

            try
            {
                int result = Int32.Parse(UserText);
                checkNumber(result);
            }
            catch (FormatException)
            {
                FeedBackTextBox!.Text = "Please use a number";
            }
        }

        private void UpdateAttempts()
        {
            Attempts++;

            if (Attempts >= Max_Attempts)
            {
                ShowLoser();
                return;
            }

            Image img = new Image();
            img.Width = 40;

            BitmapImage myBitmapImage = new BitmapImage();
            // BitmapImage.UriSource should be in a BeginInit/EndInit block  
            myBitmapImage.BeginInit();
            myBitmapImage.UriSource = new Uri(@"\Images\fire.png", UriKind.Relative);
            myBitmapImage.EndInit();

            img.Source = myBitmapImage;

            if (myBitmapImage != null && img != null)
            {
                UserAttemptsContainer!.Children.Add(img);
            }
        }

        private void ShowLoser()
        {
            FeedBackTextBox!.Text = "Hombre! you're dead.";
            ShowGameButtons();
            ClearAttempts();
        }

        private void ShowGameButtons()
        {

            GameButtonsContainer!.Visibility = Visibility.Visible;


        }

        private void HideGameButtons()
        {

            GameButtonsContainer!.Visibility = Visibility.Hidden;


        }

        private void ClearAttempts()
        {
            Attempts = 0;
            UserAttemptsContainer!.Children.Clear();
        }

        private void checkNumber(int result)
        {
            if (result < _SecretNumber)
            {
                FeedBackTextBox!.Text = "Try to go higher";
                UpdateAttempts();
            }
            else if (result > _SecretNumber)
            {
                FeedBackTextBox!.Text = "Try to go lower";
                UpdateAttempts();
            }
            else
            {
                FeedBackTextBox!.Text = $"Well Done. Number was {_SecretNumber}.";
                ClearAttempts();
            }
        }

        private void UserTextBoxEvent(object sender, EventArgs e)
        {
            FeedBackTextBox!.Text = "?";
        }
    }
}