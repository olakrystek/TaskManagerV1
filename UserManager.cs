using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagerV1
{
    class UserManager
    {
        private readonly List<User> _users;
        private const string fileName = "users.csv";

        public UserManager()
        {
            _users = LoadData();

        }

        public static List<User> LoadData()
        {
            List<User> result = new List<User>();
            try
            {
                using (StreamReader sr = new StreamReader(fileName))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] fields = line.Split(',');

                        int id = int.Parse(fields[0]);
                        string username = fields[1];
                        string email = fields[2];
                        string password = fields[3];

                        User user = result.Find(u => u.Id == id);

                        if (user == null)
                        {
                            user = new User(id, username, email, password);
                            result.Add(user);
                        }

                        int taskId = int.Parse(fields[4]);
                        string title = fields[5].Trim('"');
                        string description = fields[6].Trim('"');
                        DateTime dueDate = DateTime.Parse(fields[7]);
                        TaskPriority priority = (TaskPriority)Enum.Parse(typeof(TaskPriority), fields[8]);
                        List<string> tags = fields[9].Replace("\"", "").Trim().Split(';').ToList();
                        TaskStatus status = (TaskStatus)Enum.Parse(typeof(TaskStatus), fields[10]);
                
                        UserTask task = new SimpleTask(taskId, title, description, dueDate, priority, tags, status);
                        user.TaskList.AddTask(task);
                        // Update the last id value for the user's task list
                        user.TaskList.UpdateLastId();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            return result;
        }


        public void SaveData()
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(fileName))
                {
                    foreach (User user in _users)
                    {
                        foreach (UserTask task in user.TaskList.GetTasks())
                        {
                            sw.WriteLine($"{user.Id},{user.Username},{user.Email},{user.Password},{task.Id},\"{task.Title}\",\"{task.Description}\",{task.DueDate},{task.Priority},{string.Join(";", task.Tags)},{task.Status}");
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
        }

        public User Register(string username, string email, string password)
        {
            // Check if the username or email is already taken by another user
            if (_users.Any(u => u.Username == username || u.Email == email))
            {
                Console.WriteLine("Username or email already exists.");
                return null;
            }

            // Check if the password is valid (at least 8 characters long and contains at least one digit and one letter)
            if (!IsValidPassword(password))
            {
                Console.WriteLine("Password is not valid.");
                return null;
            }

            // Generate a unique id for the new user
            int id = _users.Count + 1;

            // Create a new user object with the given information
            User user = new(id, username, email, password);

            // Add the user to the list of registered users
            _users.Add(user);

            // Return the user object
            return user;
        }

        public User Login(string username, string password)
        {
            // Find the user with the given name in the list of registered users
            User user = _users.Find(u => u.Username == username);

            // Check if the user exists and the password matches
            if (user != null && user.Password == password)
            {
                // Return the user object
                return user;
            }
            else
            {
                Console.WriteLine("Invalid username or password.");
                return null;
            }
        }

        private static bool IsValidPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                return false;
            }

            if (password.Length < 8)
            {
                return false;
            }

            bool hasDigit = false;
            bool hasLetter = false;

            foreach (char c in password)
            {
                if (char.IsDigit(c))
                {
                    hasDigit = true;
                }

                if (char.IsLetter(c))
                {
                    hasLetter = true;
                }
            }

            return hasDigit && hasLetter;
        }

    }
}
