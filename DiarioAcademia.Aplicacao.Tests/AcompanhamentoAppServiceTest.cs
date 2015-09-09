using DiarioAcademia.Dominio;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using FluentAssertions;

namespace DiarioAcademia.Aplicacao.Tests
{
    [TestClass]
    public class AcompanhamentoAppServiceTest
    {
        [TestMethod]
        public void Deveria_realizar_feedback_das_provas()
        {           
            DateTime antiga = DateTime.Now.AddDays(-6);

            Prova prova1 = new Prova(antiga);
            Prova prova2 = new Prova(antiga);

            // mas como passo os leilões criados para o EncerradorDeLeilao,
            // já que ele os busca no DAO?

            AcompanhamentoAppService acompanhamento = new AcompanhamentoAppService();

            acompanhamento.RepassarFeedbackDasProvas();

            prova1.FeedbackRealizado.Should().BeTrue();
            prova2.FeedbackRealizado.Should().BeTrue();
        }
    }
}
