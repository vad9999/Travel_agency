using Microsoft.Win32;
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

namespace Travel_agency
{
    /// <summary>
    /// Логика взаимодействия для AdminAddTour.xaml
    /// </summary>
    public partial class AdminAddTour : Window
    {
        string imagePath = null!;
        public event EventHandler ItemAdded;

        public AdminAddTour()
        {
            InitializeComponent();
        }

        private void NameTourBox_MouseEnter(object sender, MouseEventArgs e)
        {
            if (NameTourBox.Text == "Введите название тура")
                NameTourBox.Text = "";
        }

        private void NameTourBox_MouseLeave(object sender, MouseEventArgs e)
        {
            if (NameTourBox.Text == "")
                NameTourBox.Text = "Введите название тура";
        }

        private void CountryTourBox_MouseEnter(object sender, MouseEventArgs e)
        {
            if (CountryTourBox.Text == "Введите страну тура")
                CountryTourBox.Text = "";
        }

        private void CountryTourBox_MouseLeave(object sender, MouseEventArgs e)
        {
            if (CountryTourBox.Text == "")
                CountryTourBox.Text = "Введите страну тура";
        }

        private void DescriptionTourBox_MouseEnter(object sender, MouseEventArgs e)
        {
            if (DescriptionTourBox.Text == "Введите описание тура")
                DescriptionTourBox.Text = "";
        }

        private void DescriptionTourBox_MouseLeave(object sender, MouseEventArgs e)
        {
            if (DescriptionTourBox.Text == "")
                DescriptionTourBox.Text = "Введите описание тура";
        }

        private void PriceTourBox_MouseEnter(object sender, MouseEventArgs e)
        {
            if (PriceTourBox.Text == "Введите цену тура")
                PriceTourBox.Text = "";
        }

        private void PriceTourBox_MouseLeave(object sender, MouseEventArgs e)
        {
            if (PriceTourBox.Text == "")
                PriceTourBox.Text = "Введите цену тура";
        }

        private void ImageButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.jpg, *.jpeg, *.png, *.gif)|*.jpg;*.jpeg;*.png;*.gif|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
                imagePath = openFileDialog.FileName;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new AppDbContext())
            {
                ITourRepository TourRepository = new TourRepository(context);

                string tourName = NameTourBox.Text;
                string tourDescription = DescriptionTourBox.Text;
                string tourPrice = PriceTourBox.Text;
                string tourCountry = CountryTourBox.Text;

                if (string.IsNullOrEmpty(tourName) ||
                    string.IsNullOrEmpty(tourDescription) ||
                    string.IsNullOrEmpty(tourPrice) ||
                    string.IsNullOrEmpty(tourCountry) ||
                    string.IsNullOrEmpty(imagePath))
                {
                    MessageBox.Show("Пожалуйста заполните все поля и добавьте фотографию", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (!decimal.TryParse(tourPrice, out decimal price))
                {
                    MessageBox.Show("Цена должна быть цислом", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                TourRepository.AddTour(new Tours { Name = tourName, Description = tourDescription, Country = tourCountry, Price = decimal.Parse(tourPrice), PathImage = imagePath });

                ItemAdded?.Invoke(this, EventArgs.Empty);

                this.Close();
            }
        }
    }
}