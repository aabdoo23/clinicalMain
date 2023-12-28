using clinical.BaseClasses;
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

namespace clinical.Pages.adminSettingsNewPages
{
    /// <summary>
    /// Interaction logic for newTreatmentPlan.xaml
    /// </summary>
    public partial class newTreatmentPlan : Page
    {
        List<Injury> injuries = DB.GetAllInjuries();
        public newTreatmentPlan()
        {
            InitializeComponent();
            IDTextBox.Text = new Random().Next(1000).ToString();
            injuriesCB.ItemsSource= injuries;
        }
        bool viewing=false;
        public newTreatmentPlan(TreatmentPlan plan)
        {
            InitializeComponent();
            
            viewing=true;
            IDTextBox.IsEnabled = false;
            IDTextBox.Text = plan.PlanID.ToString();
            injuriesCB.ItemsSource = injuries;
            int cnt = 0;
            foreach(Injury i in injuries)
            {
                if (i.InjuryID == plan.InjuryID)
                {
                    injuriesCB.SelectedIndex = cnt;
                    break;
                }
                cnt++;
            }
            planTimeTextBox.Text=plan.PlanTimeInWeeks.ToString();
            descriptionTextBox.Text = plan.Notes;
            NameTextBox.Text = plan.PlanName;
            priceTextBox.Text=plan.Price.ToString();

        }

        private void save(object sender, MouseButtonEventArgs e)
        {
            int id = int.Parse(IDTextBox.Text.Trim());
            int time=int.Parse(planTimeTextBox.Text.Trim());
            string notes = descriptionTextBox.Text.Trim();
            string name = NameTextBox.Text.Trim();
            int injuryId = ((Injury)injuriesCB.SelectedItem).InjuryID;
            double price=double.Parse(priceTextBox.Text.Trim());    

            TreatmentPlan tp = new TreatmentPlan(id, name, time, price, notes, injuryId);


            if (viewing)
            {
                DB.UpdateTreatmentPlan(tp);

                MessageBox.Show("Treatment Plan updated, ID: " + id.ToString());
            }
            else
            {
                DB.InsertTreatmentPlan(tp);

                MessageBox.Show("New Treatment Plan added, ID: " + id.ToString());
            }

            Window.GetWindow(this).Close();

        }
    }
}
