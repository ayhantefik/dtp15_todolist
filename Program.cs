using System.Data;

using System.Data;

namespace dtp15_todolist
{
    public class Todo
    {
        public static List<TodoItem> list = new List<TodoItem>();

        public const int Active = 1;
        public const int Waiting = 2;
        public const int Ready = 3;
        public static string StatusToString(int status)
        {
            switch (status)
            {
                case Active: return "aktiv";
                case Waiting: return "väntande";
                case Ready: return "klar";
                default: return "(felaktig)";
            }
        }
        public class TodoItem
        {
            public int status;
            public int priority;
            public string task;
            public string taskDescription;
            public TodoItem(int priority, string task)
            {
                this.status = Active;
                this.priority = priority;
                this.task = task;
                this.taskDescription = "";
            }
            public TodoItem(string todoLine)
            {
                string[] field = todoLine.Split('|');
                status = Int32.Parse(field[0]);
                priority = Int32.Parse(field[1]);
                task = field[2];
                taskDescription = field[3];
            }
            public void Print(string command)
            {
                string statusString = StatusToString(status);
                Console.Write($"|{statusString,-12}|{priority,-6}|{task,-20}|");
                if (command == "beskriv")
                {
                    Console.WriteLine($"{taskDescription,-40}|");
                }
                else if (command == "beskriv allt")
                {
                    Console.WriteLine($"{taskDescription,-40}|");
                }
                else
                    Console.WriteLine();
            }
            public void PrintAllt()
            {
                string statusString = StatusToString(status);
                Console.Write($"|{statusString,-12}|{priority,-6}|{task,-20}|");
                Console.WriteLine();
            }
        }
        public static void SaveList()
        {
            using (StreamWriter sw = new StreamWriter("todo.lis"))
            {
                for (int i = 0; i < list.Count; i++)
                {
                    string line = $"{list[i].status}|{list[i].priority}|{list[i].task}|{list[i].taskDescription}";
                    sw.WriteLine(line);
                }
            }


        }
        public static void ReadListFromFile()
        {
            string todoFileName = "todo.lis";
            Console.Write($"Läser från fil {todoFileName} ... ");
            StreamReader sr = new StreamReader(todoFileName);
            int numRead = 0;

            string line;
            while ((line = sr.ReadLine()) != null)
            {
                TodoItem item = new TodoItem(line);
                list.Add(item);
                numRead++;
            }
            sr.Close();
            Console.WriteLine($"Läste {numRead} rader.");
        }
        public static void NewAssignment()
        {
            string status = "1";
            Console.Write("Uppgiftens namn: ");
            string name = Console.ReadLine();
            Console.Write("Prioritet: ");
            string prio = Console.ReadLine();
            Console.Write("Beskrivning: ");
            string desc = Console.ReadLine();
            string line = $"{status}|{prio}|{name}|{desc}";
            TodoItem item = new TodoItem(line);
            list.Add(item);
        }
        private static void PrintHeadOrFoot(string command, bool head, bool verbose)
        {
            if (head)
            {
                Console.Write("|status      |prio  |namn                |");
                if (command == "beskriv") Console.WriteLine("beskrivning                             |");
                else if (command == "beskriv allt") Console.WriteLine("beskrivning                             |");
                else Console.WriteLine();
            }
            Console.Write("|------------|------|--------------------|");
            if (command == "beskriv") Console.WriteLine("----------------------------------------|");
            else if (command == "beskriv allt") Console.WriteLine("----------------------------------------|");
            else Console.WriteLine();
        }
        private static void PrintHead(string command, bool verbose)
        {
            PrintHeadOrFoot(command, head: true, verbose);
        }
        private static void PrintFoot(string command, bool verbose)
        {
            PrintHeadOrFoot(command, head: false, verbose);
        }
        public static void PrintTodoList(string command, bool verbose = false)
        {
            PrintHead(command, verbose);
            if (!verbose)
            {
                if (command == "lista")
                {
                    for (int i = 0; i < list.Count; i++)
                    {
                        if (Todo.list[i].status == 1)
                        {
                            Todo.list[i].Print(command);
                        }
                    }
                }
                else if (command == "lista väntande")
                {
                    for (int i = 0; i < list.Count; i++)
                    {
                        if (Todo.list[i].status == 2)
                        {
                            Todo.list[i].Print(command);
                        }
                    }
                }
                else if (command == "lista klara")
                {
                    for (int i = 0; i < list.Count; i++)
                    {
                        if (Todo.list[i].status == 3)
                        {
                            Todo.list[i].Print(command);
                        }
                    }
                }
                else if (command == "beskriv")
                {
                    for (int i = 0; i < list.Count; i++)
                    {
                        if (Todo.list[i].status == 1)
                        {
                            Todo.list[i].Print(command);
                        }
                    }
                }
            }
            else
            {
                foreach (TodoItem item in list)
                {
                    item.Print(command);
                }
            }

            PrintFoot(command, verbose);
        }
        public static void ChangeToActiv(string command)
        {
            string status = command.Trim();
            string[] cwords = status.Split(' ');
            string expected = $"{cwords[1]} {cwords[2]}";
            if (status == "")
            {
                Console.WriteLine("FEL!");
            }
            else
            {
                for (int i = 0; i < Todo.list.Count; i++)
                {
                    if (expected == Todo.list[i].task)
                    {
                        Todo.list[i].status = 1;
                    }
                }
                Console.WriteLine($"{cwords[1]} {cwords[2]} har ändrats till aktiv!");
            }
        }
        public static void ChangeToWaiting(string command)
        {
            string status = command.Trim();
            string[] cwords = status.Split(' ');
            string expected = $"{cwords[1]} {cwords[2]}";
            if (status == "")
            {
                Console.WriteLine("FEL!");
            }
            else
            {
                for (int i = 0; i < Todo.list.Count; i++)
                {
                    if (expected == Todo.list[i].task)
                    {
                        Todo.list[i].status = 2;
                    }
                }
                Console.WriteLine($"{cwords[1]} {cwords[2]} har ändrats till vänta!");
            }
        }
        public static void ChangeToReady(string command)
        {
            string status = command.Trim();
            string[] cwords = status.Split(' ');
            string expected = $"{cwords[1]} {cwords[2]}";
            if (status == "")
            {
                Console.WriteLine("FEL!");
            }
            else
            {
                for (int i = 0; i < Todo.list.Count; i++)
                {
                    if (expected == Todo.list[i].task)
                    {
                        Todo.list[i].status = 3;
                    }
                }
                Console.WriteLine($"{cwords[1]} {cwords[2]} har ändrats till klar!");
            }

        }
        public static void PrintHelp()
        {
            Console.WriteLine("Kommandon:");
            Console.WriteLine();
            Console.WriteLine("hjälp           listar denna hjälp");
            Console.WriteLine();
            Console.WriteLine("ny              skapar en ny uppgift");
            Console.WriteLine("beskriv         listar alla Active uppgifter och beskrivning");
            Console.WriteLine("beskriv allt    listar alla uppgifter (oavsätt status) och beskrivning");
            Console.WriteLine("lista           listar alla Active uppgifter");
            Console.WriteLine("lista allt      listar alla uppgifter oavsett status");
            Console.WriteLine("lista väntante  listar alla väntande uppgifter");
            Console.WriteLine("lista klara     listar alla klara uppgifter");
            Console.WriteLine("spara           sparar uppgifterna");
            Console.WriteLine("ladda           laddar listan todo.lis");
            Console.WriteLine("aktivera        /uppgift/");
            Console.WriteLine("klar            /uppgift/");
            Console.WriteLine("vänta           /uppgift/");
            Console.WriteLine("sluta           sparar senast laddade filen och avsluta programmet!");
            Console.WriteLine("");
        }
    }
    class MainClass
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Välkommen till att-göra-listan!");
            Todo.PrintHelp();
            string command;
            string raw;
            do
            {
                command = MyIO.ReadCommand("> ");
                if (MyIO.Equals(command, "hjälp"))
                {
                    Todo.PrintHelp();
                }
                else if (MyIO.Equals(command, "sluta"))
                {
                    Console.WriteLine("Hej då!");
                    break;
                }
                else if (MyIO.Equals(command, "ladda"))
                {
                    Todo.ReadListFromFile();
                }
                else if (MyIO.Equals(command, "spara"))
                {
                    Todo.SaveList();
                }

                else if (MyIO.Equals(command, "ny"))
                {
                    Todo.NewAssignment();
                }
                else if (MyIO.Equals(command, "beskriv"))
                {
                    if (MyIO.HasArgument(command, "allt"))
                        Todo.PrintTodoList(command, verbose: true);
                    else
                        Todo.PrintTodoList(command, verbose: false);
                }
                else if (MyIO.Equals(command, "lista"))
                {
                    if (MyIO.HasArgument(command, "allt"))
                        Todo.PrintTodoList(command, verbose: true);
                    else
                        Todo.PrintTodoList(command, verbose: false);
                }
                else if (MyIO.Equals(command, "lista väntande"))
                {
                    Todo.PrintTodoList(command, verbose: false);
                }
                else if (MyIO.Equals(command, "lista klara"))
                {
                    Todo.PrintTodoList(command, verbose: false);
                }
                else if (MyIO.Equals(command, "aktivera"))
                {
                    Todo.ChangeToActiv(command);
                }
                else if (MyIO.Equals(command, "vänta"))
                {
                    Todo.ChangeToWaiting(command);
                }
                else if (MyIO.Equals(command, "klar"))
                {
                    Todo.ChangeToReady(command);
                }
                else
                {
                    Console.WriteLine($"Okänt kommando: {command}");
                }
            }
            while (true);
        }
    }
    class MyIO
    {
        static public string ReadCommand(string prompt)
        {
            Console.Write(prompt);
            return Console.ReadLine();
        }
        static public bool Equals(string rawCommand, string expected)
        {
            string command = rawCommand.Trim();
            if (command == "") return false;
            else
            {
                string[] cwords = command.Split(' ');
                if (cwords[0] == expected) return true;
            }
            return false;
        }
        static public bool HasArgument(string rawCommand, string expected)
        {
            string command = rawCommand.Trim();
            if (command == "") return false;
            else
            {
                string[] cwords = command.Split(' ');
                if (cwords.Length < 2) return false;
                if (cwords[1] == expected) return true;
            }
            return false;
        }
    }
}