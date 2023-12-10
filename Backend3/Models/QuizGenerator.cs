using System.Collections.Generic;

namespace Backend3.Models
{
    public class QuizGenerator
    {
        public static QuizGenerator Instance { get; set; } = new QuizGenerator();

        public List<MathExpression> Expressions { get; set; }

        public int RightAnswers { get; set; }

        public QuizGenerator()
        {
            Expressions = new List<MathExpression>();
        }

        public MathExpression AddExpression()
        {
            MathExpression expression = new MathExpression(Expressions.Count + 1);
            Expressions.Add(expression);
            return expression;
        }

        public MathExpression FindExpression(int id)
        {
            foreach (MathExpression expression in Expressions)
            {
                if (expression.Id == id)
                {
                    return expression;
                }
            }

            return null;
        }

        public void CheckAnswer(int id, int userAnswer)
        {
            MathExpression expression = FindExpression(id);

            if (expression != null)
            {
                expression.UserAnswer = userAnswer;

                if (expression.Answer == userAnswer)
                {
                    RightAnswers++;
                }
            }
        }

        public bool IsEmpty()
        {
            return Expressions.Count == 0;
        }

        public void Reset()
        {
            Expressions = new List<MathExpression>();
            RightAnswers = 0;
        }
    }
}
