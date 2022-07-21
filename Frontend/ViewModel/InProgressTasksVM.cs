using IntroSE.Kanban.Frontend.Model;
using IntroSE.Kanban.Frontend.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace IntroSE.Kanban.Frontend.ViewModel
{
    class InProgressTasksVM : NotifiableObject
    {
        private TaskController taskController;
        public TaskController TaskController
        {
            get => this.taskController;
            private set
            {
                taskController = value;
            }
        }

        private InProgressTasksM inProgressTasksM;
        public InProgressTasksM InProgressTasksM
        {
            get => this.inProgressTasksM;
            private set
            {
                inProgressTasksM = value;
            }
        }


        private UserM userM;

        public UserM UserM
        {
            get => userM;
            private set
            {
                this.userM = value;
            }
        }

        private string email;
        public string Email
        {
            get => email;
            set
            {
                this.email = value;
                RaisePropertyChanged("Email");
            }
        }
        private TaskM selectedTask;
        public TaskM SelectedTask
        {
            get
            {
                return selectedTask;
            }
            set
            {
                selectedTask = value;
                RaisePropertyChanged("selectedTask");
            }
        }

        public InProgressTasksVM(BackendController controller, UserM userM)
        {
            taskController = new TaskController(controller.Service);
            UserM = userM;
            Email = userM.Email;
            InProgressTasksM = new InProgressTasksM(TaskController, userM);

        }

        public void sort()
        {
            InProgressTasksM.sort();
        }



    }
}
