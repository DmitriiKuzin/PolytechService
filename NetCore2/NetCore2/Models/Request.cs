using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NetCore2.Models
{
    public class Request
    {
        public int id { get; set; }
        public int PriorityId { get; set; }
        public Priority Priority { get; set; }
        [Required]
        public int DormId { get; set; }
        public Dorm Dorm { get; set; }
        public int Room { get; set; }
        [Required]
        public string CreatorId { get; set; }
        public AppUser Creator { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }        
        public string ExecutorId { get; set; }
        public AppUser Executor { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int StatusId { get; set; }
        public Status Status { get; set; }
        public int LifecycleId { get; set; }
        public Lifecycle Lifecycle { get; set; }
      
    }

}
