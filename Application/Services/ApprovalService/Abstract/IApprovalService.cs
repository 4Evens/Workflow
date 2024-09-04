using Application.Services.DTOs.Approval;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.ApprovalService.Abstract
{
    public interface IApprovalService
    {
        Task<ApprovalDto> ApproveStepAsync(ApprovalCreateDto approvalCreateDto);
        Task<List<ApprovalDto>> GetApprovalsByStepAsync(int workflowStepId);
    }

}
