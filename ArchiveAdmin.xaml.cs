﻿using System;
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
    /// Логика взаимодействия для ArchiveAdmin.xaml
    /// </summary>
    public partial class ArchiveAdmin : Window
    {
        public event EventHandler ItemNonArchive;
        public ArchiveAdmin()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            using (var context = new AppDbContext())
            {
                ITourRepository TourRepository = new TourRepository(context);
                IHotelRepository HotelRepository = new HotelRepository(context);

                var combinedData = new List<object>();

                combinedData.AddRange(TourRepository.GetAllToursArchive());
                combinedData.AddRange(HotelRepository.GetAllHotelsArchive());

                ArchiveListView.ItemsSource = combinedData;
            }
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            ItemNonArchive?.Invoke(this, EventArgs.Empty);
            this.Close();
        }

        private void UnZipButton_Click(object sender, RoutedEventArgs e)
        {
            if(ArchiveListView.SelectedItem != null)
            {
                if (ArchiveListView.SelectedItem is Tours)
                {
                    using (var context = new AppDbContext())
                    {
                        ITourRepository TourRepository = new TourRepository(context);
                        Tours selectedTour = (Tours)ArchiveListView.SelectedItem;
                        selectedTour.IsArchive = false;
                        TourRepository.UpdateTour(selectedTour);
                    }   
                }
                if (ArchiveListView.SelectedItem is Hotels)
                {
                    using (var context = new AppDbContext())
                    {
                        IHotelRepository HotelRepository = new HotelRepository(context);
                        Hotels selectedHotel = (Hotels)ArchiveListView.SelectedItem;
                        selectedHotel.IsArchive = false;
                        HotelRepository.UpdateHotel(selectedHotel);
                    } 
                }
                LoadData();
                ItemNonArchive?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
