using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Frontend.Model
{
    public class ColumnM: NotifiableModelObject
    {
        private string userEmail;
        public string UserEmail { get => userEmail; set { userEmail = value; } }

        private string boardName;
        public string BoardName { get => boardName; set { boardName = value; } }

        private string creatorEmail;
        public string CreatorEmail { get => creatorEmail; set { creatorEmail = value; } }

        private string name;
        public string Name { get => name;
            set {
                name = value;
                RaisePropertyChanged("name");

            }
        }

        public void UpdateName(string name) 
        {
            ((ColumnController)Controller).UpdateColumnName(UserEmail, CreatorEmail, BoardName, Ordinal, name);
        }
        public void UpdateLimit(int limit)
        {
            ((ColumnController)Controller).UpdateColumnLimit(UserEmail, CreatorEmail, BoardName, Ordinal, limit);
        }

        private int limit;
        public int Limit { get => limit;
            set{
                
                limit = value;
                RaisePropertyChanged("limit");
            } }

        private int ordinal;
        public int Ordinal { get => ordinal;
            set { 

                ordinal = value;
                if (Tasks != null)
                {
                    foreach (TaskM t in Tasks)
                    {
                        t.ColumnOrdinal = ordinal;
                    }
                    RaisePropertyChanged("ordinal");
                }
            }
        }
        private TaskM selectedTask;
        public TaskM SelectedTask { get => selectedTask; set { selectedTask = value; } }

        private ObservableCollection<TaskM> tasks;
        public ObservableCollection<TaskM> Tasks
        {
            get => tasks;
            set
            {
                tasks = value;
                RaisePropertyChanged("tasks");
                foreach(TaskM t in Tasks) 
                {
                    FilteredTasks.Add(t);
                }
            }
        }
        private ObservableCollection<TaskM> filteredTasks = new ObservableCollection<TaskM>();
        public ObservableCollection<TaskM> FilteredTasks 
        {
            get => filteredTasks;
            set 
            {
                filteredTasks = value;
                RaisePropertyChanged("filteredTasks");
            }
        }

        private BoardM boardM;
        public BoardM BoardM { get => boardM; set { boardM = value;} }

        private string columnBorder;
        public string ColumnBorder 
        {
            get => columnBorder;
            set 
            {
                columnBorder = value;
                RaisePropertyChanged("ColumnBorder");
            }
        }

        public ColumnM(ColumnController Controller ,string name, int limit, int ordinal, BoardM boardM, UserM userM) : base(Controller)
        {
            BoardM = boardM;
            BoardName = boardM.BoardName;
            CreatorEmail = boardM.CreatorEmail;
            UserEmail = userM.Email;
            Name = name;
            Limit = limit;
            Ordinal = ordinal;
            ColumnBorder = "Black";
        }

        public void SortByDueDate() 
        {
            List<TaskM> sort = new List<TaskM>();
            foreach (TaskM t in Tasks) 
            {
                sort.Add(t);
            }
            sort.Sort();
            Tasks = new ObservableCollection<TaskM>();
            foreach (TaskM t in sort) 
            {
                Tasks.Add(t);
            }
            
        }
    }
}
