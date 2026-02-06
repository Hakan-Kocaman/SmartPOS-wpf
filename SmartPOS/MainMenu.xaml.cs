using Microsoft.Data.SqlClient;
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

namespace Kasa_otomasyonu
{
  
    public partial class MainMenu : Window
    {
        public MainMenu()
        {
            InitializeComponent();
            CompanyName.Text = Company.Name;
        }

        private void User_Login_Button(object sender, RoutedEventArgs e)
        {
            

            var User_name = UserNameEnterance.Text.ToLower();
            if (string.IsNullOrEmpty(User_name))
            {
                MessageBox.Show("Please Enter User Name");
                return;
            }
            var User_id = SQLManager.GetEmployeeId(User_name);
  
            if (User_id.Equals(0))
            {
                MessageBox.Show("User Name Could Not Found");
            }
            else
            {
                MessageBox.Show("Entered " + User_name);
                Employee User = new Employee(User_id, User_name);
                UserMenu newUserMenu = new UserMenu(User);
                newUserMenu.Show();
                this.Close();

            }
        }

        private void Admin_Login_Button(object sender, RoutedEventArgs e)
        {
            AdminMenu newAdminMenu = new AdminMenu();
            newAdminMenu.Show();
            this.Close();

        }
    }
}