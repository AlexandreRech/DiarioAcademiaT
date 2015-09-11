using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using DiarioAcademia.Tests.Shared;

namespace DiarioAcademia.Dominio.Tests
{
    [TestClass]
    public class AvaliadorProvaTest
    {
        private AvaliadorProva avaliador;
        private Prova prova;
        private Aluno maria;
        private Aluno jose;
        private Aluno joao;
        private Aluno luis;
        private Aluno pedro;
        private Aluno rafael;


        [TestInitialize]
        public void TestInitialize()
        {
            avaliador = new AvaliadorProva();
            maria = new Aluno(1, "Maria");
            jose = new Aluno(2, "José");
            joao = new Aluno(3, "João");
            luis = new Aluno(4, "Luis");
            pedro = new Aluno(5, "Pedro");
            rafael = new Aluno(6, "Rafael");
        }

        [TestMethod]
        public void Deveria_calcular_media_das_notas()
        {
            prova = new ProvaDataBuilder().Sobre("Portugues")
                .ComNotaDe(maria, 7).ComNotaDe(jose, 10).ComNotaDe(joao, 4)
                .Build();

            avaliador.Avaliar(prova);

            Assert.AreEqual(7, avaliador.ObtemMedia());
        }

        [TestMethod]
        public void Deveria_arredondar_a_media_das_notas()
        {
            prova = new ProvaDataBuilder().Sobre("Portugues").ComNotaDe(maria, 5)
                             .ComNotaDe(jose, 8).ComNotaDe(joao, 10).Build();

            avaliador.Avaliar(prova);

            Assert.AreEqual(7.50, avaliador.ObtemMedia());
        }

        [TestMethod]
        public void Deveria_avaliar_notas_ordem_crescente()
        {
            prova = new ProvaDataBuilder().Sobre("Portugues").ComNotaDe(maria, 5)
                             .ComNotaDe(jose, 8).ComNotaDe(joao, 10).Build();


            avaliador.Avaliar(prova);

            Assert.AreEqual(10, avaliador.ObtemMaiorNota());

            Assert.AreEqual(5, avaliador.ObtemMenorNota());
        }

        [TestMethod]
        public void Deveria_avaliar_notas_ordem_decrescente()
        {
            prova = new ProvaDataBuilder().Sobre("Portugues").ComNotaDe(maria, 10)
                               .ComNotaDe(jose, 8).ComNotaDe(joao, 5).Build();


            avaliador.Avaliar(prova);

            Assert.AreEqual(10, avaliador.ObtemMaiorNota());

            Assert.AreEqual(5, avaliador.ObtemMenorNota());
        }

        [TestMethod]
        public void Deveria_avaliar_notas_aleatorias()
        {
            prova = new ProvaDataBuilder().Sobre("Portugues").ComNotaDe(maria, 8)
                              .ComNotaDe(jose, 10).ComNotaDe(joao, 5).Build();

            avaliador.Avaliar(prova);

            Assert.AreEqual(10, avaliador.ObtemMaiorNota());

            Assert.AreEqual(5, avaliador.ObtemMenorNota());
        }

        [TestMethod]
        public void Deveria_avaliar_prova_com_apenas_uma_nota()
        {
            prova = new ProvaDataBuilder()
                .Sobre("Portugues").ComNotaDe(maria, 8).Build();

            avaliador.Avaliar(prova);

            Assert.AreEqual(8, avaliador.ObtemMaiorNota());

            Assert.AreEqual(8, avaliador.ObtemMenorNota());
        }

        [TestMethod]
        public void Deveria_retornar_as_3_piores_notas()
        {

            prova = new ProvaDataBuilder().Sobre("Portugues").ComNotaDe(maria, 5)
                             .ComNotaDe(jose, 1).ComNotaDe(joao, 2).ComNotaDe(maria, 4)
                             .ComNotaDe(pedro, 3).ComNotaDe(luis, 3)
                             .ComNotaDe(rafael, 6).Build();

            avaliador.Avaliar(prova);

            avaliador.PioresNotas.Should().HaveCount(3);

            avaliador.PioresNotas[0].Valor.Should().Be(1);
            avaliador.PioresNotas[1].Valor.Should().Be(2);
            avaliador.PioresNotas[2].Valor.Should().Be(3);
        }

        [TestMethod]
        public void Deveria_retornar_todas_as_notas_caso_nao_haja_no_minimo_3()
        {
            prova = new ProvaDataBuilder().Sobre("Portugues").ComNotaDe(joao, 1).ComNotaDe(jose, 2).Build();

            avaliador.Avaliar(prova);

            Assert.AreEqual(2, avaliador.PioresNotas.Count);

            Assert.AreEqual(1, avaliador.PioresNotas[0].Valor);
            Assert.AreEqual(2, avaliador.PioresNotas[1].Valor);
        }

        [TestMethod]
        public void Nao_deveria_avaliar_prova_sem_lancamento_de_notas()
        {
            prova = new ProvaDataBuilder().Sobre("Portugues").Build();

            Action sut = () => avaliador.Avaliar(prova);

            sut.ShouldThrow<InvalidOperationException>();
        }
    }
}