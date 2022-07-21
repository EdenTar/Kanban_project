using IntroSE.Kanban.Frontend.Model;
using IntroSE.Kanban.Frontend.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Frontend.ViewModel
{
    public class MainVM : NotifiableObject
    {
        private DataController dataController;
        public DataController DataController { get => dataController; set { dataController = value; } }

        public MainVM() 
        {
            DataController = new DataController();
            try
            {
                DataController.LoadData();
            }
            catch (Exception e) 
            {
                new PopupErrorV(e.Message).Show();

            }
        }
    }
}
