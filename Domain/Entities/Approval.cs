using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Approval : BaseEntity
    {
        public int WorkflowStepId { get; set; }
        public WorkflowStep WorkflowStep { get; set; }

        public string ApproverUserId { get; set; }
        public bool IsApproved { get; set; }
        public DateTime? ApprovalDate { get; set; }
    }
}
