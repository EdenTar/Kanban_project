using IntroSE.Kanban.Frontend.Model;
using IntroSE.Kanban.Frontend.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Frontend.ViewModel
{
    class AddTaskVM : NotifiableObject
    {
        private TaskController taskController;
        public TaskController TaskController
        {
            get => taskController;
            set
            {
                taskController = value;
            }
        }

        private ColumnM column;

        public ColumnM Column
        {
            get => column;
            set
            {
                column = value;
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


        private DateTime dueDate;
        public DateTime DueDate
        {
            get => dueDate;
            set
            {
                dueDate = value;
            }
        }

       
        public AddTaskVM(BackendController backendController, ColumnM column)
        {
           taskController = new TaskController(backendController.Service);
           Column = column;
        }


        /// <summary>
        /// Adds new task to the board
        /// </summary>
        /// <returns></returns>
        public TaskM AddTask()
        {
            try
            {
                TaskM task = taskController.AddTask(Column,Title,Description,DueDate);
                Column.Tasks.Add(task);
                return task;
            }
            catch(Exception e)
            {
                new PopupErrorV(e.Message).Show();
                return null;
                
            }
        }


    }
}
