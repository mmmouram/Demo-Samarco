using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using MyApp.Modelos;
using MyApp.Repository;
using MyApp.Service;

namespace MyApp.Tests.Service
{
    [TestFixture]
    public class DashboardServiceTests
    {
        private Mock<ICorreiaRepository> _mockRepo;
        private DashboardService _dashboardService;

        [SetUp]
        public void Setup()
        {
            _mockRepo = new Mock<ICorreiaRepository>();
            _dashboardService = new DashboardService(_mockRepo.Object);
        }

        [Test]
        public async Task ObterDashboardAsync_DeveRetornarCorreiasOrdenadasPorRiscoDesc()
        {
            // Arrange
            var now = DateTime.Now;
            var correias = new List<Correia>
            {
                new Correia
                {
                    Id = 1,
                    Nome = "Correia A",
                    Status = "Crítico",
                    DataUltimaInspecao = now.AddDays(-10),
                    LeiturasSensores = new List<LeituraSensor>
                    {
                        new LeituraSensor { Id = 1, CorreiaId = 1, DataLeitura = now, ValorSensor = 80.0 }
                    }
                },
                new Correia
                {
                    Id = 2,
                    Nome = "Correia B",
                    Status = "Aviso",
                    DataUltimaInspecao = now.AddDays(-5),
                    LeiturasSensores = new List<LeituraSensor>
                    {
                        new LeituraSensor { Id = 2, CorreiaId = 2, DataLeitura = now, ValorSensor = 50.0 }
                    }
                },
                new Correia
                {
                    Id = 3,
                    Nome = "Correia C",
                    Status = "Normal",
                    DataUltimaInspecao = now.AddDays(-20),
                    LeiturasSensores = new List<LeituraSensor>()
                }
            };

            _mockRepo.Setup(repo => repo.ObterTodasCorreiasAsync()).ReturnsAsync(correias);

            // Act
            var result = await _dashboardService.ObterDashboardAsync();
            var list = result.ToList();

            // Assert
            Assert.AreEqual(3, list.Count);
            // Verifica se a lista está ordenada em ordem decrescente de risco
            Assert.GreaterOrEqual(list[0].Risco, list[1].Risco);
            Assert.GreaterOrEqual(list[1].Risco, list[2].Risco);
        }

        [Test]
        public async Task ObterDetalhesCorreiaAsync_DeveRetornarCorreia_DadoIdValido()
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
            _mockRepo.Setup(repo => repo.ObterCorreiaPorIdAsync(1)).ReturnsAsync(correia);

            // Act
            var resultado = await _dashboardService.ObterDetalhesCorreiaAsync(1);

            // Assert
            Assert.IsNotNull(resultado);
            Assert.AreEqual(correia, resultado);
        }
    }
}
