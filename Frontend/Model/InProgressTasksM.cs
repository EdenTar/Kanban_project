using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Frontend.Model
{
    class InProgressTasksM: NotifiableModelObject
    {
        public ObservableCollection<TaskM> tasks;
        public ObservableCollection<TaskM> Tasks
        {
            get => tasks;
            set
            { tasks = value; }
        }

        private UserM userM;

        private TaskM selectedTask;
        public TaskM SelectedTask { get => selectedTask; set { selectedTask = value; } }

        public InProgressTasksM(TaskController taskController, UserM userM): base(taskController)
        {
            this.userM = userM;
            tasks = taskController.InProgressTasks(userM);
            //tasks.CollectionChanged += HandleChange;
        }



        /* private void HandleChange(object sender, NotifyCollectionChangedEventArgs e)
         {
             if (e.Action == NotifyCollectionChangedAction.Remove)
             {
                 foreach (TaskM y in e.OldItems)
                 {

                    // Controller.RemoveMessage(userM.Email, y.Id);
                 }

             }
         }*/

        public void sort()
        {
            Tasks = new ObservableCollection<TaskM>(Tasks.OrderBy(i => i.DueDate));
            RaisePropertyChanged("tasks");
        }
    }
}
