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
    /// Interaction logic for AddGuestWindow.xaml
    /// </summary>
    public partial class AddGuestWindow : Window
    {
        private GuestBLL _guestBLL = new GuestBLL();
        private CarBLL _carBLL = new CarBLL();
        private int _ferryId;
        private int _carId;


        public AddGuestWindow(int? carId, int ferryId)
        {
            InitializeComponent();
            _ferryId = ferryId;
            _carId = carId ?? 0;

            LoadCarOptions();
        }


        private void LoadCarOptions()
        {
            CarComboBox.Items.Clear();
            CarComboBox.Items.Add(new ComboBoxItem { Content = "No Car", IsSelected = true });

            var cars = _carBLL.GetAllCarsForFerry(_ferryId);
            foreach (var car in cars)
            {
                // Hent alle gæster for denne specifikke bil
                var guestsForCar = _guestBLL.GetAllGuests(_ferryId).Where(g => g.CarID == car.CarID).ToList();
                int currentGuestCount = guestsForCar.Count;

                // Tjek at antallet af gæster ikke overstiger bilens kapacitet
                string displayText = $"{car.Name} ({currentGuestCount}/5)";
                CarComboBox.Items.Add(new ComboBoxItem { Content = displayText, Tag = car.CarID });
            }
        }


        private void AddGuest_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedCarItem = CarComboBox.SelectedItem as ComboBoxItem;
                int? carId = selectedCarItem?.Tag as int?;  // det valgte bil-ID, kan være null

                // Hvis der er valgt en bil, tjek at antallet af gæster ikke overskrider grænsen
                if (carId != null)
                {
                    var car = _carBLL.GetCar(carId.Value);
                    var guestsInCar = _guestBLL.GetAllGuests(_ferryId).Count(g => g.CarID == carId);
                    if (guestsInCar >= 5)
                    {
                        MessageBox.Show($"The car {car.Name} has reached its maximum capacity.", "Capacity Exceeded", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                }

                var newGuest = new GuestDTO
                {
                    Name = NameTextBox.Text,
                    Gender = ((ComboBoxItem)GenderComboBox.SelectedItem).Content.ToString(),
                    Birthdate = BirthDatePicker.SelectedDate ?? DateTime.Now,
                    CarID = carId,  // kan være null
                    FerryID = _ferryId
                };

                _guestBLL.AddGuestToFerry(_ferryId, newGuest);
                MessageBox.Show("Guest added successfully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                this.DialogResult = true;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding guest: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}
