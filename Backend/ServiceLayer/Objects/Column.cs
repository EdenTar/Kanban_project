using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IntroSE.Kanban.Backend.BuisnessLayer;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.ServiceLayer
{
    public class Column
    {
        private int ordinalNumber;
        public int OrdinalNumber { get => ordinalNumber; set { ordinalNumber = value; } }
        private string name;
        public string Name { get => name; set { name = value; } }

        private int limit;
        public int Limit { get => limit; set { limit = value; } }

        private IList<Task> tasks;
        public IList<Task> Tasks { get => tasks; set { tasks = value; } }
        
        /// <summary>
        /// sets the tasks using List of tasks from buisness layer.
        /// </summary>
        /// <param name="tasks">the list of buisness layer tasks.</param>
        private void SetTasks(List<BuisnessLayer.ITask> tasks)
        {            
            Tasks = new List<Task>();
            foreach (BuisnessLayer.Task bTask in tasks)
            {
                Tasks.Add(new Task(bTask));
            } 
        }

        /// <summary>
        /// initialize Column object using column object from buisness layer
        /// </summary>
        /// <param name="column">the column object from buisness layer</param>
        internal Column(BuisnessLayer.IColumn column) 
        {
            SetTasks(column.Tasks.Values.ToList());
            Limit = column.MaxNumOfTasks;
            Name = column.Name;
            OrdinalNumber = column.NumberOfColumn;
        }
    }
}
