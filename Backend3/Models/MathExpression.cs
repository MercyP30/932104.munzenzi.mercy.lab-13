using System.ComponentModel.DataAnnotations;

namespace Backend3.Models
    {
        public class MathExpression
        {
            public int Id { get; set; }
            public int FirstValue { get; set; }
            public int SecondValue { get; set; }
            public string Operation { get; set; }
            public int Answer { get; set; }

            [Required]
            public int UserAnswer { get; set; }

            public MathExpression(int expId)
            {
                Random random = new Random(DateTime.Now.Millisecond);

                Id = expId;
                FirstValue = random.Next() % 10;
                SecondValue = random.Next() % 10;

                if (random.Next() % 2 == 1)
                {
                    Operation = "+";
                    Answer = FirstValue + SecondValue;
                }
                else
                {
                    Operation = "-";
                    Answer = FirstValue - SecondValue;
                }
            }

            public class MathExpressionValidationAttribute : ValidationAttribute
            {
                private readonly string[] _validOperations = { "+", "-" };

                public override bool IsValid(object value)
                {
                    if (value != null && Array.Exists(_validOperations, op => op == value.ToString()))
                    {
                        return true;
                    }

                    return false;
                }
            }
        }
    }
