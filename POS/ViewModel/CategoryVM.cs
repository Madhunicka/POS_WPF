using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using POS.DataContext;
using POS.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows;

namespace POS.ViewModel
{
    public partial class CategoryVM : ObservableObject

    {

        public class CategoryModel
        {
            public int CategoryId { get; set; }
            public string CategoryName { get; set; }

            public int CategoryQ { get; set; }  
            // Other properties relevant to a category
        }

        public ObservableCollection<CategoryModel> Categories { get; set; }


        [ObservableProperty]
        public int id;

        [ObservableProperty]
        public string categoryName;

        [ObservableProperty]
        public int categoryQ;

       



        [ObservableProperty]
        ObservableCollection<Product> products;
        public Product SelectedProduct { get; set; }

        // private Person filteredPerson;



        [RelayCommand]
        public void Insertcategory()
        {
            if (string.IsNullOrEmpty(CategoryName))
            {
                MessageBox.Show("Please enter relevant details.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            Product p = new Product() { Id = Id, CategoryName = CategoryName, CategoryQ = CategoryQ };
            using (var db = new ProductContext())
            {
                db.Products.Add(p);
                db.SaveChanges();
                Id = 0;
                CategoryName = "";
                CategoryQ = 0;
               

                MessageBox.Show("Category Successfully Added");

            }


          

            LoadPerson();


        }


        [RelayCommand]
        public void UpdatePerson()
        {
            if (SelectedProduct == null)
            {
                MessageBox.Show("Please select a Category to update.");
                return;
            }
            using (var db = new ProductContext())
            {

                var selectedPerson = db.Products.FirstOrDefault(p => p.Id == SelectedProduct.Id);

                if (selectedPerson != null)
                {

                    selectedPerson.CategoryName = CategoryName;
                    selectedPerson.CategoryQ = CategoryQ;

                   
                    var filteredPerson = Products.FirstOrDefault(p => p.Id == selectedPerson.Id);


                    if (filteredPerson != null)
                    {
                        filteredPerson.CategoryName = selectedPerson.CategoryName;
                        filteredPerson.CategoryQ = selectedPerson.CategoryQ;
                     

                        CollectionViewSource.GetDefaultView(Products).Refresh();

                    }

                    //    dataGrid.ItemsSource = new List<Person> { filteredPerson };



                    db.SaveChanges();
                    Id = 0;
                    CategoryName = "";
                    CategoryQ = 0;
                   


                    LoadPerson();
                }
                else
                {
                    MessageBox.Show("Selected category not found in database.");
                }
            }
        }
        [RelayCommand]
        public void DeletePerson()
        {
            if (SelectedProduct == null)
            {
                MessageBox.Show("Please select a Category to delete.");
                return;
            }

            using (var db = new ProductContext())
            {

                var selectedPerson = db.Products.FirstOrDefault(p => p.Id == SelectedProduct.Id);

                if (selectedPerson != null)
                {

                    db.Products.Remove(selectedPerson);


                    db.SaveChanges();
                    Products.Remove(selectedPerson);

                    MessageBox.Show("Category successfully deleted.");

                    // Clear the selected product after deletion.
                    SelectedProduct = null;


                    LoadPerson();
                }
                else
                {
                    MessageBox.Show("Selected Category not found in database.");
                }
            }
        }

        public Product GetPersonById(int id)
        {
            using (var db = new ProductContext())
            {
                return db.Products.FirstOrDefault(p => p.Id == id);
            }
        }
        [RelayCommand]
        private void ReadPerson()
        {
            if (id == 0)
            {
                MessageBox.Show("Please enter a valid ID.");
                return;
            }

            Product product = GetPersonById(id);

            if (product != null)
            {
                MessageBox.Show($"Id: {product.Id}\nCategory_Name: {product.CategoryName}\nCategory_Quantity: {product.CategoryQ}\n");
            }
            else
            {
                MessageBox.Show("Category not found.");
            }
        }





        public void LoadPerson()
        {
            using (var db = new ProductContext())
            {

                var list = db.Products.OrderByDescending(p => p.Id).ToList();
                Products = new ObservableCollection<Product>(list);
            }
        }

       
        public CategoryVM()
        {
            LoadPerson();

        }


    }
}
