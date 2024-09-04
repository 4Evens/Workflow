using Application.Services.DTOs.Approval;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators.Approval
{
    public class ApprovalCreateDtoValidator : AbstractValidator<ApprovalCreateDto>
    {
        public ApprovalCreateDtoValidator()
        {
            RuleFor(x => x.ApproverUserId)
                .NotEmpty().WithMessage("Approver User ID is required.");

            RuleFor(x => x.WorkflowStepId)
                .GreaterThan(0).WithMessage("Workflow Step ID must be greater than 0.");
        }
    }
}
