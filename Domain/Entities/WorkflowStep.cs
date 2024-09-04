using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class WorkflowStep : BaseEntity
    {
        public int WorkflowId { get; set; }
        public Workflow Workflow { get; set; }

        public string StepType { get; set; }
        public int Order { get; set; }
        public bool RequiresApproval { get; set; }

        public ICollection<Approval> Approvals { get; set; }
        public ICollection<Evaluation> Evaluations { get; set; }
    }
}
