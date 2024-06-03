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
        public void Test_GetBasicOperators_Ok(IEnumerable<char> expectedOperator, bool expectedResult)
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
    }
}
