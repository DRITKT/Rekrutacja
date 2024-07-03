using Rekrutacja.Workers.Template.Interfaces;

namespace Rekrutacja.Workers.Template.Strategies
{
    public class AdditionStrategy : ICalculationStrategy
    {
        public double Calculate(double x, double y) => x + y;
    }
}
