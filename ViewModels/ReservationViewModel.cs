using Hotel_Una_Legacy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;

namespace Hotel_Una_Legacy.ViewModels
{
    public class ReservationViewModel
    {
        private readonly Reservation _reservation;

        public int ID => _reservation.ID;
        public int RoomNum => _reservation.RoomNum;
        public string FirstName => _reservation.FirstName;
        public string LastName => _reservation.LastName;
        public DateTime ReservationDate => _reservation.ReservationDate;
        public DateTime StartDate => _reservation.StartDate;
        public DateTime EndDate => _reservation.EndDate;
        public int NumberOfGuests => _reservation.NumberOfGuests;

        public ReservationViewModel(Reservation reservation)
        {
            _reservation = reservation;
        }
        public UIElement GetReservationDataContentControl()
        {
            Grid grid = new Grid();
            grid.DataContext = this;
            grid.RowDefinitions.Add(new RowDefinition()); // 0
            grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(8) }); // 1
            grid.RowDefinitions.Add(new RowDefinition()); // 2
            grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(8) }); // 3
            grid.RowDefinitions.Add(new RowDefinition()); // 4
            grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(8) }); // 5
            grid.RowDefinitions.Add(new RowDefinition()); // 6

            grid.ColumnDefinitions.Add(new ColumnDefinition());
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(8) });
            grid.ColumnDefinitions.Add(new ColumnDefinition());

            StackPanel roomNumStackPanel = GetDataStackPanel("Broj sobe: ", "RoomNum");
            Grid.SetRow(roomNumStackPanel, 0);
            grid.Children.Add(roomNumStackPanel);

            StackPanel numberOfGuestsStackPanel = GetDataStackPanel("Broj gostiju: ", "NumberOfGuests");
            Grid.SetRow(numberOfGuestsStackPanel, 0);
            Grid.SetColumn(numberOfGuestsStackPanel, 2);
            grid.Children.Add(numberOfGuestsStackPanel);

            StackPanel firstNameStackPanel = GetDataStackPanel("Ime: ", "FirstName");
            Grid.SetRow(firstNameStackPanel, 2);
            grid.Children.Add(firstNameStackPanel);

            StackPanel lastNameStackPanel = GetDataStackPanel("Prezime: ", "LastName");
            Grid.SetRow(lastNameStackPanel, 2);
            Grid.SetColumn(lastNameStackPanel, 2);
            grid.Children.Add(lastNameStackPanel);

            StackPanel startDateStackPanel = GetDataStackPanel("Check in: ", "StartDate");
            Grid.SetRow(startDateStackPanel, 4);
            Grid.SetColumn(startDateStackPanel, 0);
            grid.Children.Add(startDateStackPanel);

            StackPanel endDateStackPanel = GetDataStackPanel("Check out: ", "EndDate");
            Grid.SetRow(endDateStackPanel, 4);
            Grid.SetColumn(endDateStackPanel, 2);
            grid.Children.Add(endDateStackPanel);

            StackPanel reservationDateStackPanel = GetDataStackPanel("Datum rezervacije: ", "ReservationDate");
            Grid.SetRow(reservationDateStackPanel, 6);
            Grid.SetColumn(reservationDateStackPanel, 0);
            grid.Children.Add(reservationDateStackPanel);

            Border border = new Border
            {
                Child = grid,
                Background = App.Current.Resources["SecondaryColorBrush"] as SolidColorBrush,
                Padding = new Thickness(20),
                CornerRadius = new CornerRadius(8),
            };

            return border;
        }
        public UIElement GetReservationInputContentControl()
        {
            Grid grid = new Grid();
            grid.RowDefinitions.Add(new RowDefinition()); // 0
            grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(8) }); // 1
            grid.RowDefinitions.Add(new RowDefinition()); // 2
            grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(8) }); // 3
            grid.RowDefinitions.Add(new RowDefinition()); // 4
            grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(8) }); // 5
            grid.RowDefinitions.Add(new RowDefinition()); // 6

            grid.ColumnDefinitions.Add(new ColumnDefinition());
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(8) });
            grid.ColumnDefinitions.Add(new ColumnDefinition());

            StackPanel roomStackPanel = GetInputStackPanel("Broj sobe", "RoomNum");
            grid.Children.Add(roomStackPanel);

            StackPanel firstNameStackPanel = GetInputStackPanel("Ime: ", "FirstName");
            Grid.SetRow(firstNameStackPanel, 2);
            grid.Children.Add(firstNameStackPanel);

            StackPanel lastNameStackPanel = GetInputStackPanel("Prezime: ", "LastName");
            Grid.SetRow(lastNameStackPanel, 2);
            Grid.SetColumn(lastNameStackPanel, 2);
            grid.Children.Add(lastNameStackPanel);

            StackPanel startDateStackPanel = GetInputStackPanel("Check in: ", "StartDate", "DatePicker");

            Grid.SetRow(startDateStackPanel, 4);
            Grid.SetColumn(startDateStackPanel, 0);
            grid.Children.Add(startDateStackPanel);

            StackPanel endDateStackPanel = GetInputStackPanel("Check out: ", "EndDate", "DatePicker");
            Grid.SetRow(endDateStackPanel, 4);
            Grid.SetColumn(endDateStackPanel, 2);
            grid.Children.Add(endDateStackPanel);

            StackPanel numberOfGuestsStackPanel= GetInputStackPanel("Broj gostiju: ", "NumberOfGuests");
            Grid.SetRow(numberOfGuestsStackPanel, 6);
            Grid.SetColumn(numberOfGuestsStackPanel, 0);
            grid.Children.Add(numberOfGuestsStackPanel);

            Border border = new Border()
            {
                Child = grid,
                Background = App.Current.Resources["SecondaryColorBrush"] as SolidColorBrush,
                Padding = new Thickness(20),
                CornerRadius = new CornerRadius(8),

            };
            return border;
        }
        private StackPanel GetInputStackPanel(string textBlockValue, string binding, string inputElementType="TextBox")
        {
            StackPanel stackPanel = new StackPanel();
            TextBlock textBlock = new TextBlock()
            {
                Text = textBlockValue,
                Foreground = App.Current.Resources["LightColorBrush"] as SolidColorBrush,
                Margin = new Thickness(0, 0, 0, 8),
            };
            if (inputElementType == "TextBox")
            {
                TextBox textBox = new TextBox()
                {
                    Width = 150,
                    Height = 30,
                    BorderThickness = new Thickness(0),
                    HorizontalAlignment = HorizontalAlignment.Left,
                };
                Binding textBoxBinding = new Binding(binding);
                textBoxBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
                textBox.SetBinding(TextBox.TextProperty, textBoxBinding);
                stackPanel.Children.Add(textBox);
            }
            else if(inputElementType == "DatePicker")
            {
                DatePicker datePicker = new DatePicker();
                datePicker.SetBinding(DatePicker.SelectedDateProperty, binding);
                stackPanel.Children.Add(datePicker);
            }

            stackPanel.Children.Add(textBlock);
            return stackPanel;
        }
        private StackPanel GetDataStackPanel(string propertyLabel, string propertyValue)
        {
            StackPanel stackPanel = new StackPanel()
            {
                Orientation = Orientation.Horizontal
            };

            TextBlock propertyLabelTB = new TextBlock
            {
                Text = propertyLabel,
                Foreground = App.Current.Resources["LightColorBrush"] as SolidColorBrush,
            };
            Binding propertyBinding = new Binding(propertyValue);
            TextBlock propertyValueTB = new TextBlock()
            {
                FontWeight = FontWeights.Bold,
                Foreground = App.Current.Resources["LightColorBrush"] as SolidColorBrush,
            };
            propertyValueTB.SetBinding(TextBlock.TextProperty, propertyBinding);

            stackPanel.Children.Add(propertyLabelTB);
            stackPanel.Children.Add(propertyValueTB);
            return stackPanel;
        }
    }
}
