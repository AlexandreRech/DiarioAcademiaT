using DiarioAcademia.Dominio;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using FluentAssertions;
using DiarioAcademia.Tests.Shared;
using DiarioAcademia.Infra.Dao;
using System.Collections.Generic;
using Moq;

namespace DiarioAcademia.Aplicacao.Tests
{
    [TestClass]
    public class AcompanhamentoAppServiceTest
    {
        [TestMethod]
        public void Deveria_realizar_feedback_das_provas()
        {
            DateTime antiga = DateTime.Now.AddDays(-6);

            Prova prova1 = new ProvaDataBuilder().Sobre("Design Patterns").NaData(antiga)
                .ComNotaDe(new Aluno(1, "Rech"), 7)
                .ComNotaDe(new Aluno(2, "Wesley"), 10)
                .ComNotaDe(new Aluno(3, "Guilherme"), 4)
                .Build();

            Prova prova2 = new ProvaDataBuilder().Sobre("Herança").NaData(antiga)
               .ComNotaDe(new Aluno(1, "Rech"), 7)
               .ComNotaDe(new Aluno(2, "Wesley"), 10)
               .ComNotaDe(new Aluno(3, "Guilherme"), 4)
               .Build();

            Mock<ProvaDao> mockDaoFalso = new Mock<ProvaDao>();

            mockDaoFalso.Setup(x => x.SelecionarProvasPendentesFeedback())
                .Returns(new List<Prova> { prova1, prova2 });

            AcompanhamentoAppService acompanhamento = new AcompanhamentoAppService(mockDaoFalso.Object);

            acompanhamento.RepassarFeedbackDasProvas();

            acompanhamento.TotalEncerrados.Should().Be(2);
            prova1.FeedbackRealizado.Should().BeTrue();
            prova2.FeedbackRealizado.Should().BeTrue();
        }
    }
}