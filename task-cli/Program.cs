using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace task_cli
{

  public class TaskCLI
  {
    private static readonly string filePath = "tasksList.json";
    private static string inputString = "";

    public static List<string> commandsList = CommandList();
    public static void Main()
    {
      do
      {
        // input text 
        Console.Write("$ ");
        inputString = Console.ReadLine().Trim();

        // check text if not 'q'
        if (inputString != "q")
        {
          // if is not valid, ask retry
          bool IsValidCommand = CheckCommand(inputString);

          // if is valid execute command
          if (IsValidCommand)
          {
            //Do the command
            var commandAndVariables = GetCommandAndVariables(inputString!);
            var command = commandAndVariables[0];
            
            switch (command)
            {
              case "add":
                string description = string.Join(" ", commandAndVariables.Skip(1)).Replace("\"", "");
                CreateTask(new TaskTodo(0, description));
                break;
              case "update":
                {
                  int id = int.Parse(commandAndVariables[1]);
                  string newDescription = string.Join(" ", commandAndVariables.Skip(2)).Replace("\"", "");
                  UpdateTaskDescription(id, newDescription);
                }
                break;
              case "delete":
                {
                  int id = int.Parse(commandAndVariables[1]);
                  DeleteTask(id);
                }
                break;
              case "mark-in-progress":
                {
                  int id = int.Parse(commandAndVariables[1]);
                  UpdateTaskStatus(id, "in-progress");
                }
                break;
              case "mark-done":
                {
                  int id = int.Parse(commandAndVariables[1]);
                  UpdateTaskStatus(id, "done");
                }
                break;
              case "list":
                string status = commandAndVariables.Count > 1 ? commandAndVariables[1] : "" ;
                if (status == "done")
                {
                  ShowTasks("done");
                }
                else if (status == "in-progress")
                {
                  ShowTasks("in-progress");
                }
                else if (status == "todo")
                {
                  ShowTasks("todo");
                }
                else
                {
                  ShowTasks();
                }
                break;
            }
          }
        }
        // if text is q Exit the program
      } while (inputString != "q");
    }
    private static bool CheckCommand(string text)
    {
      // Check if exist and not null
      if (text == null || text == "") { return false; }

      // Split and separate to command and variables
      List<string> splicedText = text.Split(' ').ToList();

      // Check nameCLI is correct
      string nameCLI = splicedText[0];
      if (nameCLI != "task-cli")
      {
        Console.WriteLine("Error, trataste de decir task-cli?");
        return false;
      }
      if (splicedText.Count < 2)
      {
        Console.WriteLine("Porfavor ingrese: task-cli [comando] [parametros]");
        return false;
      }

      string command = splicedText[1];
      if (command != "list" && splicedText.Count <= 2)
      {
        Console.WriteLine("Porfavor ingrese: task-cli [comando] [parametros]");
        return false;
      }
      if (!commandsList.Contains(command))
      {
        Console.WriteLine("Porfavor ingrese un comando valido");
        foreach (var item in commandsList)
        {
          Console.WriteLine($"task-cli {item}");
        }
        return false;
      }
      return true;
    }
    private static List<string> GetCommandAndVariables(string text)
    {
      List<string> splicedText = text.Split(' ').ToList();
      List<string> commandAndVariables = splicedText.Slice(1, splicedText.Count - 1);
      return commandAndVariables;
    }
    private static void CreateTask(TaskTodo task)
    {
      List<TaskTodo> taskList = GetTasks();
      task.Id = taskList.Count == 0 ? 1 : taskList.Max(task => task.Id) + 1;
      taskList.Add(task);
      SaveTasks(taskList);
      Console.WriteLine($"Tarea guardada con éxito (ID:{task.Id})");
    }
    private static void SaveTasks(List<TaskTodo> tasks)
    {
      var options = new JsonSerializerOptions { WriteIndented = true };
      var json = JsonSerializer.Serialize(tasks, options);
      File.WriteAllText(filePath, json);
    }
    private static void DeleteTask(int id)
    {
      var tasks = GetTasks();
      var task = tasks.FirstOrDefault(task => task.Id == id);
      tasks.Remove(task);
      SaveTasks(tasks);
    }
    private static List<TaskTodo> GetTasks()
    {
      if (!File.Exists(filePath)) return new List<TaskTodo>();
      string json = File.ReadAllText(filePath);
      List<TaskTodo> tasksList = JsonSerializer.Deserialize<List<TaskTodo>>(json);
      return tasksList ?? new List<TaskTodo>();
    }
    private static void ShowTasks(string? status = null)
    {
      var tasks = GetTasks();
      if (status != null) tasks = tasks.Where(task => task.Status == status).ToList();
      foreach (var task in tasks)
      {
        Console.WriteLine($"Task Id: {task.Id}, Description: {task.Description}, Status: {task.Status}");
      }
    }
    private static void UpdateTaskDescription(int id, string description)
    {
      var tasks = GetTasks();
      var task = tasks.FirstOrDefault(task => task.Id == id);
      if (task == null)
      {
        Console.WriteLine($"tarea no existe con id:{id}");
        return;
      }
      task.Description = description;
      SaveTasks(tasks);
    }
    private static void UpdateTaskStatus(int id, string status)
    {
      var tasks = GetTasks();
      var task = tasks.FirstOrDefault(task => task.Id == id);
      if (task == null)
      {
        Console.WriteLine($"tarea no existe con id:{id}");
        return;
      }
      task.Status = status;
      SaveTasks(tasks);
    }
    private static List<string> CommandList()
    {
      List<string> commandsTask = [];
      commandsTask.Add("add");
      commandsTask.Add("update");
      commandsTask.Add("delete");
      commandsTask.Add("mark-in-progress");
      commandsTask.Add("mark-done");
      commandsTask.Add("list");
      commandsTask.Add("exit");
      return commandsTask;
    }
  }

  public class TaskTodo : ITask
  {
    public int Id { get; set; }
    public string Description { get; set; }
    public string Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public TaskTodo(int id, string description)
    {
      Id = id;
      Description = description;
      Status = "todo";
      CreatedAt = DateTime.Now;
      UpdatedAt = DateTime.Now;
    }

    public void UpdateDescription(string description)
    {
      Description = description;
      UpdatedAt = DateTime.Now;
    }

    public void UpdateStatus(string status)
    {
      Status = status;
      UpdatedAt = DateTime.Now;
    }
  }
}


//Console.WriteLine("Porfavor ingrese un comando");
//string? commandString = Console.ReadLine();
//Console.WriteLine(comandString);


//if (comandString == null)
//{
//    Console.WriteLine("Porfavor ingrese un comando");
//}


//public class taskCLI
//{

//Console.WriteLine("Hello, World!");

//string comandString = Console.ReadLine();

//}