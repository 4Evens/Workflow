using Application.Services.ApprovalService.Abstract;
using Application.Services.ApprovalService.Concrete;
using Application.Services.EvaluationService.Abstract;
using Application.Services.EvaluationService.Concrete;
using Application.Services.WorkflowService.Abstract;
using Application.Services.WorkflowService.Concrete;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public static class ServiceRegistration
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IWorkflowService, WorkflowService>();
            services.AddScoped<IApprovalService, ApprovalService>();
            services.AddScoped<IEvaluationService, EvaluationService>();
        }
    }
}
