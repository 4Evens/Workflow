using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Repositories;
using Domain.Entities;
using Persistence.Context;

namespace Persistence.Repositories
{
    public class ApprovalReadRepository : ReadRepository<Approval>,IApprovalReadRepository
    {
        public ApprovalReadRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
