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
using System.Windows.Navigation;
using System.Windows.Shapes;
using FerryManagementData.Context;
using System.Data.Entity;
using FerryManagementData.Models;
using DTO.Models;
using BusinessLogic.BLL;


namespace WPFTESTAPP
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private FerryContext _context;
        private FerryBLL _ferryLogic;
        private CarBLL _carLogic;
        private GuestBLL _guestLogic;

        public MainWindow()
        {
            InitializeComponent();
            _context = new FerryContext();

            _ferryLogic = new FerryBLL();
            _carLogic = new CarBLL();
            _guestLogic = new GuestBLL();

        }

        // Ferries
        private void LoadFerries_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                
                var ferries = _ferryLogic.GetFerries();
                ferriesGrid.ItemsSource = ferries;
                
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading ferries: {ex.Message}");
            }
        }

        private void AddFerry_Click(object sender, RoutedEventArgs e)
        {
            AddFerryWindow addFerryWindow = new AddFerryWindow(_ferryLogic);

            if (addFerryWindow.ShowDialog() == true)
            {
                //refresh the grid
                LoadFerries_Click(null, null);
            }
        }

        private void DeleteFerry_Click(object sender, RoutedEventArgs e)
        {
            if (ferriesGrid.SelectedItem is FerryDTO selectedFerry)
            {
                if (MessageBox.Show("Are you sure you want to delete this ferry?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    _ferryLogic.DeleteFerry(selectedFerry.FerryId);
                    LoadFerries_Click(null, null);
                    MessageBox.Show("Ferry deleted successfully.");
                }
            }
            else
            {
                MessageBox.Show("Please select a ferry to delete.");
            }

        }


        //Cars

        private void AddCar_Click(object sender, RoutedEventArgs e)
        {
            if (ferriesGrid.SelectedItem is FerryDTO selectedFerry)
            {
                Console.WriteLine($"Selected ferry ID: {selectedFerry.FerryId}");
                AddCarWindow addCarWindow = new AddCarWindow(selectedFerry, _carLogic);

                if (addCarWindow.ShowDialog() == true)
                {
                    Console.WriteLine("Reloading cars and ferries data.");
                    LoadCars_Click(null, null);
                    LoadFerries_Click(null, null);
                }
            }
            else
            {
                MessageBox.Show("Please select a ferry to add a car to");
            }
        }

        private void DeleteCar_Click(object sender, RoutedEventArgs e)
        {
            if (carsGrid.SelectedItem is CarDTO selectedCar)
            {
                if (MessageBox.Show("Are you sure you want to delete this car?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    _carLogic.DeleteCar(selectedCar.CarId);
                    LoadCars_Click(null, null); // Opdater bil-listen
                    LoadFerries_Click(null, null); // Opdater færge-listen
                    MessageBox.Show("Car deleted successfully.");
                }
            }
            else
            {
                MessageBox.Show("Please select a car to delete.");
            }
        }

        private void LoadCars_Click(object sender, RoutedEventArgs e)
        {
            if (ferriesGrid.SelectedItem is FerryDTO selectedFerry)
            {
                try
                {
                    var cars = _carLogic.GetCarsByFerryId(selectedFerry.FerryId);
                    if (cars != null)
                    {
                        carsGrid.ItemsSource = cars;
                        Console.WriteLine($"Loaded {cars.Count} cars for ferry ID: {selectedFerry.FerryId}");
                    }
                    else
                    {
                        MessageBox.Show("No cars found for the selected ferry.");
                        Console.WriteLine("No cars found for the selected ferry.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading cars: {ex.Message}");
                    Console.WriteLine($"Error loading cars: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Please select a ferry to load cars.");
                Console.WriteLine("Attempted to load cars without selecting a ferry.");
            }
        }


        // Guests
        private void LoadGuests_Click(object sender, RoutedEventArgs e)
        {
            if (carsGrid.SelectedItem is CarDTO selectedCar)
            {
                try
                {
                    var guests = _guestLogic.GetGuestsByCarId(selectedCar.CarId);
                    guestsGrid.ItemsSource = guests;
                    Console.WriteLine($"Loaded {guests.Count} guests for car ID: {selectedCar.CarId}");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading guests: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Please select a car to load guests from.");
            }
        }


        private void AddGuest_Click(object sender, RoutedEventArgs e)
        {
            if (carsGrid.SelectedItem is CarDTO selectedCar)
            {
                GuestBLL guestlogic = new GuestBLL();
                AddGuestWindow addGuestWindow = new AddGuestWindow(guestlogic, selectedCar.CarId);

                if(addGuestWindow.ShowDialog() == true)
                {
                    LoadGuests_Click(null, null);
                }
            }
            else
            {
                MessageBox.Show("Please select a car to add a guest to.");
            }
        }
    }
}
