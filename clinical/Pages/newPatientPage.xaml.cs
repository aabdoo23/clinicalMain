﻿using clinical.BaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

using System.Windows.Input;


namespace clinical.Pages
{
    /// <summary>
    /// Interaction logic for newPatientPage.xaml
    /// </summary>
    public partial class newPatientPage : Page
    {
        bool edit=false;
        Patient patient;
        public newPatientPage(Patient toEdit)
        {
            InitializeComponent();
            if (toEdit == null) edit = false;
            else { edit = true; patient = toEdit; }

            if (edit &&toEdit!=null)
            {
                firstNameTextBox.Text = toEdit.FirstName;
                lastNameTextBox.Text= toEdit.LastName;
                if(toEdit.Gender=="Male")maleRB.IsChecked=true;
                else maleRB.IsChecked=false;
                addressTextBox.Text=toEdit.Address.Split(", ")[0];
                cityTextBox.Text=toEdit.Address.Split(", ")[1];
                phoneTextBox.Text = toEdit.PhoneNumber;
                ageTextBox.Text = toEdit.Age().ToString();
            }
        }

        private void save(object sender, MouseButtonEventArgs e)
        {
            string fn = firstNameTextBox.Text;
            string ln= lastNameTextBox.Text;
            string name = fn + " " + ln;
            string gender;
            if (maleRB.IsChecked == true)
            {
                gender = "Male";
            }
            else gender = "Female";
            string address= addressTextBox.Text+", "+cityTextBox.Text;
            string phone= phoneTextBox.Text;
            DateTime bd = globals.CalculateBirthdate(int.Parse(ageTextBox.Text));

            //Patient newPatient = new Patient(globals.generateNewPatientID(),name,address,bd,phone,gender);
            //DB.InsertPatient(newPatient);
        }

        private void addChronic(object sender, MouseButtonEventArgs e)
        {

        }

        private void viewInjury(object sender, RoutedEventArgs e)
        {

        }

        private void viewChronic(object sender, RoutedEventArgs e)
        {

        }

        
    }
}
