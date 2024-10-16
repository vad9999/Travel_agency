using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Travel_agency
{
    /// <summary>
    /// Логика взаимодействия для AdminTour.xaml
    /// </summary>
    public partial class AdminTour : Window
    {
        public AdminTour()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            using (var context = new AppDbContext())
            {
                // Получаем данные о турах и отелях
                List<Tours> tours = context.Tours.ToList();
                List<Hotels> hotels = context.Hotels.ToList();

                // Объединяем списки для отображения в ListView
                var combinedData = new List<object>();
                combinedData.AddRange(tours);
                combinedData.AddRange(hotels);

                // Устанавливаем источником данных для ListView
                TourHotelListView.ItemsSource = combinedData;
            }
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void AllUsersButton_Click(object sender, RoutedEventArgs e)
        {
            AdminListUsers adminListUsers = new AdminListUsers();
            adminListUsers.Show();
        }

        private void AddTourButton_Click(object sender, RoutedEventArgs e)
        {
            AdminAddTour adminAddTour = new AdminAddTour();
            adminAddTour.ItemAdded += AddWindow_ItemAdded;
            adminAddTour.ShowDialog();
        }

        private void AddHotelButton_Click(object sender, RoutedEventArgs e)
        {
            AdminAddHotel adminAddHotel = new AdminAddHotel();
            adminAddHotel.ItemAdded += AddWindow_ItemAdded;
            adminAddHotel.ShowDialog();
        }

        private void EditTourButton_Click(object sender, RoutedEventArgs e)
        {
            AdminEdit adminEdit = new AdminEdit();
            adminEdit.Show();
        }

        private void ReservationListButton_Click(object sender, RoutedEventArgs e)
        {
            AdminReservationList adminReservationList = new AdminReservationList();
            adminReservationList.Show();
        }

        private void AddWindow_ItemAdded(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}
