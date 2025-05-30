


namespace ConsoleApp1
{

    public class Program
    {
        private static LinkedList<ToDoTask> toDoTasks = new LinkedList<ToDoTask>();
        private static LinkedList<ToDoTask> inProgressTasks = new LinkedList<ToDoTask>();
        private static LinkedList<ToDoTask> completedTasks = new LinkedList<ToDoTask>();
        private static int nextTaskId = 1;

        public static void Main(string[] args)
        {
            Console.WriteLine("Welcome to your To-Do Application!");
            DisplayMenu();
        }

        public static void DisplayMenu()
        {
            while (true)
            {
                Console.WriteLine("\n--- Main Menu ---");
                Console.WriteLine("1. Add New To-Do Task");
                Console.WriteLine("2. Move Task to In Progress");
                Console.WriteLine("3. Complete Task");
                Console.WriteLine("4. View To-Do Tasks");
                Console.WriteLine("5. View In Progress Tasks");
                Console.WriteLine("6. View Completed Tasks");
                Console.WriteLine("7. Exit");
                Console.Write("Enter your choice: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddNewTask();
                        break;
                    case "2":
                        MoveTaskToInProgress();
                        break;
                    case "3":
                        MoveTaskToComplete();
                        break;
                    case "4":
                        Console.WriteLine("\n--- To-Do Tasks ---");
                        DisplayTasks(toDoTasks);
                        break;
                    case "5":
                        Console.WriteLine("\n--- In Progress Tasks ---");
                        DisplayTasks(inProgressTasks);
                        break;
                    case "6":
                        Console.WriteLine("\n--- Completed Tasks ---");
                        DisplayTasks(completedTasks);
                        break;
                    case "7":
                        Console.WriteLine("Exiting application. Goodbye!");
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        public static void AddNewTask()
        {
            Console.WriteLine("\n--- Add New Task ---");
            Console.Write("Enter Task Name: ");
            string name = Console.ReadLine();

            Console.Write("Enter Task Description: ");
            string description = Console.ReadLine();

            DateTime date;
            while (true)
            {
                Console.Write("Enter Due Date (YYYY-MM-DD): ");
                if (DateTime.TryParse(Console.ReadLine(), out date))
                {
                    break;
                }
                Console.WriteLine("Invalid date format. Please use YYYY-MM-DD.");
            }

            ToDoTask newTask = new ToDoTask(name, nextTaskId++, description, date);
            InsertToDoTask(newTask);
            Console.WriteLine($"Task '{name}' (ID: {newTask.TaskId}) added to To-Do list.");
        }

        public static void MoveTaskToInProgress()
        {
            Console.WriteLine("\n--- Move Task to In Progress ---");
            Console.Write("Enter Task ID to move to In Progress: ");
            if (int.TryParse(Console.ReadLine(), out int taskId))
            {
                ChangeStatusToInProgress(taskId);
            }
            else
            {
                Console.WriteLine("Invalid Task ID. Please enter a number.");
            }
        }

        public static void MoveTaskToComplete()
        {
            Console.WriteLine("\n--- Complete Task ---");
            ChangeStatusToComplete();
        }

        public static void InsertToDoTask(ToDoTask newTask)
        {
            newTask.Status = "To Do";
            if (toDoTasks.Count == 0 || newTask.Date <= toDoTasks.First.Value.Date)
            {
                toDoTasks.AddFirst(newTask);
                return;
            }

            LinkedListNode<ToDoTask> current = toDoTasks.First;
            while (current != null && newTask.Date > current.Value.Date)
            {
                current = current.Next;
            }

            if (current == null)
            {
                toDoTasks.AddLast(newTask);
            }
            else
            {
                toDoTasks.AddBefore(current, newTask);
            }
        }

        public static void ChangeStatusToInProgress(int taskId)
        {
            ToDoTask taskToMove = null;
            foreach (ToDoTask task in toDoTasks)
            {
                if (task.TaskId == taskId)
                {
                    taskToMove = task;
                    break;
                }
            }

            if (taskToMove != null)
            {
                toDoTasks.Remove(taskToMove);
                taskToMove.Status = "In Progress";
                inProgressTasks.AddFirst(taskToMove);
                Console.WriteLine($"Task '{taskToMove.TaskName}' (ID: {taskToMove.TaskId}) moved to In Progress.");
            }
            else
            {
                Console.WriteLine($"Task with ID {taskId} not found in To-Do list.");
            }
        }

        public static void ChangeStatusToComplete()
        {
            if (inProgressTasks.Count == 0)
            {
                Console.WriteLine("No tasks in In Progress to complete.");
                return;
            }

            ToDoTask completedTask = inProgressTasks.First.Value;
            inProgressTasks.RemoveFirst();

            completedTask.Status = "Completed";
            completedTasks.AddLast(completedTask);
            Console.WriteLine($"Task '{completedTask.TaskName}' (ID: {completedTask.TaskId}) moved to Completed.");
        }

        public static void DisplayTasks(LinkedList<ToDoTask> taskList)
        {
            if (taskList.Count == 0)
            {
                Console.WriteLine("  No tasks to display.");
                return;
            }
            foreach (ToDoTask task in taskList)
            {
                Console.WriteLine($"  ID: {task.TaskId}, Name: {task.TaskName}, Date: {task.Date.ToShortDateString()}, Status: {task.Status}");
            }
        }
    }
}