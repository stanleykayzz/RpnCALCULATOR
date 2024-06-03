using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Service
{
    public interface ICalculator
    {
        IEnumerable<char> GetOperands();
        IEnumerable<Stack<double>> GetStacks();
        Stack<double> FindStack(int stackId);
        IEnumerable<Stack<double>> DeleteStack(int stackId);
        int CreateStack();
        IEnumerable<double> AddValue(int stackId, double value);
        IEnumerable<double> OperateStack(char op, int stackId);
    }
}
