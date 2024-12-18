using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.DataAccess.Interfaces;
using TaskManagementSystem.Domain.Domain;
using TaskManagementSystem.Dto.Tasks;
using TaskManagementSystem.Mappers.Tasks;
using TaskManagementSystem.Services.Interfaces;

namespace TaskManagementSystem.Services.Implementations
{
    public class TaskModelService : ITaskModelService
    {
        //making the application core logic dependent on interface, not implementation
        public ITaskRepository _taskModelRepository;
        public TaskModelService(ITaskRepository taskModelRepository) {
            _taskModelRepository = taskModelRepository;
        }

        //a method that makes a call to the DataAccess layer(TaskModelRepository), to add a Task resource
        public void AddTask(TaskModelDto task)
        {
            //validation
            if(string.IsNullOrEmpty(task.Name))
            {
                throw new Exception("Task name is required!");
            }
            //mapping
            TaskModel taskModel = TaskMapper.toTaskModel(task);
            //calling the repository to Add a task to the database
            _taskModelRepository.Add(taskModel);
        }

        //a method that makes a call to the DataAccess layer(TaskModelRepository) to delete a Task resource
        public void DeleteTask(int id)
        {
            //getting the appropriate Task resource based on Task name
            TaskModel taskModel = _taskModelRepository.GetTaskById(id);
            //calling the repository to delete the Task resource
            _taskModelRepository.Delete(taskModel);
        }
        //a method that makes a call to the DataAccess layer(TaskModelRepository), to get all Tasks from the Database
        public List<TaskModelDto> GetAllTasks(int pageNumber = 1, int pageSize = 10)
        {
            //calling the repository to get all Task resources from Database
            var data = _taskModelRepository.GetAll(pageNumber, pageSize);

            //var totalRecords = _taskModelRepository.GetAll(pageNumber, pageSize).Count();
            //mapping
            List<TaskModelDto> taskDto = data.Select(x => TaskMapper.toTaskDto(x)).ToList();
            //validation
            if (data == null) {
                throw new Exception("An error occured");
            }
            
            //returning tasks from database
            return taskDto;
        }

        //a method that makes a call to the DataAccess layer(TaskModelRepository), to get Task resource based on Task name
        public TaskModelDto GetTask(string taskName)
        {
            //validation
            if (string.IsNullOrEmpty(taskName))
            {
                throw new Exception("Task name must be valid");
            }
            else
            {
                //calling the repository to get Task resource based on task name
                TaskModel taskModel = _taskModelRepository.GetTaskByTitle(taskName);
                //mapping and returning Task
                return TaskMapper.toTaskDto(taskModel);
            }
        }

        public int GetTaskCount()
        {
            return _taskModelRepository.GetTaskCount();
        }

        //a method that filters tasks based on their completion status
        public List<TaskModelDto> GetTasksBasedOnStatus(bool isCompleted, int pageNumber, int pageSize)
        {
            //calling the repository to retrieve Task based on status
            List<TaskModel> taskModels = _taskModelRepository.GetTaskByStatus(isCompleted, pageNumber, pageSize);
            //mapping
            List<TaskModelDto> mappedTasksModels = taskModels.Select(x => TaskMapper.toTaskDto(x)).ToList();
            //returning a list of tasks
            return mappedTasksModels;
        }

        //a method that calls the DataAccess layer(TaskModelRepository) to update a Task  resource
        public void UpdateTask(TaskModelDto task)
        {
            //validation
            if(string.IsNullOrEmpty(task.Name))
            {
                throw new Exception("Task name must be valid");
            }
            if (string.IsNullOrEmpty(task.Description))
            {
                throw new Exception("Task description must be valid.");
            }
            //calling the repository to get appropriate Task based on Task name
            var taskModel = _taskModelRepository.GetTaskByTitle(task.Name);
            //validation
            if(taskModel == null)
            {
                throw new Exception("Such a task does not exist!");
            }
            //mapping
            taskModel.Description = task.Description;
            taskModel.Name = task.Name;
            taskModel.IsCompleted = task.IsCompleted;
            //calling repository to Update resorces
            _taskModelRepository.Update(taskModel);
        }
    }
}
