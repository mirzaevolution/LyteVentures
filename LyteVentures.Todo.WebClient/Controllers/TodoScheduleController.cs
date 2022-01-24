using LyteVentures.Todo.WebClient.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace LyteVentures.Todo.WebClient.Controllers
{
    [Authorize]
    public class TodoScheduleController: Controller
    {
        private readonly HttpClient _httpClient;
        public TodoScheduleController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("Api");
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetAllSchedules()
        {
           var response = await _httpClient.GetAsync("/api/v1/TodoSchedule");
            if (response.IsSuccessStatusCode)
            {
                var dataResponse = JsonConvert.DeserializeObject<DataResponseViewModel<IEnumerable<TodoViewModel>>>(
                        await response.Content.ReadAsStringAsync(
                    ));
                if (dataResponse.IsSuccess)
                {
                    return Json(new
                    {
                        data = dataResponse.Data != null && dataResponse.Data.Count() > 0 ?
                        dataResponse.Data : new List<TodoViewModel>()
                    });
                }
            }

            return Json(new
            {
                data = new List<TodoViewModel>()
            });
        }



        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateTodoViewModel model)
        {
            if (ModelState.IsValid)
            {
                var response = await _httpClient.PostAsJsonAsync<CreateTodoViewModel>("/api/v1/TodoSchedule", model);
                if (response.IsSuccessStatusCode)
                {
                    var dataResponse = JsonConvert.DeserializeObject<DataResponseViewModel<TodoViewModel>>(
                            await response.Content.ReadAsStringAsync(
                        ));
                    if (dataResponse.IsSuccess)
                    {
                        return RedirectToAction(nameof(Index), "TodoSchedule");
                    }
                    ViewBag.Message = $"DataError: {dataResponse.Message}";
                }
                else
                {
                    ViewBag.Message = $"HttpError: {response.StatusCode.ToString()}";

                }
            }
            return View(model);
        }



        public async Task<IActionResult> Edit(string id)
        {
            var response = await _httpClient.GetAsync($"/api/v1/TodoSchedule/{id}");
            if (response.IsSuccessStatusCode)
            {
                var dataResponse = JsonConvert.DeserializeObject<DataResponseViewModel<TodoViewModel>>(
                        await response.Content.ReadAsStringAsync(
                    ));
                if (dataResponse.IsSuccess)
                {
                    return View(new UpdateTodoViewModel
                    {
                        Id = id,
                        Title = dataResponse.Data.Title,
                        Description = dataResponse.Data.Description,
                        StartSchedule = dataResponse.Data.StartSchedule,
                        EndSchedule = dataResponse.Data.EndSchedule
                    });
                }
                ViewBag.Message = $"DataError: {dataResponse.Message}";
            }
            else
            {
                ViewBag.Message = $"HttpError: {response.StatusCode.ToString()}";
               
            }
            return View(new UpdateTodoViewModel
            {
                Id = id
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UpdateTodoViewModel model)
        {
            if (ModelState.IsValid)
            {
                var response = await _httpClient.PutAsJsonAsync<UpdateTodoViewModel>("/api/v1/TodoSchedule", model);
                if (response.IsSuccessStatusCode)
                {
                    var dataResponse = JsonConvert.DeserializeObject<DataResponseViewModel<TodoViewModel>>(
                            await response.Content.ReadAsStringAsync(
                        ));
                    if (dataResponse.IsSuccess)
                    {
                        return RedirectToAction(nameof(Index), "TodoSchedule");
                    }
                    ViewBag.Message = $"DataError: {dataResponse.Message}";
                }
                else
                {
                    ViewBag.Message = $"HttpError: {response.StatusCode.ToString()}";

                }
            }
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var response = await _httpClient.DeleteAsync($"/api/v1/TodoSchedule/{id}");
            return RedirectToAction(nameof(Index), "TodoSchedule");

        }
    }
}
