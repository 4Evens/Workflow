using Application.Repositories;
using Application.Services.ApprovalService.Abstract;
using Application.Services.DTOs.Approval;
using Application.Services.Infrastructure;
using AutoMapper;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services.ApprovalService.Concrete
{
    public class ApprovalService : IApprovalService
    {
        private readonly IApprovalReadRepository _approvalReadRepository;
        private readonly IApprovalWriteRepository _approvalWriteRepository;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;
        private readonly IMessageQueueService _messageQueueService;

        public ApprovalService(
            IApprovalReadRepository approvalReadRepository,
            IApprovalWriteRepository approvalWriteRepository,
            IMapper mapper,
            ICacheService cacheService,
            IMessageQueueService messageQueueService)
        {
            _approvalReadRepository = approvalReadRepository;
            _approvalWriteRepository = approvalWriteRepository;
            _mapper = mapper;
            _cacheService = cacheService;
            _messageQueueService = messageQueueService;
        }

        public async Task<ApprovalDto> ApproveStepAsync(ApprovalCreateDto approvalCreateDto)
        {
            var approval = _mapper.Map<Approval>(approvalCreateDto);
            approval.IsApproved = true;
            approval.ApprovalDate = DateTime.Now;

            // Onay adımını veritabanına kaydet
            await _approvalWriteRepository.AddAsync(approval);
            await _approvalWriteRepository.SaveAsync();

            // RabbitMQ ile onay bilgisi gönder
            _messageQueueService.SendMessage($"Onaylandı: WorkflowStepId: {approval.WorkflowStepId}, Onaylayan: {approval.ApproverUserId}");

            // Redis'e cache olarak onay bilgisi kaydet
            await _cacheService.SetAsync($"approval_{approval.Id}", approval.ToString());

            return _mapper.Map<ApprovalDto>(approval);
        }

        public async Task<List<ApprovalDto>> GetApprovalsByStepAsync(int workflowStepId)
        {
            // Cache'den kontrol et
            var cachedApprovals = await _cacheService.GetAsync($"approvals_step_{workflowStepId}");
            if (!string.IsNullOrEmpty(cachedApprovals))
            {
                // Cache'den veriyi al
                return _mapper.Map<List<ApprovalDto>>(cachedApprovals);
            }

            // Veritabanından getir
            var approvals = await _approvalReadRepository.GetWhere(a => a.WorkflowStepId == workflowStepId).ToListAsync();

            // Veriyi Redis'e cache olarak kaydet
            await _cacheService.SetAsync($"approvals_step_{workflowStepId}", approvals.ToString());

            return _mapper.Map<List<ApprovalDto>>(approvals);
        }
    }
}
