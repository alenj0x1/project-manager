using Microsoft.Extensions.Logging;
using ProjectManager.Domain.Context;
using ProjectManager.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Domain.Repositories
{
    public class TaskRepository(PostgresContext context, ILogger<ITaskRepository> logger) : ITaskRepository
    {
        private readonly PostgresContext _context = context;
        private readonly ILogger<ITaskRepository> _logger = logger;

        public async Task<Context.Task> Create(Context.Task task)
        {
            try
            {
                await _context.Tasks.AddAsync(task);
                await _context.SaveChangesAsync();

                return task;
            }
            catch (Exception e)
            {
                _logger.LogInformation("{ExceptionMessage} {ExceptionTrace}", e.Message, e.StackTrace);
                throw;
            }
        }

        public Context.Task? Get(Guid projectId, Guid taskId)
        {
            try
            {
                return _context.Tasks.FirstOrDefault(task => task.TaskId == taskId && projectId == projectId);
            }
            catch (Exception e)
            {
                _logger.LogInformation("{ExceptionMessage} {ExceptionTrace}", e.Message, e.StackTrace);
                throw;
            }
        }

        public bool IfExistsTaskStatus(int taskStatusId)
        {
            try
            {
                return _context.TasksStatuses.Any(x => x.TaskStatusId == taskStatusId);
            }
            catch (Exception e)
            {
                _logger.LogInformation("{ExceptionMessage} {ExceptionTrace}", e.Message, e.StackTrace);
                throw;
            }
        }

        public bool IfTaskIsFromMember(Guid projectId, Guid taskId, Guid userId)
        {
            try
            {
                return _context.Tasks
                    .Any(x => x.ProjectId == projectId && 
                        x.TaskId == taskId && 
                        x.UserId == userId);
            }
            catch (Exception e)
            {
                _logger.LogInformation("{ExceptionMessage} {ExceptionTrace}", e.Message, e.StackTrace);
                throw;
            }
        }

        public bool IfTaskIsInFromProject(Guid projectId, Guid taskId)
        {
            try
            {
                return _context.Tasks
                    .Any(x => x.ProjectId == projectId &&
                        x.TaskId == taskId);
            }
            catch (Exception e)
            {
                _logger.LogInformation("{ExceptionMessage} {ExceptionTrace}", e.Message, e.StackTrace);
                throw;
            }
        }

        public async System.Threading.Tasks.Task Remove(Context.Task task)
        {
            try
            {
                _context.Tasks.Remove(task);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                _logger.LogInformation("{ExceptionMessage} {ExceptionTrace}", e.Message, e.StackTrace);
                throw;
            }
        }

        public async Task<Context.Task> Update(Context.Task task)
        {
            try
            {
                _context.Tasks.Update(task);
                await _context.SaveChangesAsync();

                return task;
            }
            catch (Exception e)
            {
                _logger.LogInformation("{ExceptionMessage} {ExceptionTrace}", e.Message, e.StackTrace);
                throw;
            }
        }
    }
}
