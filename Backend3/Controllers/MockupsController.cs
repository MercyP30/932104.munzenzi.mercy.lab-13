using Backend3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Backend3.Controllers
{
    public class MockupsController : Controller
    {
        private readonly ILogger<MockupsController> _logger;
        private readonly QuizGenerator _quiz;

        public MockupsController(ILogger<MockupsController> logger)
        {
            _logger = logger;
            _quiz = QuizGenerator.Instance;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Quiz()
        {
            MathExpression expression = _quiz.AddExpression();
            return View(expression);
        }

        [HttpPost]
        public IActionResult Quiz(int id, int userAnswer, string action)
        {
            if (ModelState.IsValid)
            {
                _quiz.CheckAnswer(id, userAnswer);

                if (action == "Next")
                {
                    MathExpression expression = _quiz.AddExpression();
                    return View(expression);
                }

                return View("Result", _quiz);
            }
            else
            {
                MathExpression expression = _quiz.FindExpression(id);

                if (expression != null)
                {
                    ViewData["data"] = "Incorrect data";
                    return View(expression);
                }
                else
                {
                    return Error();
                }
            }
        }

        public IActionResult Result()
        {
            return View("Result", _quiz);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

