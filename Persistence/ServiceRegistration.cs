using Application.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Persistence.Repositories;
using Persistence.Context;

namespace Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("SqlServer")));

            services.AddScoped<IWorkflowReadRepository, WorkflowReadRepository>();
            services.AddScoped<IWorkflowWriteRepository, WorkflowWriteRepository>();

            services.AddScoped<IApprovalReadRepository, ApprovalReadRepository>();
            services.AddScoped<IApprovalWriteRepository, ApprovalWriteRepository>();

            services.AddScoped<IEvaluationReadRepository, EvaluationReadRepository>();
            services.AddScoped<IEvaluationWriteRepository, EvaluationWriteRepository>();

            services.AddScoped<IWorkflowStepReadRepository, WorkflowStepReadRepository>();
            services.AddScoped<IWorkflowStepWriteRepository, WorkflowStepWriteRepository>();
        }
    }
}