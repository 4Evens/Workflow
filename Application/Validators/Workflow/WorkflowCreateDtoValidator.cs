using Application.Services.DTOs.Workflow;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators.Workflow
{
    public class WorkflowCreateDtoValidator : AbstractValidator<WorkflowCreateDto>
    {
        public WorkflowCreateDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(100).WithMessage("Title cannot be longer than 100 characters.");

            RuleFor(x => x.StartDate)
                .LessThan(x => x.EndDate).WithMessage("Start Date must be before End Date.");

            RuleFor(x => x.EndDate)
                .NotEmpty().WithMessage("End Date is required.");
        }
    }
}
