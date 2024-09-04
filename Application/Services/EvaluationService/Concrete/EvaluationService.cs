using Application.Repositories;
using Application.Services.DTOs.Evaluation;
using Application.Services.EvaluationService.Abstract;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Application.Services.Infrastructure;

namespace Application.Services.EvaluationService.Concrete
{
    public class EvaluationService : IEvaluationService
    {
        private readonly IEvaluationReadRepository _evaluationReadRepository;
        private readonly IEvaluationWriteRepository _evaluationWriteRepository;
        private readonly ICacheService _cacheService;
        private readonly IMessageQueueService _messageQueueService;
        private readonly IMapper _mapper;

        public EvaluationService(
            IEvaluationReadRepository evaluationReadRepository,
            IEvaluationWriteRepository evaluationWriteRepository,
            IMapper mapper,
            ICacheService cacheService,
            IMessageQueueService messageQueueService)
        {
            _evaluationReadRepository = evaluationReadRepository;
            _evaluationWriteRepository = evaluationWriteRepository;
            _mapper = mapper;
            _cacheService = cacheService;
            _messageQueueService = messageQueueService;
        }

        public async Task<EvaluationDto> AddEvaluationAsync(EvaluationCreateDto evaluationCreateDto)
        {
            var evaluation = _mapper.Map<Evaluation>(evaluationCreateDto);
            await _evaluationWriteRepository.AddAsync(evaluation);
            await _evaluationWriteRepository.SaveAsync();

            // RabbitMQ ile mesaj gönder
            _messageQueueService.SendMessage($"Değerlendirme yapıldı. Değerlendirme ID: {evaluation.Id}");

            // Redis'e cache olarak kaydet
            await _cacheService.SetAsync($"evaluation_{evaluation.Id}", evaluation.ToString());

            return _mapper.Map<EvaluationDto>(evaluation);
        }

        public async Task<List<EvaluationDto>> GetEvaluationsByStepAsync(int workflowStepId)
        {
            // Redis Cache kontrolü
            var cachedEvaluations = await _cacheService.GetAsync($"evaluations_step_{workflowStepId}");
            if (!string.IsNullOrEmpty(cachedEvaluations))
            {
                return _mapper.Map<List<EvaluationDto>>(cachedEvaluations);
            }

            // Veritabanından veriyi al
            var evaluations = await _evaluationReadRepository.GetWhere(e => e.WorkflowStepId == workflowStepId).ToListAsync();

            // Redis'e cache olarak kaydet
            await _cacheService.SetAsync($"evaluations_step_{workflowStepId}", evaluations.ToString());

            return _mapper.Map<List<EvaluationDto>>(evaluations);
        }
    }
}
