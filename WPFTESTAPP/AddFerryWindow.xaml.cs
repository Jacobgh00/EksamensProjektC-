using BusinessLogic.BLL;
using FerryManagementData.Context;
using FerryManagementData.Models;
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

namespace WPFTESTAPP
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class AddFerryWindow : Window
    {
        private FerryBLL _ferryLogic;

        public AddFerryWindow(FerryBLL ferryLogic)
        {
            InitializeComponent();
            _ferryLogic = ferryLogic;
        }

        private void AddFerry_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(ferryLengthTextBox.Text, out int length) ||
                !int.TryParse(maxCarsTextBox.Text, out int maxCars) ||
                !int.TryParse(maxGuestsTextBox.Text, out int maxGuests) ||
                !decimal.TryParse(guestPriceTextBox.Text, out decimal guestPrice) ||
                !decimal.TryParse(carPriceTextBox.Text, out decimal carPrice))
            {
                MessageBox.Show("Please enter valid numeric values.");
                return;
            }

            var newFerry = new FerryDTO
            {
                Length = length,
                MaxCars = maxCars,
                MaxGuests = maxGuests,
                GuestPrice = guestPrice,
                CarPrice = carPrice
            };

            // Add the ferry using the BLL
            _ferryLogic.AddFerry(newFerry);

            MessageBox.Show("Ferry added successfully.");
            this.DialogResult = true;  // Indicate success
            this.Close();
        }
    }
}
