using Forum.Application.Repositories;
using Forum.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Forum.Application.Services
{
    public class TopicService
    {
        private readonly ITopicRepository _topicRepository;

        public TopicService(ITopicRepository topicRepository)
        {
            _topicRepository = topicRepository;
        }

        public async Task<IEnumerable<Topic>> GetAllTopicsAsync()
        {
            return await _topicRepository.GetAllAsync();
        }

        public async Task<Topic?> GetTopicByIdAsync(long id)
        {
            return await _topicRepository.GetByIdAsync(id);
        }

        public async Task<Topic> AddTopicAsync(Topic topic)
        {
            return await _topicRepository.AddAsync(topic);
        }

        public async Task<Topic> UpdateTopicAsync(Topic topic)
        {
            return await _topicRepository.UpdateAsync(topic);
        }

        public async Task DeleteTopicAsync(long id)
        {
            await _topicRepository.DeleteAsync(id);
        }
    }
}