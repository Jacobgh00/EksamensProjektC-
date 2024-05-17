﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.Remoting.Messaging;
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
using BusinessLogic.BLL;
using DTO.Models;
using Newtonsoft.Json;

namespace WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private FerryBLL _ferryBLL = new FerryBLL();
        private CarBLL _carBLL = new CarBLL();
        private GuestBLL _guestBLL = new GuestBLL();
        
        private static readonly string SERVER_URL = "https://localhost:44393/api/";
        
        
        public MainWindow()
        {
            InitializeComponent();
            RefreshAllData();


        }


        //------------------------------------LOAD SECTION------------------------------------------------------------//
        private void RefreshFerry_Click(object sender, RoutedEventArgs e)
        {
            Loadferries();
        }

        private void RefreshCar_Click(object sender, RoutedEventArgs e)
        {
            LoadCars();
        }

        private void RefreshGuest_Click(object sender, RoutedEventArgs e)
        {
            LoadGuests();
        }

        //til at opdatere data direkte uden at skulle trykke på en knap
        private void RefreshAllData()
        {
            Loadferries();
            LoadCars();
            LoadGuests();

        }

        private void Loadferries()
        {
            var ferries = _ferryBLL.GetAllFerries();
            DataGridFerries.ItemsSource = ferries;
        }

        private void LoadCars()
        {
            if (DataGridFerries.SelectedItem is FerryDTO selectedFerry)
            {
                var cars = _carBLL.GetAllCarsForFerry(selectedFerry.FerryID).ToList();
                cars.ForEach(car =>
                {
                    car.NumberOfGuests = _guestBLL.GetAllGuests(car.FerryID).Count(g => g.CarID == car.CarID);
                });

                DataGridCars.ItemsSource = cars;
            }
            else
            {
                DataGridCars.ItemsSource = null;
            }
        }

        private void LoadGuests()
        {
            // Clear previous items
            DataGridGuests.ItemsSource = null;

            if (DataGridFerries.SelectedItem is FerryDTO selectedFerry)
            {
                List<GuestDTO> guestsToLoad = new List<GuestDTO>();

                if (DataGridCars.SelectedItem is CarDTO selectedCar)
                {
                    // Load guests for the selected car
                    guestsToLoad = _guestBLL.GetAllGuests(selectedFerry.FerryID)
                        .Where(g => g.CarID == selectedCar.CarID).ToList();
                }
                else
                {
                    // Load guests for the ferry without filtering by car
                    guestsToLoad = _guestBLL.GetAllGuests(selectedFerry.FerryID)
                        .Where(g => g.FerryID == selectedFerry.FerryID).ToList();
                }

                DataGridGuests.ItemsSource = guestsToLoad;
                Console.WriteLine($"Loaded guests for the current selection - Count: {guestsToLoad.Count}");
            }
        }



        //Load cars og guests efter selection changed
        private void DataGridFerries_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadCars();
        }

        private void DataGridCars_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadGuests();
        }

        private void MainTabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.Source is TabControl)
            {
                if (MainTabControl.SelectedIndex == 2)
                {
                    LoadGuests();
                }
            }
        }


        //------------------------------------CRUD SECTION------------------------------------------------------------//

        //Add, edit og delete Ferry
        private void AddFerry_Click(object sender, RoutedEventArgs e)
        {
            var addFerryWindow = new AddFerryWindow();
            addFerryWindow.ShowDialog();
            Loadferries();
        }

        private void EditFerry_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridFerries.SelectedItem is FerryDTO selectedFerry)
            {
                var editFerryWindow = new EditFerryWindow(selectedFerry);
                editFerryWindow.ShowDialog();
                Loadferries();  // Reload the list to reflect updates
            }
            else
            {
                MessageBox.Show("Please select a ferry to edit.", "No Ferry Selected", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void DeleteFerry_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridFerries.SelectedItem is FerryDTO selectedFerry)
            {
                var result = MessageBox.Show($"Are you sure you want to delete the ferry '{selectedFerry.Name}'?",
                                            "Confirm Deletion", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        _ferryBLL.DeleteFerry(selectedFerry.FerryID);
                        MessageBox.Show("Ferry deleted successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        Loadferries();
                    }
                    catch
                    {
                        MessageBox.Show("Error deleting ferry.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a ferry to delete.", "No Ferry Selected", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }



        //Add, edit og delete Car
        private void AddCar_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridFerries.SelectedItem is FerryDTO selectedFerry)
            {
                var addCarWindow = new AddCarWindow(selectedFerry.FerryID);
                if (addCarWindow.ShowDialog() == true)
                {
                    LoadCars(); 
                }
            }
            else
            {
                MessageBox.Show("Please select a ferry to add cars to.", "No Ferry Selected", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void EditCar_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridCars.SelectedItem is CarDTO selectedCar)
            {
                var editCarWindow = new EditCarWindow(selectedCar);
                if (editCarWindow.ShowDialog() == true)
                {
                    LoadCars();
                }
            } 
            else
            {
                MessageBox.Show("Please select a car to edit.", "No Car Selected", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void DeleteCar_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridCars.SelectedItem is CarDTO selectedCar)
            {
                MessageBoxResult result = MessageBox.Show($"Are you sure you want to delete the car '{selectedCar.Name}'?",
                                                               "Confirm Deletion", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        var guestsToDelete = _guestBLL.GetAllGuests(selectedCar.FerryID).Where(g => g.CarID == selectedCar.CarID).ToList();

                        foreach (var guest in guestsToDelete)
                        {
                            _guestBLL.DeleteGuest(guest.GuestID);
                        }

                        _carBLL.DeleteCar(selectedCar.CarID);
                        MessageBox.Show("Car deleted successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        LoadCars();
                        LoadGuests();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error deleting car: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a car to delete.", "No Car Selected", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }


        //Add, edit og delete Guest
        private void AddGuest_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridFerries.SelectedItem is FerryDTO selectedFerry)
            {
                CarDTO selectedCar = DataGridCars.SelectedItem as CarDTO;

                var addGuestWindow = new AddGuestWindow(selectedCar?.CarID, selectedFerry.FerryID);
                if (addGuestWindow.ShowDialog() == true)
                {
                    LoadGuests();
                    LoadCars(); // måske
                }
            }
            else
            {
                MessageBox.Show("Please select a ferry to add guests to.", "No Ferry Selected", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void EditGuest_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridGuests.SelectedItem is GuestDTO selectedGuest)
            {
                if (DataGridFerries.SelectedItem is FerryDTO selectedFerry)
                {
                    var editGuestWindow = new EditGuestWindow(selectedGuest, selectedFerry.FerryID);
                    if (editGuestWindow.ShowDialog() == true)
                    {
                        LoadGuests();
                    }
                }
                else
                {
                    MessageBox.Show("Please select a ferry and a guest to edit.", "Selection Missing", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show("Please select a guest to edit.", "No Guest Selected", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void DeleteGuest_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridGuests.SelectedItem is GuestDTO selectedGuest)
            {
                var result = MessageBox.Show($"Are you sure you want to delete the guest '{selectedGuest.Name}'?",
                                                               "Confirm Deletion", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        _guestBLL.DeleteGuest(selectedGuest.GuestID);
                        MessageBox.Show("Guest deleted successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        LoadGuests();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error deleting guest: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a guest to delete.", "No Guest Selected", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }


        
    //------------------------------------------API SECTION------------------------------------------------------------//

        //loader færger fra API
        private async void LoadFerriesAPI2_Click(object sender, RoutedEventArgs e)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync($"{SERVER_URL}/Ferries");
                    response.EnsureSuccessStatusCode();
                    var ferriesJson = await response.Content.ReadAsStringAsync();
                    var ferries = JsonConvert.DeserializeObject<List<FerryDTO>>(ferriesJson);

                    UpdateDataGrid(ferries, "Ferries");
                    ApiDataGrid2.ItemsSource = ferries;

                }
                catch (HttpRequestException ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        
        
        //her loader vi bilerne til en specifik færge
        private async void LoadCarsForFerryAPI_Click(object sender, RoutedEventArgs e)
        {
            if (ApiDataGrid2.SelectedItem is FerryDTO selectedFerry)
            {
                using (HttpClient client = new HttpClient())
                {
                    try
                    {
                        HttpResponseMessage response = await client.GetAsync($"{SERVER_URL}/Ferry/{selectedFerry.FerryID}/Cars");
                        response.EnsureSuccessStatusCode();
                        var carsJson = await response.Content.ReadAsStringAsync();
                        var cars = JsonConvert.DeserializeObject<List<CarDTO>>(carsJson);
                        UpdateDataGrid(cars, "Cars");
                        ApiDataGrid2.ItemsSource = cars;
                    }
                    catch (HttpRequestException ex)
                    {
                        MessageBox.Show($"Error loading cars from API: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a ferry first.", "No Ferry Selected", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }


        //her loader vi gæsterne til en færge
        private async void LoadGuestsAPI_Click(object sender, RoutedEventArgs e)
        {
            if (ApiDataGrid2.SelectedItem is FerryDTO selectedFerry)
            {
                using (HttpClient client = new HttpClient())
                {
                    try
                    {
                        HttpResponseMessage response = await client.GetAsync($"{SERVER_URL}/Ferry/{selectedFerry.FerryID}/Guests");
                        response.EnsureSuccessStatusCode();
                        var guestsJson = await response.Content.ReadAsStringAsync();
                        var guests = JsonConvert.DeserializeObject<List<GuestDTO>>(guestsJson);
                        UpdateDataGrid(guests, "Guests");
                        ApiDataGrid2.ItemsSource = guests;
                    }
                    catch (HttpRequestException ex)
                    {
                        MessageBox.Show($"Error loading guests from API: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a ferry first.", "No Ferry Selected", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }


        //alt den her gør at vi kan opdatere datagriden med de forskellige typer af data. Den ser overskuelig ud, men den er rimelig simpel
        private void UpdateDataGrid<T>(List<T> items, string type)
        {
            ApiDataGrid2.Columns.Clear();

            if (type == "Ferries")
            {
                //kolonner specifik for færger
                ApiDataGrid2.Columns.Add(new DataGridTextColumn { Header = "Ferry ID", Binding = new Binding("FerryID"), Width = new DataGridLength(1, DataGridLengthUnitType.Star) });
                ApiDataGrid2.Columns.Add(new DataGridTextColumn { Header = "Name", Binding = new Binding("Name"), Width = new DataGridLength(1, DataGridLengthUnitType.Star) });
                ApiDataGrid2.Columns.Add(new DataGridTextColumn { Header = "Max Cars", Binding = new Binding("MaxCars"), Width = new DataGridLength(1, DataGridLengthUnitType.Star) });
                ApiDataGrid2.Columns.Add(new DataGridTextColumn { Header = "Max Guests", Binding = new Binding("MaxGuests"), Width = new DataGridLength(1, DataGridLengthUnitType.Star) });
                ApiDataGrid2.Columns.Add(new DataGridTextColumn { Header = "Price Per Guest", Binding = new Binding("PriceGuests"), Width = new DataGridLength(1, DataGridLengthUnitType.Star) });
                ApiDataGrid2.Columns.Add(new DataGridTextColumn { Header = "Price Per Car", Binding = new Binding("PriceCar"), Width = new DataGridLength(1, DataGridLengthUnitType.Star) });
                ApiDataGrid2.Columns.Add(new DataGridTextColumn { Header = "Name", Binding = new Binding("Name"), Width = new DataGridLength(1, DataGridLengthUnitType.Star) });
                ApiDataGrid2.Columns.Add(new DataGridTextColumn { Header = "Total Guests", Binding = new Binding("TotalGuests"), Width = new DataGridLength(1, DataGridLengthUnitType.Star) });
                ApiDataGrid2.Columns.Add(new DataGridTextColumn { Header = "Total Cars", Binding = new Binding("TotalCars"), Width = new DataGridLength(1, DataGridLengthUnitType.Star) });
                ApiDataGrid2.Columns.Add(new DataGridTextColumn { Header = "Total Price", Binding = new Binding("TotalPrice"), Width = new DataGridLength(1, DataGridLengthUnitType.Star) });
            }
            if (type == "Cars")
            {
                //kolonner specifik for biler
                ApiDataGrid2.Columns.Add(new DataGridTextColumn { Header = "Ferry ID", Binding = new Binding("FerryID"), Width = new DataGridLength(1, DataGridLengthUnitType.Star) });
                ApiDataGrid2.Columns.Add(new DataGridTextColumn { Header = "Car ID", Binding = new Binding("CarID"), Width = new DataGridLength(1, DataGridLengthUnitType.Star) });
                ApiDataGrid2.Columns.Add(new DataGridTextColumn { Header = "Name", Binding = new Binding("Name"), Width = new DataGridLength(1, DataGridLengthUnitType.Star) });
                ApiDataGrid2.Columns.Add(new DataGridTextColumn { Header = "Numberplate", Binding = new Binding("Numberplate"), Width = new DataGridLength(1, DataGridLengthUnitType.Star) });
                ApiDataGrid2.Columns.Add(new DataGridTextColumn { Header = "Number of Guests", Binding = new Binding("NumberOfGuests"), Width = new DataGridLength(1, DataGridLengthUnitType.Star) });
            }


            if (type == "Guests")
            {
                //kolonner specifik for gæster
                ApiDataGrid2.Columns.Add(new DataGridTextColumn { Header = "Guest ID", Binding = new Binding("GuestID"), Width = new DataGridLength(1, DataGridLengthUnitType.Star) });
                ApiDataGrid2.Columns.Add(new DataGridTextColumn { Header = "Name", Binding = new Binding("Name"), Width = new DataGridLength(1, DataGridLengthUnitType.Star) });
                ApiDataGrid2.Columns.Add(new DataGridTextColumn { Header = "Gender", Binding = new Binding("Gender"), Width = new DataGridLength(1, DataGridLengthUnitType.Star) });
                ApiDataGrid2.Columns.Add(new DataGridTextColumn
                {
                    Header = "Birthdate",
                    Binding = new Binding("Birthdate")
                    {
                        
                        StringFormat = "dd/MM/yyyy" // Sætter datovisningsformatet

                    }
                    , Width = new DataGridLength(1, DataGridLengthUnitType.Star)
                });
            }
            

            ApiDataGrid2.ItemsSource = items;
        }

       
    }
}
