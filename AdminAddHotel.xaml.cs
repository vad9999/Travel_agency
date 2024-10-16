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
using Microsoft.Win32;

namespace Travel_agency
{
    /// <summary>
    /// Логика взаимодействия для AdminAddHotel.xaml
    /// </summary>
    public partial class AdminAddHotel : Window
    {
        string imagePath = null!;
        public event EventHandler ItemAdded;

        public AdminAddHotel()
        {
            InitializeComponent();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new AppDbContext())
            {
                IHotelRepository HotelRepository = new HotelRepository(context);

                string hotelName = NameHotelBox.Text;
                string hotelDescription = DiscriptionHotelBox.Text;
                string hotelPrice = PriceHotelBox.Text;
                string hotelCountry = CountryHotelBox.Text;

                if (string.IsNullOrEmpty(hotelName) ||
                    string.IsNullOrEmpty(hotelDescription) ||
                    string.IsNullOrEmpty(hotelPrice) ||
                    string.IsNullOrEmpty(hotelCountry) ||
                    string.IsNullOrEmpty(imagePath))
                {
                    MessageBox.Show("Пожалуйста заполните все поля и добавьте фотографию", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (!decimal.TryParse(hotelPrice, out decimal price))
                {
                    MessageBox.Show("Цена должна быть цислом", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                HotelRepository.AddHotel(new Hotels { Name = hotelName, Description = hotelDescription, Country = hotelCountry, Price = decimal.Parse(hotelPrice), PathImage = imagePath });

                ItemAdded?.Invoke(this, EventArgs.Empty);

                this.Close();
            }
        }

        private void CountryHotelBox_MouseEnter(object sender, MouseEventArgs e)
        {
            if (CountryHotelBox.Text == "Введите стану отеля")
                CountryHotelBox.Text = "";
        }

        private void CountryHotelBox_MouseLeave(object sender, MouseEventArgs e)
        {
            if (CountryHotelBox.Text == "")
                CountryHotelBox.Text = "Введите стану отеля";
        }

        private void NameHotelBox_MouseEnter(object sender, MouseEventArgs e)
        {
            if (NameHotelBox.Text == "Введите название отеля")
                NameHotelBox.Text = "";
        }

        private void NameHotelBox_MouseLeave(object sender, MouseEventArgs e)
        {
            if (NameHotelBox.Text == "")
                NameHotelBox.Text = "Введите название отеля";
        }

        private void DiscriptionHotelBox_MouseEnter(object sender, MouseEventArgs e)
        {
            if (DiscriptionHotelBox.Text == "Введите описание отеля")
                DiscriptionHotelBox.Text = "";
        }

        private void DiscriptionHotelBox_MouseLeave(object sender, MouseEventArgs e)
        {
            if (DiscriptionHotelBox.Text == "")
                DiscriptionHotelBox.Text = "Введите описание отеля";
        }

        private void PriceHotelBox_MouseEnter(object sender, MouseEventArgs e)
        {
            if (PriceHotelBox.Text == "Введите цену отеля")
                PriceHotelBox.Text = "";
        }

        private void PriceHotelBox_MouseLeave(object sender, MouseEventArgs e)
        {
            if (PriceHotelBox.Text == "")
                PriceHotelBox.Text = "Введите цену отеля";
        }

        private void AddImageButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.jpg, *.jpeg, *.png, *.gif)|*.jpg;*.jpeg;*.png;*.gif|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
                imagePath = openFileDialog.FileName;
        }
    }
}
