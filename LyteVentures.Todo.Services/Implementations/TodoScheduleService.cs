using AutoMapper;
using LyteVentures.Todo.DataTransferObjects;
using LyteVentures.Todo.Entities;
using LyteVentures.Todo.Repositories.Interfaces;
using LyteVentures.Todo.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LyteVentures.Todo.Services.Implementations
{
    public class TodoScheduleService: ITodoScheduleService
    {
        private readonly ITodoScheduleRepository _todoRepository;
        private readonly IMapper _mapper;
        public TodoScheduleService(ITodoScheduleRepository todoRepository, IMapper mapper)
        {
            _todoRepository = todoRepository;
            _mapper = mapper;

        }
        public async Task<TodoScheduleDto> GetById(string id)
        {
            var entity = await _todoRepository.GetById(id);
            var dto = _mapper.Map<TodoSchedule, TodoScheduleDto>(entity);
            return dto;
        }
        
        public async Task<IEnumerable<TodoScheduleDto>> GetAll(string userId)
        {
            var entities = await _todoRepository.GetAll(userId);
            var dtoList = _mapper.Map<IEnumerable<TodoSchedule>, IEnumerable<TodoScheduleDto>>(entities);
            return dtoList;
        }

        public async Task<TodoScheduleDto> Insert(TodoScheduleDto dto)
        {
            if (dto.EndSchedule < dto.StartSchedule)
            {
                throw new Exception("End schedule cannot be less than start schedule");
            }
            var entity = _mapper.Map<TodoScheduleDto, TodoSchedule>(dto);
            entity = await _todoRepository.Insert(entity);
            //re-gain to get relationship data
            entity = await _todoRepository.GetById(entity.Id);
            return _mapper.Map<TodoSchedule, TodoScheduleDto>(entity);
        }

        public async Task<TodoScheduleDto> Update(TodoScheduleDto dto)
        {
            if (dto.EndSchedule < dto.StartSchedule)
            {
                throw new Exception("End schedule cannot be less than start schedule");
            }
            var entity = _mapper.Map<TodoScheduleDto, TodoSchedule>(dto);
            entity = await _todoRepository.Update(entity);
            //re-gain to get relationship data
            entity = await _todoRepository.GetById(entity.Id);
            return _mapper.Map<TodoSchedule, TodoScheduleDto>(entity);
        }

        public async Task<bool> Delete(string userId, string todoScheduleId)
        {
            bool result = await _todoRepository.Delete(userId, todoScheduleId);
            return result;
        }

        public async Task<bool> DoesAlreadyExist(string userId, DateTime startSchedule, DateTime endSchedule)
        {
            if (endSchedule < startSchedule)
            {
                throw new Exception("End schedule cannot be less than start schedule");
            }
            bool exists = await _todoRepository.DoesAlreadyExist(userId, startSchedule, endSchedule);
            return exists;
        }
    }
}
