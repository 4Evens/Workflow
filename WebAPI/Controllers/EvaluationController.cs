using Application.Services.DTOs.Evaluation;
using Application.Services.EvaluationService.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EvaluationController : ControllerBase
    {
        private readonly IEvaluationService _evaluationService;

        public EvaluationController(IEvaluationService evaluationService)
        {
            _evaluationService = evaluationService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddEvaluation([FromBody] EvaluationCreateDto evaluationCreateDto)
        {
            var evaluation = await _evaluationService.AddEvaluationAsync(evaluationCreateDto);
            return Ok(evaluation);
        }

        [HttpGet("step/{workflowStepId}")]
        public async Task<IActionResult> GetEvaluationsByStep(int workflowStepId)
        {
            var evaluations = await _evaluationService.GetEvaluationsByStepAsync(workflowStepId);
            return Ok(evaluations);
        }
    }
}