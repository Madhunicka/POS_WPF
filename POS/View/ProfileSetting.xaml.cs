using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;
using POS.DataContext;
using POS.Model;
using POS.ViewModel;

namespace POS.View
{
    /// <summary>
    /// Interaction logic for ProfileSetting.xaml
    /// </summary>
    /// 
    public partial class ProfileSetting : Window
    {

        private Person filteredPerson;
        public ProfileSetting()
        {
           DataContext = new LoginVM();
            InitializeComponent();

            // Set an empty collection as the initial ItemsSource
           
        }
        private int selectedPersonId = 0;
        private void Search_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(txtSearchPersonId.Text, out int personId))
             {
                selectedPersonId = personId;

                filteredPerson = GetPersonById(personId);

                 // Filter the DataGrid based on the provided person ID
               dataGrid.ItemsSource = new List<Person> { filteredPerson };
             }
             else
             {
                 MessageBox.Show("Invalid Person ID. Please enter a valid number.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
             }


            /*if (int.TryParse(txtSearchPersonId.Text, out int personId))
            {
                filteredPerson = GetPersonById(personId);
                dataGrid.ItemsSource = new List<Person> { filteredPerson };


                if (filteredPerson != null)
                {
                    // Set the DataContext of the window to the filteredPerson
                    this.DataContext = filteredPerson;
                }
                else
                {
                    // If the person is not found, clear the text boxes and show an error message.
                    MessageBox.Show("Person not found.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    ClearTextBoxes();
                }
            }
            else
            {
                MessageBox.Show("Invalid Person ID. Please enter a valid number.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }*/
        }

        // Method to retrieve the person details based on the person ID
        private Person GetPersonById(int personId)
        {
            using (var context = new PersonContext())
            {
                return context.Passwords.FirstOrDefault(p => p.Id == personId);
            }
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            if (selectedPersonId != 0)
            {
                // Refresh the filteredPerson with updated details
                filteredPerson = GetPersonById(selectedPersonId);

                // Update the DataGrid with the refreshed data
                dataGrid.ItemsSource = new List<Person> { filteredPerson };
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            var win1 = new MainScreen();
            win1.Show();
            this.Close();
        }
    }
}
