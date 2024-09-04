﻿using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Repositories;
using Persistence.Context;

namespace Persistence.Repositories
{
    public class EvaluationWriteRepository : WriteRepository<Evaluation>,IEvaluationWriteRepository
    {
        public EvaluationWriteRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}