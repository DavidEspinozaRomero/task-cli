using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task_cli
{
  class Task : ITask
  {
    public int Id { get; set; }
    public string Description {get;set;}
    public string Status {get;set;}
    public DateTime CreatedAt {get;set;}
    public DateTime UpdatedAt { get; set;}
    Task(int id, string description)
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

