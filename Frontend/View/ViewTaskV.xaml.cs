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
    /// Interaction logic for ViewTaskV.xaml
    /// </summary>
    public partial class ViewTaskV : Window
    {
        ViewTaskVM viewTaskVM;
        public ViewTaskV(BackendController backendController, UserM userM,TaskM taskM)
        {
            InitializeComponent();
            this.viewTaskVM = new ViewTaskVM(backendController, userM, taskM);
            this.DataContext = viewTaskVM;
        }

        /// <summary>
        /// Returns to the previous page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackButton(object sender, RoutedEventArgs e)
        {
            InProgressTasksV inProgressTasksV = new InProgressTasksV(viewTaskVM.TaskController, viewTaskVM.UserM);
            inProgressTasksV.Show();
            this.Close();
        }
    }
}
