using LyteVentures.Todo.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LyteVentures.Todo.Services.Interfaces
{
    public interface ITodoScheduleService
    {
        Task<TodoScheduleDto> GetById(string id);
        Task<IEnumerable<TodoScheduleDto>> GetAll(string userId);
        Task<TodoScheduleDto> Insert(TodoScheduleDto dto);
        Task<TodoScheduleDto> Update(TodoScheduleDto dto);
        Task<bool> DoesAlreadyExist(string userId, DateTime startSchedule, DateTime endSchedule);
    }
}
