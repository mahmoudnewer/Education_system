using AutoMapper;
using Education.Models;
using Education.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Education.Services
{
    public class TopicService : ITopicService
    {
        private readonly IGenericRepository<Topic> _iTopicRepository;

        public TopicService(IGenericRepository<Topic> iBookRepository)
        {
            _iTopicRepository = iBookRepository;
        }
        public IEnumerable<Topic> GetAll()
        {
            return _iTopicRepository.GetAll().Include(x =>x.instructor);
        }

        public Topic GetById(int id)
        {
            return _iTopicRepository.GetById(id);
        }
        public void Insert(Topic topic)
        {

            _iTopicRepository.Insert(topic);
            _iTopicRepository.Save();
        }

    }
}
