using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Repositories;
using Persistence.Context;

namespace Persistence.Repositories
{
    public class WorkflowReadRepository : ReadRepository<Workflow>,IWorkflowReadRepository
    {
        public WorkflowReadRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
