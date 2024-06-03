using System.Collections;

namespace WebApplication1.Models
{
    public class RpnCalculator
    {
        public IDictionary<int, Stack<double>> dictionnary {  get; set; }
        public int indexCounter { get; set; }
        public enum Operators 
        {
            Add = '+',
            Substract = '-',
            Multiply = '*',
            Divide = '/'
        }

        public RpnCalculator()
        {
            dictionnary = new Dictionary<int, Stack<double>>();
            indexCounter = 0;
        }
    }
}