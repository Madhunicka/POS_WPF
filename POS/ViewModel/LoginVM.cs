using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POS.Model;
using CommunityToolkit.Mvvm.Input;
using System.Windows;
using POS.DataContext;
using System.Windows.Controls;
using POS.View;
using System.Windows.Data;


namespace POS.ViewModel
{
    public partial class LoginVM: ObservableObject
    {


        [ObservableProperty]
        public int id;

        [ObservableProperty]
        public string staffName;
        [ObservableProperty]
        public int age;
        [ObservableProperty]
        public int phoneNumber;

        [ObservableProperty]
        public string userName;

        [ObservableProperty]
        public string passwrd;


        [ObservableProperty]
        ObservableCollection<Person> passwords;
        public Person SelectedPerson { get; set; }
        private Person filteredPerson;



        [RelayCommand]
        public void InsertPerson()
        {
            if (string.IsNullOrEmpty(StaffName))
            {
                MessageBox.Show("Please enter relevant details.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            Person p = new Person() { Id = Id, StaffName = StaffName, UserName = UserName, Passwrd = Passwrd, Age = Age, PhoneNumber = PhoneNumber };
            using (var db = new PersonContext())
            {
                db.Passwords.Add(p);
                db.SaveChanges();
                Id = 0;
                StaffName = "";
                UserName = "";
                Passwrd = "";
                Age = 0;
                PhoneNumber = 0;

                MessageBox.Show("Person Successfully Registered");
                
            }

          
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Window currentWindow = Application.Current.MainWindow;
            currentWindow.Close();

            LoadPerson();


        }
       

        [RelayCommand]
        public void UpdatePerson()
        {
            if (SelectedPerson == null)
            {
                MessageBox.Show("Please select a person to update.");
                return;
            }
            using (var db = new PersonContext())
            {

                var selectedPerson = db.Passwords.FirstOrDefault(p => p.Id == SelectedPerson.Id);

                if (selectedPerson != null)
                {

                    selectedPerson.StaffName = StaffName;
                    selectedPerson.UserName = UserName;
                   
                    selectedPerson.Age = Age;
                    selectedPerson.PhoneNumber = PhoneNumber;
                    var filteredPerson = Passwords.FirstOrDefault(p => p.Id == selectedPerson.Id);


                    if (filteredPerson != null)
                    {
                        filteredPerson.StaffName = selectedPerson.StaffName;
                        filteredPerson.UserName = selectedPerson.UserName;
                        filteredPerson.Age = selectedPerson.Age;
                        filteredPerson.PhoneNumber = selectedPerson.PhoneNumber;

                        CollectionViewSource.GetDefaultView(Passwords).Refresh();

                    }

                //    dataGrid.ItemsSource = new List<Person> { filteredPerson };



                    db.SaveChanges();
                    Id = 0;
                    StaffName = "";
                    UserName = "";
          
                    Age = 0;
                    PhoneNumber = 0;



                    LoadPerson();
                }
                else
                {
                    MessageBox.Show("Selected person not found in database.");
                }
            }
        }
        [RelayCommand]
        public void DeletePerson()
        {
            if (SelectedPerson == null)
            {
                MessageBox.Show("Please select a person to delete.");
                return;
            }

            using (var db = new PersonContext())
            {

                var selectedPerson = db.Passwords.FirstOrDefault(p => p.Id == SelectedPerson.Id);

                if (selectedPerson != null)
                {

                    db.Passwords.Remove(selectedPerson);


                    db.SaveChanges();


                    LoadPerson();
                }
                else
                {
                    MessageBox.Show("Selected person not found in database.");
                }
            }
        }

        public Person GetPersonById(int id)
        {
            using (var db = new PersonContext())
            {
                return db.Passwords.FirstOrDefault(p => p.Id == id);
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

            Person password = GetPersonById(id);

            if (password != null)
            {
                MessageBox.Show($"Id: {password.Id}\nStaff_Name: {password.StaffName}\nUser_Name: {password.UserName}\nPassword: {password.Passwrd}\n");
            }
            else
            {
                MessageBox.Show("Person not found.");
            }
        }





        public void LoadPerson()
        {
            using (var db = new PersonContext())
            {

                var list = db.Passwords.OrderByDescending(p => p.Id).ToList();
                Passwords = new ObservableCollection<Person>(list);
            }
        }
        public LoginVM()
        {
            LoadPerson();

        }
    }
}
