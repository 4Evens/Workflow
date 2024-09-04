using Application.Services.DTOs.Workflow;
using Application.Services.WorkflowService.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkflowController : ControllerBase
    {
        private readonly IWorkflowService _workflowService;

        public WorkflowController(IWorkflowService workflowService)
        {
            _workflowService = workflowService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllWorkflows()
        {
            var workflows = await _workflowService.GetAllWorkflowsAsync();
            return Ok(workflows);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetWorkflowById(int id)
        {
            var workflow = await _workflowService.GetWorkflowByIdAsync(id);
            if (workflow == null)
                return NotFound();

            return Ok(workflow);
        }

        [HttpPost]
        public async Task<IActionResult> CreateWorkflow([FromBody] WorkflowCreateDto workflowCreateDto)
        {
            if (workflowCreateDto == null)
                return BadRequest();

            var createdWorkflow = await _workflowService.CreateWorkflowAsync(workflowCreateDto);
            return CreatedAtAction(nameof(GetWorkflowById), new { id = createdWorkflow.Id }, createdWorkflow);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateWorkflow(int id, [FromBody] WorkflowUpdateDto workflowUpdateDto)
        {
            var updated = await _workflowService.UpdateWorkflowAsync(id, workflowUpdateDto);
            if (!updated)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorkflow(int id)
        {
            var deleted = await _workflowService.DeleteWorkflowAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
