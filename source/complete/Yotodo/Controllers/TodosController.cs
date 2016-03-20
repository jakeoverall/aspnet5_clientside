using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Yotodo.Data;
using Yotodo.Models;
using Yotodo.ViewModels;

namespace Yotodo.Controllers
{
    public class TodosController : Controller
    {
        protected ApplicationDataContext DataContext;
        
        public TodosController (ApplicationDataContext dataContext){
                DataContext = dataContext;
        }
        
        public IActionResult Index()
        {
            var model = DataContext.Todos.Where(x => x.IsCompleted == false).ToList();
            
            ViewData.Model = model;
            
            return View();
        }

        [HttpGet]
        public IActionResult Add(){
            ViewData.Model = new TodoAddViewModel();
            return View();
        }
        
        [HttpPost]
        public IActionResult Add(TodoAddViewModel model){
            
            if(ModelState.IsValid){
                var entity = new Todo(){
                    Title = model.Title, 
                    Description = model.Description,
                    Id = Guid.NewGuid()
                };
                DataContext.Todos.Add(entity);
                DataContext.SaveChanges();
                
                return RedirectToAction("Index", "Home");
            }
            
            return View(model);
        }
        
        [HttpPut("/todos/complete/{id}")]
        public IActionResult Complete(Guid id){
            
            var entity = DataContext.Todos.FirstOrDefault(x => x.Id == id);
            
            if(entity == null){
                return HttpBadRequest("Could not find Todo: " + id);
            }
            
            entity.IsCompleted = true;
            entity.CompletionDate = DateTime.UtcNow;
            DataContext.Todos.Update(entity);
            
            DataContext.SaveChanges();
            
            return new NoContentResult();
        }
        
        [HttpGet("/todos/spa")]
        public IActionResult Spa(){
            return View();
        }
    }
}
