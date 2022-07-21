using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Frontend.Model
{
    public class TaskM : NotifiableModelObject, IComparable

    {

        private string borderColor;
        public string BorderColor 
        {
            get => borderColor;
            set 
            {
                borderColor = value;
                RaisePropertyChanged("borderColor");
            } 
        }

        private string dueDateColor;
        public string DueDateColor 
        {
            get => dueDateColor;
            set 
            {
                dueDateColor = value;
                RaisePropertyChanged("dueDateColor");
            }
        }

        private string userEmail;
        public string UserEmail { get => userEmail; set { userEmail = value; } }

        private string boardName;
        public string BoardName { get => boardName; set { boardName = value; } }

        private string creatorEmail;
        public string CreatorEmail { get => creatorEmail; set { creatorEmail = value; } }

        private int columnOrdinal;
        public int ColumnOrdinal { get => columnOrdinal; set { columnOrdinal = value; } }

        private ColumnM columnM;
        public ColumnM ColumnM { get => columnM; set { columnM = value; } }


        private int id;
        public int Id
        {
            get => id;
            private set
            {
                id = value;
            }
        }

        private string title;
        public string Title
        {
            get => title;
            set
            { 
               title = value;
            }
        }

        private string description;
        public string Description
        {
            get => description;
            set
            {
                description = value;
            }
        }

        private double timeTillDueDate;
        public double TimeTillDueDate 
        {
            get => timeTillDueDate;
            set 
            {
                if (value > 100)
                {
                    timeTillDueDate = 100;
                }
                else
                {
                    timeTillDueDate = value;
                }
                RaisePropertyChanged("timeTillDueDate");
            }
        }

        private DateTime dueDate;
        public DateTime DueDate
        {
            get => dueDate;
            set
            {
                TimeTillDueDate = DateTime.Now.Subtract(CreationTime).Divide(value.Subtract(CreationTime)) * 100;
                if (DateTime.Now.CompareTo(value) > 0)
                {

                    DueDateColor = "Red";
                }
                else if (DateTime.Now.Subtract(CreationTime).Divide(value.Subtract(CreationTime)) > 0.75)
                {
                    DueDateColor = "Orange";
                }
                else
                {
                    DueDateColor = "Green";
                }

                dueDate = value;
            }
        }

        private string assignee;
        public string Assignee
        {
            get => assignee;
            set
            {
                
                if (userEmail == value)
                {
                    BorderColor = "Blue";
                }
                else
                {
                    BorderColor = "Black";
                }
                assignee = value;
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

        internal TaskM(TaskController controller, int id, string title, string description,string assignee, 
            DateTime dueDate, DateTime creationTime ,ColumnM columnM) : base(controller)
        {
            Id = id;
            Title= title;
            Description = description;
            UserEmail = columnM.UserEmail;
            Assignee = assignee;
            CreationTime = creationTime;
            DueDate = dueDate;
            CreatorEmail = columnM.CreatorEmail;
            BoardName = columnM.BoardName;
            ColumnOrdinal = columnM.Ordinal;
            ColumnM = columnM;
        }

        internal TaskM(TaskController controller, int id, string title, string description, string assignee,
          DateTime dueDate, DateTime creationTime) : base(controller)
        {
            Id = id;
            Title = title;
            Description = description;
            Assignee = assignee;
            CreationTime = creationTime;
            DueDate = dueDate;
            
          
        }

        public int CompareTo(object obj)
        {
            return dueDate.CompareTo(((TaskM)obj).DueDate);
        }


    }
}
