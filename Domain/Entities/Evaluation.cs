using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Evaluation : BaseEntity
    {
        public int WorkflowStepId { get; set; }
        public WorkflowStep WorkflowStep { get; set; }

        public string Comments { get; set; }
        public string Documents { get; set; } 
    }
}
