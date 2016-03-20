using Microsoft.AspNet.Mvc;
using Microsoft.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yotodo.Data;
using Yotodo.Models;
using Yotodo.ViewModels;
namespace Yotodo.Services{
    
    
    public interface IStatisticsService{
        
        Task<int> GetCount();
        
        Task<int> GetCompletedCount();
        
    }
    
    public class StatisticsService : IStatisticsService{
        
        private readonly ApplicationDataContext _context;
        
        public StatisticsService(ApplicationDataContext context){
              _context = context;
        }
        
        public Task<int> GetCount(){
            return _context.Todos.CountAsync();
        }
        
        public Task<int> GetCompletedCount(){
            return  _context.Todos.Where(x => x.IsCompleted).CountAsync();
        }
    }
}