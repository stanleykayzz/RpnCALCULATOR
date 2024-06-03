using System.Net;
using WebApplication1.DbContext;
using WebApplication1.Service;
using RpnCalculator.Utils;

namespace WebApplication1.Repository
{
    public class CalculatorRepository : ICalculator
    {
        public RpnCalculatorContext _context;

        public CalculatorRepository(RpnCalculatorContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Add a new value to Stack matching id
        /// </summary>
        /// <param name="stackId"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public IEnumerable<double> AddValue(int stackId, double value)
        {
            _context.RpnCalculator.dictionnary[stackId].Push(value);

            return _context.RpnCalculator.dictionnary[stackId];
        }

        /// <summary>
        /// Creating a new stack
        /// </summary>
        /// <returns></returns>
        public int CreateStack()
        {
            try
            {
                int index = _context.RpnCalculator.indexCounter += 1;
                _context.RpnCalculator.dictionnary.Add(index, new Stack<double>());

                return index;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        /// <summary>
        /// Delete a stack matching the id
        /// </summary>
        /// <param name="stackId"></param>
        /// <returns></returns>
        public IEnumerable<Stack<double>> DeleteStack(int stackId)
        {
            _context.RpnCalculator.dictionnary.Remove(stackId);

            return _context.RpnCalculator.dictionnary.Values;
        }

        /// <summary>
        /// return a stack matching the id
        /// </summary>
        /// <param name="stackId"></param>
        /// <returns></returns>
        public Stack<double> FindStack(int stackId)
        {
            return _context.RpnCalculator.dictionnary[stackId];
        }

        /// <summary>
        /// Returning all enumerator values as IEnumerable<char>
        /// </summary>
        /// <returns></returns>
        public IEnumerable<char> GetOperands()
        {
            foreach (Utils.Operators op in Enum.GetValues(typeof(Utils.Operators)))
            {
                yield return (char)op;
            }
        }

        /// <summary>
        /// get all stacks
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Stack<double>> GetStacks()
        {
            return _context.RpnCalculator.dictionnary.Values;
        }

        /// <summary>
        /// Apply operation to Stack
        /// </summary>
        /// <param name="op"></param>
        /// <param name="stackId"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public IEnumerable<double> OperateStack(char op, int stackId)
        {
            if (!GetOperands().Contains(op))
            {
                throw new InvalidOperationException("Unknown operator.");
            }

            var selectedStack = FindStack(stackId);

            if (selectedStack.Count < 2)
            {
                throw new InvalidOperationException("Stack must contains at least 2 values to operate.");
            }

            var value1 = selectedStack.Pop();
            var value2 = selectedStack.Pop();
            
            var result = op switch
            {
                '+' => value2 + value1,
                '-' => value2 - value1,
                '*' => value2 * value1,
                '/' => value2 / value1,
                _ => throw new NotImplementedException()
            };

            selectedStack.Push(result);

            return selectedStack;
        }
    }
}
