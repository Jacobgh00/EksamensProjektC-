using BusinessLogic.BLL;
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
using DTO.Models;

namespace WPF
{
    /// <summary>
    /// Interaction logic for AddFerryWindow.xaml
    /// </summary>
    public partial class AddFerryWindow : Window
    {

        private FerryBLL _ferryBLL = new FerryBLL();

        public AddFerryWindow()
        {
            InitializeComponent();
        }

        private void AddFerry_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var newFerry = new FerryDTO
                {
                    Name = NameTextBox.Text,
                    MaxCars = int.Parse(MaxCarsTextBox.Text),
                    MaxGuests = int.Parse(MaxGuestsTextBox.Text),
                    PriceGuests = int.Parse(PricePerGuestTextBox.Text),
                    PriceCar = int.Parse(PricePerCarTextBox.Text)
                };

                _ferryBLL.AddFerry(newFerry);
                this.Close();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "input error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
