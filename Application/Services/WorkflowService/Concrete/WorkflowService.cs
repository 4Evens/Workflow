using Application.Repositories;
using Application.Services.DTOs.Workflow;
using Application.Services.Infrastructure;
using Application.Services.WorkflowService.Abstract;
using AutoMapper;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services.WorkflowService.Concrete
{
    public class WorkflowService : IWorkflowService
    {
        private readonly IWorkflowReadRepository _workflowReadRepository;
        private readonly IWorkflowWriteRepository _workflowWriteRepository;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;
        private readonly IMessageQueueService _messageQueueService;

        public WorkflowService(
            IWorkflowReadRepository workflowReadRepository,
            IWorkflowWriteRepository workflowWriteRepository,
            IMapper mapper,
            ICacheService cacheService,
            IMessageQueueService messageQueueService)
        {
            _workflowReadRepository = workflowReadRepository;
            _workflowWriteRepository = workflowWriteRepository;
            _mapper = mapper;
            _cacheService = cacheService;
            _messageQueueService = messageQueueService;
        }

        public async Task<List<WorkflowDto>> GetAllWorkflowsAsync()
        {
            // Tüm Workflow'ları getir
            var workflows = await _workflowReadRepository.GetAll(false).ToListAsync();

            // Verileri cache'e kaydetme
            await _cacheService.SetAsync("workflows", workflows.ToString());

            return _mapper.Map<List<WorkflowDto>>(workflows);
        }

        public async Task<WorkflowDto> GetWorkflowByIdAsync(int id)
        {
            // Cache'de olup olmadığını kontrol et
            var cachedWorkflow = await _cacheService.GetAsync($"workflow_{id}");
            if (cachedWorkflow != null)
                return _mapper.Map<WorkflowDto>(cachedWorkflow);

            // Veritabanından getir
            var workflow = await _workflowReadRepository.GetByIdAsync(id, false);
            return _mapper.Map<WorkflowDto>(workflow);
        }

        public async Task<WorkflowDto> CreateWorkflowAsync(WorkflowCreateDto workflowCreateDto)
        {
            var workflow = _mapper.Map<Workflow>(workflowCreateDto);
            await _workflowWriteRepository.AddAsync(workflow);
            await _workflowWriteRepository.SaveAsync();

            // RabbitMQ ile mesaj gönderme
            _messageQueueService.SendMessage($"Yeni iş akışı oluşturuldu: {workflow.Title}");

            return _mapper.Map<WorkflowDto>(workflow);
        }

        public async Task<bool> UpdateWorkflowAsync(int id, WorkflowUpdateDto workflowUpdateDto)
        {
            var workflow = await _workflowReadRepository.GetByIdAsync(id);
            if (workflow == null)
                return false;

            _mapper.Map(workflowUpdateDto, workflow);
            _workflowWriteRepository.Update(workflow);
            await _workflowWriteRepository.SaveAsync();

            _messageQueueService.SendMessage($"İş akışı güncellendi: {workflow.Title}");
            return true;
        }

        public async Task<bool> DeleteWorkflowAsync(int id)
        {
            var workflow = await _workflowReadRepository.GetByIdAsync(id);
            if (workflow == null)
                return false;

            await _workflowWriteRepository.RemoveAsync(id);
            await _workflowWriteRepository.SaveAsync();

            _messageQueueService.SendMessage($"İş akışı silindi: {workflow.Title}");
            return true;
        }
    }
}
