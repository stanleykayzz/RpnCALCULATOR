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
        public IEnumerable<char> GetOperands()
        {
            return _calculatorRepository.GetOperands();
        }

        /// <summary>
        /// Get All RPN Calculator stacks
        /// </summary>
        /// <returns></returns>
        [HttpGet("stack")]
        public IEnumerable<Stack<double>> GetStacks()
        {
            return _calculatorRepository.GetStacks();
        }

        /// <summary>
        /// Get stack matching stackId
        /// </summary>
        /// <param name="stackId"></param>
        /// <returns></returns>
        [HttpGet("stack/{stackId}")]
        public Stack<double> FindStack([FromQuery] int stackId)
        {
            return _calculatorRepository.FindStack(stackId);
        }
        #endregion

        #region POST

        /// <summary>
        /// Create new Stack
        /// </summary>
        /// <returns></returns>
        [HttpPost("stack")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult CreateStack()
        {
            int result = _calculatorRepository.CreateStack();
            return (result > 0)? Ok(new { StackCreatedId = result }) : BadRequest(new { NotCreated = result });
        }

        /// <summary>
        /// Add a value to stack in parameter
        /// </summary>
        /// <param name="stackId"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost("stack/{stackId}/{value}")]
        public IActionResult AddValue([FromQuery] int stackId, [FromQuery] double value)
        {
            _calculatorRepository.AddValue(stackId, value);
            return Ok();
        }

        /// <summary>
        /// Apply operation to last values of stack
        /// </summary>
        /// <param name="op"></param>
        /// <param name="stackId"></param>
        /// <returns></returns>
        [HttpPost("op/{op}/stack/{stackId}")]
        public IActionResult OperateStack([FromQuery] char op, [FromQuery] int stackId)
        {
            _calculatorRepository.OperateStack(op, stackId);
            return Ok();
        }
        #endregion

        #region DELETE
        /// <summary>
        /// Delete stack at Id
        /// </summary>
        /// <param name="stackId"></param>
        /// <returns></returns>
        [HttpDelete("stack/{stackId}")]
        public IActionResult DeleteStack([FromQuery] int stackId)
        {
            _calculatorRepository.DeleteStack(stackId);
            return Ok();
        }
        #endregion

    }
}