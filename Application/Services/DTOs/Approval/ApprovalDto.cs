using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.DTOs.Approval
{
    public class ApprovalDto
    {
        public int Id { get; set; }
        public int WorkflowStepId { get; set; }
        public string ApproverUserId { get; set; }
        public bool IsApproved { get; set; }
        public DateTime? ApprovalDate { get; set; }
    }
}
