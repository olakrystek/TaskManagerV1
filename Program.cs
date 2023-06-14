using System.Threading.Tasks;
using TaskManagerV1;

class Program
{
    static void Main()
    {
        UserManager userManager = new();
        UserInterface userInterface = new(userManager);
        userInterface.Run();

    }
}

