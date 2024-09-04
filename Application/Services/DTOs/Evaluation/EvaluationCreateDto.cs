using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.DTOs.Evaluation
{
    public class EvaluationCreateDto
    {
        public int WorkflowStepId { get; set; }
        public string Comments { get; set; }
        public string Documents { get; set; }
    }
}
