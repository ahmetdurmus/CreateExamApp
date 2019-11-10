using CreateExamApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreateExamApp.Models
{
    public class ExamRepository : IExamRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public ExamRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public void AddExam(Exam exam)
        {
            _applicationDbContext.exams.Add(exam);
            _applicationDbContext.SaveChanges();
        }

        public List<Exam> GetAllExams()
        {
            return _applicationDbContext.exams.ToList<Exam>();
        }

        public Exam GetExam(int id)
        {
            Exam temp = _applicationDbContext.exams.FirstOrDefault(i => i.Id == id);
            return temp;
        }

        public void RemoveExam(int id)
        {
            Exam temp = _applicationDbContext.exams.FirstOrDefault(i => i.Id == id);
            _applicationDbContext.exams.Remove(temp);
            _applicationDbContext.SaveChanges();
        }
    }
}
