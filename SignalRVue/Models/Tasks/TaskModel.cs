using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRVue.Models.Tasks
{
    public class TaskModel
    {
        public int Id { get; set; }
        public String Desc { get; set; }
        public bool Done { get; set; } = false;
    }
}
