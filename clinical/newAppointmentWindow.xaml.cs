using clinical.BaseClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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

namespace clinical
{
    /// <summary>
    /// Interaction logic for newAppointmentWindow.xaml
    /// </summary>
    public partial class newAppointmentWindow : Window
    {
        private ICollectionView dataView;
        Patient selectedPatient = null;
        User selectedPhysician = null;
        DateTime selectedDateTime = DateTime.Now;
        DateTime lastSelectedDT = DateTime.Now;
        AppointmentType selectedType;
        
        List<string> times = new List<string>();


        public newAppointmentWindow()
        {
            InitializeComponent();

            for(int i = 16; i <= 22; i++) //slots here
            {
                times.Add($"{i}:00");
                times.Add($"{i}:30");

            }

            List<Patient> list = DB.GetAllPatients();
            foreach (Patient pat in list)
            {
                User phys = DB.GetUserById(pat.PhysicianID);
                pat.PhysicianName = phys.FirstName + " " + phys.LastName;
            }
            allPatientsDataGrid.ItemsSource = list;
            dataView = CollectionViewSource.GetDefaultView(allPatientsDataGrid.ItemsSource);
            textBoxFilter.TextChanged += textBoxFilter_TextChanged;

            datePicker.SelectedDate=DateTime.Now;
            List<AppointmentType> types = DB.GetAllAppointmentTypes();

            visitTypeCB.ItemsSource = types;
            timePicker.ItemsSource = times;
            timePicker.SelectedIndex = 0;
            visitTypeCB.SelectedIndex = 0;
            

            physicianPicker.ItemsSource = DB.GetAllPhysiotherapists();

        }

        private void PackIconMaterial_MouseDown(object sender, MouseButtonEventArgs e)
        {
            GetWindow(this).Close();

        }

        private void PackIconMaterial_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            GetWindow(this).WindowState = WindowState.Minimized;

        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        Package selectedPackage;

        private void view(object sender, RoutedEventArgs e)
        {
            selectedPatient = (Patient)allPatientsDataGrid.SelectedItem;
            patientName.Text = selectedPatient.FirstName + " " + selectedPatient.LastName;
            physicianName.Text = selectedPatient.PhysicianName;
            selectedPhysician= DB.GetUserById(selectedPatient.PhysicianID);
            selectedPackage = DB.GetPackageById(selectedPatient.ActivePackageID);
            packageTB.Text = selectedPackage.ToString();
            patientDueTB.Text=selectedPatient.DueAmount.ToString();
            handleFinances();
            Refresh();


        }

        private void textBoxFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            dataView.Filter = item => FilterItem(item, textBoxFilter.Text);

        }

        private bool FilterItem(object item, string filterText)
        {
            if (string.IsNullOrWhiteSpace(filterText))
            {
                return true; // No filter, show all items
            }

            foreach (var property in item.GetType().GetProperties())
            {
                var cellValue = property.GetValue(item);
                if (cellValue != null && cellValue.ToString().Contains(filterText, StringComparison.OrdinalIgnoreCase))
                {
                    return true; // Found a match in any column
                }
            }

            return false; // No match found
        }

        private void saveClicked(object sender, MouseButtonEventArgs e)
        {
            if(selectedPatient == null) { return; }
            MessageBoxResult result = MessageBox.Show("Book "+selectedPatient.FullName+" a reservation on "+ selectedDateTime.ToString("g"), "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

            // Check the user's response
            if (result == MessageBoxResult.Yes)
            {
                int id = globals.generateNewVisitID(selectedPatient.PatientID, selectedDateTime);
                AppointmentType ap = (AppointmentType)visitTypeCB.SelectedItem;
                Visit visit = new Visit(id, selectedPhysician.UserID, selectedPatient.PatientID, selectedPackage.PackageID, selectedDateTime, 1, "",selectedPatient.Height,selectedPatient.Weight,false,ap.TypeID);

                globals.ScheduleVisit(visit);

                //updating patient

                selectedPatient.DueAmount = Double.Parse(patientDueTB.Text);

                if(selectedPatient.RemainingSessions>0)selectedPatient.RemainingSessions--;
                if (selectedPatient.RemainingSessions == 0)
                {
                    selectedPatient.ActivePackageID = 0;
                }
                DB.UpdatePatient(selectedPatient);

                double paid = Double.Parse(paidTB.Text.Trim());

                Payment payment = new Payment(globals.generateNewPaymentID(selectedPatient.PatientID, DateTime.Now), paid, DateTime.Now, selectedPhysician.UserID, selectedPatient.PatientID);
                DB.InsertPayment(payment);

                Window.GetWindow(this).Close();
            }


            
        }

        private void dateChanged(object sender, SelectionChangedEventArgs e)
        {
            lastSelectedDT = selectedDateTime;
            selectedDateTime = (DateTime)datePicker.SelectedDate;
            Refresh();
        }

        private void timeChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((string)timePicker.SelectedItem==null) return;
            string s= (string)timePicker.SelectedItem;
            string hr = "",min="";
            hr+= s[0];
            hr+= s[1];
            min += s[3];
            min += s[4];

            lastSelectedDT = selectedDateTime;
            selectedDateTime = new DateTime(selectedDateTime.Year, selectedDateTime.Month, selectedDateTime.Day, int.Parse(hr), int.Parse(min), 0);

        }
        private void first_avail(object sender, RoutedEventArgs e)
        {

            if (selectedPatient == null ||selectedPhysician==null)
            {
                return;
            }
            DateTime firstFree=globals.FindFirstFreeSlot(selectedPhysician.UserID, DateTime.Today);
            datePicker.IsEnabled = false;
            timePicker.IsEnabled = false;
            datePicker.SelectedDate = firstFree;
            timePicker.SelectedItem = firstFree.ToString("HH:mm");

            Refresh();
        }

        private void not_first_avail(object sender, RoutedEventArgs e)
        {
            selectedDateTime = lastSelectedDT;
            datePicker.IsEnabled = true;
            timePicker.IsEnabled = true;
        }

        private void physicianChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedPhysician= (User)(physicianPicker.SelectedItem);
            physicianName.Text="Dr. "+selectedPhysician.FirstName+" "+selectedPhysician.LastName;
            Refresh();
        }

        private void typeChanged(object sender, SelectionChangedEventArgs e)
        {

            handleFinances();
        }

        void Refresh()
        {
            if (selectedPhysician == null) return;
            List<string> availTimes= globals.GetAvailableTimeSlotsOnDay(datePicker.SelectedDate.Value, selectedPhysician.UserID);
            if (availTimes.Count == 0) {
                datePicker.SelectedDate.Value.AddDays(1);
                }
            timePicker.ItemsSource = availTimes;
        }

        //current visit due amount:
        //1- patient has a package active, thus all visits are rendered paid and only patient due amount is displayed
        //2- patient doesn't have an active package, thus each visit is controlled by visit type, and patient due amount is calculated by: patientDue+ visitDue

        void handleFinances()
        {
            selectedType = (AppointmentType) visitTypeCB.SelectedItem;
            if (selectedPatient != null) //selected a patient? YES
            {
                if (selectedPackage == null || selectedPackage.PackageID == 0 || selectedPatient.RemainingSessions==0) //patient has a package? NO
                {
                    double price = selectedType.Cost;
                    selectedPatient.ActivePackageID = 0;
                    double patientDueAmount = price + selectedPatient.DueAmount  ;
                    double visitDueAmount = price;

                    if (paidTB.Text != null && paidTB.Text != "")
                    {
                        patientDueAmount-=Double.Parse(paidTB.Text.Trim());
                        visitDueAmount-=Double.Parse(paidTB.Text.Trim());
                    }

                    visitDueTB.Text = visitDueAmount.ToString();
                    patientDueTB.Text = patientDueAmount.ToString();


                }
                else
                {
                    

                    double patientDueAmount = selectedPatient.DueAmount;
                    double visitDueAmount = 0;

                    if (paidTB.Text != null && paidTB.Text != "")
                    {
                        patientDueAmount -= Double.Parse(paidTB.Text.Trim());
                        visitDueAmount -= Double.Parse(paidTB.Text.Trim());
                    }

                    visitDueTB.Text = visitDueAmount.ToString();
                    patientDueTB.Text = patientDueAmount.ToString();
                }
            }
        }

        private void paidTxtChanged(object sender, TextChangedEventArgs e)
        {
            handleFinances();
        }
    }
}
