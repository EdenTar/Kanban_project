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
    /// Interaction logic for AddTask.xaml
    /// </summary>
    public partial class AddTask : Window
    {
        

        private AddTaskVM addTaskVM;
        public AddTask(BackendController controller, ColumnM columnM)
        {
            InitializeComponent();
         ///////check
            this.addTaskVM = new AddTaskVM(controller,columnM);
            this.DataContext = addTaskVM;
        }

        /// <summary>
        /// Adds new task button 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewTaskButton(object sender, RoutedEventArgs e)
        {
            addTaskVM.AddTask();
            BoardV board = new BoardV(new BoardController(addTaskVM.TaskController.Service), addTaskVM.Column.BoardM);
            board.Show();
            this.Close();
        }

        /// <summary>
        /// Returns to the previous window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackButton(object sender, RoutedEventArgs e)
        {
            BoardV board = new BoardV(new BoardController(addTaskVM.TaskController.Service),addTaskVM.Column.BoardM);
            board.Show();
            this.Close();
        }
    }
}
