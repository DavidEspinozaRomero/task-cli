using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

  internal interface ITask
  {
    public int Id { get; set; }
    public string Description { get; set; }
    public string Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    void UpdateDescription(string description);
    void UpdateStatus(string status);

  }

/*
id: A unique identifier for the task
description: A short description of the task
status: The status of the task (todo, in-progress, done)
createdAt: The date and time when the task was created
updatedAt: The date and time when the task was last updated
*/