using BusinessLogic.BLL;
using FerryManagementData.Context;
using FerryManagementData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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
    public partial class AddCarWindow : Window
    {
        private CarBLL _carLogic;
        private FerryDTO _selectedFerry;

        public AddCarWindow(FerryDTO selectedFerry, CarBLL carLogic)
        {
            InitializeComponent();
            _carLogic = carLogic;
            _selectedFerry = selectedFerry;
        }

        private void AddCar_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Attempting to add a new car.");

            // Valider input
            if (string.IsNullOrWhiteSpace(driverNameTextBox.Text))
            {
                MessageBox.Show("Please enter the driver's name.");
                Console.WriteLine("Failed to add car: Driver's name is missing.");
                return;
            }

            // Sikre at guestCount er et tal og ikke negativt
            if (!int.TryParse(guestCountTextBox.Text, out int guestCount) || guestCount < 0)
            {
                MessageBox.Show("Please enter a valid number of guests.");
                Console.WriteLine("Failed to add car: Invalid number of guests.");
                return;
            }

            Console.WriteLine($"Driver: {driverNameTextBox.Text}, Guest Count: {guestCount}");

            // Opret en ny car med en driver
            var carDTO = new CarDTO
            {
                Driver = new GuestDTO { Name = driverNameTextBox.Text },
                Guests = Enumerable.Range(1, guestCount).Select(i => new GuestDTO { Name = $"Guest {i}" }).ToList()
            };

            try
            {
                _carLogic.AddCarToFerry(carDTO, _selectedFerry.FerryId);
                Console.WriteLine($"Car successfully added to ferry ID: {_selectedFerry.FerryId}");
                MessageBox.Show("Car added successfully.");
                this.DialogResult = true;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding car: {ex.Message}");
                Console.WriteLine($"Exception thrown when adding car: {ex}");
            }
        }
    }
}
