using IntroSE.Kanban.Backend.DataAccessLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("Tests")]
namespace IntroSE.Kanban.Backend.BuisnessLayer
{

    internal interface IColumn
    {
        
        ColumnDTO ColumnDTO { get; set; }
        string ColumnId { get; set; }
        int NumberOfTasks { get; set; }
        int MaxNumOfTasks { get; set; }
        int NumberOfColumn { get; set; }
        string Name { get; set; }
        Dictionary<int, ITask> Tasks { get; set; }
        int LoadTasksInColumn();
        ITask GetTask(int taskId);
        void AddTask(ITask task);
        void RemoveTask(int taskId);
        void Delete();
        void RemoveColumnTo(IColumn column);
        bool IsFull();
        
    }
}
