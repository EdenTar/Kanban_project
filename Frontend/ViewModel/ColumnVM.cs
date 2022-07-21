using IntroSE.Kanban.Frontend.Model;
using IntroSE.Kanban.Frontend.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Frontend.ViewModel
{
    class ColumnVM : NotifiableObject
    {
        private ColumnM columnM;
        public ColumnM ColumnM 
        {
            get => columnM;
        }

        private string name;
        public string Name
        {
            get => name;
            set
            {
                name = value;
                RaisePropertyChanged("name");
            }
        }

        private int limit;
        public int Limit
        {
            get => limit;
            set
            {
                limit = value;
                RaisePropertyChanged("limit");
            }
        }

        public ColumnVM(ColumnM columnM)
        {
            Name = columnM.Name;
            Limit = columnM.Limit;
            this.columnM = columnM;
        }

        /// <summary>
        /// updates the name of the column.
        /// </summary>
        public void UpdateName() 
        {
            try
            {
                columnM.UpdateName(Name);
            }
            catch (Exception e) 
            {
                new PopupErrorV(e.Message).Show();
            }
        }

        /// <summary>
        /// updates the limit of the column.
        /// </summary>
        public void UpdateLimit()
        {
            try
            {
                columnM.UpdateLimit(Limit);
            }
            catch (Exception e) 
            {
                new PopupErrorV(e.Message).Show();
            }
        }
    }
}
