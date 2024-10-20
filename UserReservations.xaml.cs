using Microsoft.EntityFrameworkCore.Diagnostics;
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
    /// Логика взаимодействия для UserReservations.xaml
    /// </summary>
    public partial class UserReservations : Window
    {
        public UserReservations()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            using(var context = new AppDbContext())
            {
                IReservationRepository ReservationRepository = new ReservationRepository(context);
                IUserRepository UserRepository = new UserRepository(context);
                string userEmail = UserRepository.UserAutentification().Email; 
                List<Reservation> reservations = ReservationRepository.UserReservation(userEmail);
                ReservationListView.ItemsSource = reservations;
            }
        }
    }
}
