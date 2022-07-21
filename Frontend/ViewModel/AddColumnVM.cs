using IntroSE.Kanban.Frontend.Model;
using IntroSE.Kanban.Frontend.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Frontend.ViewModel
{
    class AddColumnVM: NotifiableObject
    {
        private ColumnController columnController;
        

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

        private string ordinal;
        public string Ordinal
        {
            get => ordinal;
            set
            {
                ordinal = value;
                RaisePropertyChanged("ordinal");
            }
        }

        private BoardM boardM;
        public BoardM BoardM
        {
            get => boardM;
             private set
            {
                boardM = value;
            }
        }
        private UserM userM;
        public UserM UserM
        {
            get => userM;
            private set
            {
                userM = value;
      
            }
        }

        public AddColumnVM (BackendController controller,BoardM boardM, UserM userM)
        {
            columnController = new ColumnController(controller.Service);
            BoardM = boardM;
            UserM = userM;
        }

        /// <summary>
        /// Adds new column to the board
        /// </summary>
        /// <returns></returns>
        public ColumnM AddColumn()
        {
            try
            {
                return columnController.AddColumn(UserM,BoardM,Int32.Parse(Ordinal),Name);
            }
            catch (Exception e)
            {
                new PopupErrorV(e.Message).Show();
                return null;
            }
        }
    }

}
