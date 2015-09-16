using DiarioAcademia.Dominio.AvaliacaoModule;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using FluentAssertions;
using DiarioAcademia.Tests.Shared;
using DiarioAcademia.Infra.AvaliacaoModule.Dao;
using System.Collections.Generic;
using Moq;
using DiarioAcademia.Infra.AvaliacaoModule.Arquivo;
using System.Data.Common;
using System.Data.SqlClient;
using DiarioAcademia.Infra.Shared;

namespace DiarioAcademia.Aplicacao.Tests
{
    [TestClass]
    public class FeedbackServiceTest
    {
        [TestMethod()]
        public void Deveria_mudar_feedback_das_provas_para_realizado()
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

            Mock<GeradorFeedback> geradorMock = new Mock<GeradorFeedback>();

            FeedbackService feedback = new FeedbackService(daoFalso.Object, geradorMock.Object);
            feedback.GerarFeedbackAlunos(mes, ano);

            prova1.Feedback.Should().Be(FeedbackEnum.Realizado);
            prova2.Feedback.Should().Be(FeedbackEnum.Realizado);
        }

        [TestMethod()]
        public void Deveria_atualizar_as_provas()
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

            Mock<GeradorFeedback> geradorMock = new Mock<GeradorFeedback>();

            FeedbackService feedback = new FeedbackService(daoFalso.Object, geradorMock.Object);
            feedback.GerarFeedbackAlunos(mes, ano);

            daoFalso.Verify(x => x.Atualizar(prova1));
            daoFalso.Verify(x => x.Atualizar(prova2));
        }

        [TestMethod()]
        public void Deveria_salvar_feedback_das_provas()
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

            Mock<GeradorFeedback> geradorMock = new Mock<GeradorFeedback>();

            FeedbackService feedback = new FeedbackService(daoFalso.Object, geradorMock.Object);
            feedback.GerarFeedbackAlunos(mes, ano);

            geradorMock.Verify(x => x.SalvarPdf(It.IsAny<FeedbackProva>()));
        }

        [TestMethod()]
        public void Deveria_atualizar_feedback_da_prova_para_pendente()
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

            Mock<GeradorFeedback> geradorMock = new Mock<GeradorFeedback>();

            daoFalso.Setup(x => x.Atualizar(prova1)).Throws(new DaoException());

            FeedbackService feedback = new FeedbackService(daoFalso.Object, geradorMock.Object);
            feedback.GerarFeedbackAlunos(mes, ano);

            daoFalso.Verify(x => x.Atualizar(prova2));
            geradorMock.Verify(x => x.SalvarPdf(It.IsAny<FeedbackProva>()), Times.Once());
        }


    }
}