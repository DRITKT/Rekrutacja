using Rekrutacja.Workers.Template.Interfaces;
using System;

namespace Rekrutacja.Workers.Template.Strategies
{
    public class DivisionStrategy : ICalculationStrategy
    {
        public double Calculate(double x, double y)
        {
            if (y == 0)
                throw new DivideByZeroException(Constants.DivisionByZeroMessage);
            return x / y;
        }
    }
}
