using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using MVCwAPI.Models;
using MVCwAPI.Models.ViewModels;

public class ToDoController : Controller
{
    private readonly HttpClient _httpClient;

    public ToDoController(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    // Get all ToDo items
    public async Task<IActionResult> Index()
    {
        var response = await _httpClient.GetAsync("http://localhost:5155/api/ToDoItems");
        var jsonResponse = await response.Content.ReadAsStringAsync();
        var toDoItems = JsonConvert.DeserializeObject<List<ToDoItemViewModel>>(jsonResponse);
        return View(toDoItems);
    }

    // Get a ToDo item by Id for editing
    public async Task<IActionResult> Edit(int id)
    {
        var response = await _httpClient.GetAsync($"http://localhost:5155/api/ToDoItems/{id}");
        var jsonResponse = await response.Content.ReadAsStringAsync();
        var toDoItem = JsonConvert.DeserializeObject<ToDoItemViewModel>(jsonResponse);
        return View(toDoItem);
    }

    // Update a ToDo item
    [HttpPost]
    public async Task<IActionResult> Edit(ToDoItemViewModel toDoItem)
    {
        var jsonContent = JsonConvert.SerializeObject(toDoItem);
        var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

        await _httpClient.PutAsync($"http://localhost:5155/api/ToDoItems/{toDoItem.Id}", content);
        return RedirectToAction(nameof(Index));
    }

    // Add a new ToDo item
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(ToDoItemViewModel toDoItem)
    {
        var jsonContent = JsonConvert.SerializeObject(toDoItem);
        var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

        await _httpClient.PostAsync("http://localhost:5155/api/ToDoItems", content);
        return RedirectToAction(nameof(Index));
    }

    // Delete a ToDo item
    public async Task<IActionResult> Delete(int id)
    {
        await _httpClient.DeleteAsync($"http://localhost:5155/api/ToDoItems/{id}");
        return RedirectToAction(nameof(Index));
    }
}
