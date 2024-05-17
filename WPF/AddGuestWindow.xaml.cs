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
            
            var cars = _carBLL.GetAllCarsForFerry(_ferryId);
            CarComboBox.Items.Add(new ComboBoxItem { Content = "No Car", IsSelected = true });
            foreach (var car in cars)
            {
                CarComboBox.Items.Add(new ComboBoxItem { Content = car.Name, Tag = car.CarID });
            }
        }


        private void AddGuest_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedCarItem = CarComboBox.SelectedItem as ComboBoxItem;
                int? carId = selectedCarItem?.Tag as int?; // This will be null if "No Car" is selected

                var newGuest = new GuestDTO
                {
                    Name = NameTextBox.Text,
                    Gender = ((ComboBoxItem)GenderComboBox.SelectedItem).Content.ToString(),
                    Birthdate = BirthDatePicker.SelectedDate ?? DateTime.Now,
                    CarID = carId,  // Can be null
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
