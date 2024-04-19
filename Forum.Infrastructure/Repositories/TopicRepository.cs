using System.Collections.Generic;
using System.Threading.Tasks;
using Forum.Application.Repositories;
using Forum.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Forum.Infrastructure.Repositories
{
    public class TopicRepository : ITopicRepository
    {
        private readonly DatabaseContext _context;

        public TopicRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Topic>> GetAllAsync()
        {
            return await _context.Topics.ToListAsync();
        }

        public async Task<Topic?> GetByIdAsync(long id)
        {
            return await _context.Topics.FindAsync(id);
        }

        public async Task<Topic> AddAsync(Topic topic)
        {
            _context.Topics.Add(topic);
            await _context.SaveChangesAsync();
            return topic;
        }

        public async Task<Topic> UpdateAsync(Topic topic)
        {
            _context.Entry(topic).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return topic;
        }

        public async Task DeleteAsync(long id)
        {
            var topic = await _context.Topics.FindAsync(id);
            if (topic != null)
            {
                _context.Topics.Remove(topic);
                await _context.SaveChangesAsync();
            }
        }
    }
}