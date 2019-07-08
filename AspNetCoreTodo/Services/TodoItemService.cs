using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AspNetCoreTodo.Models;
using AspNetCoreTodo.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace AspNetCoreTodo.Services
{
    public class TodoItemService : ITodoItemService
    {
        private readonly ApplicationDbContext _context;
        
        public TodoItemService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<TodoItem[]> GetInCompleteItemsAsync()
        {
            var items = await _context.Items
                                .Where(x=>x.IsDone == false)
                                .ToArrayAsync();

            return items;
        }

        public async Task<bool> AddItemAsync(TodoItem newItem)
        { 
            newItem.Id = Guid.NewGuid(); 
            newItem.IsDone = false; 
            newItem.DueAt = DateTimeOffset.Now.AddDays(3); 
            
            _context.Items.Add(newItem); 
            var saveResult = await _context.SaveChangesAsync(); 
            return saveResult == 1; 
        } 
    }
}