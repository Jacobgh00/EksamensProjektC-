﻿using BusinessLogic.BLL;
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
    /// Interaction logic for EditGuestWindow.xaml
    /// </summary>
    public partial class EditGuestWindow : Window
    {

        private GuestBLL _guestBLL = new GuestBLL();
        private GuestDTO _guest;

        public EditGuestWindow(GuestDTO guest)
        {
            InitializeComponent();
            _guest = guest;
            LoadGuestDetails();
        }

        private void LoadGuestDetails()
        {
            NameTextBox.Text = _guest.Name;
            GenderComboBox.SelectedItem = GenderComboBox.Items.Cast<ComboBoxItem>().FirstOrDefault(item => item.Content.ToString() == _guest.Gender);
            BirthDatePicker.SelectedDate = _guest.Birthdate;
        }


        private void SaveChanges_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _guest.Name = NameTextBox.Text;
                _guest.Gender = ((ComboBoxItem)GenderComboBox.SelectedItem).Content.ToString();
                _guest.Birthdate = BirthDatePicker.SelectedDate ?? DateTime.Now;

                _guestBLL.UpdateGuest(_guest);
                MessageBox.Show("Guest updated successfully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                
                this.DialogResult = true;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating guest: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
