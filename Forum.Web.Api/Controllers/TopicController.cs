using Forum.Application.Repositories;
using Forum.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Forum.Application.Dto;
using Forum.Infrastructure;
using Forum.Web.Api.Controllers;

namespace Forum.Application.Services
{
    public class TopicService
    {
        private readonly ITopicRepository _topicRepository;
        private readonly IMapper _mapper;

        public TopicService(ITopicRepository topicRepository, IMapper mapper)
        {
            _topicRepository = topicRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TopicDetailDto>> GetAllTopicsAsync()
        {
            var topics = await _topicRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<TopicDetailDto>>(topics);
        }

        public async Task<TopicDetailDto?> GetTopicByIdAsync(long id)
        {
            var topic = await _topicRepository.GetByIdAsync(id);
            return topic == null ? null : _mapper.Map<TopicDetailDto>(topic);
        }

        public async Task<TopicDetailDto> AddTopicAsync(CreateTopicDto createTopicDto)
        {
            var topic = _mapper.Map<Topic>(createTopicDto);
            var addedTopic = await _topicRepository.AddAsync(topic);
            return _mapper.Map<TopicDetailDto>(addedTopic);
        }

        public async Task<TopicDetailDto?> UpdateTopicAsync(long id, UpdateTopicDto updateTopicDto)
        {
            var existingTopic = await _topicRepository.GetByIdAsync(id);
            if (existingTopic == null)
            {
                return null;
            }
            _mapper.Map(updateTopicDto, existingTopic);
            var updatedTopic = await _topicRepository.UpdateAsync(existingTopic);
            return _mapper.Map<TopicDetailDto>(updatedTopic);
        }

        public async Task DeleteTopicAsync(long id)
        {
            await _topicRepository.DeleteAsync(id);
        }
    }
}