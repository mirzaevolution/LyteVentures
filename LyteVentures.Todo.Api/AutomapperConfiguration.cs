using AutoMapper;
using LyteVentures.Todo.Api.Models;
using LyteVentures.Todo.DataTransferObjects;
using LyteVentures.Todo.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LyteVentures.Todo.Api
{
    public class AutomapperConfiguration: Profile
    {
        public AutomapperConfiguration()
        {
            Init();
        }

        private void Init()
        {
            CreateMap<TodoSchedule, TodoScheduleDto>()
                .ForMember(dst => dst.CreatedBy, src => src.MapFrom(prop => prop.User.FullName ?? "-"));
            CreateMap<TodoScheduleDto, TodoSchedule>();

            CreateMap<InsertTodoRequest, TodoScheduleDto>();
            CreateMap<UpdateTodoRequest, TodoScheduleDto>();
        }
    }
}
