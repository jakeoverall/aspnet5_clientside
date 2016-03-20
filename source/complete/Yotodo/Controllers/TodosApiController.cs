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
    [Route("/api/todos")]
    public class TodosApiController : Controller
    {
        protected ApplicationDataContext DataContext;
        
        public TodosApiController (ApplicationDataContext dataContext){
                DataContext = dataContext;
        }
        
        [HttpGet]
        public IActionResult GetTodos()
        {
            var model = DataContext.Todos.Where(x => x.IsCompleted == false).ToList();
            return Ok(model);
        }
        
        [HttpPost]
        public IActionResult Add([FromBody]TodoAddViewModel model){
            
            if(ModelState.IsValid){
                var entity = new Todo(){
                    Title = model.Title, 
                    Description = model.Description,
                    Id = Guid.NewGuid()
                };
                DataContext.Todos.Add(entity);
                DataContext.SaveChanges();
                
                return Ok(entity);
            }
            
            return HttpBadRequest(ModelState);
        }
        
        [HttpPut("complete/{id}")]
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
    }
}
