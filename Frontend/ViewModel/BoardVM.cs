using IntroSE.Kanban.Frontend.Model;
using IntroSE.Kanban.Frontend.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Frontend.ViewModel
{
    class BoardVM : NotifiableObject
    {
        private BoardM boardM;
        public BoardM BoardM { get => boardM; set { boardM = value; } }

        private ColumnM columnToMove;
        public ColumnM ColumnToMove 
        {
            get => columnToMove;
            set 
            {
                columnToMove = value;
            }
        }

        private ColumnM selectedColumn;
        public ColumnM SelectedColumn { get => selectedColumn; set { selectedColumn = value; } }
        public BoardVM(BoardM boardM)
        { 
                BoardController controller = new BoardController(boardM.Controller.Service);
                BoardM = controller.GetBoard(boardM);   
        }

        /// <summary>
        /// access the selected task from the right column
        /// </summary>
        /// <returns>TaskM representing the selected task</returns>
        public TaskM GetSelectedTask() 
        {
            try
            {
                foreach (ColumnM c in BoardM.Columns)
                {
                    if (c.SelectedTask != null)
                    {
                        return c.SelectedTask;
                    }
                }
            }
            catch (Exception e) 
            {
                new PopupErrorV(e.Message).Show();
            }
            return null;
        }

        /// <summary>
        /// logs out the user.
        /// </summary>
        public void Logout()
        {
            try
            {
                UserController userController = new UserController(boardM.Controller.Service);
                userController.Logout(boardM.UserM.Email);
            }
            catch (Exception e) 
            {
                new PopupErrorV(e.Message).Show();
            }
        }

        /// <summary>
        /// sorts the tasks by due date
        /// </summary>
        public void SortTasks() 
        {
            BoardM.SortTasks();
        }
        private string filterKeyword;
        public string FilterKeyword 
        {
            get => filterKeyword;
            set 
            {
                filterKeyword = value;
                RaisePropertyChanged("filterKeyword");
            }
        }

        private string chosenKey;
        public string ChosenKey { get => chosenKey; set { chosenKey = value; } }
        
        /// <summary>
        /// filters the tasks by chosen key.
        /// </summary>
        public void Filter() 
        {
            BoardM.Filter(FilterKeyword);
        }

        /// <summary>
        /// moves the column to a selected column.
        /// </summary>
        public void MoveColumn() 
        {
            try
            {
                if (SelectedColumn != null && ColumnToMove != null)
                {
                    ColumnController controller = new ColumnController(boardM.Controller.Service);
                    int oldI = ColumnToMove.Ordinal;
                    int newI = SelectedColumn.Ordinal;
                    int shift = newI - oldI;
                    controller.MoveColumn(ColumnToMove.UserEmail, ColumnToMove.CreatorEmail, ColumnToMove.BoardM.BoardName, ColumnToMove.Ordinal, shift);
                    BoardM.Columns.Move(oldI, newI);
                    int ordinal = 0;
                    foreach (ColumnM c in BoardM.Columns)
                    {
                        c.Ordinal = ordinal;
                        ordinal++;
                    }
                    ColumnToMove.ColumnBorder = "Black";
                    ColumnToMove = null;
                    SelectedColumn = null;
                }
            }
            catch (Exception e) 
            {
                ColumnToMove.ColumnBorder = "Black";
                ColumnToMove = null;
                SelectedColumn = null;
                new PopupErrorV(e.Message).Show();
            }
        }

        /// <summary>
        /// stage the column to move.
        /// </summary>
        public void StageMoveColumn() 
        {
            try
            {
                if (SelectedColumn != null)
                {
                    ColumnToMove = SelectedColumn;
                    ColumnToMove.ColumnBorder = "Cyan";
                }
            }
            catch (Exception e) 
            {
                new PopupErrorV(e.Message).Show();
            }
        }

        /// <summary>
        /// removes column from board.
        /// </summary>
        public void RemoveColumn() 
        {
            try
            {
                if (SelectedColumn != null)
                {
                    if (columnToMove != null)
                    {
                        ColumnToMove.ColumnBorder = "Black";
                    }
                    ColumnToMove = null;
                    BoardM.RemoveColumn(SelectedColumn);
                }
            }
            catch (Exception e) 
            {
                new PopupErrorV(e.Message).Show();
            }
        }
    }
}
