using Application.Services.DTOs.Evaluation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.EvaluationService.Abstract
{
    public interface IEvaluationService
    {
        Task<EvaluationDto> AddEvaluationAsync(EvaluationCreateDto evaluationCreateDto);
        Task<List<EvaluationDto>> GetEvaluationsByStepAsync(int workflowStepId);
    }
}
