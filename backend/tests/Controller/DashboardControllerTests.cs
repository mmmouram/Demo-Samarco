using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Microsoft.AspNetCore.Mvc;
using MyApp.Controller;
using MyApp.Modelos;
using MyApp.Service;

namespace MyApp.Tests.Controller
{
    [TestFixture]
    public class DashboardControllerTests
    {
        private Mock<IDashboardService> _mockService;
        private DashboardController _controller;

        [SetUp]
        public void Setup()
        {
            _mockService = new Mock<IDashboardService>();
            _controller = new DashboardController(_mockService.Object);
        }

        [Test]
        public async Task ObterDashboard_DeveRetornarOkComListaDeDashboardCorreiaResponse()
        {
            // Arrange
            var now = DateTime.Now;
            var responses = new List<DashboardCorreiaResponse>
            {
                new DashboardCorreiaResponse { Id = 1, Nome = "Correia A", Risco = 120.0, DataUltimaInspecao = now, Alerta = true },
                new DashboardCorreiaResponse { Id = 2, Nome = "Correia B", Risco = 80.0, DataUltimaInspecao = now, Alerta = false }
            };

            _mockService.Setup(s => s.ObterDashboardAsync(null, null)).ReturnsAsync(responses);

            // Act
            IActionResult actionResult = await _controller.ObterDashboard(null, null);
            var okResult = actionResult as OkObjectResult;

            // Assert
            Assert.IsNotNull(okResult);
            var data = okResult.Value as IEnumerable<DashboardCorreiaResponse>;
            Assert.IsNotNull(data);
            Assert.AreEqual(2, data.Count());
        }

        [Test]
        public async Task ObterDetalhesCorreia_DeveRetornarOkQuandoCorreiaEncontrada()
        {
            // Arrange
            var now = DateTime.Now;
            var correia = new Correia
            {
                Id = 1,
                Nome = "Correia A",
                DataUltimaInspecao = now,
                LeiturasSensores = new List<LeituraSensor>()
            };

            _mockService.Setup(s => s.ObterDetalhesCorreiaAsync(1)).ReturnsAsync(correia);

            // Act
            IActionResult actionResult = await _controller.ObterDetalhesCorreia(1);
            var okResult = actionResult as OkObjectResult;

            // Assert
            Assert.IsNotNull(okResult);
            Assert.AreEqual(correia, okResult.Value);
        }

        [Test]
        public async Task ObterDetalhesCorreia_DeveRetornarNotFoundQuandoCorreiaNaoEncontrada()
        {
            // Arrange
            _mockService.Setup(s => s.ObterDetalhesCorreiaAsync(1)).ReturnsAsync((Correia)null);

            // Act
            IActionResult actionResult = await _controller.ObterDetalhesCorreia(1);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(actionResult);
        }
    }
}
