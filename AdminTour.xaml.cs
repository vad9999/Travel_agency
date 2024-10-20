using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        private int _currentPage = 1;
        private const int _itemsPerPage = 6;

        public AdminTour()
        {
            InitializeComponent();
            UpdateListView();
            PreviousButtonn.IsEnabled = false;
            IsOnePage();
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
            if (TourHotelListView.SelectedItem != null)
            {
                object selectedItem = TourHotelListView.SelectedItem;
                AdminEdit adminEdit = new AdminEdit(selectedItem);
                adminEdit.ItemAdded += AddWindow_ItemAdded;
                adminEdit.ShowDialog();
            }    
        }

        private void ReservationListButton_Click(object sender, RoutedEventArgs e)
        {
            AdminReservationList adminReservationList = new AdminReservationList();
            adminReservationList.Show();
        }

        private void AddWindow_ItemAdded(object sender, EventArgs e)
        {
            UpdateListView();
            IsOnePage();
        }

        private void DeleteTourButton_Click(object sender, RoutedEventArgs e)
        {
            if (TourHotelListView.SelectedItem != null)
            {
                if(TourHotelListView.SelectedItem is Tours)
                {
                    Tours selectedTour = (Tours)TourHotelListView.SelectedItem;
                    using (var context = new AppDbContext())
                    {
                        ITourRepository TourRepository = new TourRepository(context);
                        TourRepository.DeleteTour(selectedTour.Id);
                    }
                }
                if(TourHotelListView.SelectedItem is Hotels)
                {
                    Hotels selectedHotel = (Hotels)TourHotelListView.SelectedItem;
                    using (var context = new AppDbContext())
                    {
                        IHotelRepository HotelRepository = new HotelRepository(context);
                        HotelRepository.DeleteHotel(selectedHotel.Id);
                    }
                }
                UpdateListView();
                IsOnePage();
            }
        }

        private void ArchiveButton_Click(object sender, RoutedEventArgs e)
        {
            ArchiveAdmin archiveAdmin = new ArchiveAdmin();
            archiveAdmin.ItemNonArchive += ArchiveWindow_ItemNonArchive;
            archiveAdmin.ShowDialog();
        }

        private void ArchiveWindow_ItemNonArchive(object sender, EventArgs e)
        {
            UpdateListView();
            IsOnePage();
        }

        private void ZipButton_Click(object sender, RoutedEventArgs e)
        {
            if (TourHotelListView.SelectedItem != null)
            {
                if (TourHotelListView.SelectedItem is Tours)
                {
                    using (var context = new AppDbContext())
                    {
                        ITourRepository TourRepository = new TourRepository(context);
                        Tours selectedTour = (Tours)TourHotelListView.SelectedItem;
                        selectedTour.IsArchive = true;
                        TourRepository.UpdateTour(selectedTour);
                    }
                }
                if (TourHotelListView.SelectedItem is Hotels)
                {
                    using (var context = new AppDbContext())
                    {
                        IHotelRepository HotelRepository = new HotelRepository(context);
                        Hotels selectedHotel = (Hotels)TourHotelListView.SelectedItem;
                        selectedHotel.IsArchive = true;
                        HotelRepository.UpdateHotel(selectedHotel);
                    }      
                }
                if (GetListToursHotels().Count == 6)
                    _currentPage = 1;
                UpdateListView();
                IsOnePage();
            }
        }

        private void PreviousButtonn_Click(object sender, RoutedEventArgs e)
        {
            NextButton.IsEnabled = true;
            if (_currentPage > 1)
            {
                _currentPage--;
                UpdateListView();
            }
            if (_currentPage == 1)
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
