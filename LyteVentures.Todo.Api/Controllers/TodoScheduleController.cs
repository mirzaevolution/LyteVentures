using AutoMapper;
using LyteVentures.Todo.Api.Models;
using LyteVentures.Todo.DataTransferObjects;
using LyteVentures.Todo.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LyteVentures.Todo.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class TodoScheduleController : ControllerBase
    {
        private readonly ITodoScheduleService _todoScheduleService;
        private readonly IMapper _mapper;

        public TodoScheduleController(ITodoScheduleService todoScheduleService, IMapper mapper)
        {
            _todoScheduleService = todoScheduleService;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var result = await _todoScheduleService.GetById(id);
            return Ok(new DataResponse<TodoScheduleDto>
            {
                IsSuccess = true,
                Data = result
            });
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            string userId = User.FindFirstValue("sub");
            var result = await _todoScheduleService.GetAll(userId);
            return Ok(new DataResponse<IEnumerable<TodoScheduleDto>>
            {
                IsSuccess = true,
                Data = result
            });
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] InsertTodoRequest request)
        {
            string userId = User.FindFirstValue("sub");
            var dto = _mapper.Map<InsertTodoRequest, TodoScheduleDto>(request);
            dto.UserId = userId;
            dto = await _todoScheduleService.Insert(dto);
            return Ok(new DataResponse<TodoScheduleDto>
            {
                IsSuccess = true,
                Data = dto
            });
        }

        [HttpPost("ScheduleExists")]
        public async Task<IActionResult> ScheduleExists([FromBody] ScheduleExistsRequest request)
        {
            string userId = User.FindFirstValue("sub");
            bool exists = await _todoScheduleService.DoesAlreadyExist(userId, request.StartSchedule, request.EndSchedule);
            return Ok(new DataResponse<bool>
            {
                IsSuccess = true,
                Data = exists
            });
        }


        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateTodoRequest request)
        {
            string userId = User.FindFirstValue("sub");
            var dto = _mapper.Map<UpdateTodoRequest, TodoScheduleDto>(request);
            dto.UserId = userId;
            dto = await _todoScheduleService.Update(dto);
            return Ok(new DataResponse<TodoScheduleDto>
            {
                IsSuccess = true,
                Data = dto
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            string userId = User.FindFirstValue("sub");
            bool result = await _todoScheduleService.Delete(userId, id);
            return Ok(new BaseResponse
            {
                IsSuccess = result,
                Message = result ? "Todo schedule removed successfully" : "Failed to remove the schedule"
            });
        }

    }
}
