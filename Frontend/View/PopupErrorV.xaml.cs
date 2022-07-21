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
    /// Interaction logic for PopupErrorV.xaml
    /// </summary>
    public partial class PopupErrorV : Window
    {
        public PopupErrorV(string errorMsg)
        {
            InitializeComponent();
            PopupErrorVM popupErrorVM = new PopupErrorVM(errorMsg);
            DataContext = popupErrorVM;
        }
        public void Accept(object sender, RoutedEventArgs e) 
        {
            Close();
        }
    }
}
