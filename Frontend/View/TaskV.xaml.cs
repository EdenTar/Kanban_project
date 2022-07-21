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
    /// Interaction logic for Task.xaml
    /// </summary>
    public partial class TaskV : Window
    {
        private TaskVM taskVM;
        public TaskV(TaskM task)
        {
            InitializeComponent();
            this.taskVM = new TaskVM(task);
            this.DataContext = taskVM;
            taskVM.DueDateMessage();
        }

        /// <summary>
        /// Call updateTitle function in the dataContext
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TitleApplyButton(object sender, RoutedEventArgs e)
        {
            taskVM.UpdateTitle();
        }

        /// <summary>
        ///Call updateDescription function in the dataContext
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DescriptionApplyButton(object sender, RoutedEventArgs e)
        {
            taskVM.UpdateDescription();
        }

        /// <summary>
        /// Call updateDueDate function in the dataContext
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DueDateApplyButton(object sender, RoutedEventArgs e)
        {
            taskVM.UpdateDueDate();
        }

        /// <summary>
        /// Call assignTask function in the dataContext
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AssigneeApplyButton(object sender, RoutedEventArgs e)
        {
            taskVM.AssignTask();
        }

        /// <summary>
        /// Advance the task button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AdvanceTaskButton(object sender, RoutedEventArgs e)
        {
            taskVM.AdvanceTask();
            BoardV board = new BoardV(new BoardController(taskVM.TaskController.Service), taskVM.TaskM.ColumnM.BoardM);
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
            BoardV board = new BoardV(new BoardController(taskVM.TaskController.Service), taskVM.TaskM.ColumnM.BoardM);
            board.Show();
            this.Close();
        }
       
    }
}
