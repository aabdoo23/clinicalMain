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
    /// Interaction logic for recordPage.xaml
    /// </summary>
    public partial class recordPage : Page
    {
        public recordPage()
        {
            InitializeComponent();
        }

        private void goBackPage(object sender, MouseButtonEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
