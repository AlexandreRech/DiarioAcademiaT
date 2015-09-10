using DiarioAcademia.Dominio;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using FluentAssertions;
using DiarioAcademia.Tests.Shared;

namespace DiarioAcademia.Aplicacao.Tests
{
    [TestClass]
    public class AcompanhamentoAppServiceTest
    {
        [TestMethod]
        public void Deveria_realizar_feedback_das_provas()
        {           
            DateTime antiga = DateTime.Now.AddDays(-6);

            Prova prova1 = new ProvaDataBuilder().NaData(antiga)
                .ComNotaDe(new , 7).ComNotaDe(jose, 10).ComNotaDe(joao, 4)
                .Build();
            

            AcompanhamentoAppService acompanhamento = new AcompanhamentoAppService();

            acompanhamento.RepassarFeedbackDasProvas();

            prova1.FeedbackRealizado.Should().BeTrue();
            prova2.FeedbackRealizado.Should().BeTrue();
        }
    }
}