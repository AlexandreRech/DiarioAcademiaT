using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.QualityTools.Testing.Fakes;
using System.IO.Fakes;

namespace DiarioAcademia.Dominio.AvaliacaoModule.Tests
{
    [TestClass]
    public class FeedbackMensalTest
    {
        [TestMethod]
        public void Deveria_gerar_nome_arquivo_feedback_mensal()
        {
            //arrange
            FeedbackMensal feedbackMensal = new FeedbackMensal(1, 2015, null, null);

            //action and assert
            feedbackMensal.NomeArquivo().Should()
                .Be(feedbackMensal.Diretorio + "\\" + "feedback-provas-janeiro-2015.pdf");
        }

        [TestMethod]
        public void Deveria_gerar_nome_arquivo_com_controle_duplicacao_feedback_mensal()
        {
            //action and assert
            FeedbackMensal feedbackMensal = new FeedbackMensal(1, 2015, null, null);

            feedbackMensal.NomeArquivo()
                .Should().Be(feedbackMensal.Diretorio + "\\" + "feedback-provas-janeiro-2015(1).pdf");
        }
    }
}