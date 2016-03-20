using Microsoft.AspNet.Mvc;
using Microsoft.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yotodo.Data;
using Yotodo.Models;
using Yotodo.Services;
using Yotodo.ViewModels;


namespace Yotodo.ViewComponents
{
    public class TodoStatusViewComponent : ViewComponent
    {
        private readonly ApplicationDataContext _context;

        // public TodoStatusViewComponent(ApplicationDataContext context)
        // {
        //     _context = context;
        // }
        
        private IStatisticsService _service;
        
        public TodoStatusViewComponent(IStatisticsService service)
        {
            _service = service;
        }

        public async Task<IViewComponentResult> InvokeAsync(bool isComplete)
        {
            var items = await GetItemsAsync(isComplete);
            return View(items);
        }
        
        private async Task<TodoListStatus> GetItemsAsync(bool isComplete)
        {
            // var total = await _context.Todos.CountAsync();
            // var completed = await _context.Todos.Where(x => x.IsCompleted).CountAsync();
            
            var total = await _service.GetCount();
            var completed = await _service.GetCompletedCount();
            
            var result = new TodoListStatus(){
                Total = total,
                Completed = completed
            };
            
            return result;
            
        }       
    }
}