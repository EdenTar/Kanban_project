using IntroSE.Kanban.DataAccessLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
[assembly: InternalsVisibleTo("Tests")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]
namespace IntroSE.Kanban.Backend.BuisnessLayer
{
    internal interface ITask
    {
        int Id { get; set; }
        DateTime CreationTime { get; set; }
        string Title { get; set; }
        string Description { get; set; }
        DateTime DueDate { get; set; }
        string Assignee { get; set; }
        TaskDTO TaskDTO { get; set; }
        void Delete();
        void CheckAssignee(string userEmail);
        void ChangeColumn(IColumn column);
    }
}
