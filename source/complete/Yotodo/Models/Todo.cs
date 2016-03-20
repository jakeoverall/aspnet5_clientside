using System;

namespace Yotodo.Models
{
    
    public class Todo{
        public Guid Id{get;set;}
        public String Title{get;set;}
        public String Description{get;set;}
        public DateTime? CompletionDate{get;set;}
        public bool IsCompleted{get;set;}
    }
}