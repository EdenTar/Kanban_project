using IntroSE.Kanban.Frontend.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Frontend.ViewModel
{
    class ViewTaskVM: NotifiableObject
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

          private UserM userM;
          public UserM UserM
          {
              get => userM;
              private set
              {
                  this.userM = value;
              }
          }

        private TaskM taskM;
        public TaskM TaskM
        {
            get => taskM;
            private set
            {
                this.taskM = value;
            }
        }

        private int id;
        public int Id
        {
            get => id;
            private set
            {
                id = value;
                RaisePropertyChanged("id");
            }
        }

        private string title;
        public string Title
        {
            get => title;
            private set
            {
                title = value;
                RaisePropertyChanged("title");
            }
        }


        private string description;
        public string Description
        {
            get => description;
            private set
            {

                description = value;
                RaisePropertyChanged("description");
            }
        }


        private DateTime dueDate;
        public DateTime DueDate
        {
            get => dueDate;
            private set
            {
                dueDate = value;
                RaisePropertyChanged("dueDate");

            }
        }

        private string assignee;
        public string Assignee
        {
            get => assignee;
            set
            {
                assignee = value;
                RaisePropertyChanged("assignne");
            }
        }

        private DateTime creationTime;
        public DateTime CreationTime
        {
            get => creationTime;
            private set
            {
                creationTime = value;
            }
        }

        public ViewTaskVM(BackendController controller,UserM userM, TaskM task)
        {
            taskController = new TaskController(controller.Service);
            UserM = userM;
            TaskM = task;
            Id = TaskM.Id;
            Title = TaskM.Title;
            Description = TaskM.Description;
            dueDate = TaskM.DueDate;
            CreationTime = TaskM.CreationTime;
            Assignee = TaskM.Assignee;
        }


    }
}
