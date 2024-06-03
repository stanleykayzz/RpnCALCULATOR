using Microsoft.AspNetCore.Http.HttpResults;
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

        public RpnController(ICalculator calculatorRepository)
        {
            _calculatorRepository = calculatorRepository;
        }

        #region GET
        /// <summary>
        /// Get All Operators
        /// </summary>
        /// <returns></returns>
        [HttpGet("operators")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<IEnumerable<char>> GetOperands()
        {
            try
            {
                var result = _calculatorRepository.GetOperands();

                return (result != null) ? Ok(new { result }) : BadRequest(new { Error = result });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { ErrorMessage = e.Message });
            }

        }

        /// <summary>
        /// Get All RPN Calculator stacks
        /// </summary>
        /// <returns></returns>
        [HttpGet("stack")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Stack<double>> GetStacks()
        {
            try
            {
                var result = _calculatorRepository.GetStacks();

                return (result != null) ? Ok(new { result }) : BadRequest(new { Error = result });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { ErrorMessage = e.Message });
            }
        }

        /// <summary>
        /// Get stack matching stackId
        /// </summary>
        /// <param name="stackId"></param>
        /// <returns></returns>
        [HttpGet("stack/{stackId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Stack<double>> FindStack(int stackId)
        {
            try
            {
                var result = _calculatorRepository.FindStack(stackId);

                return (result != null) ? Ok(new { result }) : BadRequest(new { Error = result });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { ErrorMessage = "Matching stack not found." });
            }
        }
        #endregion

        #region POST

        /// <summary>
        /// Create new Stack
        /// </summary>
        /// <returns></returns>
        [HttpPost("stack")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult CreateStack()
        {
            try
            {
                int result = _calculatorRepository.CreateStack();
                
                return (result > 0) ? Ok(new { StackCreatedId = result }) : BadRequest(new { Error = result });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { ErrorMessage = e.Message });
            }
        }

        /// <summary>
        /// Add a value to stack in parameter
        /// </summary>
        /// <param name="stackId"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost("stack/{stackId}/{value}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult AddValue(int stackId, double value)
        {
            try
            {
                var result = _calculatorRepository.AddValue(stackId, value);

                return (result != null) ? Ok(new { UpdatedStack = result }) : BadRequest(new { Error = result });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { ErrorMessage = "Values can't be added to unknown stack." });
            }
        }

        /// <summary>
        /// Apply operation to last values of stack
        /// </summary>
        /// <param name="op"></param>
        /// <param name="stackId"></param>
        /// <returns></returns>
        [HttpPost("op/{op}/stack/{stackId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult OperateStack(char op, int stackId)
        {
            try
            {
                var result = _calculatorRepository.OperateStack(op, stackId);

                return (result != null) ? Ok(new { OperatedStack = result }) : BadRequest(new { Error = result });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { ErrorMessage = e.Message });
            }
        }
        #endregion

        #region DELETE
        /// <summary>
        /// Delete stack at Id
        /// </summary>
        /// <param name="stackId"></param>
        /// <returns></returns>
        [HttpDelete("stack/{stackId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DeleteStack(int stackId)
        {
            try
            {
                var result = _calculatorRepository.DeleteStack(stackId);

                return (result != null) ? Ok(new { Stacks = result }) : BadRequest(new { Error = result });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { ErrorMessage = "There are no stacks to delete." });
            }
        }
        #endregion

    }
}