namespace PalMathy.Methods
{
    class NewtonMethod : BaseNumericalMethod
    {
        public NewtonMethod() { }

        public NewtonMethod(BaseNumericalMethod method) : base(method) {
            A.Hint = "Первое приближение";
            B.Hint = "Кол-во итераций";
            Description = "Использует производную функции для нахождения экстремума. " +
                        "Метод находит точку, где производная равна нулю, что является экстремумом.";
        }

        public override string CalculateResult()
        {
            string result = base.CalculateResult();

            if(result == NO_ZEROS)
            {
                return result;
            }

            double x = A.Value;
            int countOfIterations = (int) B.Value;  
            int currentIterationIndex = 0;

            for (int iterationIndex = 0; iterationIndex < countOfIterations; ++iterationIndex)
            {
                double fX = GetResultFromFunction(GetFunction(), x);
                double fXDerivative = GetResultFromFunction(GetFunction(), x, true);
                double newX = x - (fX / fXDerivative);

                if((fX == 0) || Math.Abs(newX - x) < Epsilon)
                {
                    currentIterationIndex = iterationIndex; 
                    break;
                }
                else
                {                    
                    x = newX;
                }
            }

            result += $"Корень уравнения: {x}\nКоличество итераций: {currentIterationIndex}";

            if(double.IsNaN(x) || double.IsInfinity(x))
            {
                result += "\nМетод Ньютона может показать некорректные результаты, если начальное приближение недостаточно близко к решению." +
                    "Попробуйте выбрать другие число.";
            }

            return result;

            throw new NotImplementedException();
        }
    }
}
