using IntroSE.Kanban.Frontend.Model;
using IntroSE.Kanban.Frontend.ViewModel;
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

namespace IntroSE.Kanban.Frontend.View
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        private LoginVM loginViewModel;
        private UserM userM;
        public UserM UserM
        {
            get => userM;
            set
            {
                this.userM = value;
            }
        }
        public Login(BackendController backendController)
        {
            InitializeComponent();
            this.DataContext = new LoginVM(backendController);
            this.loginViewModel = (LoginVM)DataContext;
        }


        private void Login_Button_Click(object sender, RoutedEventArgs e)
        {
            UserM u = loginViewModel.Login();
            if (u != null)
            {
                UserBoards userBoards = new UserBoards(loginViewModel.Controller, u);
                userBoards.Show();
                this.Close();
            }           
        }

        private void Register_Button_Click(object sender, RoutedEventArgs e)
        {
            //move to the register page
            Register registerWindow = new Register(loginViewModel.Controller);
            registerWindow.Show();
            this.Close();
        }
    }
}