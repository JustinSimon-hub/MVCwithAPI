using System;
using System.Collections.Generic;   
using System.Net.Http;
//this is the namespace for the extension method for deserializing JSON
using System.Net.Http.Json;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using MVCwAPI.Models;
namespace MVCwAPI.Services
{
    public class ToDoAPIService
    {
         
                private readonly HttpClient _httpclient;
                public ToDoAPIService(HttpClient httpclient)
                {
                    _httpclient = httpclient;
                    _httpclient.BaseAddress = new Uri("http://localhost:5155/api/ToDoItems");


                }    

                public async Task<List<ToDoItem>> GetToDoItemsAsync()
    {
        return await _httpclient.GetFromJsonAsync<List<ToDoItem>>("");
    }

    public async Task<ToDoItem> GetToDoItemAsync(int id)
    {
        return await _httpclient.GetFromJsonAsync<ToDoItem>($"/{id}");
    }

    public async Task<ToDoItem> CreateToDoItemAsync(ToDoItem newItem)
    {
        var response = await _httpclient.PostAsJsonAsync("", newItem);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<ToDoItem>();
    }

    public async Task UpdateToDoItemAsync(int id, ToDoItem updatedItem)
    {
        var response = await _httpclient.PutAsJsonAsync($"/{id}", updatedItem);
        response.EnsureSuccessStatusCode();
    }

    

    public async Task DeleteToDoItemAsync(int id)
    {
        var response = await _httpclient.DeleteAsync($"/{id}");
        response.EnsureSuccessStatusCode();
    }
                
                    
                

        
    }
}