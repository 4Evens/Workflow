using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.DTOs.Approval
{
    public class ApprovalCreateDto
    {
        public int WorkflowStepId { get; set; }
        public string ApproverUserId { get; set; }
    }
}
