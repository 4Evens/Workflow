using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.DTOs.WorkflowStep
{
    public class WorkflowStepDto
    {
        public int Id { get; set; }
        public int WorkflowId { get; set; }
        public string StepType { get; set; }
        public int Order { get; set; }
        public bool RequiresApproval { get; set; }
    }
}
