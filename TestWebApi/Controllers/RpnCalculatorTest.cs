using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebApplication1.Controllers;
using WebApplication1.DbContext;
using WebApplication1.Repository;
using WebApplication1.Service;

namespace TestWebApi.Controllers
{
    internal class RpnCalculatorTest
    {
        private RpnController _controller;
        private Mock<ICalculator> _repositoryMock;

        [SetUp]
        public void Setup()
        {

            _repositoryMock = new Mock<ICalculator>();
            _controller = new RpnController(_repositoryMock.Object);
        }

        [Test]
        [TestCase(new char[] {'+', '-', '*', '/'}, true)]
        public void GetOperand_ReturnsOk_GetBasicOperators(IEnumerable<char> expectedOperator, bool expectedResult)
        {
            //Arrange
            _repositoryMock.Setup(s => s.GetOperands()).Returns(expectedOperator);

            //Act
            var actual = _controller.GetOperands();

            //Assert
            Assert.IsInstanceOf<ActionResult<IEnumerable<char>>>(actual);
            var okResult = actual.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.IsInstanceOf<IEnumerable<char>>(okResult.Value.GetType().GetProperty("result").GetValue(okResult.Value));
            Assert.AreEqual(expectedOperator, okResult.Value.GetType().GetProperty("result").GetValue(okResult.Value));

        }

        [Test]
        public void CreateStack_ReturnsOk_WhenStackIsCreated()
        {
            // Arrange
            _repositoryMock.Setup(repo => repo.CreateStack()).Returns(1);

            // Act
            var result = _controller.CreateStack();

            // Assert
            Assert.IsInstanceOf<ActionResult>(result);
            var createdResult = result as OkObjectResult;
            Assert.IsNotNull(createdResult);
            Assert.AreEqual(200, createdResult.StatusCode);
            Assert.IsNotNull(createdResult.Value);
        }

        [Test]
        public void DeleteStack_ReturnsOk_WhenStackIsDeleted()
        {
            // Arrange
            var stackId = 1;
            var expectedStacks = new List<Stack<double>>(); // or whatever your expected result is
            _repositoryMock.Setup(repo => repo.DeleteStack(stackId)).Returns(expectedStacks);

            // Act
            var result = _controller.DeleteStack(stackId);

            // Assert
            Assert.IsInstanceOf<ActionResult>(result);
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(StatusCodes.Status200OK, okResult.StatusCode);
            Assert.IsNotNull(okResult.Value);
            Assert.AreEqual(expectedStacks, okResult.Value.GetType().GetProperty("Stacks").GetValue(okResult.Value));
        }

        [Test]
        public void FindStack_ReturnsOk_WhenStackIsFound()
        {
            // Arrange
            var stackId = 1;
            var expectedStack = new Stack<double>();
            _repositoryMock.Setup(repo => repo.FindStack(stackId)).Returns(expectedStack);

            // Act
            var result = _controller.FindStack(stackId);

            // Assert
            Assert.IsInstanceOf<ActionResult<Stack<double>>>(result);
            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(StatusCodes.Status200OK, okResult.StatusCode);
            Assert.IsNotNull(okResult.Value);
            Assert.AreEqual(expectedStack, okResult.Value.GetType().GetProperty("result").GetValue(okResult.Value));
        }

        [Test]
        public void OperateStack_ReturnsOk_WhenOperationIsSuccessful()
        {
            // Arrange
            var stackId = 1;
            var operation = '+';
            var expectedStack = new Stack<double>();
            _repositoryMock.Setup(repo => repo.OperateStack(operation, stackId)).Returns(expectedStack);

            // Act
            var result = _controller.OperateStack(operation, stackId);

            // Assert
            Assert.IsInstanceOf<IActionResult>(result);
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(StatusCodes.Status200OK, okResult.StatusCode);
            Assert.IsNotNull(okResult.Value);
            Assert.AreEqual(expectedStack, okResult.Value.GetType().GetProperty("OperatedStack").GetValue(okResult.Value));
        }

        [Test]
        public void AddValue_ReturnsOk_WhenValueIsAddedSuccessfully()
        {
            // Arrange
            var stackId = 1;
            var value = 5.0;
            var expectedStack = new Stack<double>();
            _repositoryMock.Setup(repo => repo.AddValue(stackId, value)).Returns(expectedStack);

            // Act
            var result = _controller.AddValue(stackId, value);

            // Assert
            Assert.IsInstanceOf<IActionResult>(result);
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(StatusCodes.Status200OK, okResult.StatusCode);
            Assert.IsNotNull(okResult.Value);
            Assert.AreEqual(expectedStack, okResult.Value.GetType().GetProperty("UpdatedStack").GetValue(okResult.Value));
        }
    }
}
