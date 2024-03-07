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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using POS.Model;
using POS.ViewModel;
using POS.DataContext;
using POS.View;

namespace POS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       
        public MainWindow()
        {
           // DataContext=new PersonContext();
            InitializeComponent();


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }



        private void ToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            if (passwordBox.PasswordChar == '\0')
            {
                passwordBox.PasswordChar = '*'; // Show the password
            }
            else
            {
                passwordBox.PasswordChar = '\0'; // Hide the password
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string username = textUsername.Text;
            string password = passwordBox.Password;

            using (var context = new PersonContext())
            {
                // Query the Passwords table to check if the entered username and password match
                var matchingPassword = context.Passwords.FirstOrDefault(p => p.UserName == username && p.Passwrd == password);

                if (matchingPassword != null)
                {
                    // Load the user's details from the context or any relevant table (if needed)
                   

                    MessageBox.Show("Login successful!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                    var mainScreen = new MainScreen(); // Pass the logged-in user details to MainScreen
                    mainScreen.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Invalid username or password.", "Login Failed", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var win1 = new Registration();
            win1.Show();
            this.Close();
        }
    }
}
