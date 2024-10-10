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
    /// Логика взаимодействия для AdminListUsers.xaml
    /// </summary>
    public partial class AdminListUsers : Window
    {
        public AdminListUsers()
        {
            InitializeComponent();
        }

        private void ExiteButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
