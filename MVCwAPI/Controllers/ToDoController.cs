using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MVCwAPI.Services;
using MVCwAPI.Models;

namespace MVCwAPI.Controllers
{                 

    public class ToDoController : Controller
    {
        private readonly ToDoAPIService _apiService;

    public ToDoController(ToDoAPIService apiService)
    {
        _apiService = apiService;
    }

    public async Task<IActionResult> Index()
    {
        var items = await _apiService.GetToDoItemsAsync();
        return View(items);
    }

    public async Task<IActionResult> Details(int id)
    {
        var item = await _apiService.GetToDoItemAsync(id);
        return View(item);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(ToDoItem newItem)
    {
        if (ModelState.IsValid)
        {
            await _apiService.CreateToDoItemAsync(newItem);
            return RedirectToAction(nameof(Index));
        }
        return View(newItem);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var item = await _apiService.GetToDoItemAsync(id);
        return View(item);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, ToDoItem updatedItem)
    {
        if (ModelState.IsValid)
        {
            await _apiService.UpdateToDoItemAsync(id, updatedItem);
            return RedirectToAction(nameof(Index));
        }
        return View(updatedItem);
    }

    public async Task<IActionResult> Delete(int id)
    {
        var item = await _apiService.GetToDoItemAsync(id);
        return View(item);
    }

    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _apiService.DeleteToDoItemAsync(id);
        return RedirectToAction(nameof(Index));
    }
    }
}