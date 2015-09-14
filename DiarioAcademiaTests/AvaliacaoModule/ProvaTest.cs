using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;

namespace DiarioAcademia.Dominio.AvaliacaoModule.Tests
{
    [TestClass]
    public class ProvaTest
    {
        [TestMethod(), TestCategory("Unit Tests")]
        public void Deveria_receber_um_lancamento_de_nota()
        {
            Prova prova = new Prova(DateTime.Now);

            prova.LancarNota(new NotaProva(10, new Aluno(1, "Rech")));

            prova.Notas.Should().HaveCount(1);
            prova.Notas[0].Valor.Should().Be(10);
        }

        [TestMethod(), TestCategory("Unit Tests")]
        public void Deveria_receber_varios_lancamentos_de_notas()
        {
            Prova prova = new Prova(DateTime.Now);

            prova.LancarNota(new NotaProva(10, new Aluno(1, "Rech")));
            prova.LancarNota(new NotaProva(10, new Aluno(2, "Carla")));

            prova.Notas.Should().HaveCount(2);
            prova.Notas[0].Valor.Should().Be(10);
            prova.Notas[1].Valor.Should().Be(10);
        }

        [TestMethod(), TestCategory("Unit Tests")]
        public void Nao_deveria_lancar_2_notas_para_o_mesmo_aluno()
        {
            Prova prova = new Prova(DateTime.Now);

            prova.LancarNota(new NotaProva(10, new Aluno(1, "Rech")));
            prova.LancarNota(new NotaProva(10, new Aluno(1, "Rech")));

            prova.Notas.Should().HaveCount(1);
            prova.Notas[0].Valor.Should().Be(10);
        }

        [TestMethod(), TestCategory("Unit Tests")]
        public void Nao_deveria_lancar_nota_para_alunos_reprovados_por_falta()
        {
            Prova prova = new Prova(DateTime.Now);

            prova.LancarNota(new NotaProva(10, new Aluno(1, "Rech") { Faltas = 6 }));
            prova.LancarNota(new NotaProva(9, new Aluno(2, "Carla")));

            prova.Notas.Should().HaveCount(1);
            prova.Notas[0].Valor.Should().Be(9);
        }

        


    }
}