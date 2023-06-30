using Education.Models;

namespace Education.Services
{
    public interface ITopicService
    {
        IEnumerable<Topic> GetAll();
        Topic GetById(int id);
        public void Insert(Topic topic);


    }
}