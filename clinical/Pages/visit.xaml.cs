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

namespace clinical.Pages
{
    /// <summary>
    /// Interaction logic for visit.xaml
    /// </summary>
    public partial class visit : Page
    {
        Visit currVisit;
        public visit(Visit selectedVisit )
        {
            InitializeComponent();
            currVisit=selectedVisit;
        }

        private void view_Click(object sender, RoutedEventArgs e)
        {

        }

        private void removeLastTest(object sender, MouseButtonEventArgs e)
        {

        }

        private void addTest(object sender, MouseButtonEventArgs e)
        {

        }

        private void navigateBack(object sender, MouseButtonEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void newPrescription(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
