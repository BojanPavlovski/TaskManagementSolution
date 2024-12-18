using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Dto.Tasks;

namespace TaskManagementSystem.Services.Interfaces
{
    public interface ITaskModelService
    {
        List<TaskModelDto> GetAllTasks(int pageNumber, int pageSize);
        void AddTask(TaskModelDto task);
        void DeleteTask(int id);
        void UpdateTask(TaskModelDto task);
        TaskModelDto GetTask(string taskName);
        List<TaskModelDto> GetTasksBasedOnStatus(bool isCompleted, int pageNumber, int pageSize);
        int GetTaskCount();
    }
}
