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
    /// Логика взаимодействия для SignUp.xaml
    /// </summary>
    public partial class SignUp : Window
    {
        public SignUp()
        {
            InitializeComponent();
        }

        private void NameBox_MouseEnter(object sender, MouseEventArgs e)
        {
            if (NameBox.Text == "Введите имя")
                NameBox.Text = "";
        }

        private void NameBox_MouseLeave(object sender, MouseEventArgs e)
        {
            if (NameBox.Text == "")
                NameBox.Text = "Введите имя";
        }

        private void EmailBox_MouseEnter(object sender, MouseEventArgs e)
        {
            if (EmailBox.Text == "Введите адрес эл. почты")
                EmailBox.Text = "";
        }

        private void EmailBox_MouseLeave(object sender, MouseEventArgs e)
        {
            if (EmailBox.Text == "")
                EmailBox.Text = "Введите адрес эл. почты";
        }

        private void PasswordBox_MouseEnter(object sender, MouseEventArgs e)
        {
            if (PasswordBox.Text == "Введите пароль")
                PasswordBox.Text = "";
        }

        private void PasswordBox_MouseLeave(object sender, MouseEventArgs e)
        {
            if (PasswordBox.Text == "")
                PasswordBox.Text = "Введите пароль";
        }

        private void SignUpButton_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new AppDbContext())
            {
                IUserRepository UserRepository = new UserRepository(context);
                if (NameBox.Text != "Введите имя" && NameBox.Text != "")
                {
                    if (EmailBox.Text != "Введите адрес эл. почты" && EmailBox.Text != "")
                    {
                        if (PasswordBox.Text != "Введите пароль" && PasswordBox.Text != "")
                        {
                            bool email = false;
                            var users = UserRepository.GetAllUsers().ToList();
                            for (int i = 0; i < users.Count; i++)
                            {
                                if (users[i].Email == EmailBox.Text)
                                {
                                    email = true;
                                    MessageBox.Show("Пользователь с такой почтой уже зарегистрирован");
                                    break;
                                }
                            }
                            if (!email)
                            {
                                UserRepository.AddUser(new User { Name = NameBox.Text, Email = EmailBox.Text, Password = PasswordBox.Text, IsAdmin = false, Blocking = false });
                                NameBox.Text = "Введите имя";
                                EmailBox.Text = "Введите адрес эл. почты";
                                PasswordBox.Text = "Введите пароль";
                            }
                        }
                    }
                }
            }
        }

        private void LogInButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
