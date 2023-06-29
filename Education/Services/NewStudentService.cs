using Education.Models;
using Education.Repositories;

namespace Education.Services
{
    public class NewStudentService : INewStudentService
    {
        IGenericRepository<NewStudentData> _newStudentData;
        public NewStudentService(IGenericRepository<NewStudentData> newStudentData)
        {
            _newStudentData = newStudentData;
    }
        public void insert(NewStudentData Newstudent)
        {
            _newStudentData.Insert(Newstudent);
            _newStudentData.Save();
        }

     
    }
}
