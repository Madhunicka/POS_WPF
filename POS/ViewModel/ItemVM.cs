using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using POS.DataContext;
using POS.Model;
using POS.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows;
using POS.DataContext;
using POS.View;
using POS.Model;

namespace POS.ViewModel
{
    public partial class ItemVM: ObservableObject
    {
       
        //private CategoryVM.CategoryModel selectedProduct;


        //public CategoryVM.CategoryModel SelectedProduct
        //{
        //    get { return selectedProduct; }
        //    set
        //    {
        //        SetProperty(ref selectedProduct, value);
        //        // Raise PropertyChanged for the SelectedProduct property
        //        OnPropertyChanged(nameof(SelectedProduct));
        //    }

        //}


        [ObservableProperty]
        public CategoryVM.CategoryModel selectedProduct;


        [ObservableProperty]
        public int id;

        [ObservableProperty]
        public string proName;

        [ObservableProperty]
        public int quantity;

        [ObservableProperty]
        public int priceQ;

        [ObservableProperty]
        public int discount;

        [ObservableProperty]
        public string categoryName;

        [ObservableProperty]
        ObservableCollection<Item> items;

        [ObservableProperty]
        public Item selectedItem;

        [ObservableProperty]
        public ObservableCollection<Product> products;
       

        [RelayCommand]
        public void InsertItem()
        {
            if (string.IsNullOrEmpty(ProName))
            {
                MessageBox.Show("Please enter relevant details.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (SelectedProduct == null)
            {
                MessageBox.Show("Please select a category.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

                
            Item p = new Item()
            {
                Id = Id,
                ProName = ProName,
                Quantity = Quantity,
                PriceQ = PriceQ,
                Discount = Discount,
                CategoryName = SelectedProduct != null ? SelectedProduct.CategoryName : string.Empty
            };
            using (var db = new ItemContext())
            {

                db.Items.Add(p);
                db.SaveChanges();
                Id = 0;
                ProName = "";
                Quantity = 0;
                PriceQ = 0;
                Discount = 0;
               


                MessageBox.Show("Category Successfully Added");

            }
            LoadPerson();
        }


        [RelayCommand]
        public void UpdateItem()
        {
            if (SelectedItem == null )
            {
                MessageBox.Show("Please select a Product to update.");
                return;
            }
            using (var db = new ItemContext())
            {

                var selectedItem = db.Items.FirstOrDefault(p => p.Id == SelectedItem.Id);
                if (selectedItem != null)
                {

                    selectedItem.ProName = ProName;
                    selectedItem.Quantity = Quantity;
                    selectedItem.PriceQ = PriceQ;
                    selectedItem.Discount = Discount;



                    var filteredPerson = Items.FirstOrDefault(p => p.Id == selectedItem.Id);


                    if (filteredPerson != null)
                    {
                        filteredPerson.ProName = selectedItem.ProName;
                        filteredPerson.Quantity = selectedItem.Quantity;
                        filteredPerson.PriceQ = selectedItem.PriceQ;
                        filteredPerson.Discount = selectedItem.Discount;


                        CollectionViewSource.GetDefaultView(Items).Refresh();

                    }

                    db.SaveChanges();
                    Id = 0;
                    ProName = "";
                    Quantity = 0;
                    PriceQ = 0;
                    Discount = 0;



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
            if (SelectedItem == null)
            {
                MessageBox.Show("Please select a Category to delete.");
                return;
            }

            using (var db = new ItemContext())
            {

                var selectedPerson = db.Items.FirstOrDefault(p => p.Id == SelectedItem.Id);

                if (selectedPerson != null)
                {
                    db.Items.Remove(selectedPerson);


                    db.SaveChanges();
                    Items.Remove(selectedPerson);

                    MessageBox.Show("Category successfully deleted.");

                   
                    SelectedItem = null;

                    LoadPerson();
                }
                else
                {
                    MessageBox.Show("Selected Category not found in database.");
                }
            }
        }

        public Item GetPersonById(int id)
        {
            using (var db = new ItemContext())
            {
                return db.Items.FirstOrDefault(p => p.Id == id);
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

            Item item = GetPersonById(id);

            if (item != null)
            {
                MessageBox.Show($"Product_Id: {item.Id}\nProduct_Name: {item.ProName}\nQuantity: {item.Quantity}\nPrice_Per_Quantity: {item.PriceQ}");
            }
            else
            {
                MessageBox.Show("Category not found.");
            }
        }





        public void LoadPerson()
        {
            using (var db = new ItemContext())
            {

                var list = db.Items.OrderByDescending(p => p.Id).ToList();
                Items = new ObservableCollection<Item>(list);
            }
        }

        public void LoadCat()
        {
            using (var db = new ProductContext())
            {

                var list = db.Products.ToList();
                Products = new ObservableCollection<Product>(list);
            }
        }


        public ItemVM()
        {
            LoadPerson();
            LoadCat();

        }



    }
}
