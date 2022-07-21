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
    /// Interaction logic for AddBoard.xaml
    /// </summary>
    public partial class AddBoard : Window
    {
        private AddBoardVM addBoardVM;
        public AddBoard(BackendController controller, UserM userM)
        {
            InitializeComponent();
            this.addBoardVM = new AddBoardVM(controller, userM);
            this.DataContext = addBoardVM;
        }

        /// <summary>
        /// Adds new board button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewBoardButton(object sender, RoutedEventArgs e)
        {
            addBoardVM.AddBoard();
            UserBoards userBoard = new UserBoards(addBoardVM.BoardController, addBoardVM.UserM);
            userBoard.Show();
            this.Close();
        }

        /// <summary>
        /// Returns to previous window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackButton(object sender, RoutedEventArgs e)
        {
            UserBoards userBoard = new UserBoards(addBoardVM.BoardController,addBoardVM.UserM);
            userBoard.Show();
            this.Close();
        }

       
    }
}
