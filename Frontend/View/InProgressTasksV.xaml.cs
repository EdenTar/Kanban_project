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
    /// Interaction logic for InProgressTasksV.xaml
    /// </summary>
    public partial class InProgressTasksV : Window
    {
        InProgressTasksVM inProgressTasksVM;
        
        public InProgressTasksV(BackendController backendController,UserM userM)
        {
            InitializeComponent();
            this.inProgressTasksVM = new InProgressTasksVM(backendController,userM);
            this.DataContext = this.inProgressTasksVM;
            
        }

      /// <summary>
      /// returns to the previous window
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
        public void BackButton(object sender, RoutedEventArgs e)
        {
            UserBoards userBoard = new UserBoards(inProgressTasksVM.TaskController, inProgressTasksVM.UserM);
            userBoard.Show();
            this.Close();
        }

       public void ViewTaskButton(object sender, RoutedEventArgs e)
        {
            ViewTaskV viewTask = new ViewTaskV(inProgressTasksVM.TaskController,inProgressTasksVM.UserM,inProgressTasksVM.SelectedTask);
            viewTask.Show();
            this.Close();
        }

        private void SortButton(object sender, RoutedEventArgs e)
        {
            inProgressTasksVM.sort();
        }
    }
}
