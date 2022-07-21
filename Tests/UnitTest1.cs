using NUnit.Framework;
using IntroSE.Kanban;
using IntroSE.Kanban.Backend.BuisnessLayer;
using Moq;
using System;
using System.IO;
using System.Diagnostics.CodeAnalysis;

namespace Tests
{
    [ExcludeFromCodeCoverage]
    public class Tests
    {

        private Column column;
        private int columnOrdinal = 0;
        internal Mock<ITask> task;
        private Mock<IColumn> columnMoq;        

        [SetUp]
        public void Setup()
        {
            task = new Mock<ITask>();
            columnMoq = new Mock<IColumn>();
            if (column!=null)
                column.Delete();
            column = new Column("Test", columnOrdinal, "Column Unit Test");         
        }

        
        [Test]
        public void Add_Task_Test()
        {
            //enter task that is null - check if there is Exception
            var exception = Assert.Throws<ArgumentNullException>(() => column.AddTask(null));
            Assert.AreEqual(exception.Message, "Value cannot be null. (Parameter 'Task cannot be null...')");

            //enter task that is not null and enter full column - check if there is Exception
            column.MaxNumOfTasks = 0;
            var exception1 = Assert.Throws<Exception>(() => column.AddTask(task.Object));
            Assert.AreEqual(exception1.Message, "The column is already full...");

            //enter task that is not null and column is nut full- check if the taskID is in task. check if numberofTasks updated
            int currentNumOfTasks = column.NumberOfTasks;
            column.MaxNumOfTasks = 2;
            column.AddTask(task.Object);
            Assert.AreEqual(true, column.Tasks.ContainsKey(task.Object.Id), "the remove task didn't entered to the dictionary");
            Assert.AreEqual(true, column.NumberOfTasks == currentNumOfTasks + 1, "the number of the tasks in the column didn't updated");
        }

        
        [Test]
        public void Remove_Task_Test()
        {
            //try to remove task that is not exist in this column - check if there is Exception
            int id = task.Object.Id + 1;
            var exception1 = Assert.Throws<Exception>(() => column.RemoveTask(id));
            Assert.AreEqual(exception1.Message, "Task does not exists, in this column...");

            //try to remove task that exist in this column - check that this task doesn't exist in the tasks dictionary anymore.
            column.AddTask(task.Object);
            column.RemoveTask(task.Object.Id);
            Assert.AreEqual(false, column.Tasks.ContainsKey(task.Object.Id), "the remove task failed in remove");
        }
        
        [Test]
        public void RemoveColumnTo_Test()
        {
            //try to remove column and add all his tasks to another column so that the sum of the two task's will be above the limit - check if there is Exception.
            columnMoq.Setup(m => m.MaxNumOfTasks).Returns(-1);
            columnMoq.Setup(m => m.NumberOfTasks).Returns(2);
            column.RemoveColumnTo(columnMoq.Object);
            Assert.AreEqual(0, column.NumberOfTasks, "the function didn't updated the field NumberOfTasks of the column that we removed.");
            Assert.AreEqual(true, !column.Tasks.ContainsKey(task.Object.Id), "the tasks didn't deleted from the column dictionary that we removed");


            //try to remove column and add all his tasks to another column so that the sum of the two task's will be above the limit - check if there is Exception.
            column.MaxNumOfTasks = 1;
            columnMoq.Setup(m => m.MaxNumOfTasks).Returns(1);
            columnMoq.Setup(m => m.NumberOfTasks).Returns(2);
            var exception3 = Assert.Throws<Exception>(() => column.RemoveColumnTo(columnMoq.Object));
            Assert.AreEqual(exception3.Message, "the amount of tasks exceeds the limit in the new column");

            //try to remove column and add all his tasks to another column.
            //the sum of the tasks's of the two columns will be under/equal to the limit. 
            //check that the column that we deleted will be empty from tasks.
            column.AddTask(task.Object);
            int columnNum = column.NumberOfTasks;
            columnMoq.Setup(m => m.MaxNumOfTasks).Returns(3);
            columnMoq.Setup(m => m.NumberOfTasks).Returns(1);
            columnMoq.Setup(m => m.AddTask(task.Object));
            column.RemoveColumnTo(columnMoq.Object);
            Assert.AreEqual(0,column.NumberOfTasks, "the function didn't updated the field NumberOfTasks of the column that we removed.");
            Assert.AreEqual(true, !column.Tasks.ContainsKey(task.Object.Id), "the tasks didn't deleted from the column dictionary that we removed");
        }


    }
}