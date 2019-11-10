using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreateExamApp.Models
{
    public interface IExamRepository
    {
        void AddExam(Exam exam);
        Exam GetExam(int id);
        List<Exam> GetAllExams();
        void RemoveExam(int id);
    }
}
