using LyteVentures.Todo.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LyteVentures.Todo.Repositories.Interfaces
{
    public interface ITodoScheduleRepository
    {
        Task<TodoSchedule> Insert(TodoSchedule entity);

        Task<TodoSchedule> Update(TodoSchedule entity);
        Task<bool> Delete(string userId, string todoScheduleId);

        Task<TodoSchedule> GetById(string id);

        Task<IEnumerable<TodoSchedule>> GetAll(string userId);
        Task<bool> DoesAlreadyExist(string userId, DateTime startSchedule, DateTime endSchedule);
    }
}
