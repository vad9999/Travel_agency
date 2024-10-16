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
    /// Логика взаимодействия для UserTour.xaml
    /// </summary>
    public partial class UserTour : Window
    {
        int _currentPage = 0;
        int _pageSize = 8;

        public UserTour()
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

        private void ReservationButton_Click(object sender, RoutedEventArgs e)
        {
            UserReservations userReservations = new UserReservations();
            userReservations.Show();
        }
    }
}
