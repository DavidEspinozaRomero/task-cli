using task_cli;

namespace TestTaskClixUnit
{
  public class UnitTest1
  {
    [Fact]
    public void commandsList_ShouldHave()
    {
      List<string> expectedCommandsList = ["add", "update", "delete", "mark-in-progress", "mark-done", "list", "exit"];
      List<string> list = TaskCLI.commandsList;

      Assert.Equal(expectedCommandsList, list);
    }
    [Fact]
    public void filePath_ShouldHave()
    {
      string filePath = "tasksList.json";

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
    public void CheckCommand_ShouldReturnTrue() {
      string inputString = "task-cli add \"Buy groceries\"";
      
      var isValid = TaskCLI.CheckCommand(inputString);

      Assert.True(isValid);
    }
    [Fact]
    public void CheckCommand_ShouldReturnFalse() {
      string inputString = "tsk-cl add";
      
      var isValid = TaskCLI.CheckCommand(inputString);

      Assert.False(isValid);
    }
    [Fact]
    public void GetCommandAndVariables_ShouldReturnListWhitCommandAndVariables() {
      string inputString = "task-cli add \"Buy groceries\"";
      List<string> expectedList = inputString.Split(" ").ToList();

      var list = TaskCLI.GetCommandAndVariables(inputString);

      Assert.Equal(list, expectedList);
    }
    //[Fact]
    //public void CheckCommand_ShouldHave(){}
    //[Fact]
    //public void CheckCommand_ShouldHave(){}
    //[Fact]
    //public void CheckCommand_ShouldHave(){}
    //[Fact]
    //public void CheckCommand_ShouldHave(){}
    //[Fact]
    //public void CheckCommand_ShouldHave(){}
    //[Fact]
    //public void CheckCommand_ShouldHave(){}
    //[Fact]
    //public void CheckCommand_ShouldHave(){}
    //[Fact]
    //public void CheckCommand_ShouldHave()
    //{
    //  string filePath = "tasksList.json";
    //  Assert.Equal(filePath, TaskCLI.filePath);
    //}
  }
}