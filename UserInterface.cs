using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagerV1
{
    public enum UserChoice
    {
        AddTask,
        EditTask,
        ChangeTaskStatus,
        DeleteTask,
        DisplayAllTasks,
        DisplayTaskDetails,
        SortTasks,
        FilterTasks,
        Report,
        ExportTaskList,
        Exit
    }

    class UserInterface
    {
        private UserManager _userManager; // The user manager object
        private User _currentUser; // The current logged in user
        public UserInterface(UserManager userManager)
        {
            _userManager = userManager;
            _currentUser = null;
        }

        public static void ShowMainMenu()
        {
            Console.WriteLine("Main menu:");
            Console.WriteLine("1. Add a new task");
            Console.WriteLine("2. Edit an existing task");
            Console.WriteLine("3. Change task status");
            Console.WriteLine("4. Delete an existing task");
            Console.WriteLine("5. Display all tasks");
            Console.WriteLine("6. Display task details");
            Console.WriteLine("7. Sort tasks");
            Console.WriteLine("8. Filter tasks");
            Console.WriteLine("9. Generate summary report");
            Console.WriteLine("10. Export task list");
            Console.WriteLine("11. Exit");
            Console.WriteLine();
        }

        public static UserChoice GetMainMenuChoice()
        {
            Console.Write("Enter your choice (1-11): ");

            string input = Console.ReadLine();

            int choice;
            if (int.TryParse(input, out choice))
            {
                if (choice >= 1 && choice <= 11)
                {
                    // Convert the choice to a UserChoice enum value and return it
                    return (UserChoice)(choice - 1);
                }
                else
                {
                    Console.WriteLine("Invalid choice. Please enter a number between 1 and 11.");
                    return UserChoice.Exit;
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a number between 1 and 11.");
                return UserChoice.Exit;
            }
        }

        public static void ShowLoginMenu()
        {
            Console.WriteLine("Welcome to the task manager program.");
            Console.WriteLine("Please choose an option:");
            Console.WriteLine("1. Login");
            Console.WriteLine("2. Register");
            Console.WriteLine("3. Exit");
            Console.WriteLine();
        }

        public static int GetLoginMenuChoice()
        {
            Console.Write("Enter your choice (1-3): ");

            string input = Console.ReadLine();

            int choice;
            if (int.TryParse(input, out choice))
            {
                if (choice >= 1 && choice <= 3)
                {
                    return choice;
                }
                else
                {
                    Console.WriteLine("Invalid choice. Please enter a number between 1 and 3.");
                    return 3;
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a number between 1 and 3.");
                return 3;
            }
        }

        public static string ReadPassword()
        {
            string password = "";

            bool done = false;

            while (!done)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.Enter)
                {
                    done = true;
                }
                else
                {
                    password += key.KeyChar;

                    Console.Write("*");
                }
            }

            Console.WriteLine();
            return password;
        }

        private void ShowReminder()
        {
            Reminder reminder = new Reminder(_currentUser);
            reminder.ShowTasksDueToday();
        }

        public void LoginUser(string username, string password)
        {
            // Try to login the user using the user manager
            User user = _userManager.Login(username, password);

            // Check if the login was successful
            if (user != null)
            {
                // Set the current user to the logged in user
                _currentUser = user;

                Console.WriteLine("Login successful.");
                Console.Clear();
            }
            else
            {
                Console.WriteLine("Login failed.");
                Console.WriteLine();
            }
        }

        public void RegisterUser(string username, string email, string password)
        {
            // Try to register the user using the user manager
            User user = _userManager.Register(username, email, password);

            // Check if the registration was successful
            if (user != null)
            {
                // Set the current user to the registered user
                _currentUser = user;

                Console.WriteLine("Registration successful.");
                Console.Clear();
            }
            else
            {
                Console.WriteLine("Registration failed.");
                Console.WriteLine();
            }
        }

        public void AddTask()
        {
            // Check if the user is logged in
            if (_currentUser != null)
            {
                Console.Clear ();
                Console.WriteLine("Enter the task title:");
                string title = Console.ReadLine();
                Console.WriteLine("Enter the task description:");
                string description = Console.ReadLine();
                Console.WriteLine("Enter the task due date (yyyy-mm-dd):");
                string date = Console.ReadLine();
                Console.WriteLine("Enter the task priority (1 - Normal, 2 - Low, 3 - Medium, 4 - High):");
                string priority = Console.ReadLine();
                Console.WriteLine("Enter the task tags (separated by commas):");
                string tags = Console.ReadLine();

                // Try to parse the date and priority as valid values
                DateTime dueDate;
                TaskPriority taskPriority;
                if (DateTime.TryParse(date, out dueDate) && Enum.TryParse(priority, out taskPriority))
                {
                    // Create a list of tags from the input string
                    List<string> tagList = tags.Split(',').Select(t => t.Trim()).ToList();

                    // Generate a new id for the new task using the user's task list
                    int id = TaskList.GenerateNewId();

                    // Create a new simple task object with the given information and the new id
                    UserTask task = new SimpleTask(id, title, description, dueDate, taskPriority, tagList);

                    // Add the task to the user's task list
                    _currentUser.TaskList.AddTask(task);

                    _userManager.SaveData();

                    // Display a success message
                    Console.WriteLine("Task added successfully.");
                    Console.WriteLine();
                }
                else
                {
                    // Invalid input, display an error message
                    Console.WriteLine("Invalid input. Please enter valid values for date and priority.");
                    Console.WriteLine();
                }
            }
            else
            {
                // No user logged in, display an error message
                Console.WriteLine("Please login or register first.");
                Console.WriteLine();
            }
        }

        public void EditTask()
        {
            if (_currentUser != null)
            {
                Console.Clear();
                Console.WriteLine("Enter the task id:");
                string input = Console.ReadLine();

                int id;
                if (int.TryParse(input, out id))
                {
                    UserTask task = _currentUser.TaskList.GetTasks().Find(t => t.Id == id);

                    if (task != null)
                    {
                        Console.WriteLine("Enter the new task title:");
                        string title = Console.ReadLine();
                        Console.WriteLine("Enter the new task description:");
                        string description = Console.ReadLine();
                        Console.WriteLine("Enter the new task due date (yyyy-mm-dd):");
                        string date = Console.ReadLine();
                        Console.WriteLine("Enter the new task priority (1 - Normal, 2 - Low, 3 - Medium, 4 - High):");
                        string priority = Console.ReadLine();
                        Console.WriteLine("Enter the new task status (0 - NotStarted, 1 - InProgress, 2 - Completed):");
                        string status = Console.ReadLine();
                        Console.WriteLine("Enter the new task tags (separated by commas):");
                        string tags = Console.ReadLine();

                        DateTime dueDate;
                        TaskPriority taskPriority;
                        TaskStatus taskStatus;
                        if (DateTime.TryParse(date, out dueDate) && Enum.TryParse(priority, out taskPriority) && Enum.TryParse(status, out taskStatus))
                        {
                            List<string> tagList = tags.Split(',').Select(t => t.Trim()).ToList();

                            _currentUser.TaskList.EditTask(id, title, description, dueDate, taskPriority, taskStatus, tagList);

                            _userManager.SaveData();

                            Console.WriteLine("Task edited successfully.");
                            Console.WriteLine();
                        }
                        else
                        {
                            Console.WriteLine("Invalid input. Please enter valid values for date, priority and status.");
                            Console.WriteLine();
                        }
                    }
                    else
                    {
                        Console.WriteLine("Task not found.");
                        Console.WriteLine();
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid task id.");
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("Please login or register first.");
                Console.WriteLine();
            }
        }

        public void ChangeTaskStatus()
        {
            if (_currentUser != null)
            {
                Console.Clear();
                Console.WriteLine("Enter the task id:");
                string input = Console.ReadLine();

                int id;
                if (int.TryParse(input, out id))
                {
                    UserTask task = _currentUser.TaskList.GetTasks().Find(t => t.Id == id);

                    if (task != null)
                    {
                        Console.WriteLine("Enter the new task status (0 - NotStarted, 1 - InProgress, 2 - Completed):");
                        string status = Console.ReadLine();

                        TaskStatus taskStatus;
                        if (Enum.TryParse(status, out taskStatus))
                        {
                            _currentUser.TaskList.EditTask(id, task.Title, task.Description, task.DueDate, task.Priority, taskStatus, task.Tags);

                            _userManager.SaveData();

                            Console.WriteLine("Task status changed successfully.");
                            Console.WriteLine();
                        }
                        else
                        {
                            Console.WriteLine("Invalid status. Please enter a number between 0 and 2.");
                            Console.WriteLine();
                        }
                    }
                    else
                    {
                        Console.WriteLine("Task not found.");
                        Console.WriteLine();
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid task id.");
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("Please login or register first.");
                Console.WriteLine();
            }
        }

        public void DeleteTask()
        {
            if (_currentUser != null)
            {
                Console.Clear();
                Console.WriteLine("Enter the task id:");
                string input = Console.ReadLine();

                int id;
                if (int.TryParse(input, out id))
                {
                    UserTask task = _currentUser.TaskList.GetTasks().Find(t => t.Id == id);

                    if (task != null)
                    {
                        _currentUser.TaskList.RemoveTask(task);

                        _userManager.SaveData();

                        Console.WriteLine("Task deleted successfully.");
                        Console.WriteLine();
                    }
                    else
                    {
                        Console.WriteLine("Task not found.");
                        Console.WriteLine();
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid task id.");
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("Please login or register first.");
                Console.WriteLine();
            }
        }

        public void DisplayAllTasks()
        {
            if (_currentUser != null)
            {
                Console.Clear();
                _currentUser.TaskList.DisplayAllTasks();
            }
            else
            {
                Console.WriteLine("Please login or register first.");
                Console.WriteLine();
            }
        }

        public void DisplayTaskDetails()
        {
            if (_currentUser != null)
            {
                Console.Clear();
                Console.WriteLine("Enter the task id:");
                string input = Console.ReadLine();

                int id;
                if (int.TryParse(input, out id))
                {
                    UserTask task = _currentUser.TaskList.GetTasks().Find(t => t.Id == id);

                    if (task != null)
                    {
                        ComplexTaskDecorator decoratedTask = new(task);
                        decoratedTask.DisplayTask();
                        Console.WriteLine();
                    }
                    else
                    {
                        Console.WriteLine("Task not found.");
                        Console.WriteLine();
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid task id.");
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("Please login or register first.");
                Console.WriteLine();
            }
        }

        public void SortTasks()
        {
            if (_currentUser != null)
            {
                Console.Clear();
                Console.WriteLine("Enter the sorting criterion:");
                Console.WriteLine("1. Priority");
                Console.WriteLine("2. Due date");
                string input = Console.ReadLine();

                int choice;
                if (int.TryParse(input, out choice))
                {
                    if (choice >= 1 && choice <= 2)
                    {
                        ITaskSortingStrategy strategy;
                        switch (choice)
                        {
                            case 1:
                                strategy = new SortByPriority();
                                break;
                            case 2:
                                strategy = new SortByDueDate();
                                break;
                            default:
                                strategy = null;
                                break;
                        }

                        // Set the sorting strategy for the user's task list
                        _currentUser.TaskList.SetSortingStrategy(strategy);

                        Console.WriteLine("Tasks sorted successfully.");
                        Console.WriteLine();

                        IEnumerable<UserTask> sortedTasks = _currentUser.TaskList.ApplyStrategies();
                        foreach (UserTask task in sortedTasks)
                        {
                            if (choice == 1)
                            {
                                UserTask priorityTask = new PriorityTaskDecorator(task);
                                priorityTask.DisplayTask();
                                Console.WriteLine();
                            }
                            else if (choice == 2)
                            {
                                UserTask deadlineTask = new DeadlineTaskDecorator(task);
                                deadlineTask.DisplayTask();
                                Console.WriteLine();
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid choice. Please enter a number between 1 and 2.");
                        Console.WriteLine();
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a number between 1 and 2.");
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("Please login or register first.");
                Console.WriteLine();
            }
        }

        public void FilterTasks()
        {
            if (_currentUser != null)
            {
                Console.Clear();
                Console.WriteLine("Enter the filtering criterion:");
                Console.WriteLine("1. Status");
                Console.WriteLine("2. Tag");
                string input = Console.ReadLine();

                int choice;
                if (int.TryParse(input, out choice))
                {
                    if (choice >= 1 && choice <= 2)
                    {
                        ITaskFilteringStrategy strategy;
                        switch (choice)
                        {
                            case 1:
                                Console.WriteLine("Enter the status value (0 - NotStarted, 1 - InProgress, 2 - Completed):");
                                string status = Console.ReadLine();

                                TaskStatus taskStatus;
                                if (Enum.TryParse(status, out taskStatus))
                                {
                                    strategy = new FilterByStatus(taskStatus);
                                }
                                else
                                {
                                    Console.WriteLine("Invalid status. Please enter a number between 0 and 2.");
                                    Console.WriteLine();
                                    return;
                                }
                                break;
                            case 2:
                                Console.WriteLine("Enter the tag value:");
                                string tag = Console.ReadLine();

                                strategy = new FilterByTag(tag);
                                break;
                            default:
                                strategy = null;
                                break;
                        }

                        // Set the filtering strategy for the user's task list
                        _currentUser.TaskList.SetFilteringStrategy(strategy);

                        Console.WriteLine("Tasks filtered successfully.");
                        Console.WriteLine();

                        IEnumerable<UserTask> filteredTasks = _currentUser.TaskList.ApplyStrategies();
                        foreach (UserTask task in filteredTasks)
                        {
                            if (choice == 1)
                            {
                                UserTask statusTask = new StatusTaskDecorator(task);
                                statusTask.DisplayTask();
                                Console.WriteLine();
                            }
                            else if (choice == 2)
                            {
                                UserTask tagTask = new TagTaskDecorator(task);
                                tagTask.DisplayTask();
                                Console.WriteLine();
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid choice. Please enter a number between 1 and 2.");
                        Console.WriteLine();
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a number between 1 and 2.");
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("Please login or register first.");
                Console.WriteLine();
            }
        }

        public void GenerateReport()
        {
            if (_currentUser != null)
            {
                string report = ReportGenerator.GenerateSummaryReport(_currentUser);

                Console.Clear();
                ReportGenerator.DisplaySummaryReport(_currentUser);

                Console.WriteLine("Report submenu:");
                Console.WriteLine("1. Export report");
                Console.WriteLine("2. Go back");
                Console.WriteLine();

                Console.Write("Enter your choice (1-2): ");
                string input = Console.ReadLine();

                int choice;
                if (int.TryParse(input, out choice))
                {
                    if (choice == 1)
                    {
                        Console.WriteLine("\nEnter a file name to export the report:");
                        string fileName = Console.ReadLine();

                        ReportGenerator.ExportReport(report, fileName);
                    }
                    else if (choice == 2)
                    {
                        // Go back option
                        // Do nothing and return to the main menu
                    }
                    else
                    {
                        Console.WriteLine("Invalid choice. Please enter 1 or 2.");
                        Console.WriteLine();
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter 1 or 2.");
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("Please login or register first.");
                Console.WriteLine();
            }
        }

        public void ExportTaskList()
        {
            if (_currentUser != null)
            {
                Console.Clear();
                Console.WriteLine("Enter a file name to export the task list:");
                string fileName = Console.ReadLine();

                ReportGenerator.ExportTaskList(_currentUser, fileName);
            }
            else
            {
                Console.WriteLine("Please login or register first.");
                Console.WriteLine();
            }
        }

        public void Run()
        {
            Console.Clear();
            ShowLoginMenu();
            int loginChoice = GetLoginMenuChoice();

            while (loginChoice != 3)
            {
                // Check the login choice
                switch (loginChoice)
                {
                    case 1:
                        // Login option
                        Console.WriteLine("Enter your username:");
                        string username = Console.ReadLine();
                        Console.WriteLine("Enter your password:");
                        string password = ReadPassword();

                        LoginUser(username, password);
                        break;
                    case 2:
                        // Register option
                        Console.WriteLine("Enter your username:");
                        string newUsername = Console.ReadLine();
                        Console.WriteLine("Enter your email:");
                        string email = Console.ReadLine();
                        Console.WriteLine("Enter your password:");
                        string newPassword = ReadPassword();

                        RegisterUser(newUsername, email, newPassword);
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please choose between 1 and 3.");
                        break;
                }

                // Check if the user is logged in
                if (_currentUser != null)
                {
                    _currentUser.DisplayUser();
                    ShowReminder();
                    ShowMainMenu();
                    UserChoice mainChoice = GetMainMenuChoice();

                    while (mainChoice != UserChoice.Exit)
                    {
                        switch (mainChoice)
                        {
                            case UserChoice.AddTask:
                                AddTask();
                                break;
                            case UserChoice.EditTask:
                                EditTask();
                                break;
                            case UserChoice.ChangeTaskStatus:
                                ChangeTaskStatus();
                                break;
                            case UserChoice.DeleteTask:
                                DeleteTask();
                                break;
                            case UserChoice.DisplayAllTasks:
                                DisplayAllTasks();
                                break;
                            case UserChoice.DisplayTaskDetails:
                                DisplayTaskDetails();
                                break;
                            case UserChoice.SortTasks:
                                SortTasks();
                                break;
                            case UserChoice.FilterTasks:
                                FilterTasks();
                                break;
                            case UserChoice.Report:
                                GenerateReport();
                                break;
                            case UserChoice.ExportTaskList:
                                ExportTaskList();
                                break;
                            default:
                                Console.WriteLine("Invalid option. Please choose between 1 and 11.");
                                break;
                        }

                        Console.ReadLine();
                        Console.Clear();
                        ShowMainMenu();
                        mainChoice = GetMainMenuChoice();
                    }

                    // Exit option
                    _currentUser = null;
                    Console.WriteLine("Goodbye!");
                    Console.WriteLine();
                }

                Console.ReadLine();
                Console.Clear();
                ShowLoginMenu();
                loginChoice = GetLoginMenuChoice();
            }

            // Exit option
            Console.WriteLine("Goodbye!");
            Console.WriteLine();
        }
    }
}
