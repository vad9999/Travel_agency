using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Travel_agency
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            using (var context = new AppDbContext())
            {
                IUserRepository UserRepository = new UserRepository(context);

                bool admin = false;
                var users = UserRepository.GetAllUsers().ToList();
                for (int i = 0; i < users.Count; i++) 
                {
                    if (users[i].IsAdmin)
                    {
                        admin = true;
                        break;
                    }
                }
                if (!admin)
                {
                    UserRepository.AddUser(new User { Name = "admin", Email = "admin", Password = "admin", IsAdmin = true });
                }
            }
        }

        private void LogInButton_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new AppDbContext())
            {
                IUserRepository UserRepository = new UserRepository(context);
                if ((EmailBox.Text != "Введите эл. почту" && EmailBox.Text != "") && (PasswordBox.Text != "Введите пароль" && PasswordBox.Text != ""))
                {
                    if(UserRepository.GetUserByEmail(EmailBox.Text) != null)
                    {
                        if (UserRepository.GetUserByEmail(EmailBox.Text).Password == PasswordBox.Text)
                        {
                            /*...*/
                        }
                        else
                        {
                            MessageBox.Show("Неправльный пароль!");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Пользователь с такой почтой не зарегистрирован!");
                    }
                }
                else
                {
                    MessageBox.Show("Введите почту или пароль");
                }
            }
        }

        private void EmailBox_MouseEnter(object sender, MouseEventArgs e)
        {
            if (EmailBox.Text == "Введите эл. почту")
            {
                EmailBox.Text = "";
            }
        }

        private void EmailBox_MouseLeave(object sender, MouseEventArgs e)
        {
            if (EmailBox.Text == "")
            {
                EmailBox.Text = "Введите эл. почту";
            }
        }

        private void PasswordBox_MouseEnter(object sender, MouseEventArgs e)
        {
            if (PasswordBox.Text == "Введите пароль")
            {
                PasswordBox.Text = "";
            }
        }

        private void PasswordBox_MouseLeave(object sender, MouseEventArgs e)
        {
            if (PasswordBox.Text == "")
            {
                PasswordBox.Text = "Введите пароль";
            }
        }

        private void SignUpButton_Click(object sender, RoutedEventArgs e)
        {
            SignUp signUp = new SignUp();
            signUp.Show();
            this.Close();
        }
    }
}