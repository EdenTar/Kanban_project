using IntroSE.Kanban.Backend.ServiceLayer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Frontend.Model
{
    public class BoardM :NotifiableModelObject
    {
        private string creatorEmail;
        public string CreatorEmail { get => creatorEmail; set { creatorEmail = value;} }
        private string userEmail;
        public string UserEmail { get => userEmail; set { userEmail = value; } }

        private string boardName;
        public string BoardName { get => boardName; set { boardName = value; } }

        private ObservableCollection<ColumnM> columns;
        public ObservableCollection<ColumnM> Columns
        {
            get => columns;
            set 
            {
                columns = value;
                columns.CollectionChanged += HandleChange;
                HasColumns = true;
            }
        }

        private void HandleChange(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (ColumnM c in e.OldItems)
                {
                    (new ColumnController(Controller.Service)).RemoveColumn(UserM.Email, c.CreatorEmail, c.BoardName, c.Ordinal);
                }
            }
        }

        private UserM userM;
        public UserM UserM { get => userM; set { userM = value; } }
        
        public string MyBoardName
        {
            get => boardName;
            set
            {
                this.boardName = value;
                RaisePropertyChanged("MyBoardName");
            }
        }
        public string CreatorEmailBoards
        {
            get => CreatorEmail;
            set
            {
                this.CreatorEmail = value;
                RaisePropertyChanged("CreatorEmailBoards");
            }
        }
        private bool hasColumns;
        public bool HasColumns { get => hasColumns; set { hasColumns = value; } }

        internal BoardM(BackendController controller, string creatorEmail, UserM userM, string boardName) : base(controller)
        {
            //update userEmail to userM
            HasColumns = false;
            UserM = userM;
            CreatorEmail = creatorEmail;
            UserEmail = userM.Email;
            BoardName = boardName;
        }
        public void SortTasks()
        {
            foreach (ColumnM c in Columns)
            {
                c.SortByDueDate();
            }
        }

        public void Filter(string keyword) 
        {
            if (keyword != null)
            {
                foreach (ColumnM c in Columns)
                {
                    c.FilteredTasks = new ObservableCollection<TaskM>();
                    foreach (TaskM t in c.Tasks)
                    {
                        if ((t.Title.Contains(keyword) || t.Description.Contains(keyword)))
                        {
                            c.FilteredTasks.Add(t);
                        }
                    }
                }
            }
        }
        public void RemoveColumn(ColumnM SelectedColumn) 
        {
            Columns.Remove(SelectedColumn);
            ColumnM update;
            if (SelectedColumn.Ordinal == Columns.Count)
            {
                update = Columns[SelectedColumn.Ordinal-1];
                update.Tasks = (new ColumnController(Controller.Service)).GetColumn(update.UserEmail, update.CreatorEmail, update.BoardName, update.Ordinal, update);
            }
            else 
            {
                update = Columns[SelectedColumn.Ordinal];
                update.Tasks = (new ColumnController(Controller.Service)).GetColumn(update.UserEmail, update.CreatorEmail, update.BoardName, SelectedColumn.Ordinal, update);
            }
            int ordinal = 0;
            foreach (ColumnM c in Columns)
            {
                c.Ordinal = ordinal;
                ordinal++;
            }
        }
    }
}
