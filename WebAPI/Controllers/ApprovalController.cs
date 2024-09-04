using Application.Services.ApprovalService.Abstract;
using Application.Services.DTOs.Approval;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApprovalController : ControllerBase
    {
        private readonly IApprovalService _approvalService;

        public ApprovalController(IApprovalService approvalService)
        {
            _approvalService = approvalService;
        }

        [HttpPost("approve")]
        public async Task<IActionResult> ApproveStep([FromBody] ApprovalCreateDto approvalCreateDto)
        {
            var approval = await _approvalService.ApproveStepAsync(approvalCreateDto);
            return Ok(approval);
        }

        [HttpGet("step/{workflowStepId}")]
        public async Task<IActionResult> GetApprovalsByStep(int workflowStepId)
        {
            var approvals = await _approvalService.GetApprovalsByStepAsync(workflowStepId);
            return Ok(approvals);
        }
    }
}