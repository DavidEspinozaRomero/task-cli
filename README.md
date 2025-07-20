# 📝 Task Tracker CLI

Task Tracker is a simple command-line interface (CLI) application built in C# to help you **track, manage, and organize your tasks** directly from the terminal. You can easily add tasks, update them, delete them, and keep track of their progress through a local JSON file.

https://roadmap.sh/projects/task-tracker
---

## 🚀 Features

- ✅ Add new tasks
- ✏️ Update existing tasks
- 🗑️ Delete tasks
- 📌 Mark tasks as **todo**, **in progress**, or **done**
- 📋 List all tasks or filter by status
- 💾 Persistent storage using a local JSON file

---

## ⚙️ Requirements

- [.NET SDK](https://dotnet.microsoft.com/download) installed on your machine
- Console or terminal access

---

## 📦 How to Use

### 🔧 Build the Application

```bash
dotnet build
```

▶️ Run the Application
```bash
dotnet run -- [command] [arguments...]
```

> Or if you compiled to an executable:
```bash
task-cli [command] [arguments...]
```


💡 Commands & Usage

📌 TO EXIT 
```bash
q
```

➕ Add a New Task
```bash
task-cli add "Buy groceries"

Output: Task added successfully (ID: 1)
```

✏️ Update a Task
```bash
task-cli update 1 "Buy groceries and cook dinner"
```
🗑️ Delete a Task
```bash
task-cli delete 1
```
🔄 Change Task Status
```bash
task-cli mark-in-progress 1
task-cli mark-done 1
```
📋 List Tasks
All tasks:
```bash
task-cli list
```
Tasks by status:
```bash
task-cli list todo
task-cli list in-progress
task-cli list done
```
📁 Data Storage
Tasks are stored in a local JSON file (tasks.json) in the application directory.

📌 Task Structure
Each task is stored with the following properties:

Property	Description
id	Unique string or numeric identifier
description	A short text describing the task
status	One of: todo, in-progress, or done
createdAt	Date and time when the task was created
updatedAt	Date and time when the task was last updated

Example of a task in JSON:
```json
{
  "id": "1",
  "description": "Buy groceries",
  "status": "todo",
  "createdAt": "2025-07-17T14:25:00",
  "updatedAt": "2025-07-17T14:25:00"
}
```

🛠 Skills Practiced
This project helps reinforce core development skills:

✅ Working with the file system

✅ Parsing command-line arguments

✅ Handling user input

✅ Structuring a basic CLI application

✅ JSON serialization/deserialization

📄 License
This project is licensed under the MIT License.
