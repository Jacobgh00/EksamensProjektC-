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
    /// Interaction logic for EditCarWindow.xaml
    /// </summary>
    public partial class EditCarWindow : Window
    {

        private CarBLL _carBLL = new CarBLL();
        private CarDTO _car;

        public EditCarWindow(CarDTO car)
        {
            InitializeComponent();
            _car = car;
            LoadCarDetails();
        }


        private void LoadCarDetails()
        {
            NameTextBox.Text = _car.Name;
            NumberplateTextBox.Text = _car.Numberplate;
        }
 
        private void SaveChanges_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _car.Name = NameTextBox.Text;
                _car.Numberplate = NumberplateTextBox.Text;
                _carBLL.UpdateCar(_car);
                MessageBox.Show("Car updated successfully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                this.DialogResult = true;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating car: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
