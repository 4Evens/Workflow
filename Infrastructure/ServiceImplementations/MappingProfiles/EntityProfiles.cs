using Application.Services.DTOs.Approval;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Services.DTOs.Evaluation;
using Application.Services.DTOs.Workflow;
using Application.Services.DTOs.WorkflowStep;

namespace Infrastructure.ServiceImplementations.MappingProfiles
{
    public class EntityProfiles : Profile
    {
        public EntityProfiles()
        {
            // Workflow mapping
            CreateMap<Workflow, WorkflowDto>();
            CreateMap<WorkflowCreateDto, Workflow>();
            CreateMap<WorkflowUpdateDto, Workflow>();

            // WorkflowStep mapping
            CreateMap<WorkflowStep, WorkflowStepDto>();
            CreateMap<WorkflowStepCreateDto, WorkflowStep>();

            // Approval mapping
            CreateMap<Approval, ApprovalDto>();
            CreateMap<ApprovalCreateDto, Approval>();

            // Evaluation mapping
            CreateMap<Evaluation, EvaluationDto>();
            CreateMap<EvaluationCreateDto, Evaluation>();
        }
    }
}
