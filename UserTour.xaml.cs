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
        private int _currentPage = 1; 
        private const int _itemsPerPage = 6;

        public UserTour()
        {
            InitializeComponent();
            UpdateListView();
            IsOnePage();
            PreviousButtonn.IsEnabled = false;
        }

        private void IsOnePage()
        {
            if (GetTotalPages() == 1)
            {
                PreviousButtonn.IsEnabled = false;
                NextButton.IsEnabled = false;
            }
            else
            {
                NextButton.IsEnabled = true;
            }
        }

        private List<object> GetListToursHotels()
        {
            using (var context = new AppDbContext())
            {
                ITourRepository TourRepository = new TourRepository(context);
                IHotelRepository HotelRepository = new HotelRepository(context);

                var combinedData = new List<object>();

                combinedData.AddRange(TourRepository.GetAllToursNonArchive());
                combinedData.AddRange(HotelRepository.GetAllHotelsNonArchive());

                return combinedData;
            }
        }

        private void UpdateListView()
        {
            var itemsToShow = GetListToursHotels().Skip((_currentPage - 1) * _itemsPerPage).Take(_itemsPerPage).ToList();
            TourHotelListView.ItemsSource = itemsToShow;
        }

        private int GetTotalPages()
        {
            return (int)Math.Ceiling((double)GetListToursHotels().Count / _itemsPerPage);
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

        private void PreviousButtonn_Click(object sender, RoutedEventArgs e)
        {
            NextButton.IsEnabled = true;
            if (_currentPage > 1)
            {
                _currentPage--;
                UpdateListView();
            }
            if(_currentPage == 1)
            {
                PreviousButtonn.IsEnabled = false;
            }
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            PreviousButtonn.IsEnabled = true;
            if (_currentPage < GetTotalPages())
            {
                _currentPage++;
                UpdateListView();
            }
            if (_currentPage == GetTotalPages())
            {
                NextButton.IsEnabled = false;
            }
        }
    }
}
