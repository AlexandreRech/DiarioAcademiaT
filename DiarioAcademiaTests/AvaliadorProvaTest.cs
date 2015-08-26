using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DiarioAcademia.Dominio;

namespace DiarioAcademia.Dominio.Tests
{
    [TestClass]
    public class AvaliadorProvaTest
    {
        [TestMethod]
        public void Deveria_avaliar_notas_ordem_crescente()
        {
            Prova prova = new Prova(DateTime.Now);

            prova.LancarNota(5, new Aluno("Maria"));
            prova.LancarNota(8, new Aluno("João"));
            prova.LancarNota(10, new Aluno("José"));

            AvaliadorProva avaliador = new AvaliadorProva();

            avaliador.Avaliar(prova);

            // imprimi 10
            Assert.AreEqual(10, avaliador.ObtemMaiorNota());

            // imprimi 5
            Assert.AreEqual(5, avaliador.ObtemMenorNota());
        }
    }
}

