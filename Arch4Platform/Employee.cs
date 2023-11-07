using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arch4Platform
{
    public class Employee
    {
        public long? Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public bool IsActive { get; set; }
        public List<TaskEntity> Tasks { get; set; } = new List<TaskEntity>();
    }
}
