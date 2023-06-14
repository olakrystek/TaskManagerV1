using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagerV1
{
    class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public TaskList TaskList { get; set; }

        public User(int id, string username, string email, string password)
        {
            Id = id;
            Username = username;
            Email = email;
            Password = password;
            TaskList = new TaskList();
        }

        public void DisplayUser()
        {
            Console.WriteLine($"Logged in as: {Username}\n");
        }
    }
}
