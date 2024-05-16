using BusinessLogic.BLL;
using DTO.Models;
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

namespace WPFTESTAPP
{
    /// <summary>
    /// Interaction logic for AddGuestWindow.xaml
    /// </summary>
    public partial class AddGuestWindow : Window
    {
        private GuestBLL _guestLogic;
        private int _carId;
        public AddGuestWindow(GuestBLL guestLogic, int carId)
        {
            InitializeComponent();
            _guestLogic = guestLogic;
            _carId = carId;
        }

        private void AddGuest_Click(object sender, RoutedEventArgs e)
        {
            var guestName = guestNameTextBox.Text;
            var guestGender = (genderComboBox.SelectedItem as ComboBoxItem).Content.ToString();

            if(string.IsNullOrWhiteSpace(guestName) || string.IsNullOrWhiteSpace(guestGender))
            {
                MessageBox.Show("Please enter the guest's name and select a gender");
                return;
            }

            var newGuest = new GuestDTO
            {
                Name = guestName,
                Gender = guestGender,
                CarId = _carId
            };

            try
            {
                _guestLogic.AddGuestToCar(newGuest, _carId);
                MessageBox.Show("Guest added successfully");
                this.DialogResult = true;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding guest: {ex.Message}");
            }
        }
    }
}
