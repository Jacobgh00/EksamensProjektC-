using BusinessLogic.BLL;
using DTO.Models;
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

namespace WPF
{
    /// <summary>
    /// Interaction logic for EditFerryWindow.xaml
    /// </summary>
    public partial class EditFerryWindow : Window
    {

        private FerryBLL _ferryBLL = new FerryBLL();
        public FerryDTO Ferry { get; set; }

        public EditFerryWindow(FerryDTO ferry)
        {
            InitializeComponent();
            Ferry = ferry;
            
            this.DataContext = Ferry;
        }



        private void SaveFerry_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _ferryBLL.UpdateFerry(Ferry);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving ferry: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
