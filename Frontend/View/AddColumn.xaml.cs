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
    /// Interaction logic for AddColumn.xaml
    /// </summary>
    public partial class AddColumn : Window
    {
        private AddColumnVM columnVM;
        public AddColumn(BackendController controller,BoardM boardM, UserM userM)
        {
            InitializeComponent();
            this.columnVM = new AddColumnVM(controller,boardM,userM);
            this.DataContext = columnVM;
        }
       
      

        private void AddColumnButton(object sender, RoutedEventArgs e)
        {
            columnVM.AddColumn();
            BoardV board = new BoardV(columnVM.UserM.Controller,columnVM.BoardM);
            board.Show();
            this.Close();
        }

        private void BackButton(object sender, RoutedEventArgs e)
        {
            BoardV board = new BoardV(columnVM.UserM.Controller, columnVM.BoardM);
            board.Show();
            this.Close();
        }

      
    }
}
