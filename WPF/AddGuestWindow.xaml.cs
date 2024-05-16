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


        public AddGuestWindow(int carId, int ferryId)
        {
            InitializeComponent();
            _carId = carId;
            _ferryId = ferryId;
        }

       
        private void AddGuest_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var newGuest = new GuestDTO
                {
                    Name = NameTextBox.Text,
                    Gender = ((ComboBoxItem)GenderComboBox.SelectedItem).Content.ToString(),
                    Birthdate = BirthDatePicker.SelectedDate ?? DateTime.Now,
                    CarID = _carId,  
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
