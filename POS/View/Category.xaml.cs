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
using System.Windows.Shapes;
using POS.View;
using POS.ViewModel;
using POS.DataContext;
using POS.Model;

namespace POS.View
{
    /// <summary>
    /// Interaction logic for Category.xaml
    /// </summary>
    public partial class Category : Window
    {

        private Product filteredPerson;
        public Category()
        {
            DataContext = new CategoryVM();
            InitializeComponent();
        }

        private int selectedProductId = 0;
        private void Search_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as CategoryVM;
            if (viewModel == null)
                return;
            if (string.IsNullOrEmpty(txtSearchProductId.Text))
            {
                // If the search input is empty, reset the DataGrid to display all products
                dataGrid.ItemsSource = viewModel.Products;
                return;
            }

           if (int.TryParse(txtSearchProductId.Text, out int categoryId))
            {
                selectedProductId = categoryId;

                filteredPerson = GetPersonById(categoryId);

                // Filter the DataGrid based on the provided person ID
                dataGrid.ItemsSource = new List<Product> { filteredPerson };
            }
            else
            {
                MessageBox.Show("Invalid Category ID. Please enter a valid Id.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
        private Product GetPersonById(int id)
        {
            using (var context = new ProductContext())
            {
                return context.Products.FirstOrDefault(p => p.Id == id);
            }
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            if (selectedProductId != 0)
            {
                // Refresh the filteredPerson with updated details
                filteredPerson = GetPersonById(selectedProductId);

                // Update the DataGrid with the refreshed data
                dataGrid.ItemsSource = new List<Product> { filteredPerson };
            }
            
        }
    
        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            var win1 = new MainScreen();
            win1.Show();
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();   
        }
    }
}
