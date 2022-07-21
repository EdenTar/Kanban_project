using IntroSE.Kanban.Frontend.Model;
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
using IntroSE.Kanban.Frontend.ViewModel;
using System.IO;

namespace IntroSE.Kanban.Frontend.View
{
    /// <summary>
    /// Interaction logic for Main.xaml
    /// </summary>
    public partial class MainV : Window
    {
        private MainVM mainViewModel;
        public MainVM MainViewModel { get => mainViewModel;set { mainViewModel = value; } }

        private BackendController backendController;
        public BackendController BackendController { get => backendController; set { backendController = value; } }
        public MainV()
        {
            InitializeComponent();
            MainViewModel = new MainVM();
            DataContext = MainViewModel;
        }

        private void Login_button_click(object sender, RoutedEventArgs e)
        {
            (new Login(MainViewModel.DataController)).Show();
            Close();
        }

        private void Register_button_click(object sender, RoutedEventArgs e) 
        {
            (new Register(MainViewModel.DataController)).Show();
            Close();
        }
    }
}
