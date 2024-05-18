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
    /// Interaction logic for AddCarWindow.xaml
    /// </summary>
    public partial class AddCarWindow : Window
    {

        private CarBLL _carBLL = new CarBLL();
        private int _ferryId;

        public AddCarWindow(int ferryId)
        {
            InitializeComponent();
            _ferryId = ferryId;
            
        }

        private void AddCar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var newCar = new CarDTO
                {
                    Name = NameTextBox.Text,
                    Numberplate = NumberplateTextBox.Text,
                    FerryID = _ferryId
                };

                _carBLL.AddCarToFerry(_ferryId, newCar);
                MessageBox.Show("Car added successfully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                this.DialogResult = true;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding car: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
