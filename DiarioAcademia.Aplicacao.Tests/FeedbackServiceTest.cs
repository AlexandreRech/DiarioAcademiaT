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
        
        [TestMethod]
        public void Deveria_mudar_feedback_das_provas_para_realizado()
        {
            //arrange 
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

            Mock<ProvaDao> provaDaoFalso = new Mock<ProvaDao>();
            provaDaoFalso.Setup(x => x.SelecionarProvasPendentesFeedback(mes, ano))
                .Returns(new List<Prova> { prova1, prova2 });

            //action
            FeedbackService feedback = new FeedbackService(provaDaoFalso.Object, Mock.Of<GeradorFeedback>());
            feedback.GerarFeedbackAlunos(mes, ano);

            //assert

            feedback.TotalFeedbackRealizados.Should().Be(2);

            prova1.Feedback.Should().Be(FeedbackEnum.Realizado);
            prova2.Feedback.Should().Be(FeedbackEnum.Realizado);           
        }

        [TestMethod]
        public void Deveria_atualizar_provas()
        {
             //arrange 
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

            Mock<ProvaDao> provaDaoFalso = new Mock<ProvaDao>();
            provaDaoFalso.Setup(x => x.SelecionarProvasPendentesFeedback(mes, ano))
                .Returns(new List<Prova> { prova1, prova2 });

            //action
            FeedbackService feedback = new FeedbackService(provaDaoFalso.Object, Mock.Of<GeradorFeedback>());
            feedback.GerarFeedbackAlunos(mes, ano);
            
            //assert
            provaDaoFalso.Verify(x => x.Atualizar(prova1));
            provaDaoFalso.Verify(x => x.Atualizar(prova2));
        }

        [TestMethod]
        public void Deveria_salvar_feedback_mensal_provas()
        {
            //arrange 
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

            Mock<ProvaDao> provaDaoFalso = new Mock<ProvaDao>();
            provaDaoFalso.Setup(x => x.SelecionarProvasPendentesFeedback(mes, ano))
                .Returns(new List<Prova> { prova1, prova2 });

            Mock<GeradorFeedback> geradorFalso = new Mock<GeradorFeedback>();

            //action
            FeedbackService feedback = new FeedbackService(provaDaoFalso.Object, geradorFalso.Object);
            feedback.GerarFeedbackAlunos(mes, ano);

            //assert
            geradorFalso.Verify(x => x.SalvarPdf(It.IsAny<FeedbackMensal>()));
        }

        [TestMethod]
        public void Deveria_cancelar_feedback_mensal_provas()
        {
            //arrange 
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

            List<Prova> provas = new List<Prova> { prova1, prova2 };

            Mock<ProvaDao> provaDaoFalso = new Mock<ProvaDao>();
            provaDaoFalso.Setup(x => x.SelecionarProvasPendentesFeedback(mes, ano))
                .Returns(new List<Prova> { prova1, prova2 });

            Mock<GeradorFeedback> geradorFalso = new Mock<GeradorFeedback>();
            geradorFalso.Setup(x => x.SalvarPdf(It.IsAny<FeedbackMensal>())).Throws<Exception>();

            //action
            FeedbackService feedback = new FeedbackService(provaDaoFalso.Object, geradorFalso.Object);
            feedback.GerarFeedbackAlunos(mes, ano);

            //assert
            provaDaoFalso.Verify(x => x.CancelarFeedback(provas));
        }

        [TestMethod]
        public void Deveria_calcular_media_feedback_mensal_provas()
        {
            //arrange 
            int mes = DateTime.Now.Month;
            int ano = DateTime.Now.Year;

            Prova prova1 = new ProvaDataBuilder().Sobre("Design Patterns").NaData(DateTime.Now)
                .ComNotaDe(new Aluno(1, "Rech"), 10)
                .ComNotaDe(new Aluno(2, "Wesley"), 10)
                .ComNotaDe(new Aluno(3, "Guilherme"), 10)
                .Build();

            Prova prova2 = new ProvaDataBuilder().Sobre("Herança").NaData(DateTime.Now)
               .ComNotaDe(new Aluno(1, "Rech"), 8)
               .ComNotaDe(new Aluno(2, "Wesley"), 8)
               .ComNotaDe(new Aluno(3, "Guilherme"), 8)
               .Build();

            List<Prova> provas = new List<Prova> { prova1, prova2 };

            Mock<ProvaDao> provaDaoFalso = new Mock<ProvaDao>();
            provaDaoFalso.Setup(x => x.SelecionarProvasPendentesFeedback(mes, ano))
                .Returns(new List<Prova> { prova1, prova2 });

            FeedbackMensal feedbackMensal = null;

            Mock<GeradorFeedback> geradorFalso = new Mock<GeradorFeedback>();
            geradorFalso.Setup(x => x.SalvarPdf(It.IsAny<FeedbackMensal>()))
                .Callback<FeedbackMensal>(x => feedbackMensal = x);

            //action
            FeedbackService feedback = new FeedbackService(provaDaoFalso.Object, geradorFalso.Object);
            feedback.GerarFeedbackAlunos(mes, ano);            

            //assert
            feedbackMensal.CalcularMedia().Should().Be(9);
        }
       
    }
}