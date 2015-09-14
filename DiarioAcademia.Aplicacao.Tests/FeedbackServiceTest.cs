using DiarioAcademia.Dominio.AvaliacaoModule;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using FluentAssertions;
using DiarioAcademia.Tests.Shared;
using DiarioAcademia.Infra.AvaliacaoModule.Dao;
using System.Collections.Generic;
using Moq;
using DiarioAcademia.Infra.AvaliacaoModule.Arquivo;

namespace DiarioAcademia.Aplicacao.Tests
{
    [TestClass]
    public class FeedbackServiceTest
    {
        [TestMethod()]
        public void Deveria_gerar_feedback_dos_alunos()
        {     
            int mes = DateTime.Now.Month;
            int ano = DateTime.Now.Year;

            Prova prova1 = new ProvaDataBuilder().Sobre("Design Patterns").NaData(DateTime.Now)
                .ComNotaDe(new Aluno(1, "Rech"), 7)
                .ComNotaDe(new Aluno(2, "Wesley"), 10)
                .ComNotaDe(new Aluno(3, "Guilherme"), 4)
                .Build();

            Prova prova2 = new ProvaDataBuilder().Sobre("Herança").NaData(DateTime.Now)
               .ComNotaDe(new Aluno(1, "Rech"), 7)
               .ComNotaDe(new Aluno(2, "Wesley"), 10)
               .ComNotaDe(new Aluno(3, "Guilherme"), 4)
               .Build();

            Mock<ProvaDao> daoFalso = new Mock<ProvaDao>();
            daoFalso.Setup(x => x.SelecionarProvasPendentesFeedback(mes, ano))
                .Returns(new List<Prova> { prova1, prova2 });
          
            FeedbackService feedback = new FeedbackService(daoFalso.Object);
            feedback.GerarFeedbackAlunos(mes, ano);

            feedback.TotalFeedbackRealizados.Should().Be(2);

            prova1.FeedbackRealizado.Should().BeTrue();
            prova2.FeedbackRealizado.Should().BeTrue();            
        }
    }
}