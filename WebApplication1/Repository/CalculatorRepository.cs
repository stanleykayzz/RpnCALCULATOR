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
            try
            {
                _context.RpnCalculator.dictionnary.Remove(stackId);

                return (IEnumerable<Stack<double>>) _context.RpnCalculator.dictionnary.ToList();
            }
            catch (Exception)
            {
                throw;
            }

        }

        /// <summary>
        /// return a stack matching the id
        /// </summary>
        /// <param name="stackId"></param>
        /// <returns></returns>
        public Stack<double> FindStack(int stackId)
        {
            try
            {
                return  _context.RpnCalculator.dictionnary[stackId];
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Returning all enumerator values as IEnumerable<char>
        /// </summary>
        /// <returns></returns>
        public IEnumerable<char> GetOperands()
        {
            foreach (Operators op in Enum.GetValues(typeof(RpnCalculator.Operators)))
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
            var selectedStack = FindStack(stackId);
            
            if (selectedStack.Count < 2)
            {
                throw new InvalidOperationException("La pile doit contenir au moins deux éléments pour effectuer l'opération.");
            }

            var values = selectedStack.Take(2).ToArray();
            var result = op switch
            {
                '+' => values[0] + values[1],
                '-' => values[0] - values[1],
                '*' => values[0] * values[1],
                '/' => values[0] / values[1],
                _ => throw new InvalidOperationException("Opération non reconnue")
            };

            selectedStack.Pop();
            selectedStack.Pop();
            selectedStack.Push(result);

            return selectedStack;
        }
    }
}
