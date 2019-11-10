using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CreateExamApp.Data;
using CreateExamApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace CreateExamApp.Controllers
{
    public class ExamController : Controller
    {
        private readonly IExamRepository _examRepository;

        public ExamController(IExamRepository examRepository)
        {
            _examRepository = examRepository;
        }

        public IActionResult ExamIndex()
        {
            return View(_examRepository.GetAllExams());
        }

        public IActionResult Exam(int id)
        {
            Exam exam = new Exam();
            exam = _examRepository.GetExam(id);

            ViewBag.TitleExam = exam.title;
            ViewBag.Context = exam.context;

            ViewBag.Question1 = exam.question1;
            ViewBag.Question2 = exam.question2;
            ViewBag.Question3 = exam.question3;
            ViewBag.Question4 = exam.question4;

            ViewBag.q1a = exam.q1a;
            ViewBag.q1b = exam.q1b;
            ViewBag.q1c = exam.q1c;
            ViewBag.q1d = exam.q1d;

            ViewBag.q2a = exam.q2a;
            ViewBag.q2b = exam.q2b;
            ViewBag.q2c = exam.q2c;
            ViewBag.q2d = exam.q2d;

            ViewBag.q3a = exam.q3a;
            ViewBag.q3b = exam.q3b;
            ViewBag.q3c = exam.q3c;
            ViewBag.q3d = exam.q3d;

            ViewBag.q4a = exam.q4a;
            ViewBag.q4b = exam.q4b;
            ViewBag.q4c = exam.q4c;
            ViewBag.q4d = exam.q4d;

            ViewBag.option1 = exam.q1Option;
            ViewBag.option2 = exam.q2Option;
            ViewBag.option3 = exam.q3Option;
            ViewBag.option4 = exam.q4Option;

            return View();
        }

        public IActionResult RemoveExam(int id)
        {
            _examRepository.RemoveExam(id);
            return RedirectToAction("ExamIndex","Exam");
        }
    }
}