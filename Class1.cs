using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    

    public class ToDoTask 
    {
        public string TaskName { get; set; }
        public int TaskId { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }

        public ToDoTask(string taskName, int taskId, string description, DateTime date)
        {
            TaskName = taskName;
            TaskId = taskId;
            Description = description;
            Date = date;
            Status = "To Do";
        }
    }
}
