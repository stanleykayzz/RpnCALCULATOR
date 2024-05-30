using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Service
{
    public interface ICalculator
    {
        List<char> GetOperands();
        List<Stack<double>> GetStacks();
        Stack<double> FindStack(int stackId);
        void DeleteStack(int stackId);
        void CreateStack();
        void AddValue(int stackId, double value);
        void OperateStack(string op, int stackId);
    }
}
