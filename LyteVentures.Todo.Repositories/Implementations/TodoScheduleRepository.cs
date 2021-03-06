using LyteVentures.Todo.DataStorageLayers;
using LyteVentures.Todo.Entities;
using LyteVentures.Todo.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LyteVentures.Todo.Repositories.Implementations
{
    public class TodoScheduleRepository: ITodoScheduleRepository
    {
        public readonly ApplicationDbContext _context;

        public TodoScheduleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<TodoSchedule> Insert(TodoSchedule entity)
        {
            if(!_context.Users.Any(c => c.Id == entity.UserId))
            {
                throw new Exception($"User with id: ${entity.Id} not found");
            }
            if (await DoesAlreadyExist(entity.UserId, entity.StartSchedule, entity.EndSchedule))
            {
                throw new Exception("Schedule already exists");
            }
            _context.TodoSchedules.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<TodoSchedule> Update(TodoSchedule entity)
        {
            if (!_context.Users.Any(c => c.Id == entity.UserId))
            {
                throw new Exception($"User with id: ${entity.Id} not found");
            }
            var existingSchedule = await _context.TodoSchedules.FirstOrDefaultAsync(c => c.Id == entity.Id);
            if (existingSchedule.StartSchedule!=entity.StartSchedule && existingSchedule.EndSchedule!=entity.EndSchedule)
            {
                if (await DoesAlreadyExist(entity.UserId, entity.StartSchedule, entity.EndSchedule))
                {
                    throw new Exception("Schedule already exists");
                }
            }
            existingSchedule.Title = entity.Title;
            existingSchedule.Description = entity.Description;
            existingSchedule.IsActive = entity.IsActive;
            existingSchedule.StartSchedule = entity.StartSchedule;
            existingSchedule.EndSchedule = entity.EndSchedule;
            _context.Entry(existingSchedule).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> Delete(string userId, string todoScheduleId)
        {
            var entity = await _context.TodoSchedules.FirstOrDefaultAsync(c => c.UserId == userId && c.Id == todoScheduleId);
            if (entity == null)
            {
                throw new Exception($"Todo schedule with id: {todoScheduleId} not found");
            }
            _context.TodoSchedules.Remove(entity);
            int total = await _context.SaveChangesAsync();
            return total > 0;
        }
        public async Task<TodoSchedule> GetById(string id)
        {
            TodoSchedule entity = await _context.TodoSchedules.Include(c => c.User).FirstOrDefaultAsync(c => c.Id == id);
            if(entity == null)
            {
                throw new Exception($"Schedule with id: ${id} not found");
            }
            return entity;
        }

        public async Task<IEnumerable<TodoSchedule>> GetAll(string userId)
        {
            List<TodoSchedule> entities =
                await _context.TodoSchedules.Include(c => c.User).Where(c => c.UserId == userId).ToListAsync() ?? new List<TodoSchedule>(); 

            return entities;
        }

        public async Task<bool> DoesAlreadyExist(string userId, DateTime startSchedule, DateTime endSchedule)
        {
             bool any = await _context.TodoSchedules.AnyAsync(c =>  c.UserId == userId &&
                                                                    c.StartSchedule == startSchedule && 
                                                                    c.EndSchedule == endSchedule && c.IsActive);
            return any;
        }
    }
}
