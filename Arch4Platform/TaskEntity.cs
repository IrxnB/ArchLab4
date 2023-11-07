using System.ComponentModel.DataAnnotations.Schema;

namespace Arch4Platform
{
    public class TaskEntity
    {
        public long? Id { get; set; }
        public string Name { get; set; }    
        public string Description { get; set; }
        public bool IsCompleted { get; set; }

        [ForeignKey(nameof(Employee))]
        public long? EmployeeId { get; set; }
        public Employee? Employee { get; set; }

    }
}