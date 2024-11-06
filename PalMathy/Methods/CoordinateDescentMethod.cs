namespace PalMathy.Methods
{
    class CoordinateDescentMethod : BaseNumericalMethod
    {
        public CoordinateDescentMethod(BaseNumericalMethod method) : base(method)
        {
            A.Hint = "Первое приближение";
            B.Hint = "Кол-во итераций";
            C.IsVisible = true;
            C.Value = 0.1;
            C.Hint = "Шаг";
            Description = "Простейший метод нахождения минимума и максимума функции.";
        }

        public override string CalculateResult()
        {
            string result = base.CalculateResult();

            double x = A.Value;
            double step = C.Value;

            double maxPoint = GetPoint(x, step, true, (int) B.Value);
            double minPoint = GetPoint(x, step, false, (int) B.Value);

            result += $"Максимум функции: {maxPoint}\nМинимум функции: {minPoint}";

            return result;
        }

        public double GetPoint(double x, double step, bool isMax, int iterations)
        {
            int direction = 1;
            double newX = x + (step * direction);

            while (Math.Abs(x - newX) > Epsilon && (x > BeginInterval & x < EndInterval) && iterations > 0)
            {
                double fx = GetResultFromFunction(GetFunction(isMax), x);
                double fNewX = GetResultFromFunction(GetFunction(isMax), newX);                

                if (fx > fNewX)
                {
                    if (Double.IsNaN(fNewX))
                    {
                        break;
                    }
                    x = newX;
                    newX = x + (step * direction);                    
                }
                else
                {
                    direction *= -1;
                    newX = x + (step * direction);
                }
                iterations -= 1;
            }

            return x;
        }
    }
}
