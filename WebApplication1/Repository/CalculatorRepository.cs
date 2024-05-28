using WebApplication1.DbContext;
using WebApplication1.Service;

namespace WebApplication1.Repository
{
    public class CalculatorRepository : ICalculator
    {
        public readonly NpmCalculatorContext _context;

        public CalculatorRepository(NpmCalculatorContext context)
        {
            _context = context;
        }
        public void AddValue(int stackId, double value)
        {
            _context.NpmCalculator.stacks.ElementAt(stackId - 1).Push(value);
        }

        public void CreateStack()
        {
            _context.NpmCalculator.stacks.Add(new Stack<double>());
        }

        public void DeleteStack(int stackId)
        {
            //_context.NpmCalculator.stacks.RemoveAt(stackId - 1);
            _context.NpmCalculator.stacks = _context.NpmCalculator.stacks
                .Where((stack, index) => index != stackId - 1)
                .ToList();
        }

        public Stack<double> FindStack(int stackId)
        {
            return _context.NpmCalculator.stacks.ElementAt(stackId - 1);
        }

        public List<string> GetOperands()
        {
            return _context.NpmCalculator.operandList.ToList();
        }

        public List<Stack<double>> GetStacks()
        {
            return _context.NpmCalculator.stacks.ToList();
        }

        public void OperateStack(string op, int stackId)
        {
            var selectedStack = _context.NpmCalculator.stacks.ElementAt(stackId - 1);
            if (selectedStack.Count < 2)
            {
                throw new InvalidOperationException("La pile doit contenir au moins deux éléments pour effectuer l'opération.");
            }

            var values = selectedStack.Take(2).ToArray();
            var result = op switch
            {
                "+" => values[0] + values[1],
                "-" => values[0] - values[1],
                "*" => values[0] * values[1],
                "/" => values[0] / values[1],
                _ => throw new InvalidOperationException("Opération non reconnue")
            };

            selectedStack.Pop();
            selectedStack.Pop();

            selectedStack.Push(result);
        }
    }
}
