using DomainLayer;
using RepositoryLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public class Service : IService
    {
        private readonly IRepository<Student> _repository;
        public Service(IRepository<Student> repository)
        {
            _repository = repository;
        }
        public void AddStudent(Student student)
        {
             _repository.Create(student);
        }

        public void DeleteStudent(int id)
        {
            Student student = _repository.Get(id);
            _repository.Remove(student);
            _repository.SaveChanges();
        }

        public IEnumerable<Student> GetAllStudents()
        {
           return _repository.GetAll();
        }

        public Student GetStudent(int id)
        {
            return _repository.Get(id);
        }

        public void UpdateStudent(Student student)
        {
            _repository.Update(student);
        }
    }
}
