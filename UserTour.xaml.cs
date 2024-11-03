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
            IHotelRepository HotelRepository = new HotelRepository(new AppDbContext());
            ITourRepository TourRepository = new TourRepository(new AppDbContext());

            var combinedData = new List<object>();

            combinedData.AddRange(TourRepository.GetAllToursNonArchive());
            combinedData.AddRange(HotelRepository.GetAllHotelsNonArchive());

            return combinedData;
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

        private void ReservationListButton_Click(object sender, RoutedEventArgs e)
        {
            UserReservations userReservations = new UserReservations();
            userReservations.ShowDialog();
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

        private void ReservarionButton_Click(object sender, RoutedEventArgs e)
        {
            if (TourHotelListView.SelectedItem != null)
            {
                UserCard userCard = new UserCard();
                userCard.Card += CardWindow;
                userCard.ShowDialog();
            }
        }

        private void CardWindow(object sender, EventArgs e)
        {
            IReservationRepository ReservationRepository = new ReservationRepository(new AppDbContext());
            IUserRepository UserRepository = new UserRepository(new AppDbContext());

            if (TourHotelListView != null)
            {
                var selectedItem = TourHotelListView.SelectedItem;

                if (selectedItem is Tours)
                {
                    
                    Tours tour = (Tours)selectedItem;
                    string user = UserRepository.UserAutentification().Email;
                    var reservations = ReservationRepository.UserReservation(user);
                    string strTour = "Tour";
                    bool IsReservated = false;
                    for (int i = 0; i < reservations.Count; i++)
                    {
                        if (reservations[i].TourOrHotelId == tour.Id && reservations[i].TourOrHotel == strTour)
                        {
                            IsReservated = true;
                        }
                    }
                    if (IsReservated)
                    {
                        MessageBox.Show("Вы уже забронировали этот тур");
                    }
                    else
                    {
                        ReservationRepository.AddReservarion(new Reservation { UserEmail = user, TourOrHotelId = tour.Id, TourOrHotel = strTour, IsConfirm = false });
                        MessageBox.Show("Тур забронирован!");
                    }
                }
                else
                {
                    string user = UserRepository.UserAutentification().Email;
                    var reservations = ReservationRepository.UserReservation(user);
                    string strHotel = "Hotel";
                    Hotels hotel = (Hotels)selectedItem;
                    bool IsReservated = false;

                    for (int i = 0; i < reservations.Count; i++)
                    {
                        if (reservations[i].TourOrHotelId == hotel.Id && reservations[i].TourOrHotel == strHotel)
                        {
                            IsReservated = true;
                        }
                    }
                    if (IsReservated)
                    {
                        MessageBox.Show("Вы уже забронировали этот отель");
                    }
                    else
                    {
                        ReservationRepository.AddReservarion(new Reservation { UserEmail = user, TourOrHotelId = hotel.Id, TourOrHotel = strHotel, IsConfirm = false });
                        MessageBox.Show("Отель забронирован!");
                    }
                }
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            IUserRepository UserRepository = new UserRepository(new AppDbContext());
            User user = UserRepository.UserAutentification();
            user.isLogin = false;
            UserRepository.UpdateUser(user);
        }
    }
}
