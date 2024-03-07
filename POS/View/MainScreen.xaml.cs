using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using POS.Model;


namespace POS.View
{
    /// <summary>
    /// Interaction logic for MainScreen.xaml
    /// </summary>
    public partial class MainScreen : Window
    {
       

        private bool areButtonsVisible = false;
        public MainScreen()
        {
            InitializeComponent();
            
        }

        private void CentralButton_Click(object sender, RoutedEventArgs e)
        {
            if (areButtonsVisible)
            {
                HideButtons();
                areButtonsVisible = false;

            }
            else
            {
                ShowButtons();
                areButtonsVisible = true;

            }

        }
        private void ShowButtons()
        {

            // Use DoubleAnimation to animate the buttons' opacity from 0 to 1
            DoubleAnimation animation = new DoubleAnimation
            {
                From = 0,
                To = 1,
                Duration = TimeSpan.FromSeconds(0.3)
            };

            // Set the visibility of the surrounding buttons to Visible
            button1.Visibility = button2.Visibility = button3.Visibility = button4.Visibility =button5.Visibility= Visibility.Visible;

            // Apply the animation to each button's Opacity property
            button1.BeginAnimation(OpacityProperty, animation);
            button2.BeginAnimation(OpacityProperty, animation);
            button3.BeginAnimation(OpacityProperty, animation);
            button4.BeginAnimation(OpacityProperty, animation);
            button5.BeginAnimation(OpacityProperty, animation);

            button1.RenderTransform = new TranslateTransform();
            button2.RenderTransform = new TranslateTransform();
            button3.RenderTransform = new TranslateTransform();
            button4.RenderTransform = new TranslateTransform();
            button5.RenderTransform = new TranslateTransform();

            double radius = 200;

            // Define the animation duration
            Duration duration = TimeSpan.FromSeconds(0.3);


            double angleIncrement = 360.0 / 5; // 5 buttons
            double currentAngle = 0;

            // Create and apply the translation animations to move the buttons to their designated positions
            AnimateButton(button1, radius * Math.Cos(DegreesToRadians(currentAngle)), radius * Math.Sin(DegreesToRadians(currentAngle)), duration);
            currentAngle += angleIncrement;

            AnimateButton(button2, radius * Math.Cos(DegreesToRadians(currentAngle)), radius * Math.Sin(DegreesToRadians(currentAngle)), duration);
            currentAngle += angleIncrement;

            AnimateButton(button3, radius * Math.Cos(DegreesToRadians(currentAngle)), radius * Math.Sin(DegreesToRadians(currentAngle)), duration);
            currentAngle += angleIncrement;

            AnimateButton(button4, radius * Math.Cos(DegreesToRadians(currentAngle)), radius * Math.Sin(DegreesToRadians(currentAngle)), duration);
            currentAngle += angleIncrement;

            AnimateButton(button5, radius * Math.Cos(DegreesToRadians(currentAngle)), radius * Math.Sin(DegreesToRadians(currentAngle)), duration);
        

    }

        private void HideButtons()
        {
            // Use DoubleAnimation to animate the buttons' opacity from 1 to 0
            DoubleAnimation animation = new DoubleAnimation
            {
                From = 1,
                To = 0,
                Duration = TimeSpan.FromSeconds(0.3)
            };

            // Apply the animation to each button's Opacity property
            button1.BeginAnimation(OpacityProperty, animation);
            button2.BeginAnimation(OpacityProperty, animation);
            button3.BeginAnimation(OpacityProperty, animation);
            button4.BeginAnimation(OpacityProperty, animation);
            button5.BeginAnimation(OpacityProperty, animation);

            double radius = 200;

            // Define the animation duration
            Duration duration = TimeSpan.FromSeconds(0.3);


            double angleIncrement = 360.0 / 5; // 5 buttons
            double currentAngle = 0;


            // Create and apply the translation animations to move the buttons to their designated positions
            AnimateButton(button1, radius * Math.Cos(DegreesToRadians(currentAngle)), radius * Math.Sin(DegreesToRadians(currentAngle)), duration);
            currentAngle += angleIncrement;

            AnimateButton(button2, radius * Math.Cos(DegreesToRadians(currentAngle)), radius * Math.Sin(DegreesToRadians(currentAngle)), duration);
            currentAngle += angleIncrement;

            AnimateButton(button3, radius * Math.Cos(DegreesToRadians(currentAngle)), radius * Math.Sin(DegreesToRadians(currentAngle)), duration);
            currentAngle += angleIncrement;

            AnimateButton(button4, radius * Math.Cos(DegreesToRadians(currentAngle)), radius * Math.Sin(DegreesToRadians(currentAngle)), duration);
            currentAngle += angleIncrement;

            AnimateButton(button5, radius * Math.Cos(DegreesToRadians(currentAngle)), radius * Math.Sin(DegreesToRadians(currentAngle)), duration);

        }

        private void SurroundingButton_Click(object sender, RoutedEventArgs e)
        {
            // Handle the click event for each surrounding button here
            // For example, you can display a message indicating which option was chosen.
            var clickedButton = (Button)sender;

           
            if (clickedButton == button2)
            {
                MessageBoxResult result = MessageBox.Show("Do you want to log out?", "Log Out", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    // If the user clicked Yes, open the MainWindow and close this window
                    var mainWindow = new MainWindow();
                    mainWindow.Show();
                    this.Close();
                }
            }


            else if (clickedButton == button3)
            {
                var profileSettingWindow = new ProfileSetting(); // Pass the logged-in user details to ProfileSetting
               profileSettingWindow.Show();
                this.Close();   
            }
            else if(clickedButton== button1)
            {
                var win2 = new Category();
                win2.Show();    
                this.Close();
            }
            else if(clickedButton==button4)
            {
                var win3 = new ProductCatalog();
                win3.Show();
                this.Close();

            }
        }
        private void AnimateButton(Button button, double translateX, double translateY, Duration duration)
        {
            // Get the TranslateTransform for the button
            TranslateTransform transform = button.RenderTransform as TranslateTransform;

            // Create the translation animation
            DoubleAnimation animationX = new DoubleAnimation
            {
                To = translateX,
                Duration = duration
            };

            DoubleAnimation animationY = new DoubleAnimation
            {
                To = translateY,
                Duration = duration
            };

            // Start the animation
            transform.BeginAnimation(TranslateTransform.XProperty, animationX);
            transform.BeginAnimation(TranslateTransform.YProperty, animationY);

           
        }
    private double DegreesToRadians(double degrees)
    {
        return degrees * Math.PI / 180.0;
    }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
           
            this.Close();
        }

        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            var win1 = new MainWindow();
            win1.Show();
            this.Close();
        }
    }
}
