namespace WebApplication1.Models
{
    public class RpnCalculator
    {
        public List<Stack<double>> stacks = new List<Stack<double>>();
        public List<string> operandList { get; set; }

        public RpnCalculator()
        {
            stacks = new List<Stack<double>>();
            operandList = new List<string> { "+", "-", "/", "*" };
        }
    }
}