using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DiarioAcademia.Dominio;

namespace DiarioAcademia.Dominio.Tests
{
    [TestClass]
    public class AvaliadorProvaTest
    {
        [TestMethod]
        public void Deveria_calcular_media_das_notas()
        {
            Prova prova = new Prova(DateTime.Now);

            prova.LancarNota(5, new Aluno("Maria"));
            prova.LancarNota(10, new Aluno("José"));

            AvaliadorProva avaliador = new AvaliadorProva();

            avaliador.Avaliar(prova);

            Assert.AreEqual(7.50, avaliador.ObtemMedia());
        }

        [TestMethod]
        public void Deveria_arredondar_a_media_das_notas()
        {
            Prova prova = new Prova(DateTime.Now);

            prova.LancarNota(5, new Aluno("Maria"));
            prova.LancarNota(8, new Aluno("Maria"));
            prova.LancarNota(10, new Aluno("José"));

            AvaliadorProva avaliador = new AvaliadorProva();

            avaliador.Avaliar(prova);

            Assert.AreEqual(7.50, avaliador.ObtemMedia());
        }

        [TestMethod]
        public void Deveria_avaliar_notas_ordem_crescente()
        {
            Prova prova = new Prova(DateTime.Now);

            prova.LancarNota(5, new Aluno("Maria"));
            prova.LancarNota(8, new Aluno("João"));
            prova.LancarNota(10, new Aluno("José"));

            AvaliadorProva avaliador = new AvaliadorProva();

            avaliador.Avaliar(prova);

            Assert.AreEqual(10, avaliador.ObtemMaiorNota());

            Assert.AreEqual(5, avaliador.ObtemMenorNota());
        }

        [TestMethod]
        public void Deveria_avaliar_notas_ordem_decrescente()
        {
            Prova prova = new Prova(DateTime.Now);

            prova.LancarNota(10, new Aluno("José"));
            prova.LancarNota(8, new Aluno("João"));
            prova.LancarNota(5, new Aluno("Maria"));

            AvaliadorProva avaliador = new AvaliadorProva();

            avaliador.Avaliar(prova);

            Assert.AreEqual(10, avaliador.ObtemMaiorNota());

            Assert.AreEqual(5, avaliador.ObtemMenorNota());
        }

        [TestMethod]
        public void Deveria_avaliar_notas_aleatorias()
        {
            Prova prova = new Prova(DateTime.Now);

            prova.LancarNota(8, new Aluno("João")); 
            prova.LancarNota(10, new Aluno("José"));            
            prova.LancarNota(5, new Aluno("Maria"));

            AvaliadorProva avaliador = new AvaliadorProva();

            avaliador.Avaliar(prova);

            Assert.AreEqual(10, avaliador.ObtemMaiorNota());

            Assert.AreEqual(5, avaliador.ObtemMenorNota());
        }

        [TestMethod]
        public void Deveria_avaliar_prova_com_apenas_uma_nota()
        {
            Prova prova = new Prova(DateTime.Now);

            prova.LancarNota(8, new Aluno("José"));       

            AvaliadorProva avaliador = new AvaliadorProva();

            avaliador.Avaliar(prova);

            Assert.AreEqual(8, avaliador.ObtemMaiorNota());

            Assert.AreEqual(8, avaliador.ObtemMenorNota());
        }
               

        [TestMethod]
        public void Deveria_encontrar_as_3_piores_notas()
        {
            Prova prova = new Prova(DateTime.Now);

            prova.LancarNota(1, new Aluno("João"));
            prova.LancarNota(2, new Aluno("José"));
            prova.LancarNota(4, new Aluno("Maria"));
            prova.LancarNota(3, new Aluno("João"));
            prova.LancarNota(2, new Aluno("José"));
            prova.LancarNota(6, new Aluno("Maria"));

            AvaliadorProva avaliador = new AvaliadorProva();

            avaliador.Avaliar(prova);

            Assert.AreEqual(3, avaliador.PioresNotas.Count);

            Assert.AreEqual(1, avaliador.PioresNotas[0].Valor);
            Assert.AreEqual(2, avaliador.PioresNotas[1].Valor);
            Assert.AreEqual(2, avaliador.PioresNotas[2].Valor);
        }


    }
}

