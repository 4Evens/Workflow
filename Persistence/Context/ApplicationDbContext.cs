using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Workflow> Workflows { get; set; }
        public DbSet<WorkflowStep> WorkflowSteps { get; set; }
        public DbSet<Approval> Approvals { get; set; }
        public DbSet<Evaluation> Evaluations { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Workflow - WorkflowStep (One to Many)
            modelBuilder.Entity<Workflow>()
                .HasMany(w => w.WorkflowSteps)
                .WithOne(ws => ws.Workflow)
                .HasForeignKey(ws => ws.WorkflowId);

            // WorkflowStep - Approval (One to Many)
            modelBuilder.Entity<WorkflowStep>()
                .HasMany(ws => ws.Approvals)
                .WithOne(a => a.WorkflowStep)
                .HasForeignKey(a => a.WorkflowStepId);

            // WorkflowStep - Evaluation (One to Many)
            modelBuilder.Entity<WorkflowStep>()
                .HasMany(ws => ws.Evaluations)
                .WithOne(e => e.WorkflowStep)
                .HasForeignKey(e => e.WorkflowStepId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
