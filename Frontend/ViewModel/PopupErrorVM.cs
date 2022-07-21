using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntroSE.Kanban.Frontend.View;

namespace IntroSE.Kanban.Frontend.ViewModel
{
    class PopupErrorVM : NotifiableObject
    {
        private string errorMsg;
        public string ErrorMsg 
        {
            get => errorMsg;
            set 
            {
                errorMsg = value;
                RaisePropertyChanged("errorMsg");
            }
        }
        public PopupErrorVM(string errorMsg)  
        {
            ErrorMsg = errorMsg;
        }
    }
}
