using WebApplication1.DbContext;
using WebApplication1.Models;
using WebApplication1.Service;
using static WebApplication1.Models.RpnCalculator;

namespace WebApplication1.Repository
{
    public class CalculatorRepository : ICalculator
    {
        public readonly RpnCalculatorContext _context;

        public CalculatorRepository(RpnCalculatorContext context)
        {
            _context = context;
        }
        public void AddValue(int stackId, double value)
        {
            _context.RpnCalculator.stacks.ElementAt(stackId - 1).Push(value);
        }

        public void CreateStack()
        {
            _context.RpnCalculator.stacks.Add(new Stack<double>());
        }

        public void DeleteStack(int stackId)
        {
            //_context.NpmCalculator.stacks.RemoveAt(stackId - 1);
            _context.RpnCalculator.stacks = _context.RpnCalculator.stacks
                .Where((stack, index) => index != stackId - 1)
                .ToList();
        }

        public Stack<double> FindStack(int stackId)
        {
            return _context.RpnCalculator.stacks.ElementAt(stackId - 1);
        }

        public List<char> GetOperands()
        {
            List<char> operatorList = new List<char>();

            foreach (var op in Enum.GetValues(typeof(RpnCalculator.Operators)))
            {
                operatorList.Add((char)op);
            }
            
            return operatorList;
        }

        public List<Stack<double>> GetStacks()
        {
            return _context.RpnCalculator.stacks.ToList();
        }

        public void OperateStack(string op, int stackId)
        {
            var selectedStack = _context.RpnCalculator.stacks.ElementAt(stackId - 1);
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
