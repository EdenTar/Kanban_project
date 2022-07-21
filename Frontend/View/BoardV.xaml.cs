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
    /// Interaction logic for BoardV.xaml
    /// </summary>
    public partial class BoardV : Window
    {
        private BoardVM boardVM;


        public BoardV(BackendController backendController, BoardM boardM)
        {
            InitializeComponent();
            boardVM = new BoardVM(boardM);
            DataContext = boardVM;
       
        }

        private void Click_AddColumn(object sender, RoutedEventArgs e)
        {
            AddColumn addColumn = new AddColumn(boardVM.BoardM.Controller, boardVM.BoardM, boardVM.BoardM.UserM);
            addColumn.Show();
            Close();
        }

        private void EnterColumn(object sender, RoutedEventArgs e) 
        {
            if (boardVM.SelectedColumn != null)
            {
                ColumnV columnV = new ColumnV(boardVM.SelectedColumn);
                columnV.Show();
                Close();
            } 
        }

        private void RemoveColumn(object sender, RoutedEventArgs e) 
        {
            boardVM.RemoveColumn();
        }    

        private void Click_AddTask(object sender, RoutedEventArgs e)
        {
            AddTask addTask = new AddTask(boardVM.BoardM.Controller,boardVM.BoardM.Columns.ElementAt(0));
            addTask.Show();
            Close();
        }
        private void EnterTask(object sender, RoutedEventArgs e)
        {
            if (boardVM.GetSelectedTask() != null)
            {
                TaskV taskV = new TaskV(boardVM.GetSelectedTask());
                taskV.Show();
                Close();
            }
        }
        private void SortTasks(object sender, RoutedEventArgs e)
        {
                boardVM.SortTasks();
        }

        private void Back(object sender, RoutedEventArgs e) 
        {
            new UserBoards(boardVM.BoardM.Controller, boardVM.BoardM.UserM).Show();
            Close();
        }
        private void Logout(object sender, RoutedEventArgs e) 
        {
            boardVM.Logout();
            MainV mainWindow = new MainV();
            mainWindow.Show();
            Close();
        }
        public void Filter(object sender, RoutedEventArgs e) 
        {
            boardVM.Filter();
        }

        public void MoveColumn(object sender, RoutedEventArgs e) 
        {
            boardVM.MoveColumn();
        }
        public void StageMoveColumn(object sender, RoutedEventArgs e)
        {
            boardVM.StageMoveColumn();
        }

    }
}
