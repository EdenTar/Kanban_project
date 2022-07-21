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
    /// Interaction logic for ColumnV.xaml
    /// </summary>
    public partial class ColumnV : Window
    {
        private ColumnVM columnVM;
        public ColumnV(ColumnM columnM)
        {
            InitializeComponent();
            columnVM = new ColumnVM(columnM);
            DataContext = columnVM;
        }

        private void ApplyName(object sender, RoutedEventArgs e)
        {
            columnVM.UpdateName();
        }

        private void ApplyLimit(object sender, RoutedEventArgs e)
        {
            columnVM.UpdateLimit();
        }
        private void Back(object sender, RoutedEventArgs e) 
        {
            new BoardV(columnVM.ColumnM.BoardM.Controller ,columnVM.ColumnM.BoardM).Show();
            Close();
        }
    }
}
