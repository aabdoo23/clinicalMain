using Microsoft.Win32;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Tesseract;
//using System.Windows.Forms;

namespace clinical.Pages
{
    /// <summary>
    /// Interaction logic for newRecordPage.xaml
    /// </summary>
    public partial class newRecordPage : System.Windows.Controls.Page
    {
        public newRecordPage()
        {
            InitializeComponent();
        }

        private void newPhysician(object sender, MouseButtonEventArgs e)
        {

        }

        private void newReport(object sender, MouseButtonEventArgs e)
        {

        }
        private void newXRAY(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Title = "Select a record to upload";
            openFileDialog.Filter = "All Files (*.*)|*.*";

            openFileDialog.ShowDialog();

            string filePath = openFileDialog.FileName;

            if (filePath != null && filePath != "")
            {
                Border border = new Border
                {
                    Style = (Style)FindResource("theBorder"),
                    MinHeight = 400,
                    Margin = new Thickness(10)
                };
                //<Border Style="{StaticResource theBorder}" Margin="10" MinHeight="400">
                //                    < Image Source = "@\..\..\images\hand.jpg" Margin = "10" />
                //                </ Border >
                Image image = new Image
                {
                    Source = new BitmapImage(new Uri(filePath)),
                    Margin = new Thickness(10),
                };

                image.MouseDown += Image_MouseDown;

                border.Child = image;


                scannedScansStackPanel.Children.Add(border);
                string resultText = PerformOCR(filePath);

                reportTXT.Text = resultText;


            }
        }

        //private MouseButtonEventHandler Image_MouseDown(string filePath)
        //{
        //    viewImage newWindow = new viewImage(filePath);
        //    newWindow.Show();
        //    return new MouseButtonEventHandler(object sender,MouseButtonEventArgs e);

        //}

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        string PerformOCR(string imagePath)
        {
            try
            {
                using (var engine = new TesseractEngine("C:\\Users\\TOP\\Desktop\\clinicalK\\clinical\\tessdata", "eng", EngineMode.Default))
                {
                    using (var img = Pix.LoadFromFile(imagePath))
                    {
                        using (var page = engine.Process(img))
                        {
                            var text = page.GetText();
                            return text;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }

    }

}
