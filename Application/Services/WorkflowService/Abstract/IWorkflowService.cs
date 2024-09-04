using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Services.DTOs.Workflow;

namespace Application.Services.WorkflowService.Abstract
{
    public interface IWorkflowService
    {
        Task<List<WorkflowDto>> GetAllWorkflowsAsync();
        Task<WorkflowDto> GetWorkflowByIdAsync(int id);
        Task<WorkflowDto> CreateWorkflowAsync(WorkflowCreateDto workflowCreateDto);
        Task<bool> UpdateWorkflowAsync(int id, WorkflowUpdateDto workflowUpdateDto);
        Task<bool> DeleteWorkflowAsync(int id);
    }
}
