using IntroSE.Kanban.Frontend.Model;

using IntroSE.Kanban.Frontend.View;
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
    /// Interaction logic for UserBoards.xaml
    /// </summary>
    public partial class UserBoards : Window
    {
        private UserBoardsVM userBoardVM;

        public UserBoards(BackendController backendController, UserM userM)
        {
            InitializeComponent();
            this.userBoardVM = new UserBoardsVM(backendController, userM);
            this.DataContext = this.userBoardVM;
        }

        private void Remove_Board_Button_Click(object sender, RoutedEventArgs e)
        {
            userBoardVM.RemoveBoard();
        }

        private void Logout_Button_Click(object sender, RoutedEventArgs e)
        {
            userBoardVM.Logout();
            MainV mainWindow = new MainV();
            mainWindow.Show();
            this.Close();
        }

        private void Add_Board_Button_Click(object sender, RoutedEventArgs e)
        {
            userBoardVM.AddBoard();            
        }

        private void Enter_Board_Button_Click(object sender, RoutedEventArgs e)
        {
            BoardM boardM = userBoardVM.EnterBoard();
            if (boardM != null)
            {
                BoardV boardV = new BoardV(userBoardVM.BoardController, boardM);
                boardV.Show();
                this.Close();
            }
        }

        private void Join_Board_Button_Click(object sender, RoutedEventArgs e)
        {
            userBoardVM.JoinBoard();
        }


        private void InProgressTasks_Button_Click(object sender, RoutedEventArgs e)
        {
            
            InProgressTasksV inProgressTasksV = new InProgressTasksV(userBoardVM.BoardController, userBoardVM.UserM);
            inProgressTasksV.Show();
            this.Close();
        }
    }
}
