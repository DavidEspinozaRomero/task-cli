using System.Collections.Generic;
using System.Text.Json;
using task_cli;

namespace TestTaskClixUnit
{
  public class TaskCLITest : IDisposable
  {
    string filePath = "test_tasks.json";
    public TaskCLITest()
    {
      // BeforeEach: se ejecuta antes de cada test
      TaskCLI.filePath = filePath;      // redirigir la escritura a un archivo temporal
    }
    public void Dispose()
    {
      // Cleanup
      if (File.Exists(filePath))
        File.Delete(filePath);
    }

    [Fact]
    public void commandsList_ShouldHave()
    {
      List<string> expectedCommandsList = ["add", "update", "delete", "mark-in-progress", "mark-done", "list", "exit"];
      List<string> list = TaskCLI.commandsList;

      Assert.Equal(expectedCommandsList, list);
    }
    [Fact]
    public void filePath_ShouldExist()
    {
      string filePath = "test_tasks.json";

      Assert.Equal(filePath, TaskCLI.filePath);
    }
    [Fact]
    public void inputString_ShouldExist()
    {
      string inputString = "";

      Assert.Equal(inputString, "");
    }

    //Methods
    [Fact]
    public void CheckCommand_ShouldReturnTrue()
    {
      string inputString = "task-cli add \"Buy groceries\"";

      var isValid = TaskCLI.CheckCommand(inputString);

      Assert.True(isValid);
    }
    [Fact]
    public void CheckCommand_ShouldReturnFalse()
    {
      string inputString = "tsk-cl add";

      var isValid = TaskCLI.CheckCommand(inputString);

      Assert.False(isValid);
    }
    [Fact]
    public void GetCommandAndVariables_ShouldReturnListWhitCommandAndVariables()
    {
      string inputString = "task-cli add \"Buy groceries\"";
      List<string> expectedList = inputString.Split(" ").ToList();

      var list = TaskCLI.GetCommandAndVariables(inputString);

      Assert.Equivalent(list, expectedList);
    }
    [Fact]
    public void CreateTask_ShouldCreateTask()
    {
      var task = new TaskTodo(0, "test create");
      var task2 = new TaskTodo(0, "test2 create");
      var task3 = new TaskTodo(0, "test3 create");
      TaskCLI.CreateTask(task);
      TaskCLI.CreateTask(task2);
      TaskCLI.CreateTask(task3);

      Assert.True(File.Exists(filePath));

      var fileContent = File.ReadAllText(filePath);
      var list = JsonSerializer.Deserialize<List<TaskTodo>>(fileContent);

      Assert.Equal(3, list!.Count);
      Assert.Equal(3, list[2].Id);

    }
    [Fact]
    public void SaveTasks_ShouldCreateJsonFileWithCorrectContent()
    {
      // Arrange

      var tasks = new List<TaskTodo>();
      tasks.Add(new TaskTodo(1, "Test task"));

      // Act
      TaskCLI.SaveTasks(tasks);

      // Assert
      Assert.True(File.Exists(filePath));

      var fileContent = File.ReadAllText(filePath);
      var deserialized = JsonSerializer.Deserialize<List<TaskTodo>>(fileContent);

      Assert.NotNull(deserialized);
      Assert.Single(deserialized);
      Assert.Equal(tasks[0].Id, deserialized[0].Id);
      Assert.Equal(tasks[0].Description, deserialized[0].Description);
      Assert.Equal(tasks[0].Status, deserialized[0].Status);

     
    }
    [Fact]
    public void GetTasks_ShouldReturnListOfTasks()
    {
      var task = new TaskTodo(1, "test save");
      List<TaskTodo> taskTodos = new List<TaskTodo>();
      taskTodos.Add(task);
      TaskCLI.SaveTasks(taskTodos);

      var list = TaskCLI.GetTasks();

      Assert.Equal(1, list.Count);
      Assert.Equivalent(list, taskTodos);
    }
    [Fact]
    public void DeleteTask_ShouldDeleteTaskById()
    {

      TaskCLI.CreateTask(new(1, "delete task1"));
      TaskCLI.CreateTask(new(0, "delete task2"));

      var list = TaskCLI.GetTasks();
      Assert.Equal(list.Count, 2);
      TaskCLI.DeleteTask(1);

      list = TaskCLI.GetTasks();
      Assert.Equal(list.Count, 1);

    }


    //[Fact]
    //public void _Should(){}
    //[Fact]
    //public void _Should(){}
    //[Fact]
    //public void _Should(){}
    //[Fact]
    //public void _Should()
    //{
    //}
  }
}