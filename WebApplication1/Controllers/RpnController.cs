using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using WebApplication1.DbContext;
using WebApplication1.Models;
using WebApplication1.Repository;
using WebApplication1.Service;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RpnController : ControllerBase
    {
        //context static car il n'y a pas de source de données externe
        private static readonly RpnCalculatorContext _context = new RpnCalculatorContext();
        private readonly ICalculator _calculatorRepository;

        public RpnController()
        {
            _calculatorRepository = new CalculatorRepository(_context);
        }

        #region GET

        [HttpGet("op")]
        public List<string> GetOperands()
        {
            return _calculatorRepository.GetOperands();
        }

        [HttpGet("stack")]
        public List<Stack<double>> GetStacks()
        {
            return _calculatorRepository.GetStacks();
        }

        [HttpGet("stack/{stackId}")]
        public Stack<double> FindStack([FromQuery] int stackId)
        {
            return _calculatorRepository.FindStack(stackId);
        }
        #endregion

        #region POST

        [HttpPost("stack")]
        public IActionResult CreateStack()
        {
            _calculatorRepository.CreateStack();
            return Ok();
        }

        [HttpPost("stack/{stackId}/{value}")]
        public IActionResult AddValue([FromQuery] int stackId, [FromQuery] double value)
        {
            _calculatorRepository.AddValue(stackId, value);
            return Ok();
        }

        [HttpPost("op/{op}/stack/{stackId}")]
        public IActionResult OperateStack([FromQuery] string op, [FromQuery] int stackId)
        {
            _calculatorRepository.OperateStack(op, stackId);
            return Ok();
        }
        #endregion

        #region DELETE
        [HttpDelete("stack/{stackId}")]
        public IActionResult DeleteStack([FromQuery] int stackId)
        {
            _calculatorRepository.DeleteStack(stackId);
            return Ok();
        }
        #endregion


    }
}